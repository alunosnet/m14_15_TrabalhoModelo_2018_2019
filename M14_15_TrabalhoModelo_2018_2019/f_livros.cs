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
            //copiar capa
            string caminho = pastaDoPrograma() + @"\" + ficheiro;
            System.IO.File.Copy(pbCapa.ImageLocation, caminho, true);
            //adicionar registo
            Livro lv = new Livro(nome,ano,data,preco,caminho);
            
            //limpar form
            //atualizar grelha
        }
        //pesquisar
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //detalhes
        private void detalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //editar
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //remover
        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private string pastaDoPrograma()
        {
            string pastaInicial = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pastaInicial += @"\M14_15_TrabalhoModelo";
            if (System.IO.Directory.Exists(pastaInicial) == false)
                System.IO.Directory.CreateDirectory(pastaInicial);
            return pastaInicial;
        }
    }
}
