using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataOracle;
using LibBaocao;
using Data;
namespace MediIT115
{
    public partial class frmBCDuocMau20 : Form
    {
        public frmBCDuocMau20()
        {
            InitializeComponent();
        }

        private void btInBaoCao_Click(object sender, EventArgs e)
        {
         
            AccessData d = new AccessData();
            CThuocBHYTNgoaiTru data = new CThuocBHYTNgoaiTru();
            string tungay = haison1.tungay;
            string denngay = haison1.denngay;
            string title = haison1.s_title.Replace("Báo cáo ", "").ToUpper();
            DataSet ds = data.f_loadBaoCaoMau20(tungay, denngay, "");
            frmReport a = new frmReport(d, ds.Tables[0], "701.3.04_LTCBCVPBangke_020_2_user_20_bhyt_kho.rpt", title, txtHoTen.Text,txtGiamDoc.Text, "", "", "", "", "", "", "");
            a.Show();
        }
    }
}