namespace M14_15_TrabalhoModelo_2018_2019
{
    partial class f_historico
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
            this.tvHistorico = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvHistorico
            // 
            this.tvHistorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvHistorico.Location = new System.Drawing.Point(0, 0);
            this.tvHistorico.Name = "tvHistorico";
            this.tvHistorico.Size = new System.Drawing.Size(800, 450);
            this.tvHistorico.TabIndex = 0;
            // 
            // f_historico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tvHistorico);
            this.Name = "f_historico";
            this.Text = "f_historico";
            this.Load += new System.EventHandler(this.f_historico_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvHistorico;
    }
}