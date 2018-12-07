using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M14_15_TrabalhoModelo_2018_2019
{
    public partial class f_consultas : Form
    {
        BaseDados bd;
        public f_consultas(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void consulta1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar o nome de todos os utilizadores ativos
            string sql = "SELECT nome FROM Leitores WHERE ativo=1";
            AtualizaGridView(sql);
        }

        private void AtualizaGridView(string sql)
        {
            dataGridView1.DataSource = bd.devolveSQL(sql);
        }

        private void consulta2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar o nome do livro mais vezes emprestado
            string sql = @"select nome from livros
                            where nlivro=(select top 1 nlivro from emprestimos
                                            group by nlivro order by count(*) desc);";
            AtualizaGridView(sql);
        }

        private void consulta3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar o nome de todos os livros emprestados
            string sql = "SELECT nome FROM Livros WHERE estado=0";
            AtualizaGridView(sql);
        }

        private void consulta4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar o número de emprestimos e devoluções
            string sql = @"select count(*) as [Nr Emprestimos],
                        (select count(*)  from emprestimos where estado = 0) as [Nr Devoluções]
                         from emprestimos";
            AtualizaGridView(sql);
        }

        private void consulta5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar os utilizadores com mais de 30 anos
            string sql = @"select nome,data_nasc 
                        from leitores
                        where datediff(year,data_nasc,getdate())>30";
            AtualizaGridView(sql);
        }

        private void consulta6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //listar nome,nr e nr de emprestimos de cada livro
            //número de empréstimos por livro (nome, nr empréstimos)
            string sql = @"select nome,livros.nlivro,count(emprestimos.nlivro) as [Nr empréstimos]
                            from emprestimos right join livros
                            on livros.nlivro=emprestimos.nlivro
                            group by livros.nome,livros.nlivro;";
            AtualizaGridView(sql);
        }
    }
}
