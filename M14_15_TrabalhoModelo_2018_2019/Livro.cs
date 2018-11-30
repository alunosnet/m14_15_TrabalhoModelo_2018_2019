using System;
using System.Collections.Generic;
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
        public bool estado;

        public Livro() { }
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
    }
}
