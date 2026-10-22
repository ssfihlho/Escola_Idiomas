using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EscolaIdiomx
{
    /// <summary>
    /// Camada de acesso a dados (DAO) da escola de idiomas.
    /// Centraliza conexão, autenticação e CRUD de usuários e alunos.
    /// </summary>
    internal class DAO
    {
        // Conexão única reutilizada por todos os métodos da instância.
        // Cada método abre a conexão no início e fecha no "finally".
        public MySqlConnection conexao;

        // Código de erro do MySQL para chave duplicada (UNIQUE).
        private const int ErroChaveDuplicada = 1062;

        // Código de erro do MySQL para violação de chave estrangeira ao excluir.
        private const int ErroChaveEstrangeira = 1451;

        public DAO()
        {
            // ATENÇÃO: usuário root sem senha serve apenas para desenvolvimento.
            // Em produção, use um usuário próprio e guarde a string de conexão
            // em um arquivo de configuração (App.config), não no código.
            conexao = new MySqlConnection(
                "server=localhost;Database=escolaIdiomas;Uid=root;password=");
        }

        // ------------------------------------------------------------------
        // CONEXÃO
        // ------------------------------------------------------------------

        /// <summary>Abre a conexão caso ainda não esteja aberta.</summary>
        /// <returns>true se a conexão está aberta; false se houve falha.</returns>
        private MySqlConnection ObterConexaoAberta()
        {
            // A string de conexão pode usar a do construtor ou uma variável privada
            string stringConexao = "server=localhost;Database=escolaIdiomas;Uid=root;password=";
            var conexao = new MySqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }


        public bool Conectar()
        {
            try
            {
                if (conexao.State != ConnectionState.Open)
                    conexao.Open();

                return true;
            }
            catch (MySqlException erro)
            {
                MessageBox.Show("Erro ao conectar ao banco:\n" + erro.Message);
                return false;
            }
        }

        /// <summary>Fecha a conexão caso esteja aberta.</summary>
        public void Desconectar()
        {
            if (conexao != null && conexao.State != ConnectionState.Closed)
                conexao.Close();
        }

        // ------------------------------------------------------------------
        // USUÁRIOS
        // ------------------------------------------------------------------

        /// <summary>
        /// Valida CPF e senha de um usuário ativo.
        /// </summary>
        /// <returns>Dados do usuário autenticado, ou null se as credenciais
        /// forem inválidas ou ocorrer algum erro.</returns>
        public UsuarioAutenticado AutenticarUsuario(string cpf, string senha)
        {
            // Validação de entrada: evita consulta e erro do BCrypt com valores vazios.
            if (string.IsNullOrWhiteSpace(cpf) || string.IsNullOrEmpty(senha))
                return null;

            if (!Conectar())
                return null;

            try
            {
                const string sql = @"
                    SELECT codigo, nome, email, senhaHash, perfil
                    FROM usuario
                    WHERE cpf = @cpf
                      AND ativo = 1
                    LIMIT 1;";

                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@cpf", cpf.Trim());

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        // CPF não encontrado (ou usuário inativo).
                        if (!reader.Read())
                            return null;

                        string hash = reader["senhaHash"].ToString();

                        // Compara a senha digitada com o hash armazenado.
                        if (!BCrypt.Net.BCrypt.Verify(senha, hash))
                            return null;

                        return new UsuarioAutenticado
                        {
                            Codigo = Convert.ToInt32(reader["codigo"]),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Perfil = reader["perfil"].ToString()
                        };
                    }
                }
            }
            catch (MySqlException erro)
            {
                MessageBox.Show("Erro ao realizar autenticação:\n" + erro.Message);
                return null;
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Hash gravado no banco não está no formato BCrypt.
                MessageBox.Show("O hash da senha armazenado é inválido.");
                return null;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Cadastra um usuário (DOCENTE ou SECRETARIA) com senha em hash BCrypt.
        /// </summary>
        /// <returns>true se o usuário foi inserido.</returns>
        public bool CadastrarUsuario(
            string nome,
            string cpf,
            string email,
            string senha,
            string perfil)
        {
            // Validação de entrada.
            if (TemCampoVazio(nome, cpf, email, senha, perfil))
                return false;

            // Apenas perfis conhecidos são aceitos.
            perfil = perfil.Trim().ToUpperInvariant();
            if (perfil != "DOCENTE" && perfil != "SECRETARIA")
                return false;

            if (!Conectar())
                return false;

            try
            {
                const string sql = @"
                    INSERT INTO usuario
                        (nome, cpf, email, senhaHash, perfil, ativo, senhaDefinida)
                    VALUES
                        (@nome, @cpf, @email, @senhaHash, @perfil, TRUE, TRUE);";

                // Nunca se grava a senha pura: apenas o hash.
                string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", nome.Trim());
                    comando.Parameters.AddWithValue("@cpf", cpf.Trim());
                    comando.Parameters.AddWithValue("@email", email.Trim());
                    comando.Parameters.AddWithValue("@senhaHash", senhaHash);
                    comando.Parameters.AddWithValue("@perfil", perfil);

                    return comando.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException erro)
            {
                if (erro.Number == ErroChaveDuplicada)
                    MessageBox.Show("Já existe um usuário com esse CPF ou e-mail.");
                else
                    MessageBox.Show("Erro ao cadastrar usuário: " + erro.Message);

                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        // ------------------------------------------------------------------
        // ALUNOS
        // ------------------------------------------------------------------

        /// <summary>
        /// Cadastra o endereço e o aluno em uma única transação:
        /// ou as duas inserções são confirmadas, ou nenhuma.
        /// </summary>
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
            // Validação de entrada (complemento é opcional).
            if (TemCampoVazio(nome, email, cidade, estado, bairro, logradouro)
                || cpf <= 0
                || telefone <= 0
                || numero < 0
                || !DataNascimentoValida(dataNascimento))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios corretamente.");
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                // 1. Inserir o endereço e recuperar o código gerado.
                const string sqlEndereco = @"
                    INSERT INTO endereco
                        (cidade, estado, bairro, logradouro, numero, complemento)
                    VALUES
                        (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);";

                long enderecoCodigo;

                using (MySqlCommand comando = new MySqlCommand(
                    sqlEndereco, conexao, transacao))
                {
                    comando.Parameters.AddWithValue("@cidade", cidade.Trim());
                    comando.Parameters.AddWithValue("@estado", estado.Trim());
                    comando.Parameters.AddWithValue("@bairro", bairro.Trim());
                    comando.Parameters.AddWithValue("@logradouro", logradouro.Trim());
                    comando.Parameters.AddWithValue("@numero", numero);
                    comando.Parameters.AddWithValue("@complemento", TextoOuNulo(complemento));

                    comando.ExecuteNonQuery();

                    // Código gerado pelo AUTO_INCREMENT (evita uma segunda consulta).
                    enderecoCodigo = comando.LastInsertedId;
                }

                // 2. Inserir o aluno vinculado ao endereço recém-criado.
                const string sqlAluno = @"
                    INSERT INTO aluno
                        (nome, cpf, email, telefone, dataNascimento, enderecoCodigo)
                    VALUES
                        (@nome, @cpf, @email, @telefone, @dataNascimento, @enderecoCodigo);";

                using (MySqlCommand comando = new MySqlCommand(
                    sqlAluno, conexao, transacao))
                {
                    comando.Parameters.AddWithValue("@nome", nome.Trim());
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    comando.Parameters.AddWithValue("@email", email.Trim());
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                    comando.Parameters.AddWithValue("@enderecoCodigo", enderecoCodigo);

                    comando.ExecuteNonQuery();
                }

                // 3. Confirma as duas operações juntas.
                transacao.Commit();
                return true;
            }
            catch (MySqlException erro) when (erro.Number == ErroChaveDuplicada)
            {
                Reverter(transacao);
                MessageBox.Show("Já existe um aluno com esse CPF ou e-mail.");
                return false;
            }
            catch (Exception erro)
            {
                Reverter(transacao);
                MessageBox.Show(
                    "Não foi possível cadastrar aluno e endereço:\n" + erro.Message);
                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }

        /// <summary>
        /// Lista alunos (com endereço) filtrando por nome ou CPF.
        /// Filtro vazio retorna todos os alunos.
        /// </summary>
        public DataTable ListarAlunos(string filtro = "")
        {
            DataTable tabela = new DataTable();

            // Evita NullReferenceException caso o filtro venha nulo.
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                // Somente os dígitos digitados são usados na busca por CPF.
                string digitos = new string(filtro.Where(char.IsDigit).ToArray());

                // CORREÇÃO: antes, filtro sem dígitos gerava "cpf LIKE '%%'",
                // que casa com TODOS os registros e anulava a busca por nome.
                // Agora a condição de CPF só entra quando há dígitos.
                string condicaoCpf = digitos.Length > 0
                    ? " OR a.cpf LIKE @cpf"
                    : string.Empty;

                string sql = @"
                    SELECT
                        a.codigo         AS Codigo,
                        a.nome           AS Nome,
                        a.cpf            AS CPF,
                        a.email          AS Email,
                        a.telefone       AS Telefone,
                        a.dataNascimento AS DataNascimento,
                        a.enderecoCodigo AS EnderecoCodigo,
                        e.cidade         AS Cidade,
                        e.estado         AS Estado,
                        e.bairro         AS Bairro,
                        e.logradouro     AS Logradouro,
                        e.numero         AS Numero,
                        e.complemento    AS Complemento
                    FROM aluno a
                    LEFT JOIN endereco e
                        ON e.codigo = a.enderecoCodigo
                    WHERE a.nome LIKE @filtro" + condicaoCpf + @"
                    ORDER BY a.nome;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    if (digitos.Length > 0)
                        cmd.Parameters.AddWithValue("@cpf", "%" + digitos + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Erro ao listar alunos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Atualiza os dados do aluno e do endereço em uma transação.
        /// Se o aluno ainda não tiver endereço, um novo é criado.
        /// </summary>
        public bool AtualizarAluno(
            int codigo,
            int? enderecoCodigo,
            string nome,
            string cpf,
            string email,
            string telefone,
            DateTime dataNascimento,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            string numero,
            string complemento)
        {
            // Validação de entrada (complemento é opcional).
            if (codigo <= 0
                || TemCampoVazio(nome, cpf, email, telefone,
                                 cidade, estado, bairro, logradouro, numero)
                || !DataNascimentoValida(dataNascimento))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios corretamente.");
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                long idEndereco;

                if (enderecoCodigo.HasValue)
                {
                    // Aluno já possui endereço: apenas atualiza.
                    idEndereco = enderecoCodigo.Value;

                    const string sqlEndereco = @"
                        UPDATE endereco
                        SET cidade = @cidade,
                            estado = @estado,
                            bairro = @bairro,
                            logradouro = @logradouro,
                            numero = @numero,
                            complemento = @complemento
                        WHERE codigo = @codigo;";

                    using (MySqlCommand cmd = new MySqlCommand(
                        sqlEndereco, conexao, transacao))
                    {
                        PreencherParametrosEndereco(
                            cmd, cidade, estado, bairro, logradouro, numero, complemento);
                        cmd.Parameters.AddWithValue("@codigo", idEndereco);

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Aluno sem endereço: cria um novo.
                    const string sqlNovoEndereco = @"
                        INSERT INTO endereco
                            (cidade, estado, bairro, logradouro, numero, complemento)
                        VALUES
                            (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);";

                    using (MySqlCommand cmd = new MySqlCommand(
                        sqlNovoEndereco, conexao, transacao))
                    {
                        PreencherParametrosEndereco(
                            cmd, cidade, estado, bairro, logradouro, numero, complemento);

                        cmd.ExecuteNonQuery();
                        idEndereco = cmd.LastInsertedId;
                    }
                }

                const string sqlAluno = @"
                    UPDATE aluno
                    SET nome = @nome,
                        cpf = @cpf,
                        email = @email,
                        telefone = @telefone,
                        dataNascimento = @dataNascimento,
                        enderecoCodigo = @enderecoCodigo
                    WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(
                    sqlAluno, conexao, transacao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.Parameters.AddWithValue("@cpf", cpf.Trim());
                    cmd.Parameters.AddWithValue("@email", email.Trim());
                    cmd.Parameters.AddWithValue("@telefone", telefone.Trim());
                    cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                    cmd.Parameters.AddWithValue("@enderecoCodigo", idEndereco);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    // Nenhuma linha afetada = aluno inexistente.
                    // Isso cancela a transação (inclusive o endereço já alterado).
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("O aluno não foi encontrado.");
                }

                transacao.Commit();
                return true;
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveDuplicada)
            {
                Reverter(transacao);
                MessageBox.Show("Já existe outro aluno com esse CPF ou e-mail.");
                return false;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show(
                    "Erro ao atualizar aluno:\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }

        /// <summary>Exclui um aluno pelo código.</summary>
        /// <returns>true se exatamente um registro foi removido.</returns>
        public bool ExcluirAluno(int codigo)
        {
            if (codigo <= 0)
                return false;

            if (!Conectar())
                return false;

            try
            {
                const string sql = "DELETE FROM aluno WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira)
            {
                // Aluno possui registros vinculados (ex.: matrículas).
                MessageBox.Show(
                    "Não é possível excluir: o aluno possui registros vinculados.",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao excluir aluno: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        // ------------------------------------------------------------------
        // MÉTODOS AUXILIARES (privados)
        // ------------------------------------------------------------------

        /// <summary>Retorna true se algum dos textos for nulo, vazio ou só espaços.</summary>
        private static bool TemCampoVazio(params string[] campos)
        {
            return campos.Any(string.IsNullOrWhiteSpace);
        }

        /// <summary>Converte texto vazio em NULL do banco; caso contrário, devolve o texto sem espaços nas pontas.</summary>
        private static object TextoOuNulo(string texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                ? (object)DBNull.Value
                : texto.Trim();
        }

        /// <summary>Data de nascimento não pode ser futura nem anterior a 1900.</summary>
        private static bool DataNascimentoValida(DateTime data)
        {
            return data.Date <= DateTime.Today && data.Year >= 1900;
        }

        /// <summary>Preenche os parâmetros de endereço usados no INSERT e no UPDATE.</summary>
        private static void PreencherParametrosEndereco(
            MySqlCommand cmd,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            string numero,
            string complemento)
        {
            cmd.Parameters.AddWithValue("@cidade", cidade.Trim());
            cmd.Parameters.AddWithValue("@estado", estado.Trim());
            cmd.Parameters.AddWithValue("@bairro", bairro.Trim());
            cmd.Parameters.AddWithValue("@logradouro", logradouro.Trim());
            cmd.Parameters.AddWithValue("@numero", numero.Trim());
            cmd.Parameters.AddWithValue("@complemento", TextoOuNulo(complemento));
        }

        /// <summary>Desfaz a transação sem lançar exceção (a conexão pode já ter caído).</summary>
        private static void Reverter(MySqlTransaction transacao)
        {
            if (transacao == null)
                return;

            try
            {
                transacao.Rollback();
            }
            catch (Exception)
            {
                // Se a conexão foi perdida, o MySQL já descarta a transação.
            }
        }

        /// <summary>
        /// Cadastra o Usuário, seu Endereço e o perfil de Docente (Professor) em uma única transação.
        /// Caso ocorra falha em qualquer etapa, nada é salvo no banco de dados (Rollback automatizado).
        /// </summary>
        public bool CadastrarProfessorComEndereco(
            string nome,
            string cpf,
            string email,
            string senha,
            string titulacao,
            string especialidade,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            string numero,
            string complemento)
        {
            // Validação inicial de campos obrigatórios
            if (TemCampoVazio(nome, cpf, email, senha, cidade, estado, bairro, logradouro, numero))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios corretamente.");
                return false;
            }

            try
            {
                using (var conexao = ObterConexaoAberta())
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        // ------------------------------------------------------------------
                        // 1. INSERIR O USUÁRIO (Dados de Login e Acesso)
                        // ------------------------------------------------------------------
                        const string sqlUsuario = @"
                    INSERT INTO usuario 
                        (nome, cpf, email, senhaHash, perfil, ativo, senhaDefinida)
                    VALUES 
                        (@nome, @cpf, @email, @senhaHash, 'DOCENTE', TRUE, TRUE);";

                        string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
                        long usuarioCodigo;

                        using (var cmdUser = new MySqlCommand(sqlUsuario, conexao, transacao))
                        {
                            cmdUser.Parameters.AddWithValue("@nome", nome.Trim());
                            cmdUser.Parameters.AddWithValue("@cpf", cpf.Trim());
                            cmdUser.Parameters.AddWithValue("@email", email.Trim());
                            cmdUser.Parameters.AddWithValue("@senhaHash", senhaHash);

                            cmdUser.ExecuteNonQuery();
                            usuarioCodigo = cmdUser.LastInsertedId;
                        }

                        // ------------------------------------------------------------------
                        // 2. INSERIR O ENDEREÇO
                        // ------------------------------------------------------------------
                        const string sqlEndereco = @"
                    INSERT INTO endereco 
                        (cidade, estado, bairro, logradouro, numero, complemento)
                    VALUES 
                        (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);";

                        long enderecoCodigo;

                        using (var cmdEnd = new MySqlCommand(sqlEndereco, conexao, transacao))
                        {
                            PreencherParametrosEndereco(
                                cmdEnd, cidade, estado, bairro, logradouro, numero, complemento);

                            cmdEnd.ExecuteNonQuery();
                            enderecoCodigo = cmdEnd.LastInsertedId;
                        }

                        // ------------------------------------------------------------------
                        // 3. INSERIR O PROFESSOR (Atrelando às FKs de Usuário e Endereço)
                        // ------------------------------------------------------------------
                        const string sqlProfessor = @"
                    INSERT INTO professor 
                        (usuarioCodigo, titulacao, especialidade, dataAdmissao, enderecoCodigo)
                    VALUES 
                        (@usuarioCodigo, @titulacao, @especialidade, @dataAdmissao, @enderecoCodigo);";

                        using (var cmdProf = new MySqlCommand(sqlProfessor, conexao, transacao))
                        {
                            cmdProf.Parameters.AddWithValue("@usuarioCodigo", usuarioCodigo);
                            cmdProf.Parameters.AddWithValue("@titulacao", TextoOuNulo(titulacao));
                            cmdProf.Parameters.AddWithValue("@especialidade", TextoOuNulo(especialidade));
                            cmdProf.Parameters.AddWithValue("@dataAdmissao", DateTime.Today);
                            cmdProf.Parameters.AddWithValue("@enderecoCodigo", enderecoCodigo);

                            cmdProf.ExecuteNonQuery();
                        }

                        // Se todas as 3 etapas executarem com sucesso, confirma as alterações no banco
                        transacao.Commit();
                        return true;
                    }
                    catch
                    {
                        // Se der erro em qualquer comando, desfaz tudo o que foi feito na transação
                        transacao.Rollback();
                        throw;
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveDuplicada)
            {
                MessageBox.Show(
                    "Já existe um usuário cadastrado com esse CPF ou e-mail.",
                    "Cadastro Duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Não foi possível salvar o cadastro do professor:\n" + ex.Message,
                    "Erro no Cadastro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Cadastra o usuário e retorna o ID gerado (AUTO_INCREMENT).
        /// Retorna -1 se houver falha ou duplicidade.
        /// </summary>
        public int CadastrarUsuarioEObterId(string nome, string cpf, string email, string senha, string perfil)
        {
            if (TemCampoVazio(nome, cpf, email, senha, perfil))
                return -1;

            perfil = perfil.Trim().ToUpperInvariant();
            if (perfil != "DOCENTE" && perfil != "SECRETARIA")
                return -1;

            if (!Conectar())
                return -1;

            try
            {
                const string sql = @"
            INSERT INTO usuario
                (nome, cpf, email, senhaHash, perfil, ativo, senhaDefinida)
            VALUES
                (@nome, @cpf, @email, @senhaHash, @perfil, TRUE, TRUE);";

                string senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

                using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", nome.Trim());
                    comando.Parameters.AddWithValue("@cpf", cpf.Trim());
                    comando.Parameters.AddWithValue("@email", email.Trim());
                    comando.Parameters.AddWithValue("@senhaHash", senhaHash);
                    comando.Parameters.AddWithValue("@perfil", perfil);

                    comando.ExecuteNonQuery();
                    return (int)comando.LastInsertedId; // Retorna o ID recém-criado
                }
            }
            catch (MySqlException erro)
            {
                if (erro.Number == ErroChaveDuplicada)
                    MessageBox.Show("Já existe um usuário com esse CPF ou e-mail.");
                else
                    MessageBox.Show("Erro ao cadastrar usuário: " + erro.Message);

                return -1;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Insere o Endereço e vincula o perfil de Professor ao usuarioCodigo recebido.
        /// Executado em uma única transação para garantir integridade.
        /// </summary>
        /// <returns>true se o endereço e o docente foram cadastrados com sucesso.</returns>
        public bool CadastrarDadosProfessorEEndereco(
            int usuarioCodigo,
            string titulacao,
            string especialidade,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            string numero,
            string complemento)
        {
            // Validação inicial
            if (usuarioCodigo <= 0 || TemCampoVazio(cidade, estado, bairro, logradouro, numero))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios do endereço.");
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                // 1. Gravar o Endereço no banco de dados
                const string sqlEndereco = @"
            INSERT INTO endereco 
                (cidade, estado, bairro, logradouro, numero, complemento)
            VALUES 
                (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);";

                long enderecoCodigo;

                using (MySqlCommand cmdEnd = new MySqlCommand(sqlEndereco, conexao, transacao))
                {
                    PreencherParametrosEndereco(cmdEnd, cidade, estado, bairro, logradouro, numero, complemento);
                    cmdEnd.ExecuteNonQuery();
                    enderecoCodigo = cmdEnd.LastInsertedId; // Resgata o ID do endereço criado
                }

                // 2. Gravar o Professor atrelando ao usuarioCodigo (Tela 1) e ao enderecoCodigo (gerado acima)
                const string sqlProf = @"
            INSERT INTO professor 
                (usuarioCodigo, titulacao, especialidade, dataAdmissao, enderecoCodigo)
            VALUES 
                (@usuarioCodigo, @titulacao, @especialidade, @dataAdmissao, @enderecoCodigo);";

                using (MySqlCommand cmdProf = new MySqlCommand(sqlProf, conexao, transacao))
                {
                    cmdProf.Parameters.AddWithValue("@usuarioCodigo", usuarioCodigo);
                    cmdProf.Parameters.AddWithValue("@titulacao", TextoOuNulo(titulacao));
                    cmdProf.Parameters.AddWithValue("@especialidade", TextoOuNulo(especialidade));
                    cmdProf.Parameters.AddWithValue("@dataAdmissao", DateTime.Today);
                    cmdProf.Parameters.AddWithValue("@enderecoCodigo", enderecoCodigo);

                    cmdProf.ExecuteNonQuery();
                }

                // Confirma as duas gravações juntas no banco
                transacao.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show("Erro ao salvar dados do docente e endereço:\n" + ex.Message,
                                "Erro de Transação",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }

        /// <summary>
        /// Traz a lista completa de professores unificando as 3 tabelas (usuario, professor e endereco).
        /// </summary>
        public DataTable ListarProfessores(string filtro = "")
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string digitos = new string(filtro.Where(char.IsDigit).ToArray());

                string condicaoCpf = digitos.Length > 0
                    ? " OR u.cpf LIKE @cpf"
                    : string.Empty;

                string sql = @"
            SELECT 
                u.codigo          AS Codigo,
                u.nome            AS Nome,
                u.cpf             AS CPF,
                u.email           AS Email,
                p.titulacao       AS Titulacao,
                p.especialidade   AS Especialidade,
                p.dataAdmissao    AS DataAdmissao,
                p.enderecoCodigo  AS EnderecoCodigo,
                e.cidade          AS Cidade,
                e.estado          AS Estado,
                e.bairro          AS Bairro,
                e.logradouro      AS Logradouro,
                e.numero          AS Numero,
                e.complemento     AS Complemento
            FROM usuario u
            INNER JOIN professor p ON u.codigo = p.usuarioCodigo
            LEFT JOIN endereco e ON p.enderecoCodigo = e.codigo
            WHERE u.ativo = 1 
              AND (u.nome LIKE @filtro" + condicaoCpf + @")
            ORDER BY u.nome;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    if (digitos.Length > 0)
                        cmd.Parameters.AddWithValue("@cpf", "%" + digitos + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar professores:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Atualiza as tabelas usuario, professor e endereco em uma transação.
        /// </summary>
        public bool AtualizarProfessorComEndereco(
            int usuarioCodigo,
            int? enderecoCodigo,
            string nome,
            string cpf,
            string email,
            string titulacao,
            string especialidade,
            string cidade,
            string estado,
            string bairro,
            string logradouro,
            string numero,
            string complemento)
        {
            if (usuarioCodigo <= 0 || TemCampoVazio(nome, cpf, email, cidade, estado, bairro, logradouro, numero))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios corretamente.");
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                // 1. Atualiza dados de acesso na tabela usuario
                const string sqlUsuario = @"
            UPDATE usuario 
            SET nome = @nome, cpf = @cpf, email = @email 
            WHERE codigo = @codigo;";

                using (MySqlCommand cmdUser = new MySqlCommand(sqlUsuario, conexao, transacao))
                {
                    cmdUser.Parameters.AddWithValue("@nome", nome.Trim());
                    cmdUser.Parameters.AddWithValue("@cpf", cpf.Trim());
                    cmdUser.Parameters.AddWithValue("@email", email.Trim());
                    cmdUser.Parameters.AddWithValue("@codigo", usuarioCodigo);
                    cmdUser.ExecuteNonQuery();
                }

                // 2. Atualiza ou cria Endereço
                long idEndereco;

                if (enderecoCodigo.HasValue)
                {
                    idEndereco = enderecoCodigo.Value;
                    const string sqlEndereco = @"
                UPDATE endereco 
                SET cidade = @cidade, estado = @estado, bairro = @bairro, 
                    logradouro = @logradouro, numero = @numero, complemento = @complemento 
                WHERE codigo = @codigo;";

                    using (MySqlCommand cmdEnd = new MySqlCommand(sqlEndereco, conexao, transacao))
                    {
                        PreencherParametrosEndereco(cmdEnd, cidade, estado, bairro, logradouro, numero, complemento);
                        cmdEnd.Parameters.AddWithValue("@codigo", idEndereco);
                        cmdEnd.ExecuteNonQuery();
                    }
                }
                else
                {
                    const string sqlNovoEndereco = @"
                INSERT INTO endereco (cidade, estado, bairro, logradouro, numero, complemento) 
                VALUES (@cidade, @estado, @bairro, @logradouro, @numero, @complemento);";

                    using (MySqlCommand cmdEnd = new MySqlCommand(sqlNovoEndereco, conexao, transacao))
                    {
                        PreencherParametrosEndereco(cmdEnd, cidade, estado, bairro, logradouro, numero, complemento);
                        cmdEnd.ExecuteNonQuery();
                        idEndereco = cmdEnd.LastInsertedId;
                    }
                }

                // 3. Atualiza dados específicos na tabela professor
                const string sqlProf = @"
            UPDATE professor 
            SET titulacao = @titulacao, especialidade = @especialidade, enderecoCodigo = @enderecoCodigo 
            WHERE usuarioCodigo = @usuarioCodigo;";

                using (MySqlCommand cmdProf = new MySqlCommand(sqlProf, conexao, transacao))
                {
                    cmdProf.Parameters.AddWithValue("@titulacao", TextoOuNulo(titulacao));
                    cmdProf.Parameters.AddWithValue("@especialidade", TextoOuNulo(especialidade));
                    cmdProf.Parameters.AddWithValue("@enderecoCodigo", idEndereco);
                    cmdProf.Parameters.AddWithValue("@usuarioCodigo", usuarioCodigo);
                    cmdProf.ExecuteNonQuery();
                }

                transacao.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show("Erro ao atualizar professor:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }

        /// <summary>
        /// Exclui um professor, seu utilizador e o endereço associado.
        /// Trata a restrição de chave estrangeira (FK 1451) caso o docente lecione em alguma turma.
        /// </summary>
        public bool ExcluirProfessor(int usuarioCodigo, int? enderecoCodigo)
        {
            if (usuarioCodigo <= 0)
            {
                MessageBox.Show("Selecione um professor válido para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                // 1. Remove da tabela usuario (com ON DELETE CASCADE, limpa a tabela professor automaticamente)
                const string sqlUsuario = "DELETE FROM usuario WHERE codigo = @codigo;";

                using (MySqlCommand cmdUser = new MySqlCommand(sqlUsuario, conexao, transacao))
                {
                    cmdUser.Parameters.AddWithValue("@codigo", usuarioCodigo);
                    cmdUser.ExecuteNonQuery();
                }

                // 2. Remove o registro da tabela endereco, caso exista
                if (enderecoCodigo.HasValue && enderecoCodigo.Value > 0)
                {
                    const string sqlEndereco = "DELETE FROM endereco WHERE codigo = @codigo;";

                    using (MySqlCommand cmdEnd = new MySqlCommand(sqlEndereco, conexao, transacao))
                    {
                        cmdEnd.Parameters.AddWithValue("@codigo", enderecoCodigo.Value);
                        cmdEnd.ExecuteNonQuery();
                    }
                }

                transacao.Commit();
                return true;
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira) // Código MySQL 1451
            {
                Reverter(transacao);
                MessageBox.Show(
                    "Não é possível excluir este professor porque ele está vinculado a turmas (tabela 'lecionar') ou cursos.\n\n" +
                    "Desvincule o professor das turmas antes de tentar excluí-lo.",
                    "Exclusão Impedida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show(
                    "Erro ao tentar excluir o professor:\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();

                Desconectar();
            }
        }

        /// <summary>
        /// Consulta todos os cursos realizando INNER JOIN com a tabela idioma.
        /// </summary>
        public DataTable ListarCursos(string filtro = "")
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                c.codigo       AS Codigo,
                c.nome         AS Nome,
                c.idiomaCodigo AS IdiomaCodigo,
                i.nome         AS Idioma,
                c.cargaHoraria AS CargaHoraria,
                c.valor        AS Valor
            FROM curso c
            INNER JOIN idioma i ON c.idiomaCodigo = i.codigo
            WHERE c.nome LIKE @filtro OR i.nome LIKE @filtro
            ORDER BY c.nome;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar cursos:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Preenche a ComboBox de idiomas na tela CursosCRUD.
        /// </summary>
        public DataTable ListarIdiomas()
        {
            DataTable tabela = new DataTable();

            if (!Conectar())
                return tabela;

            try
            {
                const string sql = "SELECT codigo, nome FROM idioma ORDER BY nome;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(tabela);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar idiomas:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Atualiza as informações do curso no banco de dados.
        /// </summary>
        public bool AtualizarCurso(int codigo, string nome, int idiomaCodigo, int cargaHoraria, decimal valor)
        {
            if (codigo <= 0 || string.IsNullOrWhiteSpace(nome) || idiomaCodigo <= 0 || cargaHoraria <= 0)
            {
                MessageBox.Show("Preencha todos os campos corretamente.");
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = @"
            UPDATE curso 
            SET nome = @nome,
                idiomaCodigo = @idiomaCodigo,
                cargaHoraria = @cargaHoraria,
                valor = @valor
            WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.Parameters.AddWithValue("@idiomaCodigo", idiomaCodigo);
                    cmd.Parameters.AddWithValue("@cargaHoraria", cargaHoraria);
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar curso:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Exclui o curso tratando a FK de turmas associadas.
        /// </summary>
        public bool ExcluirCurso(int codigo)
        {
            if (codigo <= 0)
                return false;

            if (!Conectar())
                return false;

            try
            {
                const string sql = "DELETE FROM curso WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira) // Código 1451
            {
                MessageBox.Show("Não é possível excluir este curso pois existem turmas ou avaliações vinculadas a ele.",
                                "Exclusão Impedida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir curso:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Consulta todos os idiomas cadastrados com opção de filtro.
        /// </summary>
        public DataTable ListarIdiomas(string filtro = "")
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                codigo AS Codigo,
                nome   AS Nome
            FROM idioma
            WHERE nome LIKE @filtro
            ORDER BY nome;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar idiomas:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Cadastra um novo idioma no MySQL.
        /// </summary>
        public bool CadastrarIdioma(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome do idioma.");
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = "INSERT INTO idioma (nome) VALUES (@nome);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar idioma:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

   

        /// <summary>
        /// Cadastra um novo idioma no MySQL.
        /// </summary>
        /// <param name="nome">Nome do idioma (ex: Inglês, Espanhol, Francês).</param>
        /// <returns>true se o cadastro for bem-sucedido.</returns>
        public bool CadastroIdiomas(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Informe o nome do idioma.",
                                "Campo Obrigatório",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = "INSERT INTO idioma (nome) VALUES (@nome);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveDuplicada) // Tratamento de nome duplicado (se houver UNIQUE)
            {
                MessageBox.Show("Este idioma já está cadastrado no sistema.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar idioma:\n" + ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Atualiza o nome de um idioma existente.
        /// </summary>
        public bool AtualizarIdioma(int codigo, string nome)
        {
            if (codigo <= 0 || string.IsNullOrWhiteSpace(nome))
            {
                MessageBox.Show("Selecione um idioma e informe um nome válido.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = "UPDATE idioma SET nome = @nome WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.Parameters.AddWithValue("@codigo", codigo);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveDuplicada)
            {
                MessageBox.Show("Já existe outro idioma cadastrado com esse nome.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar idioma:\n" + ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Exclui um idioma tratando a restrição de FK (caso esteja em uso na tabela curso ou ter).
        /// </summary>
        public bool ExcluirIdioma(int codigo)
        {
            if (codigo <= 0)
            {
                MessageBox.Show("Selecione um idioma válido para excluir.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = "DELETE FROM idioma WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira) // Código MySQL 1451
            {
                MessageBox.Show(
                    "Não é possível excluir este idioma porque existem cursos ou professores associados a ele.\n\n" +
                    "Remova os vínculos antes de tentar excluí-lo.",
                    "Exclusão Impedida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir idioma:\n" + ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        public bool CadastrarCurso(string nome, int idiomaCodigo, int cargaHoraria, decimal valor)
        {
            if (string.IsNullOrWhiteSpace(nome) || idiomaCodigo <= 0 || cargaHoraria <= 0)
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.");
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = @"
            INSERT INTO curso (nome, idiomaCodigo, cargaHoraria, valor)
            VALUES (@nome, @idiomaCodigo, @cargaHoraria, @valor);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome.Trim());
                    cmd.Parameters.AddWithValue("@idiomaCodigo", idiomaCodigo);
                    cmd.Parameters.AddWithValue("@cargaHoraria", cargaHoraria);
                    cmd.Parameters.AddWithValue("@valor", valor);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar curso:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Consulta todas as turmas exibindo o Curso e o Professor responsável (via JOINs).
        /// </summary>
        public DataTable ListarTurmas(string filtro = "")
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                t.codigo        AS Codigo,
                t.cursoCodigo   AS CursoCodigo,
                c.nome          AS Curso,
                t.horarioInicio AS HorarioInicio,
                t.horarioFim    AS HorarioFim,
                p.usuarioCodigo AS ProfessorCodigo,
                u.nome          AS Professor
            FROM turma t
            INNER JOIN curso c ON t.cursoCodigo = c.codigo
            LEFT JOIN lecionar l ON t.codigo = l.turmaCodigo
            LEFT JOIN professor p ON l.professorCodigo = p.usuarioCodigo
            LEFT JOIN usuario u ON p.usuarioCodigo = u.codigo
            WHERE c.nome LIKE @filtro OR u.nome LIKE @filtro
            ORDER BY t.codigo DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar turmas:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Cadastra a turma e insere o professor vinculado na tabela lecionar.
        /// </summary>
        public bool CadastrarTurma(int cursoCodigo, TimeSpan horarioInicio, TimeSpan horarioFim, int? professorCodigo)
        {
            if (cursoCodigo <= 0)
            {
                MessageBox.Show("Selecione um curso válido.");
                return false;
            }

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                const string sqlTurma = @"
            INSERT INTO turma (cursoCodigo, horarioInicio, horarioFim) 
            VALUES (@cursoCodigo, @horarioInicio, @horarioFim);";

                long idTurma;
                using (MySqlCommand cmd = new MySqlCommand(sqlTurma, conexao, transacao))
                {
                    cmd.Parameters.AddWithValue("@cursoCodigo", cursoCodigo);
                    cmd.Parameters.AddWithValue("@horarioInicio", horarioInicio);
                    cmd.Parameters.AddWithValue("@horarioFim", horarioFim);
                    cmd.ExecuteNonQuery();
                    idTurma = cmd.LastInsertedId;
                }

                if (professorCodigo.HasValue && professorCodigo.Value > 0)
                {
                    const string sqlLecionar = @"
                INSERT INTO lecionar (turmaCodigo, professorCodigo) 
                VALUES (@turmaCodigo, @professorCodigo);";

                    using (MySqlCommand cmdLec = new MySqlCommand(sqlLecionar, conexao, transacao))
                    {
                        cmdLec.Parameters.AddWithValue("@turmaCodigo", idTurma);
                        cmdLec.Parameters.AddWithValue("@professorCodigo", professorCodigo.Value);
                        cmdLec.ExecuteNonQuery();
                    }
                }

                transacao.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show("Erro ao cadastrar turma:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                Desconectar();
            }
        }

        /// <summary>
        /// Atualiza os horários, o curso e renova o vínculo com o professor na tabela lecionar.
        /// </summary>
        public bool AtualizarTurma(int turmaCodigo, int cursoCodigo, TimeSpan horarioInicio, TimeSpan horarioFim, int? professorCodigo)
        {
            if (turmaCodigo <= 0 || cursoCodigo <= 0)
                return false;

            if (!Conectar())
                return false;

            MySqlTransaction transacao = null;

            try
            {
                transacao = conexao.BeginTransaction();

                const string sqlTurma = @"
            UPDATE turma 
            SET cursoCodigo = @cursoCodigo, 
                horarioInicio = @horarioInicio, 
                horarioFim = @horarioFim 
            WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sqlTurma, conexao, transacao))
                {
                    cmd.Parameters.AddWithValue("@cursoCodigo", cursoCodigo);
                    cmd.Parameters.AddWithValue("@horarioInicio", horarioInicio);
                    cmd.Parameters.AddWithValue("@horarioFim", horarioFim);
                    cmd.Parameters.AddWithValue("@codigo", turmaCodigo);
                    cmd.ExecuteNonQuery();
                }

                // Limpa a atribuição anterior e grava a nova na tabela lecionar
                const string sqlLimpar = "DELETE FROM lecionar WHERE turmaCodigo = @turmaCodigo;";
                using (MySqlCommand cmdDel = new MySqlCommand(sqlLimpar, conexao, transacao))
                {
                    cmdDel.Parameters.AddWithValue("@turmaCodigo", turmaCodigo);
                    cmdDel.ExecuteNonQuery();
                }

                if (professorCodigo.HasValue && professorCodigo.Value > 0)
                {
                    const string sqlIns = @"
                INSERT INTO lecionar (turmaCodigo, professorCodigo) 
                VALUES (@turmaCodigo, @professorCodigo);";

                    using (MySqlCommand cmdIns = new MySqlCommand(sqlIns, conexao, transacao))
                    {
                        cmdIns.Parameters.AddWithValue("@turmaCodigo", turmaCodigo);
                        cmdIns.Parameters.AddWithValue("@professorCodigo", professorCodigo.Value);
                        cmdIns.ExecuteNonQuery();
                    }
                }

                transacao.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Reverter(transacao);
                MessageBox.Show("Erro ao atualizar turma:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                Desconectar();
            }
        }

        /// <summary>
        /// Exclui a turma tratando a restrição de FK de matrículas ativas.
        /// </summary>
        public bool ExcluirTurma(int turmaCodigo)
        {
            if (turmaCodigo <= 0)
                return false;

            if (!Conectar())
                return false;

            try
            {
                const string sql = "DELETE FROM turma WHERE codigo = @codigo;";
                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", turmaCodigo);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira)
            {
                MessageBox.Show("Não é possível excluir esta turma pois existem alunos matriculados ou avaliações vinculadas.",
                                "Exclusão Impedida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir turma:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        /// <summary>
        /// Lista as matrículas ativas realizando os JOINs necessários para exibir nomes de alunos, cursos e horários.
        /// </summary>
        public DataTable ListarMatriculas(string filtro = "")
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                m.codigo        AS Codigo,
                m.alunoCodigo   AS AlunoCodigo,
                a.nome          AS Aluno,
                m.turmaCodigo   AS TurmaCodigo,
                c.nome          AS Curso,
                t.horarioInicio AS HorarioInicio,
                t.horarioFim    AS HorarioFim
            FROM matricular m
            INNER JOIN aluno a ON m.alunoCodigo = a.codigo
            INNER JOIN turma t ON m.turmaCodigo = t.codigo
            INNER JOIN curso c ON t.cursoCodigo = c.codigo
            WHERE a.nome LIKE @filtro OR c.nome LIKE @filtro
            ORDER BY m.codigo DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao consultar matrículas:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Insere o vínculo entre aluno e turma na tabela matricular.
        /// </summary>
        public bool RealizarMatricula(int alunoCodigo, int turmaCodigo)
        {
            if (alunoCodigo <= 0 || turmaCodigo <= 0)
            {
                MessageBox.Show("Selecione um aluno e uma turma válidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = @"
            INSERT INTO matricular (alunoCodigo, turmaCodigo) 
            VALUES (@alunoCodigo, @turmaCodigo);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@alunoCodigo", alunoCodigo);
                    cmd.Parameters.AddWithValue("@turmaCodigo", turmaCodigo);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveDuplicada) // Erro 1062 - UNIQUE (turmaCodigo, alunoCodigo)
            {
                MessageBox.Show("Este aluno já está matriculado nesta turma.", "Matrícula Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao efetuar matrícula:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

 
        /// <summary>
        /// Cancela/Remove a matrícula de um aluno tratando restrição de FK (avaliações vinculadas).
        /// </summary>
        public bool CancelarMatricula(int matriculaCodigo)
        {
            if (matriculaCodigo <= 0)
            {
                MessageBox.Show("Selecione uma matrícula válida para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = "DELETE FROM matricular WHERE codigo = @codigo;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigo", matriculaCodigo);
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (MySqlException ex) when (ex.Number == ErroChaveEstrangeira) // Código MySQL 1451
            {
                MessageBox.Show(
                    "Não é possível cancelar esta matrícula pois o aluno já possui notas ou avaliações registradas nesta turma.\n\n" +
                    "Remova os lançamentos de avaliações antes de cancelar a matrícula.",
                    "Cancelamento Impedido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cancelar matrícula:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }
        public bool CadastrarAvaliacao(int alunoCodigo, int cursoCodigo, int turmaCodigo, decimal nota, string observacao, DateTime dataAvaliacao)
        {
            if (alunoCodigo <= 0 || cursoCodigo <= 0 || turmaCodigo <= 0 || nota < 0 || nota > 10)
            {
                MessageBox.Show("Preencha todos os campos corretamente e informe uma nota entre 0 e 10.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Conectar())
                return false;

            try
            {
                const string sql = @"
            INSERT INTO avaliacao (nota, dataAvaliacao, observacao, cursoCodigo, turmaCodigo, alunoCodigo)
            VALUES (@nota, @dataAvaliacao, @observacao, @cursoCodigo, @turmaCodigo, @alunoCodigo);";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.Parameters.AddWithValue("@dataAvaliacao", dataAvaliacao);
                    cmd.Parameters.AddWithValue("@observacao", string.IsNullOrWhiteSpace(observacao) ? (object)DBNull.Value : observacao.Trim());
                    cmd.Parameters.AddWithValue("@cursoCodigo", cursoCodigo);
                    cmd.Parameters.AddWithValue("@turmaCodigo", turmaCodigo);
                    cmd.Parameters.AddWithValue("@alunoCodigo", alunoCodigo);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao lançar avaliação:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Desconectar();
            }
        }

        public DataTable ObterHistoricoAluno(int alunoCodigo)
        {
            DataTable tabela = new DataTable();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                av.codigo        AS Codigo,
                c.nome           AS Curso,
                av.turmaCodigo   AS Turma,
                av.nota          AS Nota,
                av.dataAvaliacao AS Data,
                av.observacao    AS Observacao
            FROM avaliacao av
            INNER JOIN curso c ON av.cursoCodigo = c.codigo
            WHERE av.alunoCodigo = @alunoCodigo
            ORDER BY av.dataAvaliacao DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@alunoCodigo", alunoCodigo);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }

        /// <summary>
        /// Consulta o histórico de avaliações de um aluno filtrando por Nome ou CPF.
        /// </summary>
        public DataTable ObterHistoricoAlunoPorFiltro(string filtro)
        {
            DataTable tabela = new DataTable();
            filtro = (filtro ?? string.Empty).Trim();

            if (!Conectar())
                return tabela;

            try
            {
                string sql = @"
            SELECT 
                a.nome           AS Aluno,
                a.cpf            AS CPF,
                c.nome           AS Curso,
                av.turmaCodigo   AS Turma,
                av.nota          AS Nota,
                av.dataAvaliacao AS Data,
                av.observacao    AS Observacao
            FROM avaliacao av
            INNER JOIN aluno a ON av.alunoCodigo = a.codigo
            INNER JOIN curso c ON av.cursoCodigo = c.codigo
            WHERE a.nome LIKE @filtro OR a.cpf LIKE @filtro
            ORDER BY av.dataAvaliacao DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(tabela);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico do aluno:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Desconectar();
            }

            return tabela;
        }


    } // Fim da classe
} // Fim do projeto