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
        public f_leitores()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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
    }
}
