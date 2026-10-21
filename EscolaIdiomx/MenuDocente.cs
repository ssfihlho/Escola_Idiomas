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
    public partial class MenuDocente : Form
    {
        public MenuDocente()
        {
            InitializeComponent();
        }

        private void btnFrequencias_Click(object sender, EventArgs e)
        {
            LancamentoFrequencia lancFrequencia = new LancamentoFrequencia();
            lancFrequencia.ShowDialog();
        }

        private void btnAvaliacoes_Click(object sender, EventArgs e)
        {
            LancamentoAvaliacoes lancAvaliacoes = new LancamentoAvaliacoes();
            lancAvaliacoes.ShowDialog();
        }

        private void btnHistoricoAluno_Click(object sender, EventArgs e)
        {
            HistoricoDoAluno historicoDoAluno = new HistoricoDoAluno();
            historicoDoAluno.ShowDialog();
        }
    }
}
