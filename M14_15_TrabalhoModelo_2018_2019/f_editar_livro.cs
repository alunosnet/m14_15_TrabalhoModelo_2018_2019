using System;
using System.Windows.Forms;

namespace M14_15_TrabalhoModelo_2018_2019
{
    public partial class f_editar_livro : Form
    {
        string ficheiro=String.Empty;
        BaseDados bd;
        int nlivro;
        public f_editar_livro(int nlivro, BaseDados bd)
        {
            InitializeComponent();
            this.nlivro = nlivro;
            this.bd = bd;
            //mostrar dados do livro a editar
            Livro lv = new Livro();
            lv.pesquisaPorNLivro(nlivro, bd);
            lbNlivro.Text = lv.nlivro.ToString();
            txtNome.Text = lv.nome;
            txtAno.Text = lv.ano.ToString();
            txtPreco.Text = lv.preco.ToString();
            pbCapa.ImageLocation = lv.capa;
            dtpData.Value = lv.data_aquisicao;
        }
        //escolher capa
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
        //cancelar
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //atualizar
        private void button2_Click(object sender, EventArgs e)
        {
            //validar o form
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
            if (preco < 0)
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
            if (pbCapa.ImageLocation == null)
            {
                MessageBox.Show("Tem de escolher uma capa.");
                button1.Focus();
                return;
            }
            //copiar capa
            string caminho = "";
            if (ficheiro != String.Empty)    //se o utilizador escolher uma capa
            {
                caminho = Utils.pastaDoPrograma() + @"\" + ficheiro;
                System.IO.File.Copy(pbCapa.ImageLocation, caminho, true);
            }
            else
                caminho = pbCapa.ImageLocation;
            //atualizar a bd
            Livro lv = new Livro(nome, ano, data, preco, caminho);
            lv.nlivro = this.nlivro;
            lv.atualizar(bd);
            //fechar form
            this.Close();
        }
    }
}
