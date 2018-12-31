using CsvVeiculosExcecao.DAL;
using CsvVeiculosExcecao.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CsvVeiculosExcecao
{
    public class DispatcherVeiculoExcecao : AcessoDados
    {
        #region Constantes
            private const string INC_VEIC_EXCECAO = "FI_SP_AUT_VEXC_IncVeicExcecao";
            private const string CONS_ALL_VEI_EXCECAO = "FI_SP_AUT_VEXC_ConsVeicExcecaoAll";
            private const string DATASET_VEIC_EXCECAO_RELATORIO_CSV = "FI_SP_AUT_VEXC_ConsVeicExcecaoRelatorioCsv";
        #endregion
        /// <summary>
        /// Inclui um registro na tabela veiculos exceção
        /// </summary>
        public void Incluir(VeiculosExcecaoFin veiculosExcecao)
        {
            List<SqlParameter> ListaParametros = new List<SqlParameter>
            {
                new SqlParameter("VEVACODMOLI",veiculosExcecao.CodigoFipe),
                new SqlParameter("VEDESC",veiculosExcecao.DesModMarcVers),
                new SqlParameter("VEUSUCAD",veiculosExcecao.Usuario),
                new SqlParameter("VEDTCAD",veiculosExcecao.DataCadastro)
            };

            Excecutar(INC_VEIC_EXCECAO,ListaParametros);
        }


        public List<VeiculosExcecaoFin> Listar()
        {
            List<SqlParameter> listaParametros = new List<SqlParameter>();
            DataSet ds=Consultar(CONS_ALL_VEI_EXCECAO,listaParametros);

            List<VeiculosExcecaoFin> retorno = Converter(ds);

            return retorno;
        }

        private List<VeiculosExcecaoFin> Converter(DataSet ds)
        {
            List<VeiculosExcecaoFin> ListaVeiculos = new List<VeiculosExcecaoFin>();

            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count >0)
            {
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    VeiculosExcecaoFin excecao = new VeiculosExcecaoFin();

                    if (row["VEVACODMOLI"] != DBNull.Value)
                        excecao.CodigoFipe = row.Field<string>("VEVACODMOLI");
                    if (row["VEDESC"] != DBNull.Value)
                        excecao.DesModMarcVers = row.Field<string>("VEDESC");
                    if (row["VEUSUCAD"] != DBNull.Value)
                        excecao.Usuario = row.Field<string>("VEUSUCAD");
                    if (row["VEDTCAD"] != DBNull.Value)
                        excecao.DataCadastro = row.Field<DateTime>("VEDTCAD");
                    if (row["VEUSUULTMAN"] != DBNull.Value)
                        excecao.UsuarioUltManutencao = row.Field<string>("VEUSUULTMAN");
                    if (row["VEDTULTMAN"] != DBNull.Value)
                        excecao.DataUltManutencao = row.Field<DateTime>("VEDTULTMAN");
                    ListaVeiculos.Add(excecao);
                }                
            }
            return ListaVeiculos;
        }

        internal List<VeiculosExcecaoFin> ConsultarVeiculoExcecao(List<object> pChave)
        {
            List<SqlParameter> ListaParametros = new List<SqlParameter>
            {
                new SqlParameter("UsuarioCadastro",pChave[0]),
                new SqlParameter("DataInicial",pChave[1]),
                new SqlParameter("DataFinal",pChave[2]),
            };

            DataSet ds = Consultar(DATASET_VEIC_EXCECAO_RELATORIO_CSV, ListaParametros);

            List<VeiculosExcecaoFin> retorno = Converter(ds);
            return retorno;
        }
    }
}