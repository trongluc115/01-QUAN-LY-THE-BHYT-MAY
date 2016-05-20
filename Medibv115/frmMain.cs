using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using DataMySQL;
namespace MediIT115
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

      

        private void d01BáoCáoThuốcBHYTNgoạiTrúM20BVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBCDuocMau20 frm = new frmBCDuocMau20();
            frm.MdiParent = this;
            frm.Show();
        }

       

        private void duyệtBHYTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDuyetBHYT frm = new frmDuyetBHYT();
            frm.MdiParent = this;
            frm.Show();
        }

        private void báoCáoBHYTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBaoCaoNgoaiTruBHYT frm = new frmBaoCaoNgoaiTruBHYT();
            frm.MdiParent = this;
            frm.Show();
        }

        private void duyệtBHYTToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmDuyetBHYTNoiTru frm = new frmDuyetBHYTNoiTru();
            frm.MdiParent = this;
            frm.Show();
        }

        private void báoCáoBHYTToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmBaoCaoNoiTruBHYT frm = new frmBaoCaoNoiTruBHYT();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thôngSốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.MdiParent = this;
            frm.Show();
        }

        private void trangThiếtBịToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLThietBi frm = new frmQLThietBi();
            frm.MdiParent = this;
            frm.Show();
        }

       

        private void yêuCầuChỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLYeuCau frm = new frmQLYeuCau();
            frm.MdiParent = this;
            frm.Show();
        }

        private void nhậnTrảThẻBHYTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLBHYT frm = new frmQLBHYT();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                CXml xml = new CXml();
                string ID = xml.ReadXML(1, "ConfigMenu.xml");
                CTiepNhanYC _MySQL = new CTiepNhanYC();
                string _right = _MySQL.get_RightDangNhap(ID);
                string[] itemMenus = _right.Split('^');
                foreach (string itemMenu in itemMenus)
                {
                    switch (itemMenu)
                    {
                        case "1": hệThốngToolStripMenuItem.Visible = true;
                            break;

                        case "2_1": bHYTToolStripMenuItem.Visible = true;
                                   nhậnTrảThẻBHYTToolStripMenuItem.Visible = true;
                                    break;
                        case "2_2": bHYTToolStripMenuItem.Visible = true;
                                    trảThẻBHYTToolStripMenuItem.Visible = true;
                                    break;
                        case "3": quảnLýToolStripMenuItem.Visible = true;
                            break;

                    }
                }
            }
            catch { }
        }

        private void trảThẻBHYTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQLBHYT_trathe frm = new frmQLBHYT_trathe();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tiếpNhậnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTiepNhanKB frm = new frmTiepNhanKB
                ();
            frm.MdiParent = this;
            frm.Show();
        }

        private void báoCáoBảoTrìToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportQLMay frm = new frmReportQLMay();
              

            frm.MdiParent = this;
            frm.Show();
        }

        private void báoCáoKiểmKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportKiemKe frm = new frmReportKiemKe();


            frm.MdiParent = this;
            frm.Show();
        }
    }
}