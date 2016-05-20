namespace MediIT115
{
    partial class frmReportKiemKe
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
            this.btnInBC = new System.Windows.Forms.Button();
            this.cbLoaiBaoCao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView_loaimay = new Report2009.TreeView_HAISON();
            this.treeView_KhoaPhong = new Report2009.TreeView_HAISON();
            this.haison1 = new MediIT115.haison();
            this.SuspendLayout();
            // 
            // btnInBC
            // 
            this.btnInBC.Location = new System.Drawing.Point(590, 612);
            this.btnInBC.Name = "btnInBC";
            this.btnInBC.Size = new System.Drawing.Size(76, 29);
            this.btnInBC.TabIndex = 1;
            this.btnInBC.Text = "In báo cáo";
            this.btnInBC.UseVisualStyleBackColor = true;
            this.btnInBC.Click += new System.EventHandler(this.btnInBC_Click);
            // 
            // cbLoaiBaoCao
            // 
            this.cbLoaiBaoCao.FormattingEnabled = true;
            this.cbLoaiBaoCao.Items.AddRange(new object[] {
            "bc_kiemke.rpt",
            "bc_kiemke_01.rpt",
            "bc_kiemke_02.rpt"});
            this.cbLoaiBaoCao.Location = new System.Drawing.Point(88, 86);
            this.cbLoaiBaoCao.Name = "cbLoaiBaoCao";
            this.cbLoaiBaoCao.Size = new System.Drawing.Size(198, 21);
            this.cbLoaiBaoCao.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Loại báo cáo";
            // 
            // treeView_loaimay
            // 
            this.treeView_loaimay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.treeView_loaimay.BackColor = System.Drawing.Color.Transparent;
            this.treeView_loaimay.Location = new System.Drawing.Point(16, 113);
            this.treeView_loaimay.Name = "treeView_loaimay";
            this.treeView_loaimay.Size = new System.Drawing.Size(270, 493);
            this.treeView_loaimay.TabIndex = 5;
            // 
            // treeView_KhoaPhong
            // 
            this.treeView_KhoaPhong.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.treeView_KhoaPhong.BackColor = System.Drawing.Color.Transparent;
            this.treeView_KhoaPhong.Location = new System.Drawing.Point(292, 12);
            this.treeView_KhoaPhong.Name = "treeView_KhoaPhong";
            this.treeView_KhoaPhong.Size = new System.Drawing.Size(374, 594);
            this.treeView_KhoaPhong.TabIndex = 4;
            // 
            // haison1
            // 
            this.haison1.Location = new System.Drawing.Point(12, 12);
            this.haison1.Name = "haison1";
            this.haison1.Size = new System.Drawing.Size(274, 68);
            this.haison1.TabIndex = 0;
            // 
            // frmReportKiemKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 643);
            this.Controls.Add(this.treeView_loaimay);
            this.Controls.Add(this.treeView_KhoaPhong);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLoaiBaoCao);
            this.Controls.Add(this.btnInBC);
            this.Controls.Add(this.haison1);
            this.Name = "frmReportKiemKe";
            this.Text = "Kiem ke";
            this.Load += new System.EventHandler(this.frmReportQLMay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private haison haison1;
        private System.Windows.Forms.Button btnInBC;
        private System.Windows.Forms.ComboBox cbLoaiBaoCao;
        private System.Windows.Forms.Label label1;
        private Report2009.TreeView_HAISON treeView_KhoaPhong;
        private Report2009.TreeView_HAISON treeView_loaimay;
    }
}