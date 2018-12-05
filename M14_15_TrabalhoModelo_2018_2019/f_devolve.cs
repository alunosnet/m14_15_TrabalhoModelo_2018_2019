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
    public partial class f_devolve : Form
    {
        BaseDados bd;
        public f_devolve(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            //atualizar a listbox
            atualizarListaLivros();
        }

        private void atualizarListaLivros()
        {
            lbLivrosEmprestados.Items.Clear();

            DataTable dados = Livro.listaTodosLivrosEmprestados(bd);
            foreach(DataRow linha in dados.Rows)
            {
                Livro lv = new Livro(
                    int.Parse(linha["nlivro"].ToString()),
                    linha["nome"].ToString()
                    );
                lbLivrosEmprestados.Items.Add(lv);
            }

            lbLivrosEmprestados.SelectionMode = SelectionMode.One;
        }

        //devolver livro
        private void button1_Click(object sender, EventArgs e)
        {
            //validar
            if (lbLivrosEmprestados.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um livro.");
                return;
            }
            //registar devolução
            Livro lvSelecionado = lbLivrosEmprestados.SelectedItem as Livro;
            Emprestimo emprestimo = new Emprestimo(lvSelecionado.nlivro,
                0, DateTime.Now);
            emprestimo.DevolverLivro(bd);
            //refresh na listbox
            atualizarListaLivros();
        }

       
    }
}
