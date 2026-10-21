namespace EscolaIdiomx
{
    partial class MenuDocente
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHistoricoAluno = new System.Windows.Forms.Button();
            this.btnFrequencias = new System.Windows.Forms.Button();
            this.btnAvaliacoes = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(991, 61);
            this.panel1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(831, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(731, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuário:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sejam bem-vidos a escola de idiomas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHistoricoAluno);
            this.panel2.Controls.Add(this.btnFrequencias);
            this.panel2.Controls.Add(this.btnAvaliacoes);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(12, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 348);
            this.panel2.TabIndex = 6;
            // 
            // btnHistoricoAluno
            // 
            this.btnHistoricoAluno.Location = new System.Drawing.Point(123, 164);
            this.btnHistoricoAluno.Name = "btnHistoricoAluno";
            this.btnHistoricoAluno.Size = new System.Drawing.Size(152, 35);
            this.btnHistoricoAluno.TabIndex = 7;
            this.btnHistoricoAluno.Text = "Histórico do Aluno";
            this.btnHistoricoAluno.UseVisualStyleBackColor = true;
            this.btnHistoricoAluno.Click += new System.EventHandler(this.btnHistoricoAluno_Click);
            // 
            // btnFrequencias
            // 
            this.btnFrequencias.Location = new System.Drawing.Point(123, 76);
            this.btnFrequencias.Name = "btnFrequencias";
            this.btnFrequencias.Size = new System.Drawing.Size(152, 38);
            this.btnFrequencias.TabIndex = 4;
            this.btnFrequencias.Text = "Lançar Frequências";
            this.btnFrequencias.UseVisualStyleBackColor = true;
            this.btnFrequencias.Click += new System.EventHandler(this.btnFrequencias_Click);
            // 
            // btnAvaliacoes
            // 
            this.btnAvaliacoes.Location = new System.Drawing.Point(123, 120);
            this.btnAvaliacoes.Name = "btnAvaliacoes";
            this.btnAvaliacoes.Size = new System.Drawing.Size(152, 38);
            this.btnAvaliacoes.TabIndex = 3;
            this.btnAvaliacoes.Text = "Lançar Avaliações";
            this.btnAvaliacoes.UseVisualStyleBackColor = true;
            this.btnAvaliacoes.Click += new System.EventHandler(this.btnAvaliacoes_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "SAIR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(418, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 347);
            this.panel3.TabIndex = 7;
            // 
            // MenuDocente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 432);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.MaximumSize = new System.Drawing.Size(1031, 471);
            this.MinimumSize = new System.Drawing.Size(1031, 471);
            this.Name = "MenuDocente";
            this.Text = "MenuDocente";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHistoricoAluno;
        private System.Windows.Forms.Button btnFrequencias;
        private System.Windows.Forms.Button btnAvaliacoes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
    }
}