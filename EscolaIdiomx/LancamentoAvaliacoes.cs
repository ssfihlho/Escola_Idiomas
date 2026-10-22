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
    public partial class LancamentoAvaliacoes : Form
    {
        private int? avaliacaoSelecionada = null;
        private bool modoEdicao = false;
        public LancamentoAvaliacoes()
        {
            InitializeComponent();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            if (cmbAluno.SelectedValue == null || cmbTurma.SelectedValue == null)
            {
                MessageBox.Show("Selecione o aluno e a turma.");
                return;
            }

            if (!decimal.TryParse(txtNota.Text.Trim(), out decimal nota) || nota < 0 || nota > 10)
            {
                MessageBox.Show("Informe uma nota válida entre 0 e 10.");
                txtNota.Focus();
                return;
            }

            int alunoCodigo = Convert.ToInt32(cmbAluno.SelectedValue);
            int turmaCodigo = Convert.ToInt32(cmbTurma.SelectedValue);

            DataRowView drv = cmbTurma.SelectedItem as DataRowView;
            int cursoCodigo = Convert.ToInt32(drv["CursoCodigo"]);

            DAO dao = new DAO();
            bool lancado = dao.CadastrarAvaliacao(alunoCodigo, cursoCodigo, turmaCodigo, nota, txtObservacao.Text.Trim(), dtpData.Value);

            if (lancado)
            {
                MessageBox.Show("Avaliação lançada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNota.Clear();
                txtObservacao.Clear();
            }
        }
        

        private void LancamentoAvaliacoes_Load(object sender, EventArgs e)
        {

        }
    }
}
