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
    public partial class f_editar_leitor : Form
    {
        int nleitor;
        BaseDados bd;
        public f_editar_leitor(int nleitor,BaseDados bd)
        {
            InitializeComponent();
            this.nleitor = nleitor;
            this.bd = bd;
        }
        //escolher fotografia
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Imagens |*.jpg;*.png | Todos os ficheiros |*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog.FileName != String.Empty)
                    pb_foto.ImageLocation = openFileDialog.FileName;
            }
        }
        //cancelar
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void f_editar_leitor_Load(object sender, EventArgs e)
        {
            //mostrar os dados do leitor a editar
            Leitor lt = new Leitor();
            lt.pesquisaPorNLeitor(nleitor, bd);
            lbNLeitor.Text = lt.nleitor.ToString();
            txt_nome.Text = lt.nome;
            dtp_data.Value = lt.data_nasc;
            string ficheiro = System.IO.Path.GetTempPath() + @"\imagem.jpg";
            Utils.VetorParaImagem(lt.fotografia, ficheiro);
            pb_foto.ImageLocation = ficheiro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //validar o form
            if (txt_nome.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Erro. Tem de indicar um nome.");
                txt_nome.Focus();
                return;
            }
            if (dtp_data.Value > DateTime.Now.Date)
            {
                MessageBox.Show($"A data indicada tem de ser inferior" +
                    $" à data de hoje.");
                dtp_data.Focus();
                return;
            }
            TimeSpan idade = DateTime.Now - dtp_data.Value;
            if (idade.TotalDays < 3650)
            {
                MessageBox.Show("O leitor tem de ter 10 anos de idade.");
                dtp_data.Focus();
                return;
            }
            if (pb_foto.ImageLocation == null)
            {
                MessageBox.Show("Selecione uma fotografia.");
                button1.Focus();
                return;
            }
            // leitor
            string nome = txt_nome.Text;
            DateTime data = dtp_data.Value;
            var fotografia = Utils.ImagemParaVetor(pb_foto.ImageLocation);
            Leitor novo = new Leitor(nome, data, fotografia);
            novo.nleitor = int.Parse(lbNLeitor.Text);
            //atualiza a bd
            novo.atualizar(bd);
            //fechar form
            this.Close();
        }
    }
}
