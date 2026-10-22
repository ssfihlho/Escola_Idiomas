namespace EscolaIdiomx
{
    partial class CadastrarTurmas
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
            this.cmbProfessor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCurso = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butAcessar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFim = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProfessor
            // 
            this.cmbProfessor.FormattingEnabled = true;
            this.cmbProfessor.Location = new System.Drawing.Point(125, 111);
            this.cmbProfessor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbProfessor.Name = "cmbProfessor";
            this.cmbProfessor.Size = new System.Drawing.Size(373, 24);
            this.cmbProfessor.TabIndex = 90;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 89;
            this.label3.Text = "Curso:";
            // 
            // cmbCurso
            // 
            this.cmbCurso.FormattingEnabled = true;
            this.cmbCurso.Location = new System.Drawing.Point(125, 66);
            this.cmbCurso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCurso.Name = "cmbCurso";
            this.cmbCurso.Size = new System.Drawing.Size(373, 24);
            this.cmbCurso.TabIndex = 88;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 121);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 19);
            this.label6.TabIndex = 87;
            this.label6.Text = "Professor:";
            // 
            // butAcessar
            // 
            this.butAcessar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAcessar.Location = new System.Drawing.Point(141, 278);
            this.butAcessar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAcessar.Name = "butAcessar";
            this.butAcessar.Size = new System.Drawing.Size(269, 42);
            this.butAcessar.TabIndex = 86;
            this.butAcessar.Text = "Cadastrar Turma";
            this.butAcessar.UseVisualStyleBackColor = true;
            this.butAcessar.Click += new System.EventHandler(this.butAcessar_Click);
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(20, 475);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(176, 42);
            this.btnVoltar.TabIndex = 85;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(37, 177);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 19);
            this.label10.TabIndex = 91;
            this.label10.Text = "Horário de inicio:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 177);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 19);
            this.label1.TabIndex = 93;
            this.label1.Text = "Horário de término";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 46);
            this.label2.TabIndex = 97;
            this.label2.Text = "Cadastrar Turmas";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1308, 68);
            this.panel1.TabIndex = 98;
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
            this.panel2.Controls.Add(this.butAcessar);
            this.panel2.Location = new System.Drawing.Point(373, 90);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(545, 354);
            this.panel2.TabIndex = 99;
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(162, 177);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(73, 22);
            this.dtpInicio.TabIndex = 94;
            // 
            // dtpFim
            // 
            this.dtpFim.Location = new System.Drawing.Point(423, 175);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.Size = new System.Drawing.Size(75, 22);
            this.dtpFim.TabIndex = 95;
            // 
            // CadastrarTurmas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 522);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnVoltar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1369, 569);
            this.MinimumSize = new System.Drawing.Size(1369, 569);
            this.Name = "CadastrarTurmas";
            this.Text = "CadastrarTurmas";
            this.Load += new System.EventHandler(this.CadastrarTurmas_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProfessor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCurso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butAcessar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.DateTimePicker dtpInicio;
    }
}