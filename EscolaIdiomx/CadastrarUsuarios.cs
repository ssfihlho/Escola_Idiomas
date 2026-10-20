using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace EscolaIdiomx
{
    public partial class CadastrarUsuarios : Form
    {
        public CadastrarUsuarios()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CadastrarUsuarios_Load(object sender, EventArgs e)
        {
            cmbPerfil.Items.Clear();
            cmbPerfil.Items.Add("DOCENTE");
            cmbPerfil.Items.Add("SECRETARIA");

            cmbPerfil.SelectedIndex = 0;
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {

            string nome = txtNomeBox.Text.Trim();
            string cpf = new string(
                txtCPF.Text.Where(char.IsDigit).ToArray());
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text;
            string confirmarSenha = txtConfirmarSenha.Text;

            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome.");
                txtNomeBox.Focus();
                return;
            }

            if (cpf.Length != 11)
            {
                MessageBox.Show("O CPF deve conter 11 dígitos.");
                txtCPF.Focus();
                return;
            }

            try
            {
                var endereco = new MailAddress(email);

                if (endereco.Address != email)
                {
                    MessageBox.Show("Informe um e-mail válido.");
                    txtEmail.Focus();
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Informe um e-mail válido.");
                txtEmail.Focus();
                return;
            }

            if (senha.Length < 8)
            {
                MessageBox.Show("A senha deve ter pelo menos 8 caracteres.");
                txtSenha.Focus();
                return;
            }

            if (senha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                txtConfirmarSenha.Clear();
                txtConfirmarSenha.Focus();
                return;
            }

            if (cmbPerfil.SelectedItem == null)
            {
                MessageBox.Show("Selecione um perfil.");
                return;
            }

            string perfil = cmbPerfil.SelectedItem.ToString();

            DAO dao = new DAO();

            bool cadastrado = dao.CadastrarUsuario(
                nome, cpf, email, senha, perfil);

            if (cadastrado)
            {
                MessageBox.Show("Usuário cadastrado com sucesso!");

                txtNomeBox.Clear();
                txtCPF.Clear();
                txtEmail.Clear();
                txtSenha.Clear();
                txtConfirmarSenha.Clear();
                cmbPerfil.SelectedIndex = 0;

                txtNomeBox.Focus();
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNomeBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
