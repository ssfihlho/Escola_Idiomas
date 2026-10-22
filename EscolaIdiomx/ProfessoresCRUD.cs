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
        private int professorIdSelecionado = 0;
        private int? enderecoCodigoSelecionado = null;

        public ProfessoresCRUD()
        {
            InitializeComponent();
            btnExcluir.Click += new EventHandler(btnExcluir_Click);

            // Associa o evento CellClick da grelha de forma explícita
            dgvProfessores.CellClick += new DataGridViewCellEventHandler(dgvProfessores_CellClick);

        }

        private void ProfessoresCRUD_Load(object sender, EventArgs e)
        {
            // Configura a seleção por linha completa
            dgvProfessores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProfessores.MultiSelect = false;
            dgvProfessores.ReadOnly = true; // Impede editar direto na célula

            // Carrega os dados na grid
            CarregarGridProfessores();


        }

        /// <summary>
        /// Recarrega a tabela de professores e reseta as variáveis de seleção.
        /// </summary>
        private void CarregarGridProfessores(string filtro = "")
        {
            DAO dao = new DAO();
            DataTable dt = dao.ListarProfessores(filtro);

            dgvProfessores.DataSource = dt;

            // Reseta as variáveis e limpa a tela após atualizar a grid
            LimparCampos();
        }

        private void dgvProfessores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Garante que o clique foi em uma linha válida e não no cabeçalho
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvProfessores.Rows[e.RowIndex];

                // Resgata o ID do Professor / Usuário
                if (linha.Cells["Codigo"].Value != DBNull.Value && linha.Cells["Codigo"].Value != null)
                {
                    professorIdSelecionado = Convert.ToInt32(linha.Cells["Codigo"].Value);
                }

                // Resgata o ID do Endereço (se existir)
                if (dgvProfessores.Columns.Contains("EnderecoCodigo") &&
                    linha.Cells["EnderecoCodigo"].Value != DBNull.Value &&
                    linha.Cells["EnderecoCodigo"].Value != null)
                {
                    enderecoCodigoSelecionado = Convert.ToInt32(linha.Cells["EnderecoCodigo"].Value);
                }
                else
                {
                    enderecoCodigoSelecionado = null;
                }

                // Preenche os campos do formulário
                txtNome.Text = linha.Cells["Nome"].Value?.ToString() ?? string.Empty;
                txtCPF.Text = linha.Cells["CPF"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = linha.Cells["Email"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Titulacao"))
                    txtTitulacao.Text = linha.Cells["Titulacao"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Especialidade"))
                    txtEspecialidade.Text = linha.Cells["Especialidade"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Cidade"))
                    txtCidade.Text = linha.Cells["Cidade"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Estado"))
                    txtEstado.Text = linha.Cells["Estado"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Bairro"))
                    txtBairro.Text = linha.Cells["Bairro"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Logradouro"))
                    txtLogradouro.Text = linha.Cells["Logradouro"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Numero"))
                    txtNum.Text = linha.Cells["Numero"].Value?.ToString() ?? string.Empty;

                if (dgvProfessores.Columns.Contains("Complemento"))
                    txtComplemento.Text = linha.Cells["Complemento"].Value?.ToString() ?? string.Empty;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Verifica se existe um professor selecionado na grelha
            if (professorIdSelecionado <= 0)
            {
                MessageBox.Show("Selecione um professor na tabela para editar.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 2. Captura os dados dos controles do formulário
            string nome = txtNome.Text.Trim();
            string cpf = new string(txtCPF.Text.Where(char.IsDigit).ToArray());
            string email = txtEmail.Text.Trim();
            string titulacao = txtTitulacao.Text.Trim();
            string especialidade = txtEspecialidade.Text.Trim();

            // Dados de Endereço
            string logradouro = txtLogradouro.Text.Trim();
            string numero = txtNum.Text.Trim();
            string complemento = txtComplemento.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string estado = txtEstado.Text.Trim();

            // 3. Validação básica de campos obrigatórios
            if (string.IsNullOrWhiteSpace(nome) || cpf.Length != 11 || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Preencha Nome, CPF e E-mail corretamente.",
                                "Campos Pendentes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 4. Executa a atualização transacional no banco de dados via DAO
            DAO dao = new DAO();
            bool sucesso = dao.AtualizarProfessorComEndereco(
                professorIdSelecionado,
                enderecoCodigoSelecionado,
                nome,
                cpf,
                email,
                titulacao,
                especialidade,
                cidade,
                estado,
                bairro,
                logradouro,
                numero,
                complemento
            );

            // 5. Feedback ao utilizador e atualização da grelha
            if (sucesso)
            {
                MessageBox.Show("Dados do professor e endereço atualizados com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                CarregarGridProfessores(); // Recarrega a tabela com as informações atualizadas
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (professorIdSelecionado <= 0)
            {
                MessageBox.Show("Selecione um professor na tabela para excluir.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza que deseja excluir o professor selecionado?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                DAO dao = new DAO();
                bool sucesso = dao.ExcluirProfessor(professorIdSelecionado, enderecoCodigoSelecionado);

                if (sucesso)
                {
                    MessageBox.Show("Professor excluído com sucesso!",
                                    "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    CarregarGridProfessores();
                }
            }
        }

        /// <summary>
        /// Reseta os campos da tela e as variáveis de controle.
        /// </summary>
        private void LimparCampos()
        {
            professorIdSelecionado = 0;
            enderecoCodigoSelecionado = null;

            txtNome.Clear();
            txtCPF.Clear();
            txtEmail.Clear();
            txtTitulacao.Clear();
            txtEspecialidade.Clear();

            txtCidade.Clear();
            txtEstado.Clear();
            txtBairro.Clear();
            txtLogradouro.Clear();
            txtNum.Clear();
            txtComplemento.Clear();
        }

        // Evento do TextBox de busca para filtrar em tempo real
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CarregarGridProfessores(txtBuscar.Text);
        }

        // Eventos dos botões de navegação e auxiliares
        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarProfessores cadProfessor = new CadastrarProfessores();
            cadProfessor.ShowDialog();
            CarregarGridProfessores(); // Recarrega ao voltar do cadastro
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // Botão Editar redirecionado
        {
            btnSalvar_Click(sender, e);
        }

        private void label4_Click(object sender, EventArgs e) { }
        private void txtEspecialidade_TextChanged(object sender, EventArgs e) { }
        private void dgvProfessores_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (professorIdSelecionado <= 0)
            {
                MessageBox.Show("Selecione um professor na tabela para excluir.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir o professor selecionado?",
                "Confirmar Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                DAO dao = new DAO();
                bool sucesso = dao.ExcluirProfessor(professorIdSelecionado, enderecoCodigoSelecionado);

                if (sucesso)
                {
                    MessageBox.Show("Professor excluído com sucesso!",
                                    "Sucesso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    CarregarGridProfessores(); // Atualiza a tabela na tela
                }
            }
        }
    }
}