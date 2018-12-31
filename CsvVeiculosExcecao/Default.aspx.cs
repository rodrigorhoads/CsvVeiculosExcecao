using CsvVeiculosExcecao.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CsvVeiculosExcecao
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarDplTipoVeiculo();
            }
        }

        private void carregarDplTipoVeiculo()
        {
            dplTipoRelatorio.DataSource = Enum.GetValues(typeof(TipoRelatorio));                      
            dplTipoRelatorio.DataBind();
        }

        protected void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            DispatcherVeiculoExcecao dsp = new DispatcherVeiculoExcecao();

            string usuarioResponsavel = txtUsuario.Text;
            DateTime dataInicio;
            DateTime.TryParse(dtInicio.Text,out dataInicio);
            DateTime dataFim;
            DateTime.TryParse(dtFim.Text, out dataFim);
            var lista = new List<VeiculosExcecaoFin>();

                if (string.IsNullOrEmpty(dtInicio.Text))
                    dataInicio = DateTime.Now.Date;
                if (string.IsNullOrEmpty(dtFim.Text))
                        dataFim = DateTime.Now.AddMonths(3).Date;
                    List<Object> pChave = new List<object> {usuarioResponsavel,dataInicio,dataFim };
                    lista = dsp.ConsultarVeiculoExcecao(pChave);
            if (lista.Count > 0)
            {
                GerarRelatorioCSV(lista);
            }
        }

        private void GerarRelatorioCSV(List<VeiculosExcecaoFin> lista)
        {
            string delimitador = ";";
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition",string.Concat("attachment; filename=",txtUsuario.Text,DateTime.Now.ToShortDateString(),".csv"));

            StringBuilder arquivoCsv = new StringBuilder();

            String linha = String.Empty;

            linha += string.Concat("*CODFIPEMOLICAR",delimitador,
                                   "*DESCRICAO",delimitador,
                                   "*Usuario Cadastro",delimitador,
                                   "*Data Cadastro",delimitador,
                                   "*Usuario Ultima Manutençao",delimitador,
                                   "*Data Ultima Manutençao");

            arquivoCsv.Append(string.Concat(linha,Environment.NewLine));

            foreach(var item in lista)
            {
                linha = string.Concat(item.CodigoFipe,
                                      delimitador,item.DesModMarcVers,
                                      delimitador,item.Usuario,
                                      delimitador,item.DataCadastro,
                                      delimitador,item.UsuarioUltManutencao,
                                      delimitador,item.DataUltManutencao
                    );
                arquivoCsv.Append(string.Concat(linha,Environment.NewLine));
            }

            Response.Write(arquivoCsv.ToString());
            Response.End();
        }

        protected void btnCapturarCsv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(arquivo.FileName))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "UploadCSV", "alert('Por favor Selecione um arquivo');", true);
                return;
            }
            string extensaoArquivo = Path.GetExtension(arquivo.FileName);

            if (!extensaoArquivo.ToUpper().Equals(".CSV"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "FORMATOINVALIDO", "alert('Arquivo com formato inválido');", true);
                return;
            }

            string caminhoTemporario = Path.GetTempPath();
            string nomeArquivo = string.Concat(Path.GetFileName(arquivo.FileName),Guid.NewGuid().ToString(),Path.GetExtension(arquivo.FileName));

            arquivo.PostedFile.SaveAs(string.Concat(caminhoTemporario,nomeArquivo));

            try
            {
                ImportacaoBCP(caminhoTemporario, nomeArquivo);
            }
            catch (Exception E)
            {
                Response.Write(E.Message);
            }
            
        }

        private void ImportacaoBCP(string caminhoTemporario, string nomeArquivo)
        {
            string bcp = "bcp Veiculos.dbo.TVEICEXCE IN ";
            using (FileStream file = new FileStream(caminhoTemporario+"Excutar.bat",FileMode.Append,FileAccess.Write))
            {
                using (StreamWriter sw =  new StreamWriter(file,Encoding.Default))
                {
                    bcp += string.Concat(caminhoTemporario,nomeArquivo);
                    bcp += $" -o {caminhoTemporario}\\ARQUIVO.log -S DESKTOP-SKGBI4H -T -r '\n' -t ';' -c";
                    sw.Write(bcp);
                }
            }
            Process.Start(string.Concat(caminhoTemporario, "Excutar.bat"));
        }
    }
}