using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using System.IO;

namespace MediIT115
{
	public class frmReport : System.Windows.Forms.Form
	{
		//binh
        string s_dirreport = "", s_bien = "", s_bien1 = "";
		int i_soluong_le=0,i_dongia_le=0,i_thanhtien_le=0;
		int i_nhom=1;
		//
        ExportOptions crExportOptions;
        DiskFileDestinationOptions crDiskFileDestinationOptions;
        

		Excel.Application oxl;
		Excel._Workbook owb;
		Excel._Worksheet osheet;
		ReportDocument oRpt;
		private LibBaocao.AccessData d;	
        private string ReportFile, ExportPath;
        private System.Data.DataTable dt = new System.Data.DataTable();
        private System.Data.DataSet ds = new System.Data.DataSet();
        private string c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, msg = "", s_loaibc = "";
        private bool b_bienlai = false, d_co = false, bSubReport = false;
		public bool bPrinter;
		private System.Windows.Forms.NumericUpDown banin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button butKetthuc;
		private System.Windows.Forms.Button butIn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown tu;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown den;
		private System.Windows.Forms.Button butExcel;
        private System.Windows.Forms.Button butPdf;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer Report;
		private System.ComponentModel.Container components = null;

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string mMsg, string report)
		{
			InitializeComponent();
			dt=ta;d=acc;
			msg=mMsg;
			ReportFile=report;
			this.Text=report;
		}

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string mMsg, string report, decimal soban)
        {
            InitializeComponent();
            dt = ta; d = acc;
            msg = mMsg;
            banin.Value = soban;
            ReportFile = report;
            this.Text = report;
        }

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string s1, string s2, string s3, string s4, string s5, string s6)
        {
            InitializeComponent();
            d = acc; dt = ta;
            c1 = s1; c2 = s2; c3 = s3; c4 = s4; c5 = s5; c6 = s6; s_loaibc = "M";
            ReportFile = report; this.Text = report;

        }

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10)
		{
			InitializeComponent();
			d=acc;dt=ta;
			c1=s1;c2=s2;c3=s3;c4=s4;c5=s5;
			c6=s6;c7=s7;c8=s8;c9=s9;c10=s10;
			ReportFile=report;this.Text=report;
		}
       


        public frmReport(LibBaocao.AccessData acc, System.Data.DataSet ta, string report, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, bool bsr)
        {
            InitializeComponent();
            d = acc; ds = ta;
            c1 = s1; c2 = s2; c3 = s3; c4 = s4; c5 = s5;
            c6 = s6; c7 = s7; c8 = s8; c9 = s9; c10 = s10;
            bSubReport = bsr;
            ReportFile = report; this.Text = report;
        }

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12)
		{
			InitializeComponent();
            d = acc; dt = ta;
            c1 = s1; c2 = s2; c3 = s3; c4 = s4; c5 = s5; c6 = s6;
            c7 = s7; c8 = s8; c9 = s9; c10 = s10; c11 = s11; c12 = s12;
			ReportFile=report;this.Text=report;			
		}
        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, decimal soban)
        {
            InitializeComponent();
            d = acc; dt = ta;
            c1 = s1; c2 = s2; c3 = s3; c4 = s4; c5 = s5; c6 = s6;
            c7 = s7; c8 = s8; c9 = s9; c10 = s10; c11 = s11; c12 = s12; banin.Value = soban;
            ReportFile = report; this.Text = report;
        }
        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string s12, string s13, string s14, string s15, string s16, string s17, string s18, string s19, string s20, bool co)
        {
            InitializeComponent();
            d = acc; dt = ta;
            c1 = s1; c2 = s2; c3 = s3; c4 = s4; c5 = s5; c6 = s6;
            c7 = s7; c8 = s8; c9 = s9; c10 = s10; c11 = s11; c12 = s12;
            c13 = s13; c14 = s14; c15 = s15; c16 = s16; c17 = s17; c18 = s18; c19 = s19; c20 = s20;
            d_co = co;
            ReportFile = report; this.Text = report;
        }
        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string s1, string s2, string report)
		{
			InitializeComponent();
			d=acc;
			dt=ta;
			c1=s1;c2=s2;
			b_bienlai=true;
			ReportFile=report;
			this.Text=report;			
		}

        public frmReport(LibBaocao.AccessData acc, System.Data.DataTable ta, string report, string ngay, string nguoithu, string a1, string a2, string a3, string a4, string a5, string a6, string a7, string a8, string a9, string a10, string aloaibc)
		{
			InitializeComponent();
			d=acc;dt=ta;c11=ngay;c12=nguoithu;
			c1=a1;
            c2=a2;
            c3=a3;
            c4=a4;
            c5=a5;

			c6=a6;
            c7=a7;
            c8=a8;
            c9=a9;
            c10=a10;
            s_loaibc = aloaibc;
			ReportFile=report;
            this.Text=report;
			
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport));
            this.banin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.butKetthuc = new System.Windows.Forms.Button();
            this.butIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tu = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.den = new System.Windows.Forms.NumericUpDown();
            this.butExcel = new System.Windows.Forms.Button();
            this.butPdf = new System.Windows.Forms.Button();
            this.Report = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.banin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.den)).BeginInit();
            this.SuspendLayout();
            // 
            // banin
            // 
            this.banin.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.banin.Location = new System.Drawing.Point(375, 5);
            this.banin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.banin.Name = "banin";
            this.banin.Size = new System.Drawing.Size(38, 21);
            this.banin.TabIndex = 1;
            this.banin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.banin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.banin_KeyDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(310, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Số bản in :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // butKetthuc
            // 
            this.butKetthuc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           // this.butKetthuc.Image = ((System.Drawing.Image)(resources.GetObject("butKetthuc.Image")));
            this.butKetthuc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butKetthuc.Location = new System.Drawing.Point(728, 4);
            this.butKetthuc.Name = "butKetthuc";
            this.butKetthuc.Size = new System.Drawing.Size(74, 23);
            this.butKetthuc.TabIndex = 8;
            this.butKetthuc.Text = "Kết thúc";
            this.butKetthuc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butKetthuc.Click += new System.EventHandler(this.butKetthuc_Click);
            // 
            // butIn
            // 
           // this.butIn.Image = ((System.Drawing.Image)(resources.GetObject("butIn.Image")));
            //this.butIn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butIn.Location = new System.Drawing.Point(562, 4);
            this.butIn.Name = "butIn";
            this.butIn.Size = new System.Drawing.Size(51, 23);
            this.butIn.TabIndex = 6;
            this.butIn.Text = "      In";
            this.butIn.Click += new System.EventHandler(this.butIn_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(406, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Từ :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tu
            // 
            this.tu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tu.Location = new System.Drawing.Point(438, 5);
            this.tu.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.tu.Name = "tu";
            this.tu.Size = new System.Drawing.Size(44, 21);
            this.tu.TabIndex = 3;
            this.tu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.banin_KeyDown);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(476, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "đến :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // den
            // 
            this.den.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.den.Location = new System.Drawing.Point(517, 5);
            this.den.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.den.Name = "den";
            this.den.Size = new System.Drawing.Size(44, 21);
            this.den.TabIndex = 5;
            this.den.KeyDown += new System.Windows.Forms.KeyEventHandler(this.banin_KeyDown);
            // 
            // butExcel
            // 
            //this.butExcel.Image = ((System.Drawing.Image)(resources.GetObject("butExcel.Image")));
            this.butExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butExcel.Location = new System.Drawing.Point(613, 4);
            this.butExcel.Name = "butExcel";
            this.butExcel.Size = new System.Drawing.Size(60, 23);
            this.butExcel.TabIndex = 7;
            this.butExcel.Text = "      Excel";
            this.butExcel.Click += new System.EventHandler(this.butExcel_Click);
            // 
            // butPdf
            // 
          //  this.butPdf.Image = ((System.Drawing.Image)(resources.GetObject("butPdf.Image")));
            this.butPdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butPdf.Location = new System.Drawing.Point(673, 4);
            this.butPdf.Name = "butPdf";
            this.butPdf.Size = new System.Drawing.Size(55, 23);
            this.butPdf.TabIndex = 11;
            this.butPdf.Text = "PDF";
            this.butPdf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butPdf.UseVisualStyleBackColor = true;
            this.butPdf.Click += new System.EventHandler(this.butPdf_Click);
            // 
            // Report
            // 
            this.Report.ActiveViewIndex = -1;
            this.Report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Report.DisplayGroupTree = false;
            this.Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Report.Location = new System.Drawing.Point(0, 0);
            this.Report.Name = "Report";
            this.Report.SelectionFormula = "";
            this.Report.ShowCloseButton = false;
            this.Report.ShowRefreshButton = false;
            this.Report.Size = new System.Drawing.Size(808, 573);
            this.Report.TabIndex = 12;
            this.Report.ViewTimeSelectionFormula = "";
            // 
            // frmReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.butKetthuc;
            this.ClientSize = new System.Drawing.Size(808, 573);
            this.Controls.Add(this.butPdf);
            this.Controls.Add(this.tu);
            this.Controls.Add(this.butExcel);
            this.Controls.Add(this.den);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.banin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butKetthuc);
            this.Controls.Add(this.butIn);
            this.Controls.Add(this.Report);
           // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viện phí .NET";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.banin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.den)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void frmReport_Load(object sender, System.EventArgs e)
		{
			this.Report.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width,Screen.PrimaryScreen.WorkingArea.Height);
			this.Size= new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width,Screen.PrimaryScreen.WorkingArea.Height);

            string dir = System.IO.Directory.GetCurrentDirectory();
            ExportPath = ""; s_bien = ""; s_bien1 = ""; s_dirreport = "";
            int j = 0;
            for (int i = 0; i < dir.Length; i++)
            {
                if (dir.Substring(i, 1) == "\\") j++;
                if (j == 2) break;
                ExportPath += dir.Substring(i, 1);
            }
            ExportPath += "\\pdf\\";
            if (!System.IO.Directory.Exists(ExportPath)) System.IO.Directory.CreateDirectory(ExportPath);
            if (!System.IO.Directory.Exists("..\\..\\..\\Report\\")) System.IO.Directory.CreateDirectory("..\\..\\..\\Report\\");
            s_dirreport = "..\\..\\..\\Report\\" + ReportFile;
           // s_dirreport = "d:\\" + ReportFile;
            //oRpt.ReportOptions.
			/*i_nhom=get_nhomkho();
					
			i_soluong_le=d.d_soluong_le(i_nhom);
			i_dongia_le=d.d_dongia_le(i_nhom);
			i_thanhtien_le=d.d_thanhtien_le(i_nhom);
			//
            if (b_bienlai) Bienlai();
            else if (ReportFile.ToString().ToLower() == "d_phieunhap.rpt") Phieunhap();
            else if (msg != "") treem();
            else if (d_co) Kiemnhap();
            else if (s_loaibc == "HS") Baocao();
            else if (s_loaibc == "M") MauBaocao();
            else if (bSubReport) PreviewReportSub();
            else 
             */
            PreviewReport();
		}

		private void treem()
		{
			try
			{
				oRpt=new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
				oRpt.SetDataSource(dt);
                s_bien = "SoYTe";
				oRpt.DataDefinition.FormulaFields["SoYTe"].Text="'"+d.Syte+"'";
                s_bien = "BenhVien";
				oRpt.DataDefinition.FormulaFields["BenhVien"].Text="'"+d.Tenbv+"'";
                s_bien = "TenBenhAn";
				oRpt.DataDefinition.FormulaFields["TenBenhAn"].Text="'"+msg+"'";			
				Report.ReportSource=oRpt;
			}
			catch (Exception e)
			{
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
			}
		}
        
        private void Baocao()
        {
            try
            {
                oRpt = new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }

                oRpt.SetDataSource(dt);
                s_bien = "SoYTe";
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + d.Syte + "'";
                s_bien = "SoYTe";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + d.Tenbv + "'";
                s_bien = "ngaybc";
                oRpt.DataDefinition.FormulaFields["ngaybc"].Text = "'" + c11 + "'";
                s_bien = "nguoibc";
                oRpt.DataDefinition.FormulaFields["nguoibc"].Text = "'" + c12 + "'";
                s_bien = "a1";
                oRpt.DataDefinition.FormulaFields["a1"].Text = "'" + c1 + "'";
                s_bien = "a2";
                oRpt.DataDefinition.FormulaFields["a2"].Text = "'" + c2 + "'";
                s_bien = "a3";
                oRpt.DataDefinition.FormulaFields["a3"].Text = "'" + c3 + "'";
                s_bien = "a4";
                oRpt.DataDefinition.FormulaFields["a4"].Text = "'" + c4 + "'";
                s_bien = "a5";
                oRpt.DataDefinition.FormulaFields["a5"].Text = "'" + c5 + "'";
                s_bien = "c1";
                oRpt.DataDefinition.FormulaFields["c1"].Text = "'" + c6 + "'";
                s_bien = "c2";
                oRpt.DataDefinition.FormulaFields["c2"].Text = "'" + c7 + "'";
                s_bien = "c3";
                oRpt.DataDefinition.FormulaFields["c3"].Text = "'" + c8 + "'";
                s_bien = "c4";
                oRpt.DataDefinition.FormulaFields["c4"].Text = "'" + c9 + "'";
                s_bien = "c5";
                oRpt.DataDefinition.FormulaFields["c5"].Text = "'" + c10 + "'";
                
                Report.ReportSource = oRpt;

            }
            catch (Exception e)
            {
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
            }
        }

        private void s_mess(string s_loi)
        {
            if (s_bien1 != "")
                MessageBox.Show(d.s_ThieuReport(s_loi + "^" + ReportFile) + "\n Vui lòng copy report này vào thư mục Report ngang hàng với chương trình chạy !", d.Msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (s_bien != "")
            {
                DialogResult dlg = MessageBox.Show(d.s_FormulaFields(s_bien + "^" + ReportFile + "\n Bạn có muốn sửa report " + ReportFile + " không ?"), d.Msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlg == DialogResult.Yes)
                {
                    try
                    {
                        LibBaocao.run f = new LibBaocao.run("devenv", s_dirreport, false);
                        f.Launch();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void MauBaocao()
        {
            try
            {
                oRpt = new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
                foreach (CrystalDecisions.CrystalReports.Engine.Section section in oRpt.ReportDefinition.Sections)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.ReportObject reportObject in section.ReportObjects)
                    {
                        if (reportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subReport = (SubreportObject)reportObject;
                            ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName);
                            subDocument.SetDataSource(dt);
                        }
                    }
                }
                oRpt.SetDataSource(dt);
                s_bien = "soyte";
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + d.Syte + "'";
                s_bien = "benhvien";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + d.Tenbv + "'";
                s_bien = "c1";
                oRpt.DataDefinition.FormulaFields["c1"].Text = "'" + c1 + "'";
                s_bien = "c2";
                oRpt.DataDefinition.FormulaFields["c2"].Text = "'" + c2 + "'"; 
                s_bien = "c3";
                oRpt.DataDefinition.FormulaFields["c3"].Text = "'" + c3 + "'";
                s_bien = "c4";
                oRpt.DataDefinition.FormulaFields["c4"].Text = "'" + c4 + "'";
                s_bien = "c5";
                oRpt.DataDefinition.FormulaFields["c5"].Text = "'" + c5 + "'";
                s_bien = "c6";
                oRpt.DataDefinition.FormulaFields["c6"].Text = "'" + c6 + "'";

                Report.ReportSource = oRpt;

            }
            catch (Exception e)
            {
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
            }
        }

        private void Bienlai()
		{
            try
            {
                oRpt = new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
                oRpt.SetDataSource(dt);
                s_bien = "soyte";
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + d.Syte + "'";
                s_bien = "benhvien";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + d.Tenbv + "'";
                s_bien = "s_nguoithu";
                oRpt.DataDefinition.FormulaFields["s_nguoithu"].Text = "'" + c1 + "'";
                s_bien = "s_sovaovien";
                oRpt.DataDefinition.FormulaFields["s_sovaovien"].Text = "'" + c2 + "'";
                
                Report.ReportSource = oRpt;
            }
            catch (Exception e)
            {
                s_bien1 = e.Message;
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
                //s_mess(e.Message);
            }
		}		

		private void PreviewReport()
		{
			try
			{
				oRpt=new ReportDocument();				
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch(Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
                foreach (CrystalDecisions.CrystalReports.Engine.Section section in oRpt.ReportDefinition.Sections)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.ReportObject reportObject in section.ReportObjects)
                    {
                        if (reportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subReport = (SubreportObject)reportObject;
                            ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName);
                            subDocument.SetDataSource(dt);
                        }
                    }
                }
				oRpt.SetDataSource(dt);
                s_bien = "soyte";
				oRpt.DataDefinition.FormulaFields["soyte"].Text="'SYT TP HỒ CHÍ MINH'";
                s_bien = "benhvien";
				oRpt.DataDefinition.FormulaFields["benhvien"].Text="'BỆNH VIỆN NHÂN DÂN 115'";
                s_bien = "c1";
				oRpt.DataDefinition.FormulaFields["c1"].Text="'"+c1+"'";
                s_bien = "c2";
				oRpt.DataDefinition.FormulaFields["c2"].Text="'"+c2+"'";
                s_bien = "c3";
				oRpt.DataDefinition.FormulaFields["c3"].Text="'"+c3+"'";
                s_bien = "c4";
				oRpt.DataDefinition.FormulaFields["c4"].Text="'"+c4+"'";
                s_bien = "c5";
				oRpt.DataDefinition.FormulaFields["c5"].Text="'"+c5+"'";
                s_bien = "c6";
				oRpt.DataDefinition.FormulaFields["c6"].Text="'"+c6+"'";
                s_bien = "c7";
				oRpt.DataDefinition.FormulaFields["c7"].Text="'"+c7+"'";
                s_bien = "c8";
				oRpt.DataDefinition.FormulaFields["c8"].Text="'"+c8+"'";
                s_bien = "c9";
				oRpt.DataDefinition.FormulaFields["c9"].Text="'"+c9+"'";
                s_bien = "c10";
				oRpt.DataDefinition.FormulaFields["c10"].Text="'"+c10+"'";  
              			
				Report.ReportSource=oRpt;
			}
			catch (Exception e)
			{
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
			}
		}

        private void PreviewReportSub()
        {
            try
            {
                oRpt = new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
                foreach (CrystalDecisions.CrystalReports.Engine.Section section in oRpt.ReportDefinition.Sections)
                {
                    foreach (CrystalDecisions.CrystalReports.Engine.ReportObject reportObject in section.ReportObjects)
                    {
                        if (reportObject.Kind == ReportObjectKind.SubreportObject)
                        {
                            SubreportObject subReport = (SubreportObject)reportObject;
                            ReportDocument subDocument = subReport.OpenSubreport(subReport.SubreportName);
                            subDocument.SetDataSource(ds);
                        }
                    }
                }
                oRpt.SetDataSource(ds);
                s_bien = "soyte";
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + d.Syte + "'";
                s_bien = "benhvien";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + d.Tenbv + "'";
                s_bien = "c1";
                oRpt.DataDefinition.FormulaFields["c1"].Text = "'" + c1 + "'";
                s_bien = "c2";
                oRpt.DataDefinition.FormulaFields["c2"].Text = "'" + c2 + "'";
                s_bien = "c3";
                oRpt.DataDefinition.FormulaFields["c3"].Text = "'" + c3 + "'";
                s_bien = "c4";
                oRpt.DataDefinition.FormulaFields["c4"].Text = "'" + c4 + "'";
                s_bien = "c5";
                oRpt.DataDefinition.FormulaFields["c5"].Text = "'" + c5 + "'";
                s_bien = "c6";
                oRpt.DataDefinition.FormulaFields["c6"].Text = "'" + c6 + "'";
                s_bien = "c7";
                oRpt.DataDefinition.FormulaFields["c7"].Text = "'" + c7 + "'";
                s_bien = "c8";
                oRpt.DataDefinition.FormulaFields["c8"].Text = "'" + c8 + "'";
                s_bien = "c9";
                oRpt.DataDefinition.FormulaFields["c9"].Text = "'" + c9 + "'";
                s_bien = "c10";
                oRpt.DataDefinition.FormulaFields["c10"].Text = "'" + c10 + "'";

                Report.ReportSource = oRpt;
            }
            catch (Exception e)
            {
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
            }
        }

		private void Phieunhap()
		{
			try
			{
				oRpt=new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
				oRpt.SetDataSource(dt);
                s_bien = "soyte";
				oRpt.DataDefinition.FormulaFields["soyte"].Text="'"+d.Syte+"'";
                s_bien = "benhvien";
				oRpt.DataDefinition.FormulaFields["benhvien"].Text="'"+d.Tenbv+"'";
                s_bien = "c1";
				oRpt.DataDefinition.FormulaFields["c1"].Text="'"+c1+"'";
                s_bien = "c2";
				oRpt.DataDefinition.FormulaFields["c2"].Text="'"+c2+"'";
                s_bien = "c3";
				oRpt.DataDefinition.FormulaFields["c3"].Text="'"+c3+"'";
                s_bien = "c4";
				oRpt.DataDefinition.FormulaFields["c4"].Text="'"+c4+"'";
                s_bien = "c5";
				oRpt.DataDefinition.FormulaFields["c5"].Text="'"+c5+"'";
                s_bien = "c6";
				oRpt.DataDefinition.FormulaFields["c6"].Text="'"+c6+"'";
                s_bien = "c7";
				oRpt.DataDefinition.FormulaFields["c7"].Text="'"+c7+"'";
                s_bien = "c8";
				oRpt.DataDefinition.FormulaFields["c8"].Text="'"+c8+"'";
                s_bien = "c9";
				oRpt.DataDefinition.FormulaFields["c9"].Text="'"+c9+"'";
                s_bien = "c10";
				oRpt.DataDefinition.FormulaFields["c10"].Text="'"+c10+"'";
                s_bien = "diachi";
				oRpt.DataDefinition.FormulaFields["diachi"].Text="'"+c11+"'";
                s_bien = "masothue";
				oRpt.DataDefinition.FormulaFields["masothue"].Text="'"+c12+"'";
                s_bien = "giamdoc";
				oRpt.DataDefinition.FormulaFields["giamdoc"].Text="'"+d.Giamdoc(i_nhom)+"'";
                s_bien = "phutrach";
				oRpt.DataDefinition.FormulaFields["phutrach"].Text="'"+d.Phutrach(i_nhom)+"'";
                s_bien = "thongke";
				oRpt.DataDefinition.FormulaFields["thongke"].Text="'"+d.Thongke(i_nhom)+"'";
                s_bien = "ketoan";
				oRpt.DataDefinition.FormulaFields["ketoan"].Text="'"+d.Ketoan(i_nhom)+"'";
                s_bien = "thukho";
				oRpt.DataDefinition.FormulaFields["thukho"].Text="'"+d.Thukho(i_nhom)+"'";
                s_bien = "l_soluong";
				oRpt.DataDefinition.FormulaFields["l_soluong"].Text=i_soluong_le.ToString();
                s_bien = "l_dongia";
				oRpt.DataDefinition.FormulaFields["l_dongia"].Text=i_dongia_le.ToString();
                s_bien = "l_thanhtien";
				oRpt.DataDefinition.FormulaFields["l_thanhtien"].Text=i_thanhtien_le.ToString();
				//
				Report.ReportSource=oRpt;
			}
			catch (Exception e)
			{
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
			}
		}
        private void Kiemnhap()
        {
            try
            {
                oRpt = new ReportDocument();
                try
                {
                    oRpt.Load(s_dirreport, OpenReportMethod.OpenReportByTempCopy);
                }
                catch (Exception ex)
                {
                    s_bien1 = ex.Message;
                    s_mess(s_bien1);
                    return;
                }
                oRpt.SetDataSource(dt);
                s_bien = "soyte";
                oRpt.DataDefinition.FormulaFields["soyte"].Text = "'" + d.Syte + "'";
                s_bien = "benhvien";
                oRpt.DataDefinition.FormulaFields["benhvien"].Text = "'" + d.Tenbv + "'";
                s_bien = "c1";
                oRpt.DataDefinition.FormulaFields["c1"].Text = "'" + c1 + "'";
                s_bien = "c2";
                oRpt.DataDefinition.FormulaFields["c2"].Text = "'" + c2 + "'";
                s_bien = "c3";
                oRpt.DataDefinition.FormulaFields["c3"].Text = "'" + c3 + "'";
                s_bien = "c4";
                oRpt.DataDefinition.FormulaFields["c4"].Text = "'" + c4 + "'";
                s_bien = "c5";
                oRpt.DataDefinition.FormulaFields["c5"].Text = "'" + c5 + "'";
                s_bien = "c6";
                oRpt.DataDefinition.FormulaFields["c6"].Text = "'" + c6 + "'";
                s_bien = "c7";
                oRpt.DataDefinition.FormulaFields["c7"].Text = "'" + c7 + "'";
                s_bien = "c8";
                oRpt.DataDefinition.FormulaFields["c8"].Text = "'" + c8 + "'";
                s_bien = "c9";
                oRpt.DataDefinition.FormulaFields["c9"].Text = "'" + c9 + "'";
                s_bien = "c10";
                oRpt.DataDefinition.FormulaFields["c10"].Text = "'" + c10 + "'";
                s_bien = "c11";
                oRpt.DataDefinition.FormulaFields["c11"].Text = "'" + c11 + "'";
                s_bien = "c12";
                oRpt.DataDefinition.FormulaFields["c12"].Text = "'" + c12 + "'";
                s_bien = "c13";
                oRpt.DataDefinition.FormulaFields["c13"].Text = "'" + c13 + "'";
                s_bien = "c14";
                oRpt.DataDefinition.FormulaFields["c14"].Text = "'" + c14 + "'";
                s_bien = "c15";
                oRpt.DataDefinition.FormulaFields["c15"].Text = "'" + c15 + "'";
                s_bien = "c16";
                oRpt.DataDefinition.FormulaFields["c16"].Text = "'" + c16 + "'";
                s_bien = "c17";
                oRpt.DataDefinition.FormulaFields["c17"].Text = "'" + c17 + "'"; 
                s_bien = "c18";
                oRpt.DataDefinition.FormulaFields["c18"].Text = "'" + c18 + "'";
                s_bien = "c19";
                oRpt.DataDefinition.FormulaFields["c19"].Text = "'" + c19 + "'";
                s_bien = "c20";
                oRpt.DataDefinition.FormulaFields["c20"].Text = "'" + c20 + "'";
                
                Report.ReportSource = oRpt;
            }
            catch (Exception e)
            {
                s_bien1 = e.Message;
                //s_mess(e.Message);
                MessageBox.Show("Thiếu fomular '" + s_bien + "'");
            }
        }

		private void butKetthuc_Click(object sender, System.EventArgs e)
		{
			bPrinter=false;
			GC.Collect(); this.Close();
		}

		private void butIn_Click(object sender, System.EventArgs e)
		{
			Cursor=Cursors.WaitCursor;
			bPrinter=true;
			try
			{
				oRpt.PrintToPrinter(Convert.ToInt16(banin.Value),false,Convert.ToInt16(tu.Value),Convert.ToInt16(den.Value));
                if (c3 == "rptCongno")
                {
                    if (DialogResult.Yes == MessageBox.Show("Bạn có muốn In không?", "Thông báo!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        foreach (DataRow dr in dt.Rows)
                        {
                            d.execute_data_mmyy("update xxx.d_thanhtoan set lanin = lanin + 1 where id = " + decimal.Parse(dr["ID"].ToString()).ToString(), c4, c5, false);
                        }
                }
			}
			catch(Exception ex){MessageBox.Show(ex.Message);}
			Cursor=Cursors.Default;
			GC.Collect(); this.Close();
		}

		private void frmReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.F9: butIn_Click(sender,e);break;
				case Keys.F7: butExcel_Click(sender,e);break;
				case Keys.F10: butKetthuc_Click(sender,e);break;
			}
		}		
		//binh		
		private int get_nhomkho()
		{
			DataSet lds=new DataSet();
			lds.ReadXml("..\\..\\..\\xml\\d_nhomkholog.xml");
			int i_nhomkho=1;
			foreach(DataRow r in lds.Tables[0].Rows)
			{
				i_nhomkho=int.Parse(r["nhomkho"].ToString());
				break;
			}
			lds.Dispose();
			return i_nhomkho;
		}

		private void butExcel_Click(object sender, System.EventArgs e)
		{
            try
            {
                d.check_process_Excel();
                string tenfile = d.Export_Excel(dt, ReportFile.Substring(0, ReportFile.Length - 4));
                oxl = new Excel.Application();
                owb = (Excel._Workbook)(oxl.Workbooks.Open(tenfile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                osheet = (Excel._Worksheet)owb.ActiveSheet;
                oxl.ActiveWindow.DisplayGridlines = true;
                oxl.ActiveWindow.DisplayZeros = false;
                osheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                osheet.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                osheet.PageSetup.LeftMargin = 20;
                osheet.PageSetup.RightMargin = 20;
                osheet.PageSetup.TopMargin = 30;
                osheet.PageSetup.CenterFooter = "Trang : &P/&N";
                oxl.Visible = true;
            }
            catch { }
		}

		private void banin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode==Keys.Enter) SendKeys.Send("{Tab}");
		}
		#region binh_writemenubar
		//copy theo Tan
		private void f_WriteMenu(MainMenu v_m)
		{
			try
			{
				DataSet ads = new DataSet();
				ads.Tables.Add();
				ads.Tables[0].Columns.Add("TEN");
				string t="";
				for(int i=0;i<v_m.MenuItems.Count-3;i++)
				{
					for(int j=0;j<v_m.MenuItems[i].MenuItems.Count;j++)
					{
						if((v_m.MenuItems[i].MenuItems[j].Text.Trim()!="-")&&(v_m.MenuItems[i].MenuItems[j].Text.Trim().ToUpper().IndexOf("LOG OFF")<0)&&(v_m.MenuItems[i].MenuItems[j].MenuItems.Count<=0))
						{
							t=v_m.MenuItems[i].MenuItems[j].Text.Trim().Replace("&","");
							try
							{
								t=t.Substring(t.IndexOf(".")+1).Trim();
							}
							catch
							{
							}
							if(t!="")
								ads.Tables[0].Rows.Add(new string[] {t});
						}
						else
						{
							for(int k=0;k<v_m.MenuItems[i].MenuItems[j].MenuItems.Count;k++)
							{
								if((v_m.MenuItems[i].MenuItems[j].MenuItems[k].Text.Trim()!="-")&&(v_m.MenuItems[i].MenuItems[j].MenuItems[k].Text.Trim().ToUpper().IndexOf("LOG OFF")<0)&&(v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems.Count<=0))
								{
									t=v_m.MenuItems[i].MenuItems[j].MenuItems[k].Text.Trim().Replace("&","");
									try
									{
										t=t.Substring(t.IndexOf(".")+1).Trim();
									}
									catch
									{
									}
									if(t!="")
										ads.Tables[0].Rows.Add(new string[] {t});
								}
								else
								{
									for(int l=0;l<v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems.Count;k++)
									{
										if((v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems[l].Text.Trim()!="-")&&(v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems[l].Text.Trim().ToUpper().IndexOf("LOG OFF")<0)&&(v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems[l].MenuItems.Count<=0))
										{
											t=v_m.MenuItems[i].MenuItems[j].MenuItems[k].MenuItems[l].Text.Trim().Replace("&","");
											try
											{
												t=t.Substring(t.IndexOf(".")+1).Trim();
											}
											catch
											{
											}
											if(t!="")
												ads.Tables[0].Rows.Add(new string[] {t});
										}
										else
										{
										}
									}
								}
							}
						}
					}
				}
				ads.WriteXml("..//..//..//xml//d_menubar.xml");
			}
			catch
			{
			}
		}
		private void get_nhomkho(int i_userid)
		{
			string sql="select nhomkho from "+d.user+".d_dlogin where id='"+i_userid+"'";
			DataSet lds=d.get_data(sql);
			lds.WriteXml("..\\..\\..\\xml\\d_nhomkholog.xml",XmlWriteMode.WriteSchema);
		}
		#endregion

        private void butPdf_Click(object sender, EventArgs e)
        {
            string tenfile = ReportFile.ToLower().Replace(".rpt", "");
            tenfile = ExportPath + tenfile + ".pdf";
            crDiskFileDestinationOptions = new DiskFileDestinationOptions();
            crExportOptions = oRpt.ExportOptions;
            crDiskFileDestinationOptions.DiskFileName = tenfile;
            crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
            crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            oRpt.Export();
            try
            {
                string filerun = "AcroRd32.exe", arg = tenfile;
                
                if (System.IO.File.Exists(arg))
                {
                    LibBaocao.run f = new LibBaocao.run(filerun, arg, true);
                    f.Launch();
                }
            }
            catch
            {
                MessageBox.Show("Tập tin :" + tenfile);
            }
        }
	}
}