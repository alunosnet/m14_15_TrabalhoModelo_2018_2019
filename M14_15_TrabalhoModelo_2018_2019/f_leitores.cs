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
        int nrlinhas = 0;
        int nrpagina = 1;
        const int registosPorPagina = 5;

        public f_leitores(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            //atualizar a combobox com nr de páginas
            atualizaNrPaginas();
            atualizarListaLeitores();
            
        }

        private void atualizaNrPaginas()
        {
            cb_pagina.Items.Clear();
            int nrLeitores = Leitor.NrDeLeitores(bd);
            int nrPaginas =(int)Math.Ceiling(nrLeitores /(float) registosPorPagina);
            for (int i = 1; i <= nrPaginas; i++)
                cb_pagina.Items.Add(i);

            cb_pagina.SelectedIndex = 0;
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
            atualizaNrPaginas();
            atualizarListaLeitores();
        }

        private void atualizarListaLeitores()
        {
            //consulta à bd
            if(cb_pagina.SelectedIndex==-1)
                dgv_lista.DataSource = Leitor.listaTodosLeitores(bd);
            else
            {
                int nrpagina = cb_pagina.SelectedIndex + 1;
                int primeiroregisto = (nrpagina - 1) * registosPorPagina + 1;
                int ultimoregisto = primeiroregisto + registosPorPagina-1;
                dgv_lista.DataSource = Leitor.listaTodosLeitores(bd,
                    primeiroregisto, ultimoregisto);

            }
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
            atualizaNrPaginas();
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            imprimeGrelha(e, dgv_lista);
        }
        private void imprimeGrelha(System.Drawing.Printing.PrintPageEventArgs e, DataGridView grelha)
        {
            Graphics impressora = e.Graphics;
            Font tipoLetra = new Font("Arial", 10);
            Font tipoLetraMaior = new Font("Arial", 12, FontStyle.Bold);
            Brush cor = Brushes.Black;
            float mesquerda, mdireita, msuperior, minferior, linha, largura;
            Pen caneta = new Pen(cor, 2);

            //margens
            mesquerda = printDocument1.DefaultPageSettings.Margins.Left;
            mdireita = printDocument1.DefaultPageSettings.Bounds.Right - mesquerda;
            msuperior = printDocument1.DefaultPageSettings.Margins.Top;
            minferior = printDocument1.DefaultPageSettings.Bounds.Height - msuperior;
            largura = mdireita - mesquerda;
            //calcular as colunas da grelha
            float[] colunas = new float[grelha.Columns.Count];
            float lgrelha = 0;
            for (int i = 0; i < grelha.Columns.Count; i++)
                lgrelha += grelha.Columns[i].Width;
            colunas[0] = mesquerda;
            float total = mesquerda, larguraColuna;
            for (int i = 0; i < grelha.Columns.Count - 1; i++)
            {
                larguraColuna = grelha.Columns[i].Width / lgrelha;
                colunas[i + 1] = larguraColuna * largura + total;
                total = colunas[i + 1];
            }
            //cabeçalhos
            for (int i = 0; i < grelha.Columns.Count; i++)
            {
                impressora.DrawString(grelha.Columns[i].HeaderText, tipoLetraMaior, cor, colunas[i], msuperior);
            }
            linha = msuperior + tipoLetraMaior.Height;
            //ciclo para percorrer a grelha
            int l;
            for (l = nrlinhas; l < grelha.Rows.Count; l++)
            {
                //desenhar linha
                impressora.DrawLine(caneta, mesquerda, linha, mdireita, linha);
                //escrever uma linha
                for (int c = 0; c < grelha.Columns.Count; c++)
                {
                    impressora.DrawString(grelha.Rows[l].Cells[c].Value.ToString(),
                        tipoLetra, cor, colunas[c], linha);
                }
                //avançar para linha seguinte
                linha = linha + tipoLetra.Height;
                //verificar se o papel acabou
                if (linha + tipoLetra.Height > minferior)
                    break;
            }
            //tem mais páginas?
            if (l < grelha.Rows.Count)
            {
                nrlinhas = l + 1;
                e.HasMorePages = true;
            }
            //rodapé
            impressora.DrawString("12ºH- Página " + nrpagina.ToString(), tipoLetra, cor, mesquerda, minferior);
            //nr página
            nrpagina++;
            //linhas
            //linha superior
            impressora.DrawLine(caneta, mesquerda, msuperior, mdireita, msuperior);
            //linha inferior
            impressora.DrawLine(caneta, mesquerda, linha, mdireita, linha);
            //colunas
            for (int c = 0; c < colunas.Length; c++)
            {
                impressora.DrawLine(caneta, colunas[c], msuperior, colunas[c], linha);
            }
            //linha lado direito
            impressora.DrawLine(caneta, mdireita, msuperior, mdireita, linha);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nrlinhas = 0;
            nrpagina = 1;
            //só imprimir
            //printDocument1.Print();
            //previsualizar a impressão
            printPreviewDialog1.ShowDialog();
        }

        private void cb_pagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizarListaLeitores();
        }
    }
}
