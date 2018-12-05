using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M14_15_TrabalhoModelo_2018_2019
{
    public class Livro
    {
        public int nlivro;
        public string nome;
        public int ano;
        public DateTime data_aquisicao;
        public decimal preco;
        public string capa;
        public bool estado; //1-disponivel 0-emprestado

        public Livro() { }
        public Livro(int nlivro,string nome)
        {
            this.nlivro = nlivro;
            this.nome = nome;
        }
        public Livro(string nome,int ano,DateTime data_aquisicao,decimal preco,
            string capa)
        {
            this.nome = nome;
            this.ano = ano;
            this.data_aquisicao = data_aquisicao;
            this.preco = preco;
            this.capa = capa;
            this.estado = true;
        }
        public override string ToString()
        {
            return nome;
        }
        public void adicionar(BaseDados bd)
        {
            string sql;
            sql = $@"insert into livros(nome,ano,data_aquisicao,preco,capa,estado)
                    values (@nome,@ano,@data,@preco,@capa,@estado)";
            //parametros sql
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.nome},
                new SqlParameter(){ParameterName="@ano",
                    SqlDbType =System.Data.SqlDbType.Int,
                Value=this.ano},
                new SqlParameter(){ParameterName="@data",
                    SqlDbType =System.Data.SqlDbType.Date,
                Value=this.data_aquisicao},
                new SqlParameter(){ParameterName="@preco",
                    SqlDbType =System.Data.SqlDbType.Decimal,
                Value=this.preco},
                new SqlParameter(){ParameterName="@capa",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.capa},
                new SqlParameter(){ParameterName="@estado",
                    SqlDbType =System.Data.SqlDbType.Bit,
                Value=this.estado},
            };
            bd.executaSQL(sql, parametros);
        }
        /// <summary>
        /// Lista todos os livros da base de dados
        /// </summary>
        /// <param name="bd"></param>
        /// <returns></returns>
        public static DataTable listaTodosLivros(BaseDados bd)
        {
            string sql = "SELECT nlivro,nome,estado from livros";
            return bd.devolveSQL(sql);
        }
        public static DataTable listaTodosLivrosDisponiveis(BaseDados bd)
        {
            string sql = "SELECT nlivro,nome,estado from livros where estado=1";
            return bd.devolveSQL(sql);
        }
        public static DataTable listaTodosLivrosEmprestados(BaseDados bd)
        {
            string sql = "SELECT nlivro,nome,estado from livros where estado=0";
            return bd.devolveSQL(sql);
        }
        public DataTable pesquisaPorNLivro(int nlivro, BaseDados bd)
        {
            string sql = "Select * from livros where nlivro=" + nlivro;
            DataTable dados = bd.devolveSQL(sql);

            if (dados.Rows.Count > 0)
            {
                this.nlivro = int.Parse(dados.Rows[0]["nlivro"].ToString());
                this.nome = dados.Rows[0]["nome"].ToString();
                this.ano = int.Parse(dados.Rows[0]["ano"].ToString());
                this.data_aquisicao = DateTime.Parse(dados.Rows[0]["data_aquisicao"].ToString());
                this.capa = dados.Rows[0]["capa"].ToString();
                this.preco = Decimal.Parse(dados.Rows[0]["preco"].ToString());
                this.estado = bool.Parse(dados.Rows[0]["estado"].ToString());
            }

            return dados;
        }

        internal void atualizar(BaseDados bd)
        {
            string sql;
            sql = $@"update livros set nome=@nome,ano=@ano,data_aquisicao=@data, 
                    preco=@preco,capa=@capa, estado=@estado
                    where nlivro=@nlivro";
            //parametros sql
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.nome},
                new SqlParameter(){ParameterName="@ano",
                    SqlDbType =System.Data.SqlDbType.Int,
                Value=this.ano},
                new SqlParameter(){ParameterName="@data",
                    SqlDbType =System.Data.SqlDbType.Date,
                Value=this.data_aquisicao},
                new SqlParameter(){ParameterName="@preco",
                    SqlDbType =System.Data.SqlDbType.Decimal,
                Value=this.preco},
                new SqlParameter(){ParameterName="@capa",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.capa},
                new SqlParameter(){ParameterName="@estado",
                    SqlDbType =System.Data.SqlDbType.Bit,
                Value=this.estado},
                new SqlParameter(){ParameterName="@nlivro",
                    SqlDbType =System.Data.SqlDbType.Int,
                Value=this.nlivro},
            };
            bd.executaSQL(sql, parametros);
        }

        internal static void remover(int nlivro, BaseDados bd,string capa)
        {

            bd.executaSQL($"delete from livros where nlivro={nlivro}");
            System.IO.File.Delete(capa);
        }
    }
}
