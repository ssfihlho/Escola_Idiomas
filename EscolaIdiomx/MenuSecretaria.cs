using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscolaIdiomx
{
    public partial class MenuSecretaria : Form
    {
        public MenuSecretaria()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlunosCRUD gerenciarAlunos = new AlunosCRUD();
            gerenciarAlunos.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProfessoresCRUD gerenciarProf = new ProfessoresCRUD();
            gerenciarProf.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TurmasCRUD gerenciarTurmas = new TurmasCRUD();
            gerenciarTurmas.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            HistoricoDoAluno historicoDoAluno = new HistoricoDoAluno();
            historicoDoAluno.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e) //Botão de Matricula do Aluno
        {
            MatricularAluno matricularAluno = new MatricularAluno();
            matricularAluno.ShowDialog();
        }
    }
}
