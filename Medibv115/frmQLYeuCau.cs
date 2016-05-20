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

namespace MediIT115
{
    public partial class frmQLYeuCau : Form
    {
        
        CBenhNhan BN;
        CTiepNhanYC _MySQL;
        bool flagIsNew = false;
        public frmQLYeuCau()
        {
            InitializeComponent();
            
           
        }
        private void frmQLThietBi_Load(object sender, EventArgs e)
        {
            _MySQL = new CTiepNhanYC();
           
            f_LoadListKhoaPhong();
            f_LoadListLoai();
            f_LoadListHinhThucYC();
            f_LoadListNguoiNhanYC();
            f_LoadListKhoaPhong();
            f_LoadListTinhTrang();
            f_LoadDanhSachMay(txttimkiem.Text,dTKTuNgay.Value,dTKDenNgay.Value);
            init_form();
            controlToolStrip("cancel");
            panel1.Enabled = false;
        }
        #region LoadCombobox
      
        private void f_LoadListLoai()
        {
            try
            {
                cbLoai.DataSource = _MySQL.get_DanhMuc("dmloaiyeucau","1").Tables[0];
                cbLoai.DisplayMember = "Ten";
                cbLoai.ValueMember = "ID";
            }
            catch { }
        }
      
       
        private void f_LoadListKhoaPhong()
        {
            try
            {
                cbKhoaPhong.DataSource = _MySQL.get_DanhMuc("dmkhoaphong","1").Tables[0];
                cbKhoaPhong.DisplayMember = "Ten";
                cbKhoaPhong.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListHinhThucYC()
        {
            try
            {
                cbPhuongThuc.DataSource = _MySQL.get_DanhMuc("dmhinhthucyc", "1").Tables[0];
                cbPhuongThuc.DisplayMember = "Ten";
                cbPhuongThuc.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListNguoiNhanYC()
        {
            try
            {
                cbNguoiNhanYC.DataSource = _MySQL.get_DanhMuc("dmUser", "1").Tables[0];
                cbNguoiNhanYC.DisplayMember = "Ten";
                cbNguoiNhanYC.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadListTinhTrang()
        {
            try
            {
                cbTinhTrang.DataSource = _MySQL.get_DanhMuc("dmtinhtrang","1").Tables[0];
                cbTinhTrang.DisplayMember = "Ten";
                cbTinhTrang.ValueMember = "ID";
            }
            catch { }
        }
        private void f_LoadDanhSach()
        {
            try {
                //dview.Rows.Clear();
                //DataTable dt = _MySQL.get_DSMay().Tables[0];
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dview.Rows.Add(dr.ItemArray);
                //}
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
                txtNguoiYC.Text = "";
                txtNoiDung.Text = "";
            }
            catch { }
        }
        private void txtMaBN_Leave(object sender, EventArgs e)
        {

            f_setThongTin();
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
                    
                    break;

            }

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

       

        

        private void btLoaiMay_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmloaiyeucau", "DANH MỤC LOẠI YÊU CẦU");
            f_LoadListLoai();
        }
        private void f_nhapDanhMucYC(string tablename, string Title)
        {
            frmDanhMucYC f = new frmDanhMucYC(tablename, Title);
            f.Show();
        }
       


        private void btLoaiChiTiet_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmuser", "DANH MỤC NGƯỜI NHẬN YÊU CẦU");
            f_LoadListNguoiNhanYC();
          
        }

        private void btKhoaPhong_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmkhoaphong", "DANH MỤC KHOA PHÒNG");
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
            f_edit();
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
                    txtMaBN.Text = dview.Rows[row].Cells[0].Value.ToString();
                    f_setThongTin();
                }
            }
            catch { }
        }

        private void btTinhTrang_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmtinhtrang", "DANH MỤC TÌNH TRẠNG");
        }

        private void txtMaMay_Leave(object sender, EventArgs e)
        {
            try {

                if (txtMaMay.Text.Length == 8)
                {
                    CBenhNhan item = new CBenhNhan();
                    CBenhNhanOracle dataOra = new CBenhNhanOracle();
                    item = dataOra.getBenhNhan(txtMaMay.Text);
                    txtTen.Text=item.HoTen + " ("+item.NamSinh+")";
                }
                else {
                    txtTen.Text = "";
                }
                            
               
            }
            catch { }
        }
        private void f_setThongTin()
        {
            try {
                if (txtMaBN.Text.Length > 0)
                {
                    DataTable dt = _MySQL.get_TiepNhanYCFromID(txtMaBN.Text).Tables[0];
                 
                    txtMaMay.Text = dt.Rows[0]["MaBN"].ToString();
                    txtTen.Text = dt.Rows[0]["hoten"].ToString();
                    setList(cbKhoaPhong, dt.Rows[0]["khoaphong"].ToString());

                    setList(cbLoai, dt.Rows[0]["loaiyeucau"].ToString());
                    setList(cbNguoiNhanYC, dt.Rows[0]["nguoinhanyc"].ToString());
                    setList(cbPhuongThuc, dt.Rows[0]["hinhthuc"].ToString());
                    txtNguoiYC.Text = dt.Rows[0]["nguoiyeucau"].ToString();
                    setList(cbTinhTrang, dt.Rows[0]["tinhtrang"].ToString());
                    txtNoiDung.Text = dt.Rows[0]["noidung"].ToString();
                    dNgayThucHien.Value=DateTime.Parse(dt.Rows[0]["thoigianyc"].ToString());
                }
                
            }
            catch { }
        }
       
        private void cbSKhoaPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
              
            }
            catch { }
        }
      
        private void f_LoadDanhSachMay(string timkiem,DateTime TuNgay,DateTime DenNgay)
        {
            try
            {
                dview.Rows.Clear();

                DataTable dt = _MySQL.get_DanhSach(timkiem,TuNgay,DenNgay).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dview.Rows.Add(dr.ItemArray);
                }
                lbThongTin.Text = dview.Rows.Count.ToString(); 
            }
            catch { }
        }
        private void f_LoadThongTin(string timkiem, string khoaphong)
        {
            try
            {
                
               // lbThongTin.Text = _MySQL.get_DSMayThongTin(timkiem, khoaphong);
               
            }
            catch { }
        }
       

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                f_LoadDanhSachMay(txttimkiem.Text,dTKTuNgay.Value,dTKDenNgay.Value);
            }
            catch {
               
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try {
                
                if (txtMaBN.Text.Length > 0)
                {
                    frmQLXoachidinhFromMaBN frm = new frmQLXoachidinhFromMaBN(txtMaBN.Text, dNgayThucHien.Value, "DUYỆT XÓA CHỈ ĐỊNH XÉT NGHIỆM - CLS",txtMaMay.Text);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tiếp nhận yêu cầu!");
                }
            }
            catch { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try {
                DataTable dt = _MySQL.get_DanhSach( "",dTKTuNgay.Value, dTKDenNgay.Value).Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_QuanLyYeuCau_th.rpt", "rpt_QuanLyYeuCau_th.rpt");
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

       

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _MySQL.DeleteDanhMuc("tiepnhanyc", txtMaBN.Text);
                f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
               
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                
                f_LoadListLoai();
              //  f_LoadListLoaiChiTiet();
              // f_LoadDanhSachMay();
                f_LoadListKhoaPhong();
                f_LoadListTinhTrang();
            }
            catch { }
        }

        private void txttimkiem_Enter(object sender, EventArgs e)
        {
            txttimkiem.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmhinhthucyc", "DANH MỤC HÌNH THỨC YÊU CẦU");
            f_LoadListHinhThucYC();
        }

        private void btLoadCD_Click(object sender, EventArgs e)
        {
            f_loadchidinh();
        }
        private void f_loadchidinh()
        {
            try
            {
                DataSet ds = new DataSet();

                if (txtMaMay.Text.Length == 8)
                {
                    CThanhToanBHYTOracle dataOra = new CThanhToanBHYTOracle();

                    ds = dataOra.f_loadChiDinh(txtMaMay.Text, dNgayCD.Value);

                    cbChiDinh.DataSource = ds.Tables[0];
                    cbChiDinh.DisplayMember = "Tenvp";
                    cbChiDinh.ValueMember = "ID"; ;

                }
                else
                {
                    cbChiDinh.DataSource = ds.Tables[0];
                }


            }
            catch { }
        }
        private void txtMaMay_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");

                }
                if (e.KeyCode == Keys.F7)
                {
                   //F7
                    try
                    {

                        if (txtMaBN.Text.Length > 0)
                        {
                            frmQLXoachidinhFromMaBN frm = new frmQLXoachidinhFromMaBN(txtMaBN.Text, dNgayThucHien.Value, "DUYỆT XÓA CHỈ ĐỊNH XÉT NGHIỆM - CLS", txtMaMay.Text);
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng chọn tiếp nhận yêu cầu!");
                        }
                    }
                    catch { }

                }
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.N))
                {
                    //MessageBox.Show("New");
                    if (toolNew.Enabled == true)
                        f_new();
                }

                // Ctrl + O
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.E))
                {
                    //MessageBox.Show("Open");
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
        private void btThemCD_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoiDung.Text = txtNoiDung.Text + "- Ngày "+dNgayCD.Value.ToShortDateString()+" " + cbChiDinh.Text+"\n";
                //frmchidinh frm = new frmchidinh();
                //frm.Show();
            }
            catch { }
        }

        private void btRefesh_Click(object sender, EventArgs e)
        {
            f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = _MySQL.get_DanhSach("", dTKTuNgay.Value, dTKDenNgay.Value).Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_QuanLyYeuCau.rpt", "rpt_QuanLyYeuCau.rpt");
                frm.Show();

            }
            catch { }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = _MySQL.get_DanhSach("", dTKTuNgay.Value, dTKDenNgay.Value).Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_QuanLyYeuCau_th_theokhoa.rpt", "rpt_QuanLyYeuCau_th_theokhoa.rpt");
                frm.Show();

            }
            catch { }
        }
        #region control

        private void f_new()
        {
            init_form();
            dNgayThucHien.Value = DateTime.Now;
            controlToolStrip("new");
            txtMaMay.Select();
        }
        private void f_edit()
        {
            controlToolStrip("edit");
        }
        private void f_save()
        {
            try
            {
                string id = _MySQL.getID("tiepnhanyc");
                string strMaBN = txtMaMay.Text;
                string strHoTen = txtTen.Text;
                string strLoai = cbLoai.SelectedValue.ToString();
                string strMaKP = cbKhoaPhong.SelectedValue.ToString();
                string strHinhThuc = cbPhuongThuc.SelectedValue.ToString();
                string strNguoiNhan = cbNguoiNhanYC.SelectedValue.ToString();
                string strND = txtNoiDung.Text;
                string strNguoiYC = txtNguoiYC.Text;
                string tinhtrang = cbTinhTrang.SelectedValue.ToString(); ;
                DateTime NgayBDSD = dNgayCD.Value;
                if (flagIsNew == true)
                {
                    //Insert

                    _MySQL.InsertTiepNhanYC(id, strMaBN, strHoTen, strLoai, strND, strNguoiYC, strMaKP, strNguoiNhan, strHinhThuc, tinhtrang, dNgayThucHien.Value);
                    txtMaBN.Text = id;

                }
                else
                {
                    //Update
                    id = txtMaBN.Text;
                    _MySQL.UpdateTiepNhan(id, strMaBN, strHoTen, strLoai, strND, strNguoiYC, strMaKP, strNguoiNhan, strHinhThuc, tinhtrang, dNgayThucHien.Value);

                }

            }
            catch { };
            f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
            controlToolStrip("save");
        }
        private void f_cancel()
        {
            controlToolStrip("cancel");
        }
        #endregion 

        private void dNgayCD_Leave(object sender, EventArgs e)
        {
            f_loadchidinh();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = _MySQL.get_DanhSachDuyetXoaCLS(dTKTuNgay.Value, dTKDenNgay.Value).Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_BCDuyetXoaXNCLS.rpt", "rpt_BCDuyetXoaXNCLS.rpt");
                frm.Show();

            }
            catch { }
        }
    }
}