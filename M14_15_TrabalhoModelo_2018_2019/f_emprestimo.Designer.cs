namespace M14_15_TrabalhoModelo_2018_2019
{
    partial class f_emprestimo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLeitores = new System.Windows.Forms.ComboBox();
            this.cbLivros = new System.Windows.Forms.ComboBox();
            this.dtpDevolve = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Leitor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Livro";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Data Devolução";
            // 
            // cbLeitores
            // 
            this.cbLeitores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeitores.FormattingEnabled = true;
            this.cbLeitores.Location = new System.Drawing.Point(132, 56);
            this.cbLeitores.Name = "cbLeitores";
            this.cbLeitores.Size = new System.Drawing.Size(200, 21);
            this.cbLeitores.TabIndex = 4;
            // 
            // cbLivros
            // 
            this.cbLivros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLivros.FormattingEnabled = true;
            this.cbLivros.Location = new System.Drawing.Point(132, 100);
            this.cbLivros.Name = "cbLivros";
            this.cbLivros.Size = new System.Drawing.Size(200, 21);
            this.cbLivros.TabIndex = 4;
            // 
            // dtpDevolve
            // 
            this.dtpDevolve.Location = new System.Drawing.Point(132, 150);
            this.dtpDevolve.Name = "dtpDevolve";
            this.dtpDevolve.Size = new System.Drawing.Size(200, 20);
            this.dtpDevolve.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "Emprestar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // f_emprestimo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 349);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpDevolve);
            this.Controls.Add(this.cbLivros);
            this.Controls.Add(this.cbLeitores);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "f_emprestimo";
            this.Text = "Emprestar";
            this.Load += new System.EventHandler(this.f_emprestimo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLeitores;
        private System.Windows.Forms.ComboBox cbLivros;
        private System.Windows.Forms.DateTimePicker dtpDevolve;
        private System.Windows.Forms.Button button1;
    }
}