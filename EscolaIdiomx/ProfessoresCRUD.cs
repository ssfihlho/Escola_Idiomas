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
    public partial class ProfessoresCRUD : Form
    {
        public ProfessoresCRUD()
        {
            InitializeComponent();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarProfessores cadProfessor = new CadastrarProfessores();
            cadProfessor.ShowDialog();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
