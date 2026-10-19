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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoginDocent_Click(object sender, EventArgs e)
        {
            LoginDocentes log = new LoginDocentes();
            log.ShowDialog();
        }

        private void btnLoginAdm_Click(object sender, EventArgs e)
        {
            LoginAdm log = new LoginAdm();
            log.ShowDialog();
        }

        private void btnSecretaria_Click(object sender, EventArgs e)
        {
            LoginSecretaria log = new LoginSecretaria();
            log.ShowDialog();
        }
    }
}
