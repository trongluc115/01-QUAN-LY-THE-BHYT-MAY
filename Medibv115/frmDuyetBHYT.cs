using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using Data;
using DataOracle;

namespace MediIT115
{
    public partial class frmDuyetBHYT : Form
    {
        CBenhNhan BN;
        CThanhToanBHYT BHYT;
        public frmDuyetBHYT()
        {
            InitializeComponent();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CThanhToanBHYTDAO d = new CThanhToanBHYTDAO();
            string IDDuyet = d.KiemTraDaDuyet(txtMaBN.Text, txtIDThanhToan.Text, calendar.SelectionStart);
            if (IDDuyet.Length ==0)
            {
                if (txtThuoc.Text.CompareTo("0") != 0 && txtSoPhieu.Text.CompareTo("0") == 0)
                {
                    MessageBox.Show("Bệnh nhân có thuốc, đề nghị chọn option 'Duyệt mẫu có thuốc'");

                }
                else
                {
                    if (KiemTraMaThe(txtMaBHYT.Text.Substring(0, 2))&& txtMaBHYT.Text.Length==20)
                    {
                        f_Luu();
                    }
                    else
                    {
                        MessageBox.Show("Sai mã thẻ!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Bệnh án BHYT đã duyệt, ID Duyệt: "+IDDuyet);
            }
                btMoi.Select();
            
            
        }
        private bool KiemTraMaThe(string Ma)
        {
            CXml cxml = new CXml();
            string sformat = cxml.ReadXML(1, "config.xml");
            if (sformat.IndexOf(Ma) >= 0)
                return true;
            else
                return false;
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
        private void init_form()
        {
            try
            {
                txtMaBN.Text = "";
                txtHoTen.Text = "";
                txtDiaChi.Text = "";
                txtICD.Text = "";
                txtChanDoan.Text = "";
                txtMaQL.Text = "";
                f_setcbPhai(0);

                txtMaBHYT.Text = "";
                txtMaBV.Text = "";
                txtTenBV.Text = "";
                dHSD.Value = DateTime.Today;
                txtloaiba.Text = "";
                f_setTraiTuyen(0);
                txtSoBienLai.Text = "";

                txtSoPhieu.Text = "";
                txtMaVaoVien.Text = "";
                txtIDThanhToan.Text = "";
                txtThuoc.Text = "0";
                txtMau.Text = "0";
                txtXetNghiem.Text = "0";
                txtCDHA.Text = "0";

                txtDVKTThuong.Text = "0";
                txtDVKTCao.Text = "0";
                txtVTYT.Text = "0";
                txtKhamBenh.Text = "0";
                txtGiuong.Text = "0";
                txtCPVC.Text = "0";
                txtKhac.Text = "0";
                txtThamDoCN.Text = "0";
                txtTongCong.Text = "0";
                txtBHYTTra.Text = "0";
                txtBNTra.Text = "0";
                txtNamSinh.Text = "";
                
                DataTable a = new DataTable();
                dview.DataSource = a;
                dViewThuoc.DataSource = a;
            }
            catch { }
        }
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            loadBHYT();
            
        }
        private void loadBHYT()
        {
            try
            {

                CBenhNhanOracle BNOracle = new CBenhNhanOracle();
                BN = BNOracle.getBenhNhan(txtMaBN.Text);
                BN.HoTen = s_prepair(BN.HoTen);
                BN.DiaChi = s_prepair(BN.DiaChi);
                txtNamSinh.Text = BN.NamSinh.ToString();
                txtHoTen.Text =BN.HoTen;
                txtDiaChi.Text = BN.DiaChi;
                f_setcbPhai(BN.GioiTinh);

                CThanhToanBHYTOracle dataoracle = new CThanhToanBHYTOracle();
                if (ckKhacNgay.Checked == false)
                {
                    dview.DataSource = dataoracle.f_loadVienPhiChiTiet(txtMaBN.Text, calendar.SelectionStart, ckDuyetBHYT.Checked).Tables[0];
                    dViewThuoc.DataSource = dataoracle.f_loadThuocChiTiet(txtMaBN.Text, calendar.SelectionStart, ckDuyetBHYT.Checked).Tables[0];
                }
                else
                {
                    dview.DataSource = dataoracle.f_loadVienPhiChiTietKhacNgay(txtMaBN.Text, calendar.SelectionStart, ckDuyetBHYT.Checked).Tables[0];
                    dViewThuoc.DataSource = dataoracle.f_loadThuocChiTietKhacNgay(txtMaBN.Text, calendar.SelectionStart, ckDuyetBHYT.Checked).Tables[0];
                }

                        


                CThanhToanBHYT bhyt = new CThanhToanBHYT();
                if (ckLoaiDuyet.Checked == true)
                    bhyt = dataoracle.f_loadTT_BHYT(txtMaBN.Text, calendar.SelectionStart);
                else
                    bhyt = dataoracle.f_loadTT_BHYT_CLS(txtMaBN.Text, calendar.SelectionStart);
                txtMaBHYT.Text = bhyt.SoTheBHYT;
                txtMaBV.Text = bhyt.MaBV;
                txtTenBV.Text =s_prepair( bhyt.NoiDangKyBHYT);
                dHSD.Value = bhyt.HSD;
                txtloaiba.Text = bhyt.LoaiBA;
                f_setTraiTuyen(bhyt.TraiTuyen);
                txtSoBienLai.Text = bhyt.SoBienLai;
                if (ckLoaiDuyet.Checked == true)
                    bhyt = dataoracle.f_loadSoPhieu_IDTTRV(txtMaBN.Text, calendar.SelectionStart);
                else
                    bhyt = dataoracle.f_loadSoPhieu_IDTTRV_CLS(txtMaBN.Text, calendar.SelectionStart);
                txtSoPhieu.Text = bhyt.SoPhieu;
                txtMaVaoVien.Text = bhyt.MaVaoVien.ToString();
                txtIDThanhToan.Text = bhyt.IDTTRV.ToString();
                txtMaQL.Text = bhyt.MaQuanLy;
                if (ckKhacNgay.Checked == false)
                {
                    txtICD.Text = dataoracle.f_loadICDFull(txtMaBN.Text, calendar.SelectionStart);
                    txtChanDoan.Text = dataoracle.f_loadChanDoanFull(txtMaBN.Text, calendar.SelectionStart);
                }
                else
                {
                    txtICD.Text = dataoracle.f_loadICD(txtMaQL.Text, calendar.SelectionStart);
                    txtChanDoan.Text = s_prepair( dataoracle.f_loadChanDoan(txtMaQL.Text, calendar.SelectionStart));
                }
               
                f_loadChiTiet();
                f_loadTongCong();
                btLuu.Select();
                txtMaBN.Enabled = false;
            }
            catch { }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaBN_TextChanged(object sender, EventArgs e)
        {

        }
        private long f_getvalue_Long(string value)
        {
            long result = 0;
            try
            {
                result = (long)(float.Parse(value));
            }
            catch
            {       
            }
            return result;
        }
        private void f_loadChiTiet()
        {
            try
            {
                long[] Value = new long[15];
                for (int i = 0; i < 15; i++)
                {
                    Value[i] = 0;
                }
                foreach (DataGridViewRow row in dview.Rows)
                {
                    int pos=int.Parse(row.Cells["IDNhomBHYT"].Value.ToString());
                    Value[pos] += long.Parse(row.Cells["SoTien"].Value.ToString());
                }
                float Thuoc = 0;
                foreach (DataGridViewRow row in dViewThuoc.Rows)
                {
                    Thuoc+= float.Parse(row.Cells["SoTien"].Value.ToString());
                   
                }
                double v = Math.Round(Thuoc,0, MidpointRounding.AwayFromZero);
                txtThuoc.Text =f_insertDot(v.ToString());
                txtMau.Text = f_insertDot(Value[2].ToString());
                txtXetNghiem.Text  =f_insertDot( Value[3].ToString());
                txtCDHA.Text = f_insertDot(Value[4].ToString());

                txtDVKTThuong.Text = f_insertDot(Value[5].ToString());
                txtDVKTCao.Text = f_insertDot(Value[6].ToString());
                txtVTYT.Text = f_insertDot(Value[7].ToString());
                txtKhamBenh.Text = f_insertDot(Value[8].ToString());
                txtGiuong.Text = f_insertDot(Value[9].ToString());
                txtCPVC.Text = f_insertDot(Value[10].ToString());
                txtKhac.Text = f_insertDot(Value[11].ToString());
                txtThamDoCN.Text = f_insertDot(Value[12].ToString());

            }
            catch { }
        
        }
        private void f_loadTongCong()
        {
            try
            {
                long TongCP=0;
                long BHYTTra=0;
                long BNTra=0;
                foreach (DataGridViewRow row in dview.Rows)
                {
                    TongCP += long.Parse(row.Cells["SoTien"].Value.ToString());
                    BHYTTra += long.Parse(row.Cells["BHYTTra"].Value.ToString());
                }
                float fTongThuoc = 0;
                float fBHYTtra = 0;
                foreach (DataGridViewRow row in dViewThuoc.Rows)
                {
                    fTongThuoc += float.Parse(row.Cells["SoTien"].Value.ToString());
                    fBHYTtra += float.Parse(row.Cells["BHYTTra"].Value.ToString());
                }
                TongCP += (long)Math.Round(fTongThuoc,0,MidpointRounding.AwayFromZero);
                BHYTTra += (long)Math.Round(fBHYTtra, 0, MidpointRounding.AwayFromZero);
                BNTra = TongCP - BHYTTra;
                txtTongCong.Text = f_insertDot(TongCP.ToString());
                txtBHYTTra.Text = f_insertDot(BHYTTra.ToString());
                txtBNTra.Text = f_insertDot(BNTra.ToString());

            }
            catch { }

        }

        private void frmDuyetBHYT_Load(object sender, EventArgs e)
        {
            BN = new CBenhNhan();
            BHYT = new CThanhToanBHYT();
        }
        private long getLong(string s)
        {
            try
            {
                return long.Parse(s);
            }
            catch { }
            return 0;
        }
        private string s_prepair(string s)
        {
            string kq = "";
            kq = s.Replace("'", " ");
            return kq;
        }
        private void f_Luu()
        {
            try
            {
                
                BHYT.MaBN = txtMaBN.Text;
                BHYT.MaBV = txtMaBV.Text;
                
                BHYT.ICD = txtICD.Text;
                BHYT.ChanDoan = txtChanDoan.Text;
                BHYT.NoiDangKyBHYT =  txtTenBV.Text;
                BHYT.NgayLamViec = calendar.SelectionStart;
                BHYT.SoTheBHYT = txtMaBHYT.Text;
                BHYT.SoPhieu = txtSoPhieu.Text;
                BHYT.MaVaoVien = getLong(txtMaVaoVien.Text);
                BHYT.IDTTRV = getLong(txtIDThanhToan.Text);
                BHYT.TraiTuyen = cbTuyen.SelectedIndex;
                BHYT.TongTien = getLong(f_clearDot(txtTongCong.Text));
                BHYT.BHYTTra = getLong(f_clearDot(txtBHYTTra.Text));
                BHYT.BNTra = getLong(f_clearDot(txtBNTra.Text));
                BHYT.TienKham = getLong(f_clearDot(txtKhamBenh.Text));
                BHYT.Thuoc = getLong(f_clearDot(txtThuoc.Text));
                BHYT.Mau = getLong(f_clearDot(txtMau.Text));
                BHYT.XetNghiem = getLong(f_clearDot(txtXetNghiem.Text));
                BHYT.CDHA = getLong(f_clearDot(txtCDHA.Text));
                BHYT.DVKTthongthuong = getLong(f_clearDot(txtDVKTThuong.Text));
                BHYT.DVKTcao = getLong(f_clearDot(txtDVKTCao.Text));
                BHYT.VTYT = getLong(f_clearDot(txtVTYT.Text));
                BHYT.ChiPhiVC = getLong(f_clearDot(txtCPVC.Text));
                BHYT.ThamDoChucNang = getLong(f_clearDot(txtThamDoCN.Text));
                BHYT.Khac = getLong(f_clearDot(txtKhac.Text));
                BHYT.Giuong = getLong(f_clearDot(txtGiuong.Text));
                BHYT.LoaiThe = BHYT.SoTheBHYT.Substring(0, 2);
                BHYT.MaNhomThe = f_MaNhomThe(BHYT.MaBV, BHYT.TraiTuyen).ToString(); 
                BHYT.NhomThe = f_NhomThe(BHYT.MaNhomThe);
                BHYT.NgayLamViec = calendar.SelectionStart;
                BHYT.HSD = dHSD.Value;
                BHYT.LoaiBA = txtloaiba.Text;
                BHYT.SoBienLai = txtSoBienLai.Text;
                CBenhNhanDAO BenhNhanDAO = new CBenhNhanDAO();
                BenhNhanDAO.Insert(BN);
                CThanhToanBHYTDAO ttBHYTDAO = new CThanhToanBHYTDAO();
                int roweffect=ttBHYTDAO.Insert(BHYT);
                if (roweffect == 0)
                {
                    MessageBox.Show("Không kết nối được SQL Server!");
                    lbThongBao.Text = "Thao tác duyệt thất bại";
                }
                else
                {
                    lbThongBao.Text = "Thao tác duyệt thành công";
                }

            }
            catch { }
        }
        private int f_MaNhomThe(string MaBV, int traituyen)
        {
            int nhomthe = 0;
            try
            {
                if (MaBV.CompareTo("79024") == 0)
                {
                    nhomthe = 1;    
                }
                else
                {
                    if (traituyen == 0)
                    {
                        nhomthe = 2;
                    }
                    else
                    {
                        nhomthe = 3;
                    }
                }
            }
            catch { }
            return nhomthe;
        }
        private string f_NhomThe(string MaNhomThe)
        {
            string s = "";
            switch (MaNhomThe)
            { 
                case "1":
                    s = "A. Bệnh Nhân Đăng Ký KCB Ban Đầu Tại Cơ Sở KCB.";
                    break;
                case "2":
                    s = "B. Bệnh Nhân Không Đăng Ký KCB Ban Đầu Tại Cơ Sở KCB. B1. Bệnh Nhân Nội Tỉnh (Thẻ Do BHXH TP.HCM Phát Hành )";
                    break;
                case "3":
                    s = "C. Bệnh Nhân Trái Tuyến Có Trình Thẻ. C1.Bệnh Nhân Ngoại Tỉnh.";
                    break;
            }
            return s;
        }

        private void btMoi_Click(object sender, EventArgs e)
        {
            txtMaBN.Enabled = true;
            lbThongBao.Text = "...";
            init_form();
            txtMaBN.Select();
        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");

                }
            }
            catch { }
            
        }
        private string f_clearDot(string s)
        {
            return s.Replace(".", "");
        }
        private string f_insertDot(string s)
        {
            string result = "";
            string temp = s;
            string num="";
            for (int i = 1; i <= temp.Length; i++)
            {
                num = temp.Substring(temp.Length - i, 1);
                if (i % 3 == 0)
                {
                    result = "." + num + result;
                }
                else
                {
                    result = num + result;
                }
            }
            return result.TrimStart('.');
        }

      
        private void TextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox textbox = (TextBox)sender;
                textbox.Text = f_insertDot(textbox.Text);
                f_loadTongCongSua();
            }
            catch { }
        }
        private void TextBox_Setfocus(object sender, EventArgs e)
        {
            try
            {
                TextBox textbox = (TextBox)sender;
                textbox.Text = f_clearDot(textbox.Text);
            }
            catch { }
        }
        private void f_loadTongCongSua()
        {
            long TONG = 0;
            try
            {
                TONG+= getLong(f_clearDot(txtKhamBenh.Text));
                TONG += getLong(f_clearDot(txtThuoc.Text));
                TONG += getLong(f_clearDot(txtMau.Text));
                TONG += getLong(f_clearDot(txtXetNghiem.Text));
                TONG += getLong(f_clearDot(txtCDHA.Text));
                TONG += getLong(f_clearDot(txtDVKTThuong.Text));
                TONG += getLong(f_clearDot(txtDVKTCao.Text));
                TONG += getLong(f_clearDot(txtVTYT.Text));
                TONG += getLong(f_clearDot(txtCPVC.Text));
                TONG += getLong(f_clearDot(txtThamDoCN.Text));
                TONG += getLong(f_clearDot(txtKhac.Text));
                TONG += getLong(f_clearDot(txtGiuong.Text));
                txtTongCong.Text = f_insertDot(TONG.ToString());
                long BNTra=TONG-long.Parse(f_clearDot(txtBHYTTra.Text));
                txtBNTra.Text=f_insertDot(BNTra.ToString());
            }
            catch { }
            
        }

        private void ckDuyetBHYT_CheckedChanged(object sender, EventArgs e)
        {
            loadBHYT();
        }

        private void ckKhacNgay_CheckedChanged(object sender, EventArgs e)
        {
            loadBHYT();
        }

        private void ckLoaiDuyet_CheckedChanged(object sender, EventArgs e)
        {
            loadBHYT();
        }

        private void btCong_Click(object sender, EventArgs e)
        {
            DichChuyenSo(1);
        }
        private void DichChuyenSo(int num)
        {
            try
            {
                int BHYTtra = int.Parse(f_clearDot(txtBHYTTra.Text)) + num;
                int BNtra = int.Parse(f_clearDot(txtTongCong.Text)) - BHYTtra;
                txtBHYTTra.Text = f_insertDot(BHYTtra.ToString());
                txtBNTra.Text = f_insertDot(BNtra.ToString());
            }
            catch { }
        }

        private void btTru_Click(object sender, EventArgs e)
        {
            DichChuyenSo(-1);
        }

      

    }
}