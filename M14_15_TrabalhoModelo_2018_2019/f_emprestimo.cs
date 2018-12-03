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
    public partial class f_emprestimo : Form
    {
        BaseDados bd;
        public f_emprestimo(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void f_emprestimo_Load(object sender, EventArgs e)
        {
            //preencher as comboboxes com livros e com leitores
            preencheCBLivros();
            preencheCBLeitores();

        }

        private void preencheCBLeitores()
        {
            DataTable dados = Leitor.listaTodosLeitores(bd);
            cbLeitores.Items.Clear();
            foreach(DataRow linha in dados.Rows)
            {
                Leitor lt = new Leitor(
                    int.Parse(linha["nleitor"].ToString()),
                    linha["nome"].ToString()
                    );
                cbLeitores.Items.Add(lt);
            }
        }

        private void preencheCBLivros()
        {
            DataTable dados = Livro.listaTodosLivrosDisponiveis(bd);
            cbLivros.Items.Clear();
            foreach(DataRow linha in dados.Rows)
            {
                Livro lv = new Livro(
                    int.Parse(linha["nlivro"].ToString()),
                    linha["nome"].ToString()
                    );
                cbLivros.Items.Add(lv);
            }
        }
    }
}
