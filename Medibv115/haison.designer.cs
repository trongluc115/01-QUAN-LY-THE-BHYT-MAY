namespace MediIT115
{
    partial class haison
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.den = new System.Windows.Forms.DateTimePicker();
            this.tu = new System.Windows.Forms.DateTimePicker();
            this.cbnam = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cbthang = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBc = new System.Windows.Forms.Label();
            this.cbbaocao = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cbnam)).BeginInit();
            this.SuspendLayout();
            // 
            // den
            // 
            this.den.CustomFormat = "dd/MM/yyyy";
            this.den.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.den.Location = new System.Drawing.Point(189, 46);
            this.den.Name = "den";
            this.den.Size = new System.Drawing.Size(83, 20);
            this.den.TabIndex = 4;
            this.den.KeyDown += new System.Windows.Forms.KeyEventHandler(this.den_KeyDown);
            // 
            // tu
            // 
            this.tu.CustomFormat = "dd/MM/yyyy";
            this.tu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tu.Location = new System.Drawing.Point(50, 47);
            this.tu.Name = "tu";
            this.tu.Size = new System.Drawing.Size(83, 20);
            this.tu.TabIndex = 3;
            this.tu.Validated += new System.EventHandler(this.tu_Validated);
            this.tu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tu_KeyDown);
            // 
            // cbnam
            // 
            this.cbnam.Location = new System.Drawing.Point(189, 25);
            this.cbnam.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.cbnam.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.cbnam.Name = "cbnam";
            this.cbnam.Size = new System.Drawing.Size(83, 20);
            this.cbnam.TabIndex = 2;
            this.cbnam.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.cbnam.ValueChanged += new System.EventHandler(this.cbnam_ValueChanged);
            this.cbnam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbnam_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(147, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "đến:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbthang
            // 
            this.cbthang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbthang.FormattingEnabled = true;
            this.cbthang.Location = new System.Drawing.Point(50, 25);
            this.cbthang.Name = "cbthang";
            this.cbthang.Size = new System.Drawing.Size(83, 21);
            this.cbthang.TabIndex = 1;
            this.cbthang.SelectedIndexChanged += new System.EventHandler(this.cbthang_SelectedIndexChanged);
            this.cbthang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbthang_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Từ ngày:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Năm:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBc
            // 
            this.lblBc.AutoSize = true;
            this.lblBc.Location = new System.Drawing.Point(12, 28);
            this.lblBc.Name = "lblBc";
            this.lblBc.Size = new System.Drawing.Size(41, 13);
            this.lblBc.TabIndex = 8;
            this.lblBc.Text = "Tháng:";
            this.lblBc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbbaocao
            // 
            this.cbbaocao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbaocao.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbaocao.FormattingEnabled = true;
            this.cbbaocao.Items.AddRange(new object[] {
            "Từ ngày ... đến ngày",
            "Tháng",
            "Qúi",
            "Năm"});
            this.cbbaocao.Location = new System.Drawing.Point(50, 2);
            this.cbbaocao.Name = "cbbaocao";
            this.cbbaocao.Size = new System.Drawing.Size(222, 22);
            this.cbbaocao.TabIndex = 0;
            this.cbbaocao.SelectedIndexChanged += new System.EventHandler(this.cbbaocao_SelectedIndexChanged);
            this.cbbaocao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbbaocao_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Báo cáo:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // haison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.den);
            this.Controls.Add(this.tu);
            this.Controls.Add(this.cbnam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbthang);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblBc);
            this.Controls.Add(this.cbbaocao);
            this.Controls.Add(this.label1);
            this.Name = "haison";
            this.Size = new System.Drawing.Size(274, 68);
            this.Load += new System.EventHandler(this.haison_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbnam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker den;
        private System.Windows.Forms.DateTimePicker tu;
        private System.Windows.Forms.NumericUpDown cbnam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbthang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBc;
        private System.Windows.Forms.ComboBox cbbaocao;
        private System.Windows.Forms.Label label1;
    }
}
