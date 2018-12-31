using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CsvVeiculosExcecao.DAL
{
    public class AcessoDados
    {
        private string stringDeConexao
        {
            get
            {
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["VeiculoExcecao"];
                if (connectionString != null)
                {
                    return connectionString.ConnectionString;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public   void Excecutar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            using (SqlConnection conexao = new SqlConnection(stringDeConexao))
            {
                comando.Connection = conexao;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = NomeProcedure;
                foreach (var parametro in parametros)
                {
                    comando.Parameters.Add(parametro);
                }

                try
                {
                    comando.ExecuteNonQuery();
                }
                catch (Exception E)
                {
                   
                }
            }
        }

        public DataSet Consultar(string NomeProcedure,List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            using (SqlConnection conexao = new SqlConnection(stringDeConexao))
            {
                comando.Connection = conexao;
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = NomeProcedure;
                foreach (var parametro in parametros)
                {
                    comando.Parameters.Add(parametro);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
               
                try
                {
                    adapter.Fill(ds);     
                }
                catch (Exception E)
                {

                }

                return ds;
            }
        }


    }
}