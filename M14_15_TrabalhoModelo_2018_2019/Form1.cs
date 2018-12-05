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
    public partial class Form1 : Form
    {
        BaseDados bd = new BaseDados();

        public Form1()
        {
            InitializeComponent();
            //listar os livros emprestados
            dataGridView1.DataSource = Emprestimo.ListaEmprestimos(bd);
        }
        //sair do programa
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //editar->leitores
        private void leitoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormLeitores();

        }

        private void AbrirFormLeitores()
        {
            f_leitores f = new f_leitores(bd);
            f.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AbrirFormLeitores();
        }

        private void livrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_livros f = new f_livros(bd);
            f.Show();
        }

        private void empréstimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_emprestimo f = new f_emprestimo(bd);
            f.ShowDialog();
            //listar os livros emprestados
            dataGridView1.DataSource = Emprestimo.ListaEmprestimos(bd);
        }

        private void devolverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_devolve f = new f_devolve(bd);
            f.ShowDialog();
            //listar os livros emprestados
            dataGridView1.DataSource = Emprestimo.ListaEmprestimos(bd);
        }
    }
}
