namespace MediIT115
{
    partial class frmKiemke
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
            this.btYes = new System.Windows.Forms.Button();
            this.btNo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbKiemke = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btYes
            // 
            this.btYes.Location = new System.Drawing.Point(65, 46);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(97, 34);
            this.btYes.TabIndex = 0;
            this.btYes.Text = "Đã kiểm";
            this.btYes.UseVisualStyleBackColor = true;
            this.btYes.Click += new System.EventHandler(this.btYes_Click);
            // 
            // btNo
            // 
            this.btNo.Location = new System.Drawing.Point(182, 46);
            this.btNo.Name = "btNo";
            this.btNo.Size = new System.Drawing.Size(97, 34);
            this.btNo.TabIndex = 1;
            this.btNo.Text = "Chưa kiểm";
            this.btNo.UseVisualStyleBackColor = true;
            this.btNo.Click += new System.EventHandler(this.btNo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Đánh đấu kiểm kê :";
            // 
            // lbKiemke
            // 
            this.lbKiemke.AutoSize = true;
            this.lbKiemke.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKiemke.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbKiemke.Location = new System.Drawing.Point(145, 9);
            this.lbKiemke.Name = "lbKiemke";
            this.lbKiemke.Size = new System.Drawing.Size(17, 24);
            this.lbKiemke.TabIndex = 3;
            this.lbKiemke.Text = "-";
            // 
            // frmKiemke
            // 
            this.ClientSize = new System.Drawing.Size(370, 101);
            this.Controls.Add(this.lbKiemke);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btNo);
            this.Controls.Add(this.btYes);
            this.Name = "frmKiemke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmKiemke_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btYes;
        private System.Windows.Forms.Button btNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbKiemke;
    }
}