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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnGerenciarUsuarios_Click(object sender, EventArgs e)
        {
            UsuariosCRUD geren = new UsuariosCRUD();
            geren.ShowDialog();
        }

        private void btnGerenciarDocentes_Click(object sender, EventArgs e)
        {
            ProfessoresCRUD gerenciarProf = new ProfessoresCRUD();
            gerenciarProf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlunosCRUD gerenciarAlunos = new AlunosCRUD();
            gerenciarAlunos.ShowDialog();
        }

        private void btnGerenciarCursos_Click(object sender, EventArgs e)
        {
            CursosCRUD gerenciarCursos = new CursosCRUD();
            gerenciarCursos.ShowDialog();
        }

        private void btnGerenciarTurmas_Click(object sender, EventArgs e)
        {
            TurmasCRUD gerenciarTurmas = new TurmasCRUD();
            gerenciarTurmas.ShowDialog();
        }

        private void btnHistoricoAluno_Click(object sender, EventArgs e)
        {
            HistoricoDoAluno historicoDoAluno = new HistoricoDoAluno();
            historicoDoAluno.ShowDialog();
        }

        private void btnMatricularAluno_Click(object sender, EventArgs e)
        {
            MatricularAluno matricularAluno = new MatricularAluno();
            matricularAluno.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIdiomas_Click(object sender, EventArgs e)
        {
            IdiomasCRUD gerenIdi = new IdiomasCRUD();
            gerenIdi.ShowDialog();
        }
    }
}
