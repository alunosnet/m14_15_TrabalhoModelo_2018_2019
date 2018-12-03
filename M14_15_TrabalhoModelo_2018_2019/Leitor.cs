using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace M14_15_TrabalhoModelo_2018_2019
{
    class Leitor
    {
        public int nleitor;
        public string nome;
        public DateTime data_nasc;
        public byte[] fotografia;
        public bool ativo;

        public Leitor() { }
        public Leitor(int nleitor,string nome)
        {
            this.nleitor=nleitor;
            this.nome=nome;
        }
        public Leitor(string nome, DateTime data_nasc, byte[] fotografia)
        {
            this.nome = nome;
            this.data_nasc = data_nasc;
            this.fotografia = fotografia;
            ativo = true;
        }
        //para mostrar na Combobox o nome do leitor
        public override string ToString()
        {
            return nome;
        }
        public void adicionar(BaseDados bd)
        {
            string sql;
            sql = $@"insert into leitores(nome,data_nasc,fotografia,ativo)
                    values (@nome,@data_nasc,@fotografia,@ativo)";
            //parametros sql
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.nome},
                new SqlParameter(){ParameterName="@data_nasc",
                    SqlDbType =System.Data.SqlDbType.Date,
                Value=this.data_nasc},
                new SqlParameter(){ParameterName="@fotografia",
                    SqlDbType =System.Data.SqlDbType.Image,
                Value=this.fotografia},
                new SqlParameter(){ParameterName="@ativo",
                    SqlDbType =System.Data.SqlDbType.Bit,
                Value=this.ativo},
            };
            bd.executaSQL(sql, parametros);
        }
        public static DataTable listaTodosLeitores(BaseDados bd)
        {
            string sql = "SELECT nleitor,nome,ativo from leitores";
            return bd.devolveSQL(sql);
        }
        public DataTable pesquisaPorNLeitor(int nleitor, BaseDados bd)
        {
            string sql = "Select * from leitores where nleitor=" + nleitor;
            DataTable dados = bd.devolveSQL(sql);

            if (dados.Rows.Count > 0)
            {
                this.nleitor = int.Parse(dados.Rows[0]["nleitor"].ToString());
                this.nome = dados.Rows[0]["nome"].ToString();
                this.data_nasc = DateTime.Parse(dados.Rows[0]["data_nasc"].ToString());
                this.fotografia = (byte[])dados.Rows[0]["fotografia"];
                this.ativo = bool.Parse(dados.Rows[0]["ativo"].ToString());
            }

            return dados;
        }
        public static void removerLeitor(int nleitor, BaseDados bd)
        {
            string sql = "Delete from leitores where nleitor=" + nleitor;
            bd.executaSQL(sql);
        }

        internal void atualizar(BaseDados bd)
        {
            string sql;
            sql = $@"update leitores set nome=@nome,data_nasc=@data_nasc, 
                    fotografia=@fotografia, ativo=@ativo
                    where nleitor=@nleitor";
            //parametros sql
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType =System.Data.SqlDbType.VarChar,
                Value=this.nome},
                new SqlParameter(){ParameterName="@data_nasc",
                    SqlDbType =System.Data.SqlDbType.Date,
                Value=this.data_nasc},
                new SqlParameter(){ParameterName="@fotografia",
                    SqlDbType =System.Data.SqlDbType.Image,
                Value=this.fotografia},
                new SqlParameter(){ParameterName="@ativo",
                    SqlDbType =System.Data.SqlDbType.Bit,
                Value=this.ativo},
                new SqlParameter(){ParameterName="@nleitor",
                    SqlDbType =System.Data.SqlDbType.Int,
                Value=this.nleitor},
            };
            bd.executaSQL(sql, parametros);
        }

        internal static object pesquisaPorNome(string nome, BaseDados bd)
        {
            string sql = "SELECT nleitor,nome,ativo from leitores where nome like @nome";
            string filtro = "%" + nome + "%";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@nome",
                    SqlDbType=SqlDbType.VarChar,
                     Value=filtro
                }
            };
            return bd.devolveSQL(sql,parametros);
        }
    }
}
