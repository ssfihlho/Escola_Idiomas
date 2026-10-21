using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace EscolaIdiomx
{
    public partial class AlunosCRUD : Form
    {
        //Instanciando Variaveis
        private int? alunoSelecionado = null;
        private int? enderecoSelecionado = null;
        private bool modoEdicao = false;

        public AlunosCRUD()
        {
            InitializeComponent();
        }

        private void CarregarAlunos(string filtro = "")
        {
            DAO dao = new DAO();

            dgvAlunos.DataSource = dao.ListarAlunos(filtro);

            dgvAlunos.ReadOnly = true;
            dgvAlunos.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvAlunos.MultiSelect = false;
            dgvAlunos.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvAlunos.Columns.Contains("EnderecoCodigo"))
                dgvAlunos.Columns["EnderecoCodigo"].Visible = false;

            if (dgvAlunos.Columns.Contains("Codigo"))
                dgvAlunos.Columns["Codigo"].Visible = false;
        }

        private void dgvAlunos_CellClick(
    object sender, DataGridViewCellEventArgs e)
        {
         
                // Ignora cliques no cabeçalho ou fora das linhas
                if (e.RowIndex < 0 || e.RowIndex >= dgvAlunos.Rows.Count)
                    return;

                DataGridViewRow linha = dgvAlunos.Rows[e.RowIndex];

                // Ignora linhas vazias ou sem código de aluno
            if (linha.IsNewRow ||
                    linha.Cells["Codigo"].Value == null ||
                    linha.Cells["Codigo"].Value == DBNull.Value)
                    return;

                object enderecoCodigo =
                linha.Cells["EnderecoCodigo"].Value;

            enderecoSelecionado =
                enderecoCodigo == DBNull.Value
                    ? (int?)null
                    : Convert.ToInt32(enderecoCodigo);
            alunoSelecionado =
            Convert.ToInt32(linha.Cells["Codigo"].Value);

            txtNome.Text =
                Convert.ToString(linha.Cells["Nome"].Value);

            txtCPF.Text =
                Convert.ToString(linha.Cells["CPF"].Value);

            txtEmail.Text =
                Convert.ToString(linha.Cells["Email"].Value);

            txtTelefone.Text =
                Convert.ToString(linha.Cells["Telefone"].Value);

            object nascimento =
                linha.Cells["DataNascimento"].Value;

            txtDtNasc.Text =
                nascimento == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(nascimento)
                        .ToString("dd/MM/yyyy");

            txtCidade.Text =
                Convert.ToString(linha.Cells["Cidade"].Value);

            txtEstado.Text =
                Convert.ToString(linha.Cells["Estado"].Value);

            txtBairro.Text =
                Convert.ToString(linha.Cells["Bairro"].Value);

            txtLogradouro.Text =
                Convert.ToString(linha.Cells["Logradouro"].Value);

            txtNum.Text =
                Convert.ToString(linha.Cells["Numero"].Value);

            txtComplemento.Text =
                Convert.ToString(linha.Cells["Complemento"].Value);

            modoEdicao = false;

            alunoSelecionado =
            Convert.ToInt32(linha.Cells["Codigo"].Value);

            MessageBox.Show(
                "Aluno selecionado: " + alunoSelecionado);
        }

        private void AlunosCRUD_Load(object sender, EventArgs e)
        {
            CarregarAlunos();
            LimparCampos();

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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CarregarAlunos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CarregarAlunos(txtBuscar.Text.Trim());
        }

        private void btnEditar_Click(
    object sender, EventArgs e)
        {
            if (alunoSelecionado == null)
            {
                MessageBox.Show(
                    "Selecione um aluno na tabela.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            modoEdicao = true;

            MessageBox.Show(
                "Altere os dados desejados e clique em Salvar.");
        }

        private void LimparCampos()
        {
            alunoSelecionado = null;
            enderecoSelecionado = null;
            modoEdicao = false;

            txtNome.Clear();
            txtCPF.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtDtNasc.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
            txtBairro.Clear();
            txtLogradouro.Clear();
            txtNum.Clear();
            txtComplemento.Clear();
        }


        private void DefinirEdicao(bool habilitar)
        {
            txtNome.ReadOnly = !habilitar;
            txtCPF.ReadOnly = !habilitar;
            txtEmail.ReadOnly = !habilitar;
            txtTelefone.ReadOnly = !habilitar;
            txtDtNasc.ReadOnly = !habilitar;
            txtCidade.ReadOnly = !habilitar;
            txtEstado.ReadOnly = !habilitar;
            txtBairro.ReadOnly = !habilitar;
            txtLogradouro.ReadOnly = !habilitar;
            txtNum.ReadOnly = !habilitar;
            txtComplemento.ReadOnly = !habilitar;
        }


        private void dgvAlunos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (alunoSelecionado == null)
            {
                MessageBox.Show("Selecione um aluno na tabela.");
                return;
            }

            modoEdicao = true;
            DefinirEdicao(true);

            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            if (!modoEdicao || alunoSelecionado == null)
            {
                MessageBox.Show(
                    "Selecione um aluno e clique em Editar primeiro.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do aluno.");
                txtNome.Focus();
                return;
            }

            if (!DateTime.TryParseExact(
                txtDtNasc.Text.Trim(),
                "dd/MM/yyyy",
                CultureInfo.GetCultureInfo("pt-BR"),
                DateTimeStyles.None,
                out DateTime dataNascimento))
            {
                MessageBox.Show(
                    "Informe uma data de nascimento válida no formato dd/MM/aaaa.");
                txtDtNasc.Focus();
                return;
            }

            if (dataNascimento.Date > DateTime.Today)
            {
                MessageBox.Show(
                    "A data de nascimento não pode ser futura.");
                txtDtNasc.Focus();
                return;
            }

            DAO dao = new DAO();
            
            txtCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            string cpf = new string(
                txtCPF.Text.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                MessageBox.Show(
                    "O CPF deve conter exatamente 11 dígitos.",
                    "CPF inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtCPF.Focus();
                return;
            }
            bool atualizado = dao.AtualizarAluno(
                alunoSelecionado.Value,
                enderecoSelecionado,
                txtNome.Text.Trim(),
                cpf,
                txtEmail.Text.Trim(),
                new string(txtTelefone.Text.Where(char.IsDigit).ToArray()),
                dataNascimento,
                txtCidade.Text.Trim(),
                txtEstado.Text.Trim(),
                txtBairro.Text.Trim(),
                txtLogradouro.Text.Trim(),
                txtNum.Text.Trim(),
                txtComplemento.Text.Trim()
            );

            if (atualizado)
            {
                MessageBox.Show(
                    "Aluno atualizado com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DefinirEdicao(false);
                CarregarAlunos();
                LimparCampos();
            
            }

    }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!alunoSelecionado.HasValue)
            {
                MessageBox.Show(
                    "Selecione um aluno na lista antes de excluir.",
                    "Atenção",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir este aluno?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacao != DialogResult.Yes)
                return;

            DAO dao = new DAO();

            bool excluido = dao.ExcluirAluno(alunoSelecionado.Value);

            if (excluido)
            {
                MessageBox.Show("Aluno excluído com sucesso!");

                alunoSelecionado = null;
                enderecoSelecionado = null;

                // Atualize a listagem aqui.
            }
        }
    }
}
