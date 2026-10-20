using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

            DateTime dataNascimento;

            if (!DateTime.TryParseExact(
                txtboxDtNasc.Text,
                "dd/MM/yyyy",
                CultureInfo.GetCultureInfo("pt-BR"),
                DateTimeStyles.None,
                out dataNascimento))
            {
                MessageBox.Show(
                    "Informe uma data válida no formato DD/MM/AAAA.",
                    "Data inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtboxDtNasc.Focus();
                return;
            }

            // Impedir datas futuras
            if (dataNascimento.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "A data de nascimento não pode ser futura.",
                    "Data inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtboxDtNasc.Focus();
                return;
            }

            // Impedir datas anteriores a 120 anos
            if (dataNascimento.Date < DateTime.Today.AddYears(-120))
            {
                MessageBox.Show(
                    "Informe uma data de nascimento dentro dos últimos 120 anos.",
                    "Data inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtboxDtNasc.Focus();
                return;
            }



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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
