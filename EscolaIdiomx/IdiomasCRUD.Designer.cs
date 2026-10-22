namespace EscolaIdiomx
{
    partial class IdiomasCRUD
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
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.butAcessar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnListarTodos = new System.Windows.Forms.Button();
            this.dgvIdiomas = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomas)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.butAcessar);
            this.panel3.Controls.Add(this.btnVoltar);
            this.panel3.Location = new System.Drawing.Point(12, 20);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1291, 52);
            this.panel3.TabIndex = 131;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 41);
            this.label2.TabIndex = 97;
            this.label2.Text = "Gerenciar Idiomas";
            // 
            // butAcessar
            // 
            this.butAcessar.BackColor = System.Drawing.SystemColors.Control;
            this.butAcessar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAcessar.Location = new System.Drawing.Point(769, 9);
            this.butAcessar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butAcessar.Name = "butAcessar";
            this.butAcessar.Size = new System.Drawing.Size(275, 39);
            this.butAcessar.TabIndex = 116;
            this.butAcessar.Text = "NOVO IDIOMA";
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
            this.label13.Size = new System.Drawing.Size(116, 19);
            this.label13.TabIndex = 121;
            this.label13.Text = "Nome da Idioma:";
            // 
            // btnListarTodos
            // 
            this.btnListarTodos.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarTodos.Location = new System.Drawing.Point(105, 52);
            this.btnListarTodos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnListarTodos.Name = "btnListarTodos";
            this.btnListarTodos.Size = new System.Drawing.Size(411, 41);
            this.btnListarTodos.TabIndex = 120;
            this.btnListarTodos.Text = "LISTAR TODAS OS IDIOMAS";
            this.btnListarTodos.UseVisualStyleBackColor = true;
            // 
            // dgvIdiomas
            // 
            this.dgvIdiomas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIdiomas.Location = new System.Drawing.Point(25, 98);
            this.dgvIdiomas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvIdiomas.Name = "dgvIdiomas";
            this.dgvIdiomas.RowHeadersWidth = 51;
            this.dgvIdiomas.Size = new System.Drawing.Size(733, 281);
            this.dgvIdiomas.TabIndex = 119;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBuscar);
            this.panel1.Controls.Add(this.txtBuscar);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnListarTodos);
            this.panel1.Controls.Add(this.dgvIdiomas);
            this.panel1.Location = new System.Drawing.Point(27, 114);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 399);
            this.panel1.TabIndex = 130;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtNome);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnExcluir);
            this.panel2.Controls.Add(this.btnSalvar);
            this.panel2.Controls.Add(this.btnEditar);
            this.panel2.Location = new System.Drawing.Point(809, 114);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 399);
            this.panel2.TabIndex = 129;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(113, 121);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(383, 22);
            this.txtNome.TabIndex = 126;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 19);
            this.label3.TabIndex = 120;
            this.label3.Text = "Nome:";
            // 
            // IdiomasCRUD
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
            this.Name = "IdiomasCRUD";
            this.Text = "IdiomasCRUD";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butAcessar;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnListarTodos;
        private System.Windows.Forms.DataGridView dgvIdiomas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label3;
    }
}