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
    public partial class AlunosCRUD : Form
    {
        public AlunosCRUD()
        {
            InitializeComponent();
        }

        private void AlunosCRUD_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarAluno cad = new CadastrarAluno();
            cad.ShowDialog();
        }
    }
}
