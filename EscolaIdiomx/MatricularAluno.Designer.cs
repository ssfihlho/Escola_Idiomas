namespace EscolaIdiomx
{
    partial class MatricularAluno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.butAcessar = new System.Windows.Forms.Button();
            this.cmbAluno = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTurma = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancela = new System.Windows.Forms.Button();
            this.btnListarTodas = new System.Windows.Forms.Button();
            this.dgvMatriculas = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatriculas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(467, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 46);
            this.label1.TabIndex = 33;
            this.label1.Text = "Matricular Alunos";
            // 
            // txtBusca
            // 
            this.txtBusca.Location = new System.Drawing.Point(156, 21);
            this.txtBusca.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(373, 22);
            this.txtBusca.TabIndex = 79;
            this.txtBusca.TextChanged += new System.EventHandler(this.txtBusca_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 77;
            this.label2.Text = "Nome/CPF:";
            // 
            // btnConsulta
            // 
            this.btnConsulta.Location = new System.Drawing.Point(568, 21);
            this.btnConsulta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(105, 25);
            this.btnConsulta.TabIndex = 81;
            this.btnConsulta.Text = "Consultar";
            this.btnConsulta.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 283);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 23);
            this.label4.TabIndex = 85;
            this.label4.Text = "Aluno Selecionado:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnVoltar);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1304, 63);
            this.panel1.TabIndex = 87;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(1110, 9);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(176, 42);
            this.btnVoltar.TabIndex = 34;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // butAcessar
            // 
            this.butAcessar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAcessar.Location = new System.Drawing.Point(1029, 330);
            this.butAcessar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAcessar.Name = "butAcessar";
            this.butAcessar.Size = new System.Drawing.Size(269, 42);
            this.butAcessar.TabIndex = 35;
            this.butAcessar.Text = "Confirmar Matrícula";
            this.butAcessar.UseVisualStyleBackColor = true;
            this.butAcessar.Click += new System.EventHandler(this.butAcessar_Click);
            // 
            // cmbAluno
            // 
            this.cmbAluno.FormattingEnabled = true;
            this.cmbAluno.Location = new System.Drawing.Point(190, 283);
            this.cmbAluno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAluno.Name = "cmbAluno";
            this.cmbAluno.Size = new System.Drawing.Size(458, 24);
            this.cmbAluno.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(112, 335);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 19);
            this.label3.TabIndex = 83;
            this.label3.Text = "Turma:";
            // 
            // cmbTurma
            // 
            this.cmbTurma.FormattingEnabled = true;
            this.cmbTurma.Location = new System.Drawing.Point(190, 335);
            this.cmbTurma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTurma.Name = "cmbTurma";
            this.cmbTurma.Size = new System.Drawing.Size(373, 24);
            this.cmbTurma.TabIndex = 84;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvMatriculas);
            this.panel2.Controls.Add(this.btnListarTodas);
            this.panel2.Controls.Add(this.btnCancela);
            this.panel2.Controls.Add(this.butAcessar);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbTurma);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnConsulta);
            this.panel2.Controls.Add(this.cmbAluno);
            this.panel2.Controls.Add(this.txtBusca);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(21, 100);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1312, 394);
            this.panel2.TabIndex = 88;
            // 
            // btnCancela
            // 
            this.btnCancela.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancela.Location = new System.Drawing.Point(693, 330);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(280, 42);
            this.btnCancela.TabIndex = 86;
            this.btnCancela.Text = "Cancelar Matricula";
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            // 
            // btnListarTodas
            // 
            this.btnListarTodas.Location = new System.Drawing.Point(170, 65);
            this.btnListarTodas.Name = "btnListarTodas";
            this.btnListarTodas.Size = new System.Drawing.Size(327, 38);
            this.btnListarTodas.TabIndex = 87;
            this.btnListarTodas.Text = "Listar todas as Matriculas";
            this.btnListarTodas.UseVisualStyleBackColor = true;
            this.btnListarTodas.Click += new System.EventHandler(this.btnListarTodas_Click);
            // 
            // dgvMatriculas
            // 
            this.dgvMatriculas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatriculas.Location = new System.Drawing.Point(92, 109);
            this.dgvMatriculas.Name = "dgvMatriculas";
            this.dgvMatriculas.RowHeadersWidth = 51;
            this.dgvMatriculas.RowTemplate.Height = 24;
            this.dgvMatriculas.Size = new System.Drawing.Size(437, 150);
            this.dgvMatriculas.TabIndex = 88;
            // 
            // MatricularAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 522);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1369, 569);
            this.MinimumSize = new System.Drawing.Size(1369, 569);
            this.Name = "MatricularAluno";
            this.Text = "MatricularAluno";
            this.Load += new System.EventHandler(this.MatricularAluno_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatriculas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button butAcessar;
        private System.Windows.Forms.ComboBox cmbAluno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTurma;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Button btnListarTodas;
        private System.Windows.Forms.DataGridView dgvMatriculas;
    }
}