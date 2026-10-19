using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EscolaIdiomx
{
    internal class DAO
    {
        public MySqlConnection conexao;

        public DAO()
        {
            conexao = new MySqlConnection("server=localhost;Database=escolaIdiomas;Uid=root;password=");

        }
        public bool Conectar()
        {
            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                {
                    conexao.Open();
                }

                return true;
            }
            catch (MySqlException erro)
            {
                MessageBox.Show(
                    "Erro ao conectar ao banco:\n" + erro.Message
                );

                return false;
            }

        }

        public void Desconectar()
        {
            if (conexao != null &&
                conexao.State != System.Data.ConnectionState.Closed)
            {
                conexao.Close();
            }
        }



        public bool CadastrarAlunoComEndereco(
            string nome,
            long cpf,
            string email,
            long telefone,
            DateTime dataNascimento,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            int numero,
            string complemento)
        {
            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                // 1. Inserir o endereço
                string sqlEndereco = @"
            INSERT INTO endereco
            (cidade, estado, bairro, logradouro, numero, complemento)
            VALUES
            (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);
        ";

                int enderecoCodigo;

                using (MySqlCommand comando = new MySqlCommand(
                    sqlEndereco, conexao, transacao))
                {
                    comando.Parameters.AddWithValue("@cidade", cidade);
                    comando.Parameters.AddWithValue("@estado", estado);
                    comando.Parameters.AddWithValue("@bairro", bairro);
                    comando.Parameters.AddWithValue("@logradouro", logradouro);
                    comando.Parameters.AddWithValue("@numero", numero);

                    comando.Parameters.AddWithValue(
                        "@complemento",
                        string.IsNullOrWhiteSpace(complemento)
                            ? (object)DBNull.Value
                            : complemento
                    );

                    comando.ExecuteNonQuery();

                    // Recuperar o código gerado pelo MySQL
                    comando.CommandText = "SELECT LAST_INSERT_ID();";
                    comando.Parameters.Clear();

                    enderecoCodigo = Convert.ToInt32(
                        comando.ExecuteScalar()
                    );
                }

                // 2. Inserir o aluno usando o código do endereço
                string sqlAluno = @"
            INSERT INTO aluno
            (nome, cpf, email, telefone, dataNascimento, enderecoCodigo)
            VALUES
            (@nome, @cpf, @email, @telefone, @dataNascimento, @enderecoCodigo);
        ";

                using (MySqlCommand comando = new MySqlCommand(
                    sqlAluno, conexao, transacao))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                    comando.Parameters.AddWithValue("@enderecoCodigo", enderecoCodigo);

                    comando.ExecuteNonQuery();
                }

                // 3. Confirmar as duas operações
                transacao.Commit();

                return true;
            }
            catch (Exception erro)
            {
                if (transacao != null)
                {
                    try
                    {
                        transacao.Rollback();
                    }
                    catch (MySqlException)
                    {
                        // A conexão pode ter sido perdida.
                    }
                }

                MessageBox.Show(
                    "Não foi possível cadastrar aluno e endereço:\n" +
                    erro.Message
                );

                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }





    }//Fim da classe
}//Fim do projeto
