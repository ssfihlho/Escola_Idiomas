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
    public partial class LoginAdm : Form
    {
        public LoginAdm()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            string cpf = new string(
       txtCPF.Text.Where(char.IsDigit).ToArray()
   );

            string senha = txtSenha.Text;

            // Validar CPF
            if (!ValidarCPF(cpf))
            {
                MessageBox.Show(
                    "Informe um CPF válido.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCPF.Focus();
                return;
            }

            // Validar senha preenchida
            if (string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show(
                    "Informe sua senha.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtSenha.Focus();
                return;
            }

            DAO dao = new DAO();

            UsuarioAutenticado usuario =
                dao.AutenticarUsuario(cpf, senha);

            if (usuario == null)
            {
                MessageBox.Show(
                    "CPF ou senha inválidos, ou conta desativada.",
                    "Falha no login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtSenha.Clear();
                txtSenha.Focus();
                return;
            }

            Form menu = null;

            // Redirecionar conforme o perfil
            switch (usuario.Perfil)
            {
                case "ADMIN":
                    menu = new Menu();
                    break;

                case "DOCENTE":
                    menu = new MenuDocente();
                    break;

                case "SECRETARIA":
                    menu = new MenuSecretaria();
                    break;

                default:
                    MessageBox.Show(
                        "O perfil deste usuário não é reconhecido.",
                        "Erro de acesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
            }
            if (menu != null)
            {
                menu.FormClosed += (s, args) => this.Close();

                menu.Show();
                this.Hide();
            }
        }

        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return false;

            // Rejeitar CPFs com todos os dígitos iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += (cpf[i] - '0') * (10 - i);
            }

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (digito1 != cpf[9] - '0')
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += (cpf[i] - '0') * (11 - i);
            }

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digito2 == cpf[10] - '0';
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
