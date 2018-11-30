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
    public partial class f_leitores : Form
    {
        BaseDados bd;
        public f_leitores(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            atualizarListaLeitores();
        }
        //escolher a fotografia
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
        //adicionar
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
            //criar um leitor
            string nome = txt_nome.Text;
            DateTime data = dtp_data.Value;
            var fotografia = Utils.ImagemParaVetor(pb_foto.ImageLocation);
            Leitor novo = new Leitor(nome,data,fotografia);
            //adicionar à bd
            novo.adicionar(bd);
            //limpar o form
            txt_nome.Clear();
            pb_foto.ImageLocation = null;
            //atualizar a grelha
            atualizarListaLeitores();
        }

        private void atualizarListaLeitores()
        {
            //consulta à bd
            dgv_lista.DataSource = Leitor.listaTodosLeitores(bd);
        }
        //mostrar detalhes do leitor selecionado
        private void detalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nleitor = nleitorSelecionado();
            if (nleitor == -1)
            {
                MessageBox.Show("Tem de selecionar um leitor da lista");
                return;
            }
            Leitor lt = new Leitor();
            DataTable leitor = lt.pesquisaPorNLeitor(nleitor,bd);
            //mostrar no form o leitor
            //nome
            txt_nome.Text = leitor.Rows[0]["nome"].ToString();
            //datanascimento
            dtp_data.Value = DateTime.Parse(leitor.Rows[0]["data_nasc"].ToString());
            //fotografia
            byte[] imagem = (byte[])leitor.Rows[0]["fotografia"];
            //criar ficheiro temp
            string ficheiro = System.IO.Path.GetTempPath() + @"imagem.jpg";
            Utils.VetorParaImagem(imagem, ficheiro);
            pb_foto.ImageLocation = ficheiro;
        }
        //remover leitor selecionado
        private void removerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //nleitor a remover
            int nleitor = nleitorSelecionado();
            if (nleitor == -1)
            {
                MessageBox.Show("Tem de selecionar um leitor da lista");
                return;
            }
            //confirmar
            Leitor lt = new Leitor();
            lt.pesquisaPorNLeitor(nleitor, bd);
            string nome = lt.nome;
            DialogResult resposta;
            resposta=MessageBox.Show("Tem a certeza que pretende remover o leitor " + nome,
                    "Remover",MessageBoxButtons.YesNo);
            if (resposta == DialogResult.No)
                return;

            Leitor.removerLeitor(nleitor, bd);
            
            //atualizar a lista
            atualizarListaLeitores();
        }
        //editar leitor selecionado
        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //nr do leitor selecionado para editar
            int nleitor = nleitorSelecionado();
            if (nleitor == -1)
            {
                MessageBox.Show("Tem de selecionar um leitor da lista");
                return;
            }
            //abrir form editar
            f_editar_leitor f = new f_editar_leitor(nleitor,bd);
            f.ShowDialog();
            //atualizar grelha
            atualizarListaLeitores();
        }

        int nleitorSelecionado()
        {
            int linha = dgv_lista.CurrentCell.RowIndex;
            if (linha == -1)
            {
                return -1;
            }
            int nleitor = int.Parse(dgv_lista.Rows[linha].Cells[0].Value.ToString());
            return nleitor;
        }
        //pesquisar
        private void button4_Click(object sender, EventArgs e)
        {
            string nome = txt_pesquisa.Text;
            dgv_lista.DataSource = Leitor.pesquisaPorNome(nome,bd);
        }

        private void txt_pesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                button4_Click(null, null);
        }
    }
}
