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
    public partial class f_livros : Form
    {
        BaseDados bd;
        string ficheiro;
        public f_livros(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            atualizaListaLivros();
        }
        //todo: listar todos os livros
        private void atualizaListaLivros()
        {
            dgvLista.DataSource = Livro.listaTodosLivros(bd);
        }
        
        //escolher
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Imagens |*.jpg;*.png | Todos os ficheiros |*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != String.Empty)
                {
                    pbCapa.ImageLocation = openFileDialog.FileName;
                    ficheiro = openFileDialog.SafeFileName;
                }
            }
        }
        //adicionar
        private void button2_Click(object sender, EventArgs e)
        {
            //validar
            string nome = txtNome.Text;
            int ano = int.Parse(txtAno.Text);
            decimal preco = decimal.Parse(txtPreco.Text);
            DateTime data = dtpData.Value;
            if (nome.Trim() == String.Empty)
            {
                MessageBox.Show("Tem de indicar um nome.");
                txtNome.Focus();
                return;
            }
            if (ano > DateTime.Now.Date.Year)
            {
                MessageBox.Show("Tem de indicar um ano inferior ou igual ao ano atual.");
                txtAno.Focus();
                return;
            }
            if (preco<0)
            {
                MessageBox.Show("Tem de indicar um preço superior ou igual a zero.");
                txtPreco.Focus();
                return;
            }
            if (data > DateTime.Now.Date)
            {
                MessageBox.Show("Tem de indicar uma data inferior ou igual à atual.");
                dtpData.Focus();
                return;
            }
            if (pbCapa.ImageLocation == null||ficheiro==null)
            {
                MessageBox.Show("Tem de escolher uma capa.");
                button1.Focus();
                return;
            }
            //copiar capa
            string caminho = Utils.pastaDoPrograma() + @"\" + ficheiro;
            System.IO.File.Copy(pbCapa.ImageLocation, caminho, true);
            //adicionar registo
            Livro lv = new Livro(nome,ano,data,preco,caminho);
            lv.adicionar(bd);
            //limpar form
            txtAno.Clear();
            txtNome.Clear();
            txtPreco.Clear();
            dtpData.Value = DateTime.Now;
            pbCapa.ImageLocation = null;
            ficheiro = null;
            //atualizar grelha
            atualizaListaLivros();
        }
        //pesquisar
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //detalhes
        private void detalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //nlivro
            int nlivro = nlivroSelecionado();
            if (nlivro == -1) return;
            Livro lv = new Livro();
            DataTable livro = lv.pesquisaPorNLivro(nlivro, bd);
            txtNome.Text = livro.Rows[0]["nome"].ToString();
            txtAno.Text= livro.Rows[0]["ano"].ToString();
            txtPreco.Text= livro.Rows[0]["preco"].ToString();
            dtpData.Value = DateTime.Parse(livro.Rows[0]["data_aquisicao"].ToString());
            pbCapa.ImageLocation= livro.Rows[0]["capa"].ToString();

        }
        //editar
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nlivro = nlivroSelecionado();
            if (nlivro == -1) return;
            f_editar_livro f = new f_editar_livro(nlivro, bd);
            f.ShowDialog();
            atualizaListaLivros();
        }
        //remover
        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nlivro = nlivroSelecionado();
            if (nlivro == -1) return;

            //confirmar
            Livro lv = new Livro();
            lv.pesquisaPorNLivro(nlivro, bd);
            string nome = lv.nome;
            DialogResult resposta;
            resposta = MessageBox.Show("Tem a certeza que pretende remover o livro " + nome,
                    "Remover", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.No)
                return;

            Livro.remover(nlivro, bd,lv.capa);
            atualizaListaLivros();
        }
        int nlivroSelecionado()
        {
            int linha = dgvLista.CurrentCell.RowIndex;
            if (linha == -1)
            {
                return -1;
            }
            int nleitor = int.Parse(dgvLista.Rows[linha].Cells[0].Value.ToString());
            return nleitor;
        }
    }
}
