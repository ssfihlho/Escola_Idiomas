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
    public partial class CursosCRUD : Form
    {
        // Instanciando Variáveis de Estado
        private int? cursoSelecionado = null;
        private bool modoEdicao = false;
        public CursosCRUD()
        {
            InitializeComponent();
            this.Activated += CursosCRUD_Activated;
            dgvCursos.CellClick += dgvCursos_CellClick; // Garantia de vínculo da grid
        }
        private void CursosCRUD_Activated(object sender, EventArgs e)
        {
            // Não recarrega o combo se o usuário já selecionou um item na grid ou está editando
            if (modoEdicao || cursoSelecionado.HasValue)
                return;

            object valorAnterior = cmbIdioma.SelectedValue;
            CarregarComboIdiomas();

            if (valorAnterior != null)
                cmbIdioma.SelectedValue = valorAnterior;
        
        }

        private void CursosCRUD_Load(object sender, EventArgs e)
        {
            CarregarComboIdiomas();
            CarregarCursos();
            LimparCampos();
            DefinirEdicao(false);
        }

        private void CarregarComboIdiomas()
        {
            DAO dao = new DAO();
            DataTable dt = dao.ListarIdiomas();

            cmbIdioma.DataSource = dt;
            cmbIdioma.DisplayMember = "nome";
            cmbIdioma.ValueMember = "codigo";
            cmbIdioma.SelectedIndex = -1; // Mantém o campo limpo
        }

        private void CarregarCursos(string filtro = "")
        {
            DAO dao = new DAO();
            dgvCursos.DataSource = dao.ListarCursos(filtro);

            dgvCursos.ReadOnly = true;
            dgvCursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCursos.MultiSelect = false;
            dgvCursos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvCursos.Columns.Contains("Codigo"))
                dgvCursos.Columns["Codigo"].Visible = false;

            if (dgvCursos.Columns.Contains("IdiomaCodigo"))
                dgvCursos.Columns["IdiomaCodigo"].Visible = false;
        }

        private void dgvCursos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvCursos.Rows.Count)
                return;

            DataGridViewRow linha = dgvCursos.Rows[e.RowIndex];

            if (linha.IsNewRow || linha.Cells["Codigo"].Value == null || linha.Cells["Codigo"].Value == DBNull.Value)
                return;

            cursoSelecionado = Convert.ToInt32(linha.Cells["Codigo"].Value);

            txtNome.Text = Convert.ToString(linha.Cells["Nome"].Value);

            if (dgvCursos.Columns.Contains("IdiomaCodigo") && linha.Cells["IdiomaCodigo"].Value != DBNull.Value)
            {
                cmbIdioma.SelectedValue = Convert.ToInt32(linha.Cells["IdiomaCodigo"].Value);
            }

            txtCargaHoraria.Text = Convert.ToString(linha.Cells["CargaHoraria"].Value);
            txtValor.Text = Convert.ToString(linha.Cells["Valor"].Value);

            modoEdicao = false;
            DefinirEdicao(false);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cursoSelecionado == null)
            {
                MessageBox.Show("Selecione um curso na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modoEdicao = true;
            DefinirEdicao(true);
            txtNome.Focus();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastrarCursos cad = new CadastrarCursos();
            cad.ShowDialog();
            CarregarCursos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!modoEdicao || cursoSelecionado == null)
            {
                MessageBox.Show("Selecione um curso na tabela e clique em Editar primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do curso.");
                txtNome.Focus();
                return;
            }

            if (cmbIdioma.SelectedValue == null)
            {
                MessageBox.Show("Selecione um idioma.");
                cmbIdioma.Focus();
                return;
            }

            if (!int.TryParse(txtCargaHoraria.Text.Trim(), out int cargaHoraria) || cargaHoraria <= 0)
            {
                MessageBox.Show("Informe uma carga horária válida.");
                txtCargaHoraria.Focus();
                return;
            }

            if (!decimal.TryParse(txtValor.Text.Trim(), out decimal valor) || valor < 0)
            {
                MessageBox.Show("Informe um valor válido.");
                txtValor.Focus();
                return;
            }

            DAO dao = new DAO();
            int idiomaCodigo = Convert.ToInt32(cmbIdioma.SelectedValue);

            bool atualizado = dao.AtualizarCurso(
                cursoSelecionado.Value,
                txtNome.Text.Trim(),
                idiomaCodigo,
                cargaHoraria,
                valor
            );

            if (atualizado)
            {
                MessageBox.Show("Curso atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarCursos();
                LimparCampos();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!cursoSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um curso na lista antes de excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir este curso?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            DAO dao = new DAO();
            if (dao.ExcluirCurso(cursoSelecionado.Value))
            {
                MessageBox.Show("Curso excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarCursos();
                LimparCampos();
            }
        }

        private void btnListarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CarregarCursos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregarCursos(txtBuscar.Text.Trim());
        }

        private void DefinirEdicao(bool habilitar)
        {
            txtNome.ReadOnly = !habilitar;
            cmbIdioma.Enabled = habilitar;
            txtCargaHoraria.ReadOnly = !habilitar;
            txtValor.ReadOnly = !habilitar;
        }

        private void LimparCampos()
        {
            cursoSelecionado = null;
            modoEdicao = false;

            txtNome.Clear();
            cmbIdioma.SelectedIndex = -1;
            txtCargaHoraria.Clear();
            txtValor.Clear();
        }
    }
}
