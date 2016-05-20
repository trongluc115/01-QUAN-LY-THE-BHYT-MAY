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
    public partial class frmReportKiemKe: Form
    {
        DataSet ds;
        CMay _MySQLCMay;
        public frmReportKiemKe()
        {
            InitializeComponent();
        }

        private void btnInBC_Click(object sender, EventArgs e)
        {


            string DSMAKP = treeView_KhoaPhong.get_Ma;
            DSMAKP = DSMAKP.TrimEnd(',');
            DSMAKP = DSMAKP.Replace("'", "");

            string DSLOAI = treeView_loaimay.get_Ma;
            DSLOAI = DSLOAI.TrimEnd(',');
            DSLOAI = DSLOAI.Replace("'", "");
            string s_tungay = for_ngay_yyyymmdd(haison1.tungay);
            string s_denngay = for_ngay_yyyymmdd(haison1.denngay);
          
           
            CMay datamay = new CMay();
            ds = datamay.get_KiemKe(s_tungay, s_denngay, DSLOAI,DSMAKP);
            string rptName = "BC_kiemke.rpt";
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
                _MySQLCMay = new CMay();

                cbLoaiBaoCao.SelectedIndex = 0;
                load_listKhoaPhong();
                load_listLoaiMay();

            }
            catch { }
        }
        private void load_listKhoaPhong()
        {
            try
            {
               DataTable dt = _MySQLCMay.get_DanhMuc("dmkhoaphong","1").Tables[0];
                treeView_KhoaPhong.setDataSource(dt,"Ten","ID","DANH SACH KHOA PHONG",true,"DANH SACH KHOA PHONG");
                
            }
            catch { }
        }
        private void load_listLoaiMay()
        {
        try
            {
               DataTable dt = _MySQLCMay.get_DanhMuc("dmloaimay","1").Tables[0];
                treeView_loaimay.setDataSource(dt,"Ten","ID","DANH SACH KHOA PHONG",true,"DANH SACH KHOA PHONG");
                
            }
            catch { }
        }
    }
}