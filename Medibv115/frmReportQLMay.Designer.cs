namespace MediIT115
{
    partial class frmReportQLMay
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
            this.haison1 = new MediIT115.haison();
            this.btnInBC = new System.Windows.Forms.Button();
            this.cbLoaiBaoCao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // haison1
            // 
            this.haison1.Location = new System.Drawing.Point(12, 12);
            this.haison1.Name = "haison1";
            this.haison1.Size = new System.Drawing.Size(274, 68);
            this.haison1.TabIndex = 0;
            // 
            // btnInBC
            // 
            this.btnInBC.Location = new System.Drawing.Point(311, 12);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(75, 23);
            this.btnInBC.TabIndex = 1;
            this.btnInBC.Text = "In báo cáo";
            this.btnInBC.UseVisualStyleBackColor = true;
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // cbLoaiBaoCao
            // 
            this.cbLoaiBaoCao.FormattingEnabled = true;
            this.cbLoaiBaoCao.Items.AddRange(new object[] {
            "bc_baotri.rpt",
            "bc_baotri_01.rpt",
            "bc_baotri_02.rpt",
            "bc_baotri_03.rpt"});
            this.cbLoaiBaoCao.Location = new System.Drawing.Point(104, 87);
            this.cbLoaiBaoCao.Name = "cbLoaiBaoCao";
            this.cbLoaiBaoCao.Size = new System.Drawing.Size(181, 21);
            this.cbLoaiBaoCao.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Loại báo cáo";
            // 
            // frmReportQLMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 131);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLoaiBaoCao);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.haison1);
            this.Name = "frmReportQLMay";
            this.Text = "frmReportQLMay";
            this.Load += new System.EventHandler(this.frmReportQLMay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private haison haison1;
        private System.Windows.Forms.Button btnInBC;
        private System.Windows.Forms.ComboBox cbLoaiBaoCao;
        private System.Windows.Forms.Label label1;
    }
}