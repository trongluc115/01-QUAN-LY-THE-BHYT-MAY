using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataMySQL;
using LibBaocao;
namespace MediIT115
{
    public partial class frmReportQLMay : Form
    {
        DataSet ds;
        public frmReportQLMay()
        {
            InitializeComponent();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {
                   
           
          
            string s_tungay = for_ngay_yyyymmdd(haison1.tungay);
            string s_denngay = for_ngay_yyyymmdd(haison1.denngay);
          
           
            CMay datamay = new CMay();
            ds = datamay.get_BCBaoTri(s_tungay,s_denngay);
            string rptName = "BC_BAOTRI.rpt";
            rptName = cbLoaiBaoCao.Text;
            AccessData acc = new AccessData();
            frmReport a = new frmReport(acc, ds.Tables[0], rptName, "TỪ NGÀY " + haison1.tungay + " ĐẾN NGÀY " + haison1.denngay, "", "", "", "", "", "", "", "", "");
            a.Show();
           
            
            
        }
        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }

        private void frmReportQLMay_Load(object sender, EventArgs e)
        {
            try
            {
                cbLoaiBaoCao.SelectedIndex = 0;
            }
            catch { }
        }
    }
}