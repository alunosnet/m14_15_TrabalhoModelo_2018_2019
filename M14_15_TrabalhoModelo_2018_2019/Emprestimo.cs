using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M14_15_TrabalhoModelo_2018_2019
{
    class Emprestimo
    {
        public int nlivro;
        public int nleitor;
        public DateTime dataEmprestimo;
        public DateTime dataDevolve;
        public bool estado; //1->emprestado 0->devolvido

        public Emprestimo() { }
        public Emprestimo(int nlivro,int nleitor,DateTime dataDevolve)
        {
            this.nlivro = nlivro;
            this.nleitor = nleitor;
            this.dataEmprestimo = DateTime.Now;
            this.dataDevolve = dataDevolve;
        }
        public void adicionar()
        {

        }
        public void DevolverLivro()
        {

        }
        public DataTable ListaEmprestimos()
        {
            //todo: fazer esta cena
            return null;
        }
        
    }
}
