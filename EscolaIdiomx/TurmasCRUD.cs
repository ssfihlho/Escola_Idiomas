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
    public partial class TurmasCRUD : Form
    {

        private int? turmaSelecionada = null;
        private bool modoEdicao = false;

        public TurmasCRUD()
        {
            InitializeComponent();

            this.Activated += TurmasCRUD_Activated;
            dgvTurmas.CellClick += dgvTurmas_CellClick;

            // Formatação dos seletores de horário
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.CustomFormat = "HH:mm";
            dtpInicio.ShowUpDown = true;

            dtpFim.Format = DateTimePickerFormat.Custom;
            dtpFim.CustomFormat = "HH:mm";
            dtpFim.ShowUpDown = true;
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarTurmas cadTurma = new CadastrarTurmas();

            if (cadTurma.ShowDialog() == DialogResult.OK)
            {
                CarregarTurmas(); // Recarrega a DataGridView com a nova turma e seu professor vinculado
            }
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void TurmasCRUD_Load(object sender, EventArgs e)
        {
            CarregarCombos();
            CarregarTurmas();
            LimparCampos();
            DefinirEdicao(false);
        }
        private void TurmasCRUD_Activated(object sender, EventArgs e)
        {
            if (modoEdicao || turmaSelecionada.HasValue)
                return;

            CarregarCombos();
        }
        private void CarregarCombos()
        {
            DAO dao = new DAO();

            cmbCurso.DataSource = dao.ListarCursos();
            cmbCurso.DisplayMember = "Nome";
            cmbCurso.ValueMember = "Codigo";
            cmbCurso.SelectedIndex = -1;

            cmbProfessor.DataSource = dao.ListarProfessores();
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "Codigo";
            cmbProfessor.SelectedIndex = -1;
        }
        private void CarregarTurmas(string filtro = "")
        {
            DAO dao = new DAO();
            dgvTurmas.DataSource = dao.ListarTurmas(filtro);

            dgvTurmas.ReadOnly = true;
            dgvTurmas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTurmas.MultiSelect = false;
            dgvTurmas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string[] colunasOcultas = { "Codigo", "CursoCodigo", "ProfessorCodigo" };
            foreach (string col in colunasOcultas)
            {
                if (dgvTurmas.Columns.Contains(col))
                    dgvTurmas.Columns[col].Visible = false;
            } 
        }
            private void dgvTurmas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTurmas.Rows.Count)
                return;

            DataGridViewRow linha = dgvTurmas.Rows[e.RowIndex];

            if (linha.IsNewRow || linha.Cells["Codigo"].Value == null || linha.Cells["Codigo"].Value == DBNull.Value)
                return;

            turmaSelecionada = Convert.ToInt32(linha.Cells["Codigo"].Value);

            if (linha.Cells["CursoCodigo"].Value != DBNull.Value)
                cmbCurso.SelectedValue = Convert.ToInt32(linha.Cells["CursoCodigo"].Value);

            if (linha.Cells["ProfessorCodigo"].Value != DBNull.Value)
                cmbProfessor.SelectedValue = Convert.ToInt32(linha.Cells["ProfessorCodigo"].Value);
            else
                cmbProfessor.SelectedIndex = -1;

            if (TimeSpan.TryParse(linha.Cells["HorarioInicio"].Value?.ToString(), out TimeSpan hInicio))
                dtpInicio.Value = DateTime.Today.Add(hInicio);

            if (TimeSpan.TryParse(linha.Cells["HorarioFim"].Value?.ToString(), out TimeSpan hFim))
                dtpFim.Value = DateTime.Today.Add(hFim);

            modoEdicao = false;
            DefinirEdicao(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!turmaSelecionada.HasValue)
            {
                MessageBox.Show("Selecione uma turma na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modoEdicao = true;
            DefinirEdicao(true);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!modoEdicao || !turmaSelecionada.HasValue)
            {
                MessageBox.Show("Selecione uma turma na tabela e clique em Editar primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCurso.SelectedValue == null)
            {
                MessageBox.Show("Selecione um curso para a turma.");
                cmbCurso.Focus();
                return;
            }

            TimeSpan horarioInicio = dtpInicio.Value.TimeOfDay;
            TimeSpan horarioFim = dtpFim.Value.TimeOfDay;

            if (horarioFim <= horarioInicio)
            {
                MessageBox.Show("O horário final deve ser maior que o horário inicial.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cursoCodigo = Convert.ToInt32(cmbCurso.SelectedValue);
            int? professorCodigo = cmbProfessor.SelectedValue != null ? Convert.ToInt32(cmbProfessor.SelectedValue) : (int?)null;

            DAO dao = new DAO();
            bool atualizado = dao.AtualizarTurma(turmaSelecionada.Value, cursoCodigo, horarioInicio, horarioFim, professorCodigo);

            if (atualizado)
            {
                MessageBox.Show("Turma atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarTurmas();
                LimparCampos();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!turmaSelecionada.HasValue)
            {
                MessageBox.Show("Selecione uma turma na tabela antes de excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir esta turma?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            DAO dao = new DAO();
            if (dao.ExcluirTurma(turmaSelecionada.Value))
            {
                MessageBox.Show("Turma excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarTurmas();
                LimparCampos();
            } 
        }
        
            private void DefinirEdicao(bool habilitar)
        {
            cmbCurso.Enabled = habilitar;
            cmbProfessor.Enabled = habilitar;
            dtpInicio.Enabled = habilitar;
            dtpFim.Enabled = habilitar;
        }

        private void LimparCampos()
        {
            turmaSelecionada = null;
            modoEdicao = false;

            cmbCurso.SelectedIndex = -1;
            cmbProfessor.SelectedIndex = -1;
            dtpInicio.Value = DateTime.Now;
            dtpFim.Value = DateTime.Now.AddHours(1);

            DefinirEdicao(false);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CarregarTurmas(txtBuscar.Text.Trim());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregarTurmas(txtBuscar.Text.Trim());
        }

        private void btnListarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CarregarTurmas();
        }
    }
}
