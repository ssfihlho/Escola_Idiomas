namespace EscolaIdiomx
{
    partial class HistoricoDoAluno
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
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.lblMedia = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(323, 46);
            this.label7.TabIndex = 100;
            this.label7.Text = "Histórico do Aluno";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1292, 68);
            this.panel1.TabIndex = 101;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(229, 27);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(299, 29);
            this.txtBuscar.TabIndex = 102;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(536, 27);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 31);
            this.button1.TabIndex = 103;
            this.button1.Text = "Buscar Aluno";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 23);
            this.label1.TabIndex = 104;
            this.label1.Text = "Nome/CPF ou Matrícula:";
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorico.Location = new System.Drawing.Point(25, 68);
            this.dgvHistorico.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.RowHeadersWidth = 51;
            this.dgvHistorico.Size = new System.Drawing.Size(1224, 257);
            this.dgvHistorico.TabIndex = 105;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvHistorico);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.txtBuscar);
            this.panel2.Location = new System.Drawing.Point(16, 90);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1291, 348);
            this.panel2.TabIndex = 106;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(41, 473);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(175, 44);
            this.btnVoltar.TabIndex = 107;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // lblMedia
            // 
            this.lblMedia.AutoSize = true;
            this.lblMedia.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedia.Location = new System.Drawing.Point(1250, 456);
            this.lblMedia.Name = "lblMedia";
            this.lblMedia.Size = new System.Drawing.Size(27, 31);
            this.lblMedia.TabIndex = 108;
            this.lblMedia.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1157, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 31);
            this.label2.TabIndex = 109;
            this.label2.Text = "Média:";
            // 
            // HistoricoDoAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 522);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMedia);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1369, 569);
            this.MinimumSize = new System.Drawing.Size(1369, 569);
            this.Name = "HistoricoDoAluno";
            this.Text = "HistoricoDoAluno";
            this.Load += new System.EventHandler(this.HistoricoDoAluno_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Label lblMedia;
        private System.Windows.Forms.Label label2;
    }
}