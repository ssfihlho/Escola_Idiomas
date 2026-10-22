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
    public partial class MatricularAluno : Form
    {
        private int? matriculaSelecionada = null;
        private int? alunoSelecionado = null;
        public MatricularAluno()
        {

            InitializeComponent();
            this.Activated += MatricularAlunoCRUD_Activated;

            dgvMatriculas.CellClick += dgvMatriculas_CellClick;
        }

        private void MatricularAluno_Load(object sender, EventArgs e)
        {
            CarregarCombosETabelas();
            LimparCampos();
        }

        private void MatricularAlunoCRUD_Activated(object sender, EventArgs e)
        {
            if (matriculaSelecionada.HasValue)
                return;

            CarregarCombosETabelas();
        
        }

        private void CarregarCombosETabelas()
        {
            DAO dao = new DAO();

            // 1. Carrega Alunos na ComboBox
            DataTable dtAlunos = dao.ListarAlunos();
            cmbAluno.DataSource = dtAlunos;
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Codigo";
            cmbAluno.SelectedIndex = -1;

            // 2. Se houver a DataGridView dgvAlunos na tela, exibe a lista de alunos cadastrados
            if (Controls.ContainsKey("dgvAlunos") && Controls["dgvAlunos"] is DataGridView dgvAlunos)
            {
                dgvAlunos.DataSource = dtAlunos;
                dgvAlunos.ReadOnly = true;
                dgvAlunos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAlunos.MultiSelect = false;
                dgvAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvAlunos.Columns.Contains("Codigo"))
                    dgvAlunos.Columns["Codigo"].Visible = false;
            }

            // 3. Carrega Turmas com identificação formatada (Curso + Horários)
            DataTable dtTurmas = dao.ListarTurmas();
            dtTurmas.Columns.Add("DescricaoFormatada", typeof(string));

            foreach (DataRow row in dtTurmas.Rows)
            {
                row["DescricaoFormatada"] = $"Turma #{row["Codigo"]} - {row["Curso"]} ({row["HorarioInicio"]} às {row["HorarioFim"]})";
            }

            cmbTurma.DataSource = dtTurmas;
            cmbTurma.DisplayMember = "DescricaoFormatada";
            cmbTurma.ValueMember = "Codigo";
            cmbTurma.SelectedIndex = -1;

            // 4. Carrega Matrículas Efetuadas na dgvMatriculas
            CarregarMatriculas();
        }

        private void CarregarCombos()
        {
            DAO dao = new DAO();

            // Popula Alunos
            cmbAluno.DataSource = dao.ListarAlunos();
            cmbAluno.DisplayMember = "Nome";
            cmbAluno.ValueMember = "Codigo";
            cmbAluno.SelectedIndex = -1;

            // Popula Turmas formatando o texto de exibição (Turma #ID - Curso (Horário))
            DataTable dtTurmas = dao.ListarTurmas();
            dtTurmas.Columns.Add("DescricaoFormatada", typeof(string));

            foreach (DataRow row in dtTurmas.Rows)
            {
                row["DescricaoFormatada"] = $"Turma #{row["Codigo"]} - {row["Curso"]} ({row["HorarioInicio"]} às {row["HorarioFim"]})";
            }

            cmbTurma.DataSource = dtTurmas;
            cmbTurma.DisplayMember = "DescricaoFormatada";
            cmbTurma.ValueMember = "Codigo";
            cmbTurma.SelectedIndex = -1;
        }

        private void CarregarMatriculas(string filtro = "")
        {
            DAO dao = new DAO();
            dgvMatriculas.DataSource = dao.ListarMatriculas(filtro);

            dgvMatriculas.ReadOnly = true;
            dgvMatriculas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatriculas.MultiSelect = false;
            dgvMatriculas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string[] colunasOcultas = { "Codigo", "AlunoCodigo", "TurmaCodigo" };
            foreach (string col in colunasOcultas)
            {
                if (dgvMatriculas.Columns.Contains(col))
                    dgvMatriculas.Columns[col].Visible = false;
            }
        }

        private void dgvAlunos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView dgv && e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
            {
                DataGridViewRow linha = dgv.Rows[e.RowIndex];
                if (!linha.IsNewRow && linha.Cells["Codigo"].Value != DBNull.Value)
                {
                    alunoSelecionado = Convert.ToInt32(linha.Cells["Codigo"].Value);
                    cmbAluno.SelectedValue = alunoSelecionado;
                }
            }
        }

        private void dgvMatriculas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvMatriculas.Rows.Count)
                return;

            DataGridViewRow linha = dgvMatriculas.Rows[e.RowIndex];

            if (linha.IsNewRow || linha.Cells["Codigo"].Value == null || linha.Cells["Codigo"].Value == DBNull.Value)
                return;

            // CAPTURA A CHAVE PRIMÁRIA DA MATRÍCULA
            matriculaSelecionada = Convert.ToInt32(linha.Cells["Codigo"].Value);

            if (linha.Cells["AlunoCodigo"].Value != DBNull.Value)
                cmbAluno.SelectedValue = Convert.ToInt32(linha.Cells["AlunoCodigo"].Value);

            if (linha.Cells["TurmaCodigo"].Value != DBNull.Value)
                cmbTurma.SelectedValue = Convert.ToInt32(linha.Cells["TurmaCodigo"].Value);
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            if (cmbAluno.SelectedValue == null)
            {
                MessageBox.Show("Selecione o aluno que deseja matricular.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAluno.Focus();
                return;
            }

            if (cmbTurma.SelectedValue == null)
            {
                MessageBox.Show("Selecione a turma.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTurma.Focus();
                return;
            }

            int alunoCodigo = Convert.ToInt32(cmbAluno.SelectedValue);
            int turmaCodigo = Convert.ToInt32(cmbTurma.SelectedValue);

            DAO dao = new DAO();
            bool matriculado = dao.RealizarMatricula(alunoCodigo, turmaCodigo);

            if (matriculado)
            {
                MessageBox.Show("Aluno matriculado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarMatriculas();
                LimparCampos();
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            if (!matriculaSelecionada.HasValue)
            {
                MessageBox.Show("Selecione uma matrícula na tabela abaixo para poder cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja cancelar esta matrícula?",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            DAO dao = new DAO();
            bool cancelado = dao.CancelarMatricula(matriculaSelecionada.Value);

            if (cancelado)
            {
                MessageBox.Show("Matrícula cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarMatriculas();
                LimparCampos();
            }
        }
        private void LimparCampos()
        {
            matriculaSelecionada = null;
            alunoSelecionado = null;

            cmbAluno.SelectedIndex = -1;
            cmbTurma.SelectedIndex = -1;
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            CarregarMatriculas(txtBusca.Text.Trim());
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnListarTodas_Click(object sender, EventArgs e)
        {
            txtBusca.Clear();
            CarregarMatriculas();
        }
    }
}
