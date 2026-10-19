namespace EscolaIdiomx
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoginAdm = new System.Windows.Forms.Button();
            this.btnLoginDocent = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSecretaria = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(258, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sejam bem-vidos a escola de idiomas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(319, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(336, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Escolha um dos perfis para fazer login";
            // 
            // btnLoginAdm
            // 
            this.btnLoginAdm.Location = new System.Drawing.Point(26, 101);
            this.btnLoginAdm.Name = "btnLoginAdm";
            this.btnLoginAdm.Size = new System.Drawing.Size(240, 59);
            this.btnLoginAdm.TabIndex = 3;
            this.btnLoginAdm.Text = "Administrador";
            this.btnLoginAdm.UseVisualStyleBackColor = true;
            this.btnLoginAdm.Click += new System.EventHandler(this.btnLoginAdm_Click);
            // 
            // btnLoginDocent
            // 
            this.btnLoginDocent.Location = new System.Drawing.Point(26, 24);
            this.btnLoginDocent.Name = "btnLoginDocent";
            this.btnLoginDocent.Size = new System.Drawing.Size(240, 59);
            this.btnLoginDocent.TabIndex = 4;
            this.btnLoginDocent.Text = "Docentes";
            this.btnLoginDocent.UseVisualStyleBackColor = true;
            this.btnLoginDocent.Click += new System.EventHandler(this.btnLoginDocent_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 57);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSecretaria);
            this.panel2.Controls.Add(this.btnLoginDocent);
            this.panel2.Controls.Add(this.btnLoginAdm);
            this.panel2.Location = new System.Drawing.Point(342, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 264);
            this.panel2.TabIndex = 6;
            // 
            // btnSecretaria
            // 
            this.btnSecretaria.Location = new System.Drawing.Point(26, 177);
            this.btnSecretaria.Name = "btnSecretaria";
            this.btnSecretaria.Size = new System.Drawing.Size(240, 59);
            this.btnSecretaria.TabIndex = 5;
            this.btnSecretaria.Text = "Secretaria";
            this.btnSecretaria.UseVisualStyleBackColor = true;
            this.btnSecretaria.Click += new System.EventHandler(this.btnSecretaria_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 432);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.MaximumSize = new System.Drawing.Size(1031, 471);
            this.MinimumSize = new System.Drawing.Size(1031, 471);
            this.Name = "Form1";
            this.Text = "Escola de Idiomas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoginAdm;
        private System.Windows.Forms.Button btnLoginDocent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSecretaria;
    }
}

