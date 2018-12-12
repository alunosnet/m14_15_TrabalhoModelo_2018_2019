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
        private string bdName = "M14_15_trabalhomodelo_2018_19_v2.mdf";
        private string caminho;
        private string strLigacao;
        private SqlConnection ligacaoBD;

        public BaseDados()
        {
            //definir o caminho para a bd
            caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + bdName;

            //verificar se a bd já existe
            if (File.Exists(caminho) == false)
                criarBD();
            //definir a string de ligação
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            //abrir a ligação à bd
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }

        private void criarBD()
        {
            //nome da bd
            string nomeBD = System.IO.Path.GetFileNameWithoutExtension(caminho);
            strLigacao = ConfigurationManager.ConnectionStrings["servidor"].ToString();
            //abrir a ligação ao servidor
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
            //criar a bd
            string strSQL = $@"CREATE DATABASE {nomeBD} 
                        ON PRIMARY (NAME={nomeBD}, FILENAME='{caminho}')";
            this.executaSQL(strSQL);
            //criar as tabelas
            ligacaoBD.Close();
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            //abrir a ligação ao servidor
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
            strSQL = @"create table leitores(
	                        nleitor int identity primary key,
	                        nome varchar(40) not null,
	                        data_nasc date,
	                        fotografia image,
	                        ativo bit
                        )

                        create table livros(
	                        nlivro int identity primary key,
	                        nome varchar(100),
	                        ano int,
	                        data_aquisicao date,
	                        preco decimal(4,2),
	                        capa varchar(300),
	                        estado bit
                        )

                        create table emprestimos(
	                        nemprestimo int identity primary key,
	                        nlivro int references livros(nlivro),
	                        nleitor int references leitores(nleitor),
	                        data_emprestimo date,
	                        data_devolve date,
	                        estado bit
                        )";
            this.executaSQL(strSQL);
            ligacaoBD.Close();
        }
        ~BaseDados()
        {
            try
            {
                ligacaoBD.Close();
            }
            catch { }
        }
        //iniciar uma transação
        public SqlTransaction iniciarTransacao()
        {
            return ligacaoBD.BeginTransaction();
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
        public void executaSQL(string sql, List<SqlParameter> parametros,SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
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

        public DataTable devolveSQL(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            comando = null;
            return registos;
        }
    }
}
