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
using DataMySQL;
using LibBaocao;
using System.Net.NetworkInformation;
using System.Globalization;
namespace MediIT115
{
    public partial class frmQLThietBi : Form
    {
        
        CBenhNhan BN;
        CThanhToanBHYT BHYT;
        CMay _MySQLCMay;
        bool flagIsNew = false;
        public frmQLThietBi()
        {
            InitializeComponent();
            
           
        }
        private void frmQLThietBi_Load(object sender, EventArgs e)
        {
            _MySQLCMay = new CMay();
            f_LoadListCauHinh();
            f_LoadListQuocGia();
            f_LoadListKhoaPhong();
            f_LoadListCauHinhMau();
            f_LoadListLoai();
            f_LoadListLoaiChiTiet();
            f_LoadDanhSachMay();
                        f_LoadListSKhoaPhong();
            f_LoadListTinhTrang();
            init_form();
            controlToolStrip("cancel");
            panel1.Enabled = false;
        }
        #region LoadCombobox
        private void f_LoadListCauHinh()
        {
            try {
                cbChiTietCauHinh.DataSource = _MySQLCMay.get_DanhMuc("dmthietbi","1").Tables[0];
                cbChiTietCauHinh.DisplayMember = "Ten";
                cbChiTietCauHinh.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListQuocGia()
        {
            try
            {
                cbNuocSX.DataSource = _MySQLCMay.get_DanhMuc("dmquocgia","1").Tables[0];
                cbNuocSX.DisplayMember = "Ten";
                cbNuocSX.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListLoai()
        {
            try
            {
                cbLoai.DataSource = _MySQLCMay.get_DanhMuc("dmLoaiMay","1").Tables[0];
                cbLoai.DisplayMember = "Ten";
                cbLoai.ValueMember = "ID";
            }
            catch { }
        }
      
        private void f_LoadListLoaiChiTiet()
        {
            try
            {
                string loaimay=cbLoai.SelectedValue.ToString();
                cbLoaiChiTiet.DataSource = _MySQLCMay.get_DanhMucCT("dmLoaiChiTiet",loaimay).Tables[0];
                cbLoaiChiTiet.DisplayMember = "Ten";
                cbLoaiChiTiet.ValueMember = "ID";
            }
            catch {
                
                cbLoaiChiTiet.DataSource = _MySQLCMay.get_DanhMucCT("dmLoaiChiTiet", "1").Tables[0];
                cbLoaiChiTiet.DisplayMember = "Ten";
                cbLoaiChiTiet.ValueMember = "ID";
            }
        }
        private void f_LoadListKhoaPhong()
        {
            try
            {
                cbKhoaPhong.DataSource = _MySQLCMay.get_DanhMuc("dmkhoaphong","1").Tables[0];
                cbKhoaPhong.DisplayMember = "Ten";
                cbKhoaPhong.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListSKhoaPhong()
        {
            try
            {
                cbSKhoaPhong.DataSource = _MySQLCMay.get_DanhMuc("dmkhoaphong","1").Tables[0];
                cbSKhoaPhong.DisplayMember = "Ten";
                cbSKhoaPhong.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListCauHinhMau()
        {
            try
            {
                cbCauHinhMau.DataSource = _MySQLCMay.get_DanhMuc("cauhinhmaull").Tables[0];
                cbCauHinhMau.DisplayMember = "Ten";
                cbCauHinhMau.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListTinhTrang()
        {
            try
            {
                cbTinhTrang.DataSource = _MySQLCMay.get_DanhMuc("dmtinhtrang","1").Tables[0];
                cbTinhTrang.DisplayMember = "Ten";
                cbTinhTrang.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadDanhSachMay()
        {
            try {
                dview.Rows.Clear();
                DataTable dt = _MySQLCMay.get_DSMay().Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dview.Rows.Add(dr.ItemArray);
                }
            }
            catch { }
        }
        #endregion
        private void init_form()
        {
            try
            {
                txtMaBN.Text = "";
                txtTen.Text = "";
                txtMaMay.Text = "";
                txtViTri.Text = "";

                txtDonGia.Text = "1";
                dCauHinh.Rows.Clear();
              
               
                
                
               
               
            }
            catch { }
        }
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
           
            
        }

        private void setList(ComboBox cb, string value)
        {
            try
            {
                DataTable dt = (DataTable)cb.DataSource;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ID"].ToString() == value)
                    {
                        
                        cb.SelectedIndex = i;
                        return;
                    }
                    i++;
                }
            }
            catch { }   
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

        

        private void controlToolStrip(string code)
        {
            switch (code)
            {
                case "new":
                    toolDelete.Enabled = false;
                    toolEdit.Enabled = false;
              
                    toolCancel.Enabled = true;
                    toolSave.Enabled = true;
                    panel1.Enabled = true;
                    toolNew.Enabled = false;
                    flagIsNew = true;
                    
                    break;
                case "edit":
                    toolDelete.Enabled = false;
                    toolNew.Enabled = false;
                    toolSave.Enabled = true;
                   
                    toolCancel.Enabled = true;
                    toolEdit.Enabled = false;
                    panel1.Enabled = true;
                    flagIsNew = false;
                    break;
                case "cancel":
                    toolDelete.Enabled = true;
                    toolNew.Enabled = true;
                   
                    toolCancel.Enabled = false;
                    toolEdit.Enabled = true;
                    toolSave.Enabled = false;
                    panel1.Enabled = false;
                    break;
                case "save":
                    toolDelete.Enabled = true;
                    toolNew.Enabled = true;
                  
                    toolCancel.Enabled = false;
                    toolEdit.Enabled = true;
                    toolSave.Enabled = false;
                    panel1.Enabled = false;
                    f_LoadDanhSachMay();
                    break;

            }

        }

        private void btThemChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoiDungCauHinh.Text = _MySQLCMay.getID("dmthietbi");
                dCauHinh.Rows.Add(cbChiTietCauHinh.SelectedValue.ToString(), cbChiTietCauHinh.Text,txtNoiDungCauHinh.Text);
            }
            catch { }
        }
        private string f_getID()
        {
            string ID = "";
            try {
                DateTime value = DateTime.Now;
                ID = value.ToOADate().ToString();
            }
            catch { }
            return ID;
        }

        private void btThemMau_Click(object sender, EventArgs e)
        {
            try {
                string ID = _MySQLCMay.getID("cauhinhmaull");
                _MySQLCMay.InsertCauHinhMaull(ID, txtTenCauHinhMau.Text);
                foreach (DataGridViewRow dr in dCauHinh.Rows)
                {
                    _MySQLCMay.InsertCauHinhMauct(ID, dr.Index.ToString(), dr.Cells[0].Value.ToString(), dr.Cells[2].Value.ToString());
                }
                f_LoadListCauHinhMau();
                txtTenCauHinhMau.Text = "";
            }
            catch { }
        }

        private void btDanhMucCauHinh_Click(object sender, EventArgs e)
        {
            f_nhapDanhMuc ("dmthietbi", "DANH MỤC CẤU HÌNH CHI TIẾT");
            f_LoadListCauHinh();
            
        }

        private void btNuocSX_Click(object sender, EventArgs e)
        {
            f_nhapDanhMuc("dmquocgia", "DANH MỤC NƯỚC SẢN XUẤT");
            f_LoadListQuocGia();
        }

        private void btLoaiMay_Click(object sender, EventArgs e)
        {
            f_nhapDanhMuc("dmloaimay", "DANH MỤC LOẠI MÁY");
            f_LoadListLoai();
        }
        private void f_nhapDanhMuc(string tablename, string Title)
        {
            frmDanhMuc f = new frmDanhMuc(tablename, Title);
            f.Show();
        }
        private void f_nhapDanhMucCT(string tablename, string Title)
        {
            frmDanhMucCT f = new frmDanhMucCT(tablename, Title,"");
            f.Show();
        }


        private void btLoaiChiTiet_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucCT("dmloaichitiet", "DANH MỤC LOẠI CHI TIẾT");
            f_LoadListLoaiChiTiet();
        }

        private void btKhoaPhong_Click(object sender, EventArgs e)
        {
            f_nhapDanhMuc("dmkhoaphong", "DANH MỤC KHOA PHÒNG");
            f_LoadListKhoaPhong();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            f_save();
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            f_new();
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
            controlToolStrip("edit");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmBaoTri fm = new frmBaoTri("baotri","PHIẾU THEO DÕI BẢO TRÌ",txtMaBN.Text);
            fm.Show();
        }

        private void dview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (toolSave.Enabled == false)
                {
                    int row = dview.CurrentCell.RowIndex;
                    txtMaMay.Text = dview.Rows[row].Cells[1].Value.ToString();
                    txtMaBN.Text = dview.Rows[row].Cells["ID"].Value.ToString();
                    f_setThongTin();
                }
            }
            catch { }
        }

        private void btTinhTrang_Click(object sender, EventArgs e)
        {
            f_nhapDanhMuc("dmtinhtrang", "DANH MỤC TÌNH TRẠNG");
        }

        private void txtMaMay_Leave(object sender, EventArgs e)
        {
            try {
                
                
               // f_setThongTin();
            }
            catch { }
        }
        private void f_setThongTin()
        {
            try {
                DataTable dt = _MySQLCMay.get_May(txtMaBN.Text).Tables[0];
                if (dt.Rows.Count > 1)
                    MessageBox.Show("Trùng barcode!");
                txtMaBN.Text = dt.Rows[0]["ID"].ToString();
                txtTen.Text = dt.Rows[0]["ten"].ToString();
                setList(cbNuocSX,dt.Rows[0]["nuocsx"].ToString());
                setList(cbLoai, dt.Rows[0]["maloai"].ToString());
                setList(cbLoaiChiTiet, dt.Rows[0]["loaichitiet"].ToString());
                setList(cbKhoaPhong, dt.Rows[0]["makp"].ToString());
                txtViTri.Text = dt.Rows[0]["vitri"].ToString();
                txtDonGia.Text = dt.Rows[0]["dongia"].ToString();
                setList(cbTinhTrang, dt.Rows[0]["tinhtrang"].ToString());
                
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    string dateString = dt.Rows[0]["ngaybdsd"].ToString().Substring(0,10);
                    string format = "MM/dd/yyyy";
                    

                    dateNgayBDSD.Value = DateTime.ParseExact(dateString, format, provider);
                }
                catch { }
                f_loadcauhinh();
            }
            catch { }
        }
        private void f_loadcauhinh()
        {
            try
            {
                DataTable dt = _MySQLCMay.get_cauhinh(txtMaBN.Text).Tables[0];
                dCauHinh.Rows.Clear();
                foreach (DataRow dr in dt.Rows) 
                {
                    dCauHinh.Rows.Add(dr.ItemArray);
                }
            }
            catch { }
        }
        private void f_loadcauhinhmau()
        {
            try
            {
                DataTable dt = _MySQLCMay.get_cauhinhmau(cbCauHinhMau.SelectedValue.ToString()).Tables[0];
                dCauHinh.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    dCauHinh.Rows.Add(dr.ItemArray);
                }
            }
            catch { }
        }
        private void f_savecauhinh()
        {
            try
            {
                int i=0;
                _MySQLCMay.DeleteDanhMuc("cauhinh", txtMaBN.Text);
                foreach (DataGridViewRow dr in dCauHinh.Rows)
                {
                    _MySQLCMay.InsertCauHinh(txtMaBN.Text, i.ToString(), dr.Cells[0].Value.ToString(), dr.Cells[2].Value.ToString());
                    i++;
                }
            }
            catch { }
        }

        private void btChonMau_Click(object sender, EventArgs e)
        {
            f_loadcauhinhmau();
        }

        private void cbSKhoaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
              
            }
            catch { }
        }
        private void f_LoadDanhSachMay(string timkiem,string khoaphong)
        {
            try
            {
                dview.Rows.Clear();
                
                DataTable dt;
                if (khoaphong != "-1")
                {
                    dt = _MySQLCMay.get_DSMay(timkiem, khoaphong).Tables[0];
                    f_LoadThongTin(timkiem, khoaphong);
                }
                else
                    dt = _MySQLCMay.get_DSMay(timkiem).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dview.Rows.Add(dr.ItemArray);
                }
                
            }
            catch { }
        }
        private void f_LoadDanhSachMay(string timkiem)
        {
            try
            {
                dview.Rows.Clear();

                DataTable  dt = _MySQLCMay.get_DSMay(timkiem).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dview.Rows.Add(dr.ItemArray);
                }

            }
            catch { }
        }
        private void f_LoadThongTin(string timkiem, string khoaphong)
        {
            try
            {
                
                lbThongTin.Text = _MySQLCMay.get_DSMayThongTin(timkiem, khoaphong);
               
            }
            catch { }
        }
        private void btRefesh_Click(object sender, EventArgs e)
        {
            f_LoadDanhSachMay(txttimkiem.Text, cbSKhoaPhong.SelectedValue.ToString());
            

        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                f_LoadDanhSachMay(txttimkiem.Text);
            }
            catch {
               
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try {
                //a.id,a.mamay,a.ten tenmay,b.ten ,c.ten ,a.Vitri,e.Ten ,f.Ten ,a.DonGia,g.ten,a.ngaybdsd 
                AccessData acc = new AccessData();
                string ID=txtMaBN.Text;
                DataTable dt=_MySQLCMay.get_BCBaoTri(ID).Tables[0];
                DataTable thongtin = _MySQLCMay.get_MayFromID(ID).Tables[0];
                string strMamay = thongtin.Rows[0]["mamay"].ToString();
                string strTen = thongtin.Rows[0]["tenmay"].ToString();
                string strNuocSX = thongtin.Rows[0]["nuocsx"].ToString();
                string strMaKP = thongtin.Rows[0]["khoaphong"].ToString();
                string strLoai = thongtin.Rows[0]["loaimay"].ToString();
                string strChiTiet = thongtin.Rows[0]["loaichitiet"].ToString();
                string strDongia = thongtin.Rows[0]["dongia"].ToString();
                string strVitri = thongtin.Rows[0]["vitri"].ToString();
               
                string ngaybdsd = thongtin.Rows[0]["ngaybdsd"].ToString();
                string cauhinh="";
                DataTable dtcauhinh=_MySQLCMay.get_cauhinh(ID).Tables[0];
                foreach (DataRow dr in dtcauhinh.Rows)
                {
                    cauhinh += dr[1].ToString() + " : " + dr[2].ToString() + "; ";
                }
                frmReport frm = new frmReport(acc, dtcauhinh, "rpt_PCKiemke_Item.rpt", strLoai, strMamay, strTen, strNuocSX, strMaKP, strChiTiet, strDongia, strVitri, ngaybdsd, cauhinh);
                frm.Show();
            }
            catch { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try {
                DataTable dt = _MySQLCMay.get_DSMay().Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_PCManagerList.rpt", "rpt_PCManagerList.rpt");
                frm.Show();

            }
            catch { }
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            controlToolStrip("cancel");
        }

        private void dview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btXoaMau_Click(object sender, EventArgs e)
        {
            try
            {
                _MySQLCMay.DeleteDanhMuc("cauhinhmauct", cbCauHinhMau.SelectedValue.ToString());
                _MySQLCMay.DeleteDanhMuc("cauhinhmaull", cbCauHinhMau.SelectedValue.ToString());
                f_loadcauhinhmau();
            }
            catch { }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa thiết bị?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _MySQLCMay.DeleteDanhMuc("may", txtMaBN.Text);
                _MySQLCMay.DeleteDanhMuc("baotri", txtMaBN.Text);
                _MySQLCMay.DeleteDanhMuc("cauhinh", txtMaBN.Text);
                f_LoadDanhSachMay();
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            f_LoadListLoaiChiTiet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                f_LoadListCauHinh();
                f_LoadListQuocGia();
                f_LoadListKhoaPhong();
                f_LoadListCauHinhMau();
                f_LoadListLoai();
              //  f_LoadListLoaiChiTiet();
              // f_LoadDanhSachMay();
                f_LoadListSKhoaPhong();
                f_LoadListTinhTrang();
            }
            catch { }
        }

        private void txttimkiem_Enter(object sender, EventArgs e)
        {
            txttimkiem.Text = "";
        }
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.N))
                {
                    //MessageBox.Show("New");
                    if (toolNew.Enabled == true)
                        f_new();
                }

                // Ctrl + E
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.E))
                {
                    //MessageBox.Show("Edit");
                    if (toolEdit.Enabled == true)
                        f_edit();
                }

                // Ctrl + S
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S))
                {
                    //MessageBox.Show("Save");
                    if (toolSave.Enabled == true)
                        f_save();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    //MessageBox.Show("Save");
                    if (toolCancel.Enabled == true)
                        f_cancel();
                }



            }
            catch { }
        }
        #region control

        private void f_new()
        {
            init_form();
            controlToolStrip("new");
        }
        private void f_edit()
        {
            controlToolStrip("edit");
        }
        private void f_save()
        {
            try
            {
                string id = _MySQLCMay.getID("may");
                string strMamay = txtMaMay.Text;
                string strTen = txtTen.Text;
                string strNuocSX = cbNuocSX.SelectedValue.ToString();
                string strMaKP = cbKhoaPhong.SelectedValue.ToString();
                string strLoai = cbLoai.SelectedValue.ToString();
                string strChiTiet = cbLoaiChiTiet.SelectedValue.ToString();
                string strDongia = txtDonGia.Text;
                string strVitri = txtViTri.Text;
                string tinhtrang = cbTinhTrang.SelectedValue.ToString(); ;
                DateTime NgayBDSD = dateNgayBDSD.Value;
                if (flagIsNew == true)
                {
                    //Insert

                    _MySQLCMay.InsertMay(id, strMamay, strTen, strNuocSX, strMaKP, strLoai, strChiTiet, strDongia, strVitri, tinhtrang, NgayBDSD);
                    txtMaBN.Text = id;
                    f_savecauhinh();
                }
                else
                {
                    //Update
                    id = txtMaBN.Text;
                    _MySQLCMay.UpdateMay(id, strMamay, strTen, strNuocSX, strMaKP, strLoai, strChiTiet, strDongia, strVitri, tinhtrang, NgayBDSD);
                    f_savecauhinh();
                }

            }
            catch { };
            f_LoadDanhSachMay();
            controlToolStrip("save");
        }
        private void f_cancel()
        {
            controlToolStrip("cancel");
        }
        #endregion 

        private void btnAddMac_Click(object sender, EventArgs e)
        {
            txtTen.Text =getMachineName()+ " {"+getMacAddress()+"}";
        }
        private string getMacAddress()
        {
            string sss="";
            try
            {
                

                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                PhysicalAddress address = nics[0].GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sss+=String.Format("{0}", bytes[i].ToString("X2"));

                    if (i != bytes.Length - 1)
                    {
                        sss+="-";
                    }
                }
 
            }
            catch { }

            return sss;
        }
        private string getMachineName()
        {
            string result = "";
            try
            {
                result = Environment.MachineName;
                                
            }
            catch { }

            return result;
        }

        private void btnSearchMac_Click(object sender, EventArgs e)
        {
            txttimkiem.Text = "{" + getMacAddress() + "}";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                //a.id,a.mamay,a.ten tenmay,b.ten ,c.ten ,a.Vitri,e.Ten ,f.Ten ,a.DonGia,g.ten,a.ngaybdsd 
                AccessData acc = new AccessData();
                string ID = txtMaBN.Text;
                DataTable dt = _MySQLCMay.get_BCBaoTri(ID).Tables[0];
                DataTable thongtin = _MySQLCMay.get_MayFromID(ID).Tables[0];
                string strMamay = thongtin.Rows[0]["mamay"].ToString();
                string strTen = thongtin.Rows[0]["tenmay"].ToString();
                string strNuocSX = thongtin.Rows[0]["nuocsx"].ToString();
                string strMaKP = thongtin.Rows[0]["khoaphong"].ToString();
                string strLoai = thongtin.Rows[0]["loaimay"].ToString();
                string strChiTiet = thongtin.Rows[0]["loaichitiet"].ToString();
                string strDongia = thongtin.Rows[0]["dongia"].ToString();
                string strVitri = thongtin.Rows[0]["vitri"].ToString();

                string ngaybdsd = thongtin.Rows[0]["ngaybdsd"].ToString();
                string cauhinh = "";
                DataTable dtcauhinh = _MySQLCMay.get_cauhinh(ID).Tables[0];
                foreach (DataRow dr in dtcauhinh.Rows)
                {
                    cauhinh += dr[1].ToString() + " : " + dr[2].ToString() + "; ";
                }
                frmReport frm = new frmReport(acc, dt, "rpt_PCManagerItem.rpt", strLoai, strMamay, strTen, strNuocSX, strMaKP, strChiTiet, strDongia, strVitri, ngaybdsd, cauhinh);
                frm.Show();
            }
            catch { }
        }

      

        private void dview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dview.Rows[e.RowIndex];
                string s_id = row.Cells["ID"].Value.ToString();
                string s_kiemke = row.Cells["kiemke"].Value.ToString();
                frmKiemke frm = new frmKiemke(s_id,s_kiemke);
                frm.Show();
            }
            catch { }
        }

     
    }
}