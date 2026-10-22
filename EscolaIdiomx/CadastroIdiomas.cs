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
    public partial class CadastroIdiomas : Form
    {
        public CadastroIdiomas()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            string nomeIdioma = txtNome.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeIdioma))
            {
                MessageBox.Show("Por favor, informe o nome do idioma.",
                                "Campo Obrigatório",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            DAO dao = new DAO();
            bool cadastrado = dao.CadastrarIdioma(nomeIdioma);

            if (cadastrado)
            {
                MessageBox.Show("Idioma cadastrado com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Indica à tela principal que um registo foi criado
                this.Close();
            }
        }
    
    }
}
