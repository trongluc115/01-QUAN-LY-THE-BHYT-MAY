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
    public partial class frmBaoCaoNgoaiTruBHYT : Form
    {
        DataSet _ds;
        DataSet _dsChuaDuyet;
        public frmBaoCaoNgoaiTruBHYT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccessData d = new AccessData();
            CThanhToanBHYTDAO data = new CThanhToanBHYTDAO();
            string tungay = haison1.tungay;
            string denngay = haison1.denngay;
            DataSet ds = data.get_BaoCaoBHYT(tungay,denngay);
            frmReport a = new frmReport(d, ds.Tables[0], "rptDuyetbhytmau25.rpt", "TỪ NGÀY " + haison1.tungay + " ĐẾN NGÀY " + haison1.denngay, "", "", "", "", "", "", "", "", "");
            a.Show();
        }

        private void btLload_Click(object sender, EventArgs e)
        {

            f_LoadDSDaDuyet();
        }
        private void f_LoadDSDaDuyet()
        {
            CThanhToanBHYTDAO data = new CThanhToanBHYTDAO();

            string tungay = haison1.tungay;
            string denngay = haison1.denngay;
            _ds = data.get_BaoCaoBHYT(tungay, denngay);
            f_locdanhsachdaduyet(txtTimkiem.Text);
        }
        private void f_locdanhsachdaduyet(string value)
        {
            try
            {
                DataView dv = new DataView(_ds.Tables[0]);
                dv.RowFilter = "MaBN like '%" + value + "%'";
                DSDaDuyet.DataSource = dv;
                lbSoLuong.Text = DSDaDuyet.Rows.Count.ToString();
            }
            catch { }
        }
        private void f_locdanhsachchuaduyet(string value)
        {
            try
            {
                DataView dv = new DataView(_dsChuaDuyet.Tables[0]);
                dv.RowFilter = "MaBN like '%" + value + "%'";
                dsChuaDuyet.DataSource = dv;
                lbSoLuongChuaDuyet.Text = dsChuaDuyet.Rows.Count.ToString();
            }
            catch { }
        }

        private void dsChuaDuyet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = DSDaDuyet.SelectedRows[0];
                string MaBN=row.Cells["MaBN"].Value.ToString();
                string Hoten=row.Cells["HoTen"].Value.ToString();
                string NamSinh=row.Cells["NamSinh"].Value.ToString();
                string DiaChi=row.Cells["DiaChi"].Value.ToString();
                int phai=int.Parse(row.Cells["GIOITINH"].Value.ToString());
                string MaBHYT=row.Cells["SOTHEBHYT"].Value.ToString();
                string MaBV=row.Cells["MaBV"].Value.ToString();
                string TenBV=row.Cells["TenBV"].Value.ToString();
                int TraiTuyen=int.Parse(row.Cells["TraiTuyen"].Value.ToString());
                string IDDuyet=row.Cells["ID"].Value.ToString();
                setThongTin(MaBN, Hoten, NamSinh, DiaChi, phai, MaBHYT, MaBV, TenBV, TraiTuyen, IDDuyet);
            }
            catch { }
        }

        private void DSDaDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow row = DSDaDuyet.SelectedRows[0];
                string MaBN = row.Cells["MaBN"].Value.ToString();
                string Hoten = row.Cells["HoTen"].Value.ToString();
                string NamSinh = row.Cells["NamSinh"].Value.ToString();
                string DiaChi = "";
                int phai = int.Parse(row.Cells["GIOITINH"].Value.ToString());
                string MaBHYT = row.Cells["SOTHEBHYT"].Value.ToString();
                string MaBV = row.Cells["MaBV"].Value.ToString();
                string TenBV = row.Cells["TenBV"].Value.ToString();
                int TraiTuyen = int.Parse(row.Cells["TraiTuyen"].Value.ToString());
                string IDDuyet = row.Cells["ID"].Value.ToString();
                setThongTin(MaBN, Hoten, NamSinh, DiaChi, phai, MaBHYT, MaBV, TenBV, TraiTuyen, IDDuyet);
            }
            catch { }

        }
        private void setThongTin(string MaBN,string Hoten,string NamSinh,string DiaChi,int phai,string MaBHYT,string MaBV,string TenBV,int TraiTuyen,string IDDuyet)
        {
            txtMaBN.Text = MaBN;
            txtHoTen.Text = Hoten;
           
            txtNamSinh.Text = NamSinh;         
            f_setcbPhai(phai);

            txtMaBHYT.Text =MaBHYT;
          
            txtTenBV.Text = TenBV;
            txtIDDuyet.Text = IDDuyet;
         
          
            f_setTraiTuyen(TraiTuyen);
        }
        private void f_setcbPhai(int value)
        {
            try
            {
                cbPhai.SelectedIndex = value;
            }
            catch { cbPhai.SelectedIndex = 0; }
        }
        private void f_setTraiTuyen(int value)
        {
            try
            {
                cbTuyen.SelectedIndex = value;
            }
            catch { cbTuyen.SelectedIndex = 0; }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            CThanhToanBHYTDAO data = new CThanhToanBHYTDAO();
            data.Delete(txtIDDuyet.Text);
            f_LoadDSDaDuyet();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            f_locdanhsachdaduyet(txtTimkiem.Text);
        }

        private void frmBaoCaoNgoaiTruBHYT_Load(object sender, EventArgs e)
        {
            _ds = new DataSet();
            _dsChuaDuyet = new DataSet();
        }

        private void btLoadDSChuaDuyet_Click(object sender, EventArgs e)
        {
            string tungay = haison1.tungay;
            string denngay = haison1.denngay;
            CThanhToanBHYTOracle d = new CThanhToanBHYTOracle();
            CThanhToanBHYTDAO data = new CThanhToanBHYTDAO();
                       

            if (chThuoc.Checked == true)
            {
                string schuaduyet = data.get_IDTTRVdaduyet(tungay, denngay);
                _dsChuaDuyet = d.f_loadDanhSachChuaDuyet(tungay, denngay, schuaduyet);
            }
            else
            {
                string smabndaduyet = data.get_MAVAOVIENdaduyet(tungay, denngay);
                _dsChuaDuyet = d.f_loadDanhSachCLSChuaDuyet(tungay, denngay, smabndaduyet);
            }
            f_locdanhsachchuaduyet(txtTimKiemChuaDuyet.Text);
        }

        private void txtTimKiemChuaDuyet_TextChanged(object sender, EventArgs e)
        {
            f_locdanhsachchuaduyet(txtTimKiemChuaDuyet.Text);
        }

        private void haison1_Load(object sender, EventArgs e)
        {

        }

        private void cmdKiemTraTrung_Click(object sender, EventArgs e)
        {
            try
            {
                CThanhToanBHYTDAO data = new CThanhToanBHYTDAO();

                string tungay = haison1.tungay;
                string denngay = haison1.denngay;
                _ds = data.get_trungmau01bv(tungay, denngay);
                dsChuaDuyet.DataSource = _ds.Tables[0];
            }
            catch { }
        }
    }
}