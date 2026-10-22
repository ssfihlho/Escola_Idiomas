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
    public partial class IdiomasCRUD : Form
    {
        private int? idiomaSelecionado = null;
        private bool modoEdicao = false;

        public IdiomasCRUD()
        {
            InitializeComponent();
            dgvIdiomas.CellClick += dgvIdiomas_CellClick;
        }

        private void IdiomasCRUD_Load(object sender, EventArgs e)
        {
            CarregarIdiomas();
            LimparCampos();

         
        }

        private void CarregarIdiomas(string filtro = "")
        {
            DAO dao = new DAO();
            dgvIdiomas.DataSource = dao.ListarIdiomas(filtro);

            dgvIdiomas.ReadOnly = true;
            dgvIdiomas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIdiomas.MultiSelect = false;
            dgvIdiomas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvIdiomas.Columns.Contains("Codigo"))
                dgvIdiomas.Columns["Codigo"].Visible = false;
        }

        private void dgvIdiomas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvIdiomas.Rows.Count)
                return;

            DataGridViewRow linha = dgvIdiomas.Rows[e.RowIndex];

            if (linha.IsNewRow || linha.Cells["Codigo"].Value == null || linha.Cells["Codigo"].Value == DBNull.Value)
                return;

            idiomaSelecionado = Convert.ToInt32(linha.Cells["Codigo"].Value);
            txtNome.Text = Convert.ToString(linha.Cells["Nome"].Value);

            modoEdicao = false;
            DefinirEdicao(false);
        }



        private void butAcessar_Click(object sender, EventArgs e)
        {
            CadastroIdiomas telaCad = new CadastroIdiomas();
            if (telaCad.ShowDialog() == DialogResult.OK)
            {
                CarregarIdiomas();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
                if (!idiomaSelecionado.HasValue)
                {
                    MessageBox.Show("Selecione um idioma na tabela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                modoEdicao = true;
                DefinirEdicao(true);
                txtNome.Focus();
            }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!modoEdicao || !idiomaSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um idioma e clique em Editar primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do idioma.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            DAO dao = new DAO();
            bool atualizado = dao.AtualizarIdioma(idiomaSelecionado.Value, txtNome.Text.Trim());

            if (atualizado)
            {
                MessageBox.Show("Idioma atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarIdiomas();
                LimparCampos();
            }

            }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!idiomaSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um idioma na tabela antes de excluir.",
                                "Atenção",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show(
                "Tem certeza de que deseja excluir este idioma?",
                "Confirmar exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacao != DialogResult.Yes)
                return;

            DAO dao = new DAO();
            bool excluido = dao.ExcluirIdioma(idiomaSelecionado.Value);

            if (excluido)
            {
                MessageBox.Show("Idioma excluído com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                CarregarIdiomas();
                LimparCampos();
            }

        }

        private void DefinirEdicao(bool habilitar)
        {
            txtNome.ReadOnly = !habilitar;
        }

        private void LimparCampos()
        {
            idiomaSelecionado = null;
            modoEdicao = false;
            txtNome.Clear();
            DefinirEdicao(true);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregarIdiomas(txtBuscar.Text.Trim());
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CarregarIdiomas(txtBuscar.Text.Trim());
        }
    }
}
