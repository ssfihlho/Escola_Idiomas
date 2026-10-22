namespace EscolaIdiomx
{
    partial class TurmasCRUD
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.butAcessar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnListarTodos = new System.Windows.Forms.Button();
            this.dgvTurmas = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbProfessor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCurso = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurmas)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.butAcessar);
            this.panel3.Controls.Add(this.btnVoltar);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1291, 52);
            this.panel3.TabIndex = 128;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 41);
            this.label2.TabIndex = 97;
            this.label2.Text = "Gerenciar Turmas";
            // 
            // butAcessar
            // 
            this.butAcessar.BackColor = System.Drawing.SystemColors.Control;
            this.butAcessar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAcessar.Location = new System.Drawing.Point(820, 9);
            this.butAcessar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAcessar.Name = "butAcessar";
            this.butAcessar.Size = new System.Drawing.Size(224, 39);
            this.butAcessar.TabIndex = 116;
            this.butAcessar.Text = "NOVO PROFESSOR";
            this.butAcessar.UseVisualStyleBackColor = false;
            this.butAcessar.Click += new System.EventHandler(this.butAcessar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(1052, 9);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(223, 36);
            this.btnVoltar.TabIndex = 110;
            this.btnVoltar.Text = "VOLTAR";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtBuscar);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnListarTodos);
            this.panel1.Controls.Add(this.dgvTurmas);
            this.panel1.Location = new System.Drawing.Point(27, 107);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 399);
            this.panel1.TabIndex = 127;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(529, 9);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(131, 36);
            this.btnBuscar.TabIndex = 123;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(147, 16);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(373, 22);
            this.txtBuscar.TabIndex = 122;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(21, 20);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 19);
            this.label13.TabIndex = 121;
            this.label13.Text = "Nome da Turma:";
            // 
            // btnListarTodos
            // 
            this.btnListarTodos.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarTodos.Location = new System.Drawing.Point(105, 52);
            this.btnListarTodos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnListarTodos.Name = "btnListarTodos";
            this.btnListarTodos.Size = new System.Drawing.Size(411, 41);
            this.btnListarTodos.TabIndex = 120;
            this.btnListarTodos.Text = "LISTAR TODAS AS TURMAS";
            this.btnListarTodos.UseVisualStyleBackColor = true;
            this.btnListarTodos.Click += new System.EventHandler(this.btnListarTodos_Click);
            // 
            // dgvTurmas
            // 
            this.dgvTurmas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurmas.Location = new System.Drawing.Point(25, 98);
            this.dgvTurmas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvTurmas.Name = "dgvTurmas";
            this.dgvTurmas.RowHeadersWidth = 51;
            this.dgvTurmas.Size = new System.Drawing.Size(733, 281);
            this.dgvTurmas.TabIndex = 119;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpFim);
            this.panel2.Controls.Add(this.dtpInicio);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cmbProfessor);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbCurso);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btnExcluir);
            this.panel2.Controls.Add(this.btnSalvar);
            this.panel2.Controls.Add(this.btnEditar);
            this.panel2.Location = new System.Drawing.Point(809, 107);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 399);
            this.panel2.TabIndex = 126;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 219);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 125;
            this.label1.Text = "Horário de término";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(35, 183);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 19);
            this.label10.TabIndex = 124;
            this.label10.Text = "Horário de inicio:";
            // 
            // cmbProfessor
            // 
            this.cmbProfessor.FormattingEnabled = true;
            this.cmbProfessor.Location = new System.Drawing.Point(123, 117);
            this.cmbProfessor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProfessor.Name = "cmbProfessor";
            this.cmbProfessor.Size = new System.Drawing.Size(373, 24);
            this.cmbProfessor.TabIndex = 123;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 122;
            this.label3.Text = "Curso:";
            // 
            // cmbCurso
            // 
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(123, 73);
            this.cmbCurso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(373, 24);
            this.cmbCurso.TabIndex = 121;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(35, 127);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 19);
            this.label6.TabIndex = 120;
            this.label6.Text = "Professor:";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(191, 342);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(148, 46);
            this.btnExcluir.TabIndex = 118;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Location = new System.Drawing.Point(347, 341);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(148, 46);
            this.btnSalvar.TabIndex = 117;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Location = new System.Drawing.Point(35, 341);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(148, 46);
            this.btnEditar.TabIndex = 116;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(159, 180);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(200, 22);
            this.dtpInicio.TabIndex = 126;
            // 
            // dtpFim
            // 
            this.dtpFim.Location = new System.Drawing.Point(170, 219);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.Size = new System.Drawing.Size(200, 22);
            this.dtpFim.TabIndex = 127;
            // 
            // TurmasCRUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 522);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1369, 569);
            this.MinimumSize = new System.Drawing.Size(1369, 569);
            this.Name = "TurmasCRUD";
            this.Text = "TurmasCRUD";
            this.Load += new System.EventHandler(this.TurmasCRUD_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurmas)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butAcessar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnListarTodos;
        private System.Windows.Forms.DataGridView dgvTurmas;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbProfessor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCurso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.DateTimePicker dtpInicio;
    }
}