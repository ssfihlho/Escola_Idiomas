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
    public partial class CadastrarTurmas : Form
    {
        public CadastrarTurmas()
        {
            InitializeComponent();
            // Configuração dos controles de horário
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.CustomFormat = "HH:mm";
            dtpInicio.ShowUpDown = true;

            dtpFim.Format = DateTimePickerFormat.Custom;
            dtpFim.CustomFormat = "HH:mm";
            dtpFim.ShowUpDown = true;
        }

        private void CadastrarTurmas_Load(object sender, EventArgs e)
        {
            CarregarCombos();
        }

        private void CarregarCombos()
        {
            DAO dao = new DAO();

            // Popula Cursos
            cmbCurso.DataSource = dao.ListarCursos();
            cmbCurso.DisplayMember = "Nome";
            cmbCurso.ValueMember = "Codigo";
            cmbCurso.SelectedIndex = -1;

            // Popula Professores
            cmbProfessor.DataSource = dao.ListarProfessores();
            cmbProfessor.DisplayMember = "Nome";
            cmbProfessor.ValueMember = "Codigo";
            cmbProfessor.SelectedIndex = -1;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validação dos seletores obrigatórios
            if (cmbCurso.SelectedValue == null)
            {
                MessageBox.Show("Selecione um curso para a turma.",
                                "Campo Obrigatório",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                cmbCurso.Focus();
                return;
            }

            TimeSpan horarioInicio = dtpInicio.Value.TimeOfDay;
            TimeSpan horarioFim = dtpFim.Value.TimeOfDay;

            // 2. Validação lógica do intervalo de tempo
            if (horarioFim <= horarioInicio)
            {
                MessageBox.Show("O horário final deve ser superior ao horário inicial.",
                                "Horário Inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 3. Captura o professor se houver seleção
            int? professorCodigo = cmbProfessor.SelectedValue != null
                ? Convert.ToInt32(cmbProfessor.SelectedValue)
                : (int?)null;

            int cursoCodigo = Convert.ToInt32(cmbCurso.SelectedValue);

            // 4. Gravação via DAO com transação
            DAO dao = new DAO();
            bool cadastrado = dao.CadastrarTurma(cursoCodigo, horarioInicio, horarioFim, professorCodigo);

            if (cadastrado)
            {
                MessageBox.Show("Turma cadastrada com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Atualiza a lista na tela pai
                this.Close();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAcessar_Click(object sender, EventArgs e)
        {
            // ... validações ...

            // 1. Extrai apenas o horário (TimeSpan) dos DateTimePickers
            TimeSpan horarioInicio = dtpInicio.Value.TimeOfDay;
            TimeSpan horarioFim = dtpFim.Value.TimeOfDay;

            // 2. Extrai o ID do curso
            int cursoCodigo = Convert.ToInt32(cmbCurso.SelectedValue);

            // 3. Extrai o ID do professor (ou nulo)
            int? professorCodigo = cmbProfessor.SelectedValue != null
                ? Convert.ToInt32(cmbProfessor.SelectedValue)
                : (int?)null;

            DAO dao = new DAO();

            // 4. Passa as variáveis tratadas na ordem correta
            bool cadastrado = dao.CadastrarTurma(
                cursoCodigo,      // 1º parâmetro (int)
                horarioInicio,    // 2º parâmetro (TimeSpan)
                horarioFim,       // 3º parâmetro (TimeSpan)
                professorCodigo   // 4º parâmetro (int?)
            );

            if (cadastrado)
            {
                MessageBox.Show("Turma cadastrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
