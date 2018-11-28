using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M14_15_TrabalhoModelo_2018_2019
{
    class Leitor
    {
        int nleitor;
        string nome;
        DateTime data_nasc;
        byte[] fotografia;
        bool ativo;

        public Leitor() { }
        public Leitor(string nome,DateTime data_nasc,byte[] fotografia)
        {
            this.nome = nome;
            this.data_nasc = data_nasc;
            this.fotografia = fotografia;
            ativo = true;
        }

        public void adicionar(BaseDados bd)
        {
            string sql;
            sql = $@"insert intoleitores(nome,data_nasc,fotografia,ativo)
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
            bd.executaSQL(sql,parametros);
        }
    }
}
