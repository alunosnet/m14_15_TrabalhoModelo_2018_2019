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
    public partial class f_historico : Form
    {
        BaseDados bd;
        public f_historico(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
        }

        private void f_historico_Load(object sender, EventArgs e)
        {
            //leitores
            DataTable leitores = bd.devolveSQL("select * from leitores");
            //criar nós parent
            tvHistorico.Nodes.Clear();
            foreach (DataRow leitor in leitores.Rows)
            {
                TreeNode no = tvHistorico.Nodes.Add(leitor["nome"].ToString());
                //histórico de empréstimos
                Emprestimo emp = new Emprestimo();
                int nleitor = int.Parse(leitor["nleitor"].ToString());
                DataTable emprestimos = emp.ListaEmprestimos(bd, nleitor);
                foreach (DataRow EmpLeitor in emprestimos.Rows)
                {
                    no.Nodes.Add(EmpLeitor["nome"].ToString());
                }
            }
        }
    }
}
