namespace MediIT115
{
    partial class frmBaoCaoNoiTruBHYT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btInBaoCao = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chThuoc = new System.Windows.Forms.CheckBox();
            this.lbSoLuongChuaDuyet = new System.Windows.Forms.Label();
            this.txtTimKiemChuaDuyet = new System.Windows.Forms.TextBox();
            this.btLoadDSChuaDuyet = new System.Windows.Forms.Button();
            this.dsChuaDuyet = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbSoLuong = new System.Windows.Forms.Label();
            this.txtTimkiem = new System.Windows.Forms.TextBox();
            this.btLload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DSDaDuyet = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btXoa = new System.Windows.Forms.Button();
            this.txtIDDuyet = new System.Windows.Forms.TextBox();
            this.cbTuyen = new System.Windows.Forms.ComboBox();
            this.cbPhai = new System.Windows.Forms.ComboBox();
            this.txtTenBV = new System.Windows.Forms.TextBox();
            this.txtMaBHYT = new System.Windows.Forms.TextBox();
            this.txtNamSinh = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.btLuu = new System.Windows.Forms.Button();
            this.haison1 = new MediIT115.haison();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsChuaDuyet)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DSDaDuyet)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btInBaoCao
            // 
            this.btInBaoCao.Location = new System.Drawing.Point(313, 5);
            this.btInBaoCao.Name = "btInBaoCao";
            this.btInBaoCao.Size = new System.Drawing.Size(82, 28);
            this.btInBaoCao.TabIndex = 0;
            this.btInBaoCao.Text = "In Báo Cáo";
            this.btInBaoCao.UseVisualStyleBackColor = true;
            this.btInBaoCao.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 73);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(992, 603);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPage1.Controls.Add(this.chThuoc);
            this.tabPage1.Controls.Add(this.lbSoLuongChuaDuyet);
            this.tabPage1.Controls.Add(this.txtTimKiemChuaDuyet);
            this.tabPage1.Controls.Add(this.btLoadDSChuaDuyet);
            this.tabPage1.Controls.Add(this.dsChuaDuyet);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(984, 577);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Danh sách BHYT chưa duyệt";
            // 
            // chThuoc
            // 
            this.chThuoc.AutoSize = true;
            this.chThuoc.Checked = true;
            this.chThuoc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chThuoc.Location = new System.Drawing.Point(446, 11);
            this.chThuoc.Name = "chThuoc";
            this.chThuoc.Size = new System.Drawing.Size(179, 17);
            this.chThuoc.TabIndex = 31;
            this.chThuoc.Text = "Danh sách chưa duyệt có thuốc";
            this.chThuoc.UseVisualStyleBackColor = true;
            this.chThuoc.Visible = false;
            // 
            // lbSoLuongChuaDuyet
            // 
            this.lbSoLuongChuaDuyet.AutoSize = true;
            this.lbSoLuongChuaDuyet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoLuongChuaDuyet.ForeColor = System.Drawing.Color.Red;
            this.lbSoLuongChuaDuyet.Location = new System.Drawing.Point(310, 12);
            this.lbSoLuongChuaDuyet.Name = "lbSoLuongChuaDuyet";
            this.lbSoLuongChuaDuyet.Size = new System.Drawing.Size(16, 16);
            this.lbSoLuongChuaDuyet.TabIndex = 30;
            this.lbSoLuongChuaDuyet.Text = "0";
            // 
            // txtTimKiemChuaDuyet
            // 
            this.txtTimKiemChuaDuyet.BackColor = System.Drawing.Color.Yellow;
            this.txtTimKiemChuaDuyet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiemChuaDuyet.ForeColor = System.Drawing.Color.Red;
            this.txtTimKiemChuaDuyet.Location = new System.Drawing.Point(107, 8);
            this.txtTimKiemChuaDuyet.Name = "txtTimKiemChuaDuyet";
            this.txtTimKiemChuaDuyet.Size = new System.Drawing.Size(197, 21);
            this.txtTimKiemChuaDuyet.TabIndex = 29;
            this.txtTimKiemChuaDuyet.TabStop = false;
            this.txtTimKiemChuaDuyet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimKiemChuaDuyet.TextChanged += new System.EventHandler(this.txtTimKiemChuaDuyet_TextChanged);
            // 
            // btLoadDSChuaDuyet
            // 
            this.btLoadDSChuaDuyet.Location = new System.Drawing.Point(6, 5);
            this.btLoadDSChuaDuyet.Name = "btLoadDSChuaDuyet";
            this.btLoadDSChuaDuyet.Size = new System.Drawing.Size(82, 28);
            this.btLoadDSChuaDuyet.TabIndex = 28;
            this.btLoadDSChuaDuyet.Text = "Lấy dữ liệu";
            this.btLoadDSChuaDuyet.UseVisualStyleBackColor = true;
            this.btLoadDSChuaDuyet.Click += new System.EventHandler(this.btLoadDSChuaDuyet_Click);
            // 
            // dsChuaDuyet
            // 
            this.dsChuaDuyet.AllowUserToAddRows = false;
            this.dsChuaDuyet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dsChuaDuyet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dsChuaDuyet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dsChuaDuyet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dsChuaDuyet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dsChuaDuyet.Location = new System.Drawing.Point(8, 37);
            this.dsChuaDuyet.Name = "dsChuaDuyet";
            this.dsChuaDuyet.Size = new System.Drawing.Size(969, 522);
            this.dsChuaDuyet.TabIndex = 4;
            this.dsChuaDuyet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dsChuaDuyet_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabPage2.Controls.Add(this.lbSoLuong);
            this.tabPage2.Controls.Add(this.txtTimkiem);
            this.tabPage2.Controls.Add(this.btLload);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(984, 577);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Danh sách BHYT đã duyệt";
            // 
            // lbSoLuong
            // 
            this.lbSoLuong.AutoSize = true;
            this.lbSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSoLuong.ForeColor = System.Drawing.Color.Red;
            this.lbSoLuong.Location = new System.Drawing.Point(311, 12);
            this.lbSoLuong.Name = "lbSoLuong";
            this.lbSoLuong.Size = new System.Drawing.Size(16, 16);
            this.lbSoLuong.TabIndex = 27;
            this.lbSoLuong.Text = "0";
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.BackColor = System.Drawing.Color.Yellow;
            this.txtTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimkiem.ForeColor = System.Drawing.Color.Red;
            this.txtTimkiem.Location = new System.Drawing.Point(108, 8);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(197, 21);
            this.txtTimkiem.TabIndex = 26;
            this.txtTimkiem.TabStop = false;
            this.txtTimkiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTimkiem.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btLload
            // 
            this.btLload.Location = new System.Drawing.Point(9, 3);
            this.btLload.Name = "btLload";
            this.btLload.Size = new System.Drawing.Size(82, 28);
            this.btLload.TabIndex = 1;
            this.btLload.Text = "Lấy dữ liệu";
            this.btLload.UseVisualStyleBackColor = true;
            this.btLload.Click += new System.EventHandler(this.btLload_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DSDaDuyet);
            this.panel1.Location = new System.Drawing.Point(9, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 534);
            this.panel1.TabIndex = 0;
            // 
            // DSDaDuyet
            // 
            this.DSDaDuyet.AllowUserToAddRows = false;
            this.DSDaDuyet.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DSDaDuyet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DSDaDuyet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DSDaDuyet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DSDaDuyet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DSDaDuyet.Location = new System.Drawing.Point(0, 0);
            this.DSDaDuyet.Name = "DSDaDuyet";
            this.DSDaDuyet.Size = new System.Drawing.Size(969, 526);
            this.DSDaDuyet.TabIndex = 3;
            this.DSDaDuyet.Click += new System.EventHandler(this.DSDaDuyet_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btXoa);
            this.panel2.Controls.Add(this.txtIDDuyet);
            this.panel2.Controls.Add(this.cbTuyen);
            this.panel2.Controls.Add(this.cbPhai);
            this.panel2.Controls.Add(this.txtTenBV);
            this.panel2.Controls.Add(this.txtMaBHYT);
            this.panel2.Controls.Add(this.txtNamSinh);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtHoTen);
            this.panel2.Controls.Add(this.txtMaBN);
            this.panel2.Controls.Add(this.btLuu);
            this.panel2.Location = new System.Drawing.Point(401, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 87);
            this.panel2.TabIndex = 3;
            // 
            // btXoa
            // 
            this.btXoa.Location = new System.Drawing.Point(511, 54);
            this.btXoa.Name = "btXoa";
            this.btXoa.Size = new System.Drawing.Size(82, 28);
            this.btXoa.TabIndex = 4;
            this.btXoa.Text = "Xóa";
            this.btXoa.UseVisualStyleBackColor = true;
            this.btXoa.Click += new System.EventHandler(this.btXoa_Click);
            // 
            // txtIDDuyet
            // 
            this.txtIDDuyet.BackColor = System.Drawing.Color.Yellow;
            this.txtIDDuyet.ForeColor = System.Drawing.Color.Red;
            this.txtIDDuyet.Location = new System.Drawing.Point(185, 6);
            this.txtIDDuyet.Name = "txtIDDuyet";
            this.txtIDDuyet.Size = new System.Drawing.Size(107, 20);
            this.txtIDDuyet.TabIndex = 25;
            this.txtIDDuyet.TabStop = false;
            // 
            // cbTuyen
            // 
            this.cbTuyen.FormattingEnabled = true;
            this.cbTuyen.Items.AddRange(new object[] {
            "Đúng tuyến",
            "Trái tuyến"});
            this.cbTuyen.Location = new System.Drawing.Point(520, 2);
            this.cbTuyen.Name = "cbTuyen";
            this.cbTuyen.Size = new System.Drawing.Size(75, 21);
            this.cbTuyen.TabIndex = 20;
            this.cbTuyen.TabStop = false;
            // 
            // cbPhai
            // 
            this.cbPhai.FormattingEnabled = true;
            this.cbPhai.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cbPhai.Location = new System.Drawing.Point(215, 57);
            this.cbPhai.Name = "cbPhai";
            this.cbPhai.Size = new System.Drawing.Size(77, 21);
            this.cbPhai.TabIndex = 19;
            this.cbPhai.TabStop = false;
            // 
            // txtTenBV
            // 
            this.txtTenBV.Location = new System.Drawing.Point(366, 28);
            this.txtTenBV.Name = "txtTenBV";
            this.txtTenBV.Size = new System.Drawing.Size(228, 20);
            this.txtTenBV.TabIndex = 18;
            // 
            // txtMaBHYT
            // 
            this.txtMaBHYT.Location = new System.Drawing.Point(366, 3);
            this.txtMaBHYT.Name = "txtMaBHYT";
            this.txtMaBHYT.Size = new System.Drawing.Size(148, 20);
            this.txtMaBHYT.TabIndex = 16;
            // 
            // txtNamSinh
            // 
            this.txtNamSinh.Location = new System.Drawing.Point(72, 56);
            this.txtNamSinh.Name = "txtNamSinh";
            this.txtNamSinh.Size = new System.Drawing.Size(74, 20);
            this.txtNamSinh.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(302, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "NĐK BHYT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(302, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Mã BHYT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Giới tính";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Năm sinh";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Họ và tên";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã BN";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(72, 31);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(220, 20);
            this.txtHoTen.TabIndex = 6;
            // 
            // txtMaBN
            // 
            this.txtMaBN.Location = new System.Drawing.Point(72, 6);
            this.txtMaBN.MaxLength = 8;
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(74, 20);
            this.txtMaBN.TabIndex = 5;
            // 
            // btLuu
            // 
            this.btLuu.Location = new System.Drawing.Point(597, 154);
            this.btLuu.Name = "btLuu";
            this.btLuu.Size = new System.Drawing.Size(75, 23);
            this.btLuu.TabIndex = 0;
            this.btLuu.Text = "Lưu";
            this.btLuu.UseVisualStyleBackColor = true;
            // 
            // haison1
            // 
            this.haison1.Location = new System.Drawing.Point(2, 2);
            this.haison1.Name = "haison1";
            this.haison1.Size = new System.Drawing.Size(274, 68);
            this.haison1.TabIndex = 1;
            this.haison1.Load += new System.EventHandler(this.haison1_Load);
            // 
            // frmBaoCaoNoiTruBHYT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1016, 746);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.haison1);
            this.Controls.Add(this.btInBaoCao);
            this.Name = "frmBaoCaoNoiTruBHYT";
            this.Text = "Mau 25BV - Thanh toan BHYT Noi Tru";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBaoCaoNgoaiTruBHYT_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsChuaDuyet)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DSDaDuyet)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btInBaoCao;
        private haison haison1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dsChuaDuyet;
        private System.Windows.Forms.DataGridView DSDaDuyet;
        private System.Windows.Forms.Button btLload;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtIDDuyet;
        private System.Windows.Forms.ComboBox cbTuyen;
        private System.Windows.Forms.ComboBox cbPhai;
        private System.Windows.Forms.TextBox txtTenBV;
        private System.Windows.Forms.TextBox txtMaBHYT;
        private System.Windows.Forms.TextBox txtNamSinh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtMaBN;
        private System.Windows.Forms.Button btLuu;
        private System.Windows.Forms.Button btXoa;
        private System.Windows.Forms.TextBox txtTimkiem;
        private System.Windows.Forms.Label lbSoLuong;
        private System.Windows.Forms.Label lbSoLuongChuaDuyet;
        private System.Windows.Forms.TextBox txtTimKiemChuaDuyet;
        private System.Windows.Forms.Button btLoadDSChuaDuyet;
        private System.Windows.Forms.CheckBox chThuoc;
    }
}