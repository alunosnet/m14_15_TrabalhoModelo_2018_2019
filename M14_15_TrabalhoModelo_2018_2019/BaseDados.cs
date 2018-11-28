using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Data;

/// <summary>
/// Esta classe faz a ligação à base de dados
/// 
/// </summary>
namespace M14_15_TrabalhoModelo_2018_2019
{
    public class BaseDados
    {
        private string bdName = "M14_15_trabalhomodelo_2018_19.mdf";
        private string caminho;
        private string strLigacao;
        private SqlConnection ligacaoBD;

        public BaseDados()
        {
            //definir o caminho para a bd
            caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + bdName;

            //verificar se a bd já existe
            if (File.Exists(caminho) == false)
                //TODO: se não existir criar 
                criarBD();
            //definir a string de ligação
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            //abrir a ligação à bd
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }

        private void criarBD()
        {
        }
        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch { }
        }
        public void executaSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }

        public void executaSQL(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        /// <summary>
        /// Devolve o resultado de uma consulta
        /// </summary>
        /// <param name="sql">Select à base de dados</param>
        /// <returns></returns>
        public DataTable devolveSQL(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
    }
}
