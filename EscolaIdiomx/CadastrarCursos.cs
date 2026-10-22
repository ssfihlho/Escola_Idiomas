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
    public partial class CadastrarCursos : Form
    {
        public CadastrarCursos()
        {
            InitializeComponent();
        }

        private void CadastrarCursos_Load(object sender, EventArgs e)
        {
            CarregarComboIdiomas();
        }

        private void CarregarComboIdiomas()
        {
            DAO dao = new DAO();
            DataTable dt = dao.ListarIdiomas();

            cmbIdioma.DataSource = dt;
            cmbIdioma.DisplayMember = "nome";   // O texto visível na caixa
            cmbIdioma.ValueMember = "codigo";   // A chave primária (ID) correspondente
            cmbIdioma.SelectedIndex = -1;       // Inicia sem nenhum item selecionado
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            // 1. Validação dos campos do formulário
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe o nome do curso.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (cmbIdioma.SelectedValue == null)
            {
                MessageBox.Show("Selecione um idioma para o curso.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbIdioma.Focus();
                return;
            }

            if (!int.TryParse(txtCargaHoraria.Text.Trim(), out int cargaHoraria) || cargaHoraria <= 0)
            {
                MessageBox.Show("Informe uma carga horária válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCargaHoraria.Focus();
                return;
            }

            if (!decimal.TryParse(txtValor.Text.Trim(), out decimal valor) || valor < 0)
            {
                MessageBox.Show("Informe um valor válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValor.Focus();
                return;
            }

            // 2. Extrai a chave estrangeira (idiomaCodigo) selecionada
            int idiomaCodigo = Convert.ToInt32(cmbIdioma.SelectedValue);

            // 3. Executa o cadastro no banco via DAO
            DAO dao = new DAO();
            bool cadastrado = dao.CadastrarCurso(
                txtNome.Text.Trim(),
                idiomaCodigo,
                cargaHoraria,
                valor
            );

            // 4. Feedback e fechamento do modal
            if (cadastrado)
            {
                MessageBox.Show("Curso cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Sinaliza à tela pai para recarregar a grelha
                this.Close();
            }
        }
    }
}
