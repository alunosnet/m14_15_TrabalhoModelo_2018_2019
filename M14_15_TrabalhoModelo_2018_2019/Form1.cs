﻿using System;
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
    }
}
