using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M14_15_TrabalhoModelo_2018_2019
{
    class Emprestimo
    {
        public int nlivro;
        public int nleitor;
        public DateTime dataEmprestimo;
        public DateTime dataDevolve;
        public bool estado; //1->emprestado 0->devolvido

        public Emprestimo() { }
        public Emprestimo(int nlivro,int nleitor,DateTime dataDevolve)
        {
            this.nlivro = nlivro;
            this.nleitor = nleitor;
            this.dataEmprestimo = DateTime.Now;
            this.dataDevolve = dataDevolve;
        }
        public void adicionar(BaseDados bd)
        {
            SqlTransaction transacao= bd.iniciarTransacao();
            string sql = "";
            try
            {
                //alterar o estado do livro para 0
                sql = "UPDATE livros SET estado=0 WHERE nlivro=@nlivro";
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nlivro",
                        SqlDbType=SqlDbType.Int,
                        Value=this.nlivro
                    }
                };
                bd.executaSQL(sql, parametros, transacao);
                //adicionar um registo à tabela dos empréstimos
                sql = @"INSERT INTO emprestimos(nlivro,nleitor,data_emprestimo,data_devolve,estado)
                       VALUES (@nlivro,@nleitor,@data_emp,@data_dev,@estado)";
                parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nlivro",
                        SqlDbType=SqlDbType.Int,
                        Value=this.nlivro
                    },
                    new SqlParameter()
                    {
                        ParameterName="@nleitor",
                        SqlDbType=SqlDbType.Int,
                        Value=this.nleitor
                    },
                    new SqlParameter()
                    {
                        ParameterName="@data_emp",
                        SqlDbType=SqlDbType.Date,
                        Value=this.dataEmprestimo
                    },
                    new SqlParameter()
                    {
                        ParameterName="@data_dev",
                        SqlDbType=SqlDbType.Date,
                        Value=this.dataDevolve
                    },
                    new SqlParameter()
                    {
                        ParameterName="@estado",
                        SqlDbType=SqlDbType.Bit,
                        Value=true
                    }
                };
                bd.executaSQL(sql, parametros, transacao);
            }
            catch(Exception erro)
            {
                MessageBox.Show("Ocorreu o seguinte erro: " + erro.Message);
                transacao.Rollback();
                return;
            }
            transacao.Commit();

        }
        public void DevolverLivro(BaseDados bd)
        {
            SqlTransaction transacao = bd.iniciarTransacao();
            string sql = "";
            try
            {
                //alterar o estado do livro para 1
                sql = "UPDATE livros SET estado=1 WHERE nlivro=@nlivro";
                List<SqlParameter> parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nlivro",
                        SqlDbType=SqlDbType.Int,
                        Value=this.nlivro
                    }
                };
                bd.executaSQL(sql, parametros, transacao);
                //atualizar o estado do emprestimos
                sql = @"UPDATE emprestimos SET estado=0,data_devolve=@data_devolve 
                        WHERE nlivro=@nlivro and estado=1";
                parametros = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName="@nlivro",
                        SqlDbType=SqlDbType.Int,
                        Value=this.nlivro
                    },
                   new SqlParameter()
                    {
                        ParameterName="@data_devolve",
                        SqlDbType=SqlDbType.Date,
                        Value=this.dataDevolve
                    }
                };
                bd.executaSQL(sql, parametros, transacao);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Ocorreu o seguinte erro: " + erro.Message);
                transacao.Rollback();
                return;
            }
            transacao.Commit();
        }
        static public DataTable ListaEmprestimos(BaseDados bd)
        {
            string sql = @"SELECT livros.nome as [Livro emprestado],leitores.nome as [Leitor]
                        FROM livros
                        INNER JOIN emprestimos ON emprestimos.nlivro=livros.nlivro
                        INNER JOIN leitores ON emprestimos.nleitor=leitores.nleitor
                        WHERE emprestimos.estado=1";

            return bd.devolveSQL(sql);
        }
        
    }
}
