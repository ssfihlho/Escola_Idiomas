using MySql.Data.MySqlClient;
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
    public partial class CadastrarAluno : Form
    {
        public CadastrarAluno()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e) //LABEL TELEFONE
        {

        }

        private void label13_Click(object sender, EventArgs e) //Label MATRICULA ALUNO
        {

        }

        private void CadastrarAluno_Load(object sender, EventArgs e)
        {
 
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            {
                // Validar os campos obrigatórios
                if (string.IsNullOrWhiteSpace(cmbNome.Text) ||
                    string.IsNullOrWhiteSpace(txtboxCPF.Text) ||
                    string.IsNullOrWhiteSpace(txtboxEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtboxTelefone.Text) ||
                    string.IsNullOrWhiteSpace(txtCidade.Text) ||
                    string.IsNullOrWhiteSpace(txtEstado.Text) ||
                    string.IsNullOrWhiteSpace(txtBairro.Text) ||
                    string.IsNullOrWhiteSpace(txtLogradouro.Text) ||
                    string.IsNullOrWhiteSpace(txtNumero.Text))
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios.");
                    return;
                }

                // Validar CPF e telefone conforme os tipos atuais do banco
                // Remover os caracteres de formatação
                string cpfTexto = new string(
                    txtboxCPF.Text.Where(char.IsDigit).ToArray()
                );

                string telefoneTexto = new string(
                    txtboxTelefone.Text.Where(char.IsDigit).ToArray()
                );

                // Validar CPF e telefone
                if (!long.TryParse(cpfTexto, out long cpf) ||
                    !long.TryParse(telefoneTexto, out long telefone))
                {
                    MessageBox.Show("CPF e telefone devem conter números válidos.");
                    return;
                }

                // Validar número do endereço
                if (!int.TryParse(txtNumero.Text, out int numero))
                {
                    MessageBox.Show("Informe um número de endereço válido.");
                    return;
                }

                // Validar estado
                if (txtEstado.Text.Trim().Length != 2)
                {
                    MessageBox.Show("Informe a sigla do estado com duas letras.");
                    return;
                }

                DAO dao = new DAO();

                bool sucesso = dao.CadastrarAlunoComEndereco(
                    cmbNome.Text.Trim(),
                    cpf,
                    txtboxEmail.Text.Trim(),
                    telefone,
                    DateTime.Parse(txtboxDtNasc.Text),
                    txtCidade.Text.Trim(),
                    txtEstado.Text.Trim().ToUpper(),
                    txtBairro.Text.Trim(),
                    txtLogradouro.Text.Trim(),
                    numero,
                    txtComplemento.Text.Trim()
                );

                if (sucesso)
                {
                    MessageBox.Show(
                        "Aluno e endereço cadastrados com sucesso!"
                    );

                    // Limpar os campos após o cadastro
                    cmbNome.Clear();
                    txtboxCPF.Clear();
                    txtboxEmail.Clear();
                    txtboxTelefone.Clear();
                    txtCidade.Clear();
                    txtEstado.Clear();
                    txtBairro.Clear();
                    txtLogradouro.Clear();
                    txtNumero.Clear();
                    txtComplemento.Clear();

                    cmbNome.Focus();
                }





            }
        }

        private void txtboxCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtboxTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void testeConexao_Click(object sender, EventArgs e)
        {
            DAO dao = new DAO();

            try
            {
                if (dao.Conectar())
                {
                    MessageBox.Show("Conexão realizada com sucesso!");
                }
            }
            finally
            {
                dao.Desconectar();
            }
        }

        private void cmbNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
