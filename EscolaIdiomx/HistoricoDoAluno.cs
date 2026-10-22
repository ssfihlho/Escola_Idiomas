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
    public partial class HistoricoDoAluno : Form
    {
        public HistoricoDoAluno()
        {
            InitializeComponent();
        }

        private void HistoricoDoAluno_Load(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }


        private void CarregarHistorico(int alunoCodigo)
        {
            DAO dao = new DAO();
            DataTable dt = dao.ObterHistoricoAluno(alunoCodigo);

            dgvHistorico.DataSource = dt;
            dgvHistorico.ReadOnly = true;
            dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvHistorico.Columns.Contains("Codigo"))
                dgvHistorico.Columns["Codigo"].Visible = false;

            // Calcula a Média Geral do Aluno nas notas exibidas
            if (dt.Rows.Count > 0)
            {
                decimal soma = 0;
                foreach (DataRow row in dt.Rows)
                {
                    soma += Convert.ToDecimal(row["Nota"]);
                }
                decimal media = soma / dt.Rows.Count;
                lblMedia.Text = $"Média do Aluno: {media:F2}";
            }
            else
            {
                lblMedia.Text = "Média do Aluno: N/A";
            }
        }

        private void button1_Click(object sender, EventArgs e) //Botao de busca
        {
            BuscarHistorico();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarHistorico();
        }
    
    private void BuscarHistorico()
        {
            string filtro = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                dgvHistorico.DataSource = null;
                lblMedia.Text = "Média do Aluno: N/A";
                return;
            }

            DAO dao = new DAO();
            DataTable dt = dao.ObterHistoricoAlunoPorFiltro(filtro);

            dgvHistorico.DataSource = dt;
            dgvHistorico.ReadOnly = true;
            dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorico.MultiSelect = false;
            dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Calcula e exibe a média geral das avaliações encontradas
            if (dt.Rows.Count > 0)
            {
                decimal soma = 0;
                foreach (DataRow row in dt.Rows)
                {
                    soma += Convert.ToDecimal(row["Nota"]);
                }
                decimal media = soma / dt.Rows.Count;
                lblMedia.Text = $"Média do Aluno: {media:F2}";
            }
            else
            {
                lblMedia.Text = "Média do Aluno: N/A";
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    } 
}
