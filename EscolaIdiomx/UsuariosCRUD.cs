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
    public partial class UsuariosCRUD : Form
    {
        public UsuariosCRUD()
        {
            InitializeComponent();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarUsuarios cadastrarUx = new CadastrarUsuarios();
            cadastrarUx.ShowDialog();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
