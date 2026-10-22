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
    public partial class CadastrarProfessores : Form
    {

        // Variável privada para armazenar o ID do usuário gerado no Passo 1
        private readonly int _usuarioCodigo;

        // Construtor Padrão (mantido para compatibilidade com o Designer do WinForms)
        public CadastrarProfessores()
        {
            InitializeComponent();
        }

        // CONSTRUTOR PERSONALIZADO: Recebe o usuarioCodigo enviado da Tela 1
        public CadastrarProfessores(int usuarioCodigo) : this()
        {
            _usuarioCodigo = usuarioCodigo;
        }

        /// <summary>
        /// Evento do botão Concluir: Valida as entradas e salva o Endereço e o Docente.
        /// </summary>
        private void butAcessar_Click(object sender, EventArgs e)
        {
            if (_usuarioCodigo <= 0)
            {
                MessageBox.Show("Erro de integração: Código de usuário inválido.",
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // Captura das informações em formato de texto livre
            string titulacao = txtTitulacao.Text.Trim();
            string especialidade = txtEspecialidade.Text.Trim(); // Agora capturado do TextBox

            // Dados do Endereço Residencial
            string logradouro = txtLogradouro.Text.Trim();
            string numero = txtNum.Text.Trim();
            string complemento = txtComplemento.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string estado = txtEstado.Text.Trim();

            // Validação simples de campos obrigatórios de endereço
            if (string.IsNullOrWhiteSpace(logradouro) ||
                string.IsNullOrWhiteSpace(numero) ||
                string.IsNullOrWhiteSpace(bairro) ||
                string.IsNullOrWhiteSpace(cidade) ||
                string.IsNullOrWhiteSpace(estado))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios do endereço.",
                                "Campos Pendentes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Execução da gravação via DAO
            DAO dao = new DAO();
            bool sucesso = dao.CadastrarDadosProfessorEEndereco(
                _usuarioCodigo,
                titulacao,
                especialidade,
                cidade,
                estado,
                bairro,
                logradouro,
                numero,
                complemento
            );

            if (sucesso)
            {
                MessageBox.Show("Cadastro do docente e endereço concluído com sucesso!",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                this.Close(); // Fecha a Tela 2 e encerra o fluxo
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
