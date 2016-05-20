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
using DataOracle;
using Entity;
namespace MediIT115
{
    public partial class frmQLBHYT_trathe : Form
    {
        
        CBenhNhan BN;
        CQLTheBHYT _MySQL;
        int _MucDongChiTra = 0;
        bool flagIsNew = false;
        public frmQLBHYT_trathe()
        {
            InitializeComponent();
            
           
        }
        private void frmQLThietBi_Load(object sender, EventArgs e)
        {
            _MySQL = new CQLTheBHYT();
           
            f_LoadListKhoaPhong();
            f_LoadListLoai();
            f_LoadListHinhThucYC();
            f_LoadListNguoiNhanYC();
            f_LoadListKhoaPhong();
            f_LoadListTinhTrang();
           
            f_LoadDanhSachMay(txttimkiem.Text,dTKTuNgay.Value,dTKDenNgay.Value);
            init_form();
            controlToolStrip("new");
            panel1.Enabled = true
                ;
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
            f_nhapDanhMucYC("dmloaiyeucau", "DANH MỤC ĐỢT NHẬN TRẢ THẺ");
            f_LoadListLoai();
        }
        private void f_nhapDanhMucYC(string tablename, string Title)
        {
            frmDanhMucQLBHYT f = new frmDanhMucQLBHYT(tablename, Title);
            f.Show();
        }
       


        private void btLoaiChiTiet_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmuser", "DANH MỤC NGƯỜI NHẬN");
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
            init_form();
            controlToolStrip("new");
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
                
                    int row = dview.CurrentCell.RowIndex;
                    txtMaBN.Text = dview.Rows[row].Cells["ID"].Value.ToString();
                    f_setThongTin();
                
            }
            catch { }
        }

        private void btTinhTrang_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmtinhtrang", "DANH MỤC HỒ SƠ KÈM THEO");
            f_LoadListTinhTrang();
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
                    CThanhToanBHYTOracle dataBHYTOra = new CThanhToanBHYTOracle();
                    CThanhToanBHYT bhyt = new CThanhToanBHYT();
                    bhyt = dataBHYTOra.f_loadTT_BHYT_TiepNhan(txtMaMay.Text,DateTime.Today);
                    txtNguoiYC.Text = bhyt.SoTheBHYT ;
                    txtHSD.Text = string.Format("{0:dd/MM/yyyy}", bhyt.HSD);
                    setList(cbPhuongThuc, bhyt.TraiTuyen.ToString());
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
                lbThongTin.Text = dview.Rows.Count.ToString(); ;
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
                if (ckLuu.Checked == false && txttimkiem.Text.Length >0)
                {
                    f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
                }

               
            }
            catch {
               
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try {
                //a.id,a.mamay,a.ten tenmay,b.ten ,c.ten ,a.Vitri,e.Ten ,f.Ten ,a.DonGia,g.ten,a.ngaybdsd 
                //AccessData acc = new AccessData();
                //string ID=txtMaBN.Text;
                //DataTable dt=_MySQL.get_BCBaoTri(ID).Tables[0];
                //DataTable thongtin = _MySQL.get_MayFromID(ID).Tables[0];
                //string strMamay = thongtin.Rows[0]["mamay"].ToString();
                //string strTen = thongtin.Rows[0]["tenmay"].ToString();
                //string strNuocSX = thongtin.Rows[0]["nuocsx"].ToString();
                //string strMaKP = thongtin.Rows[0]["khoaphong"].ToString();
                //string strLoai = thongtin.Rows[0]["loaimay"].ToString();
                //string strChiTiet = thongtin.Rows[0]["loaichitiet"].ToString();
                //string strDongia = thongtin.Rows[0]["dongia"].ToString();
                //string strVitri = thongtin.Rows[0]["vitri"].ToString();
               
                //string ngaybdsd = thongtin.Rows[0]["ngaybdsd"].ToString();
                //string cauhinh="";
                //DataTable dtcauhinh=_MySQL.get_cauhinh(ID).Tables[0];
                //foreach (DataRow dr in dtcauhinh.Rows)
                //{
                //    cauhinh += dr[1].ToString() + " : " + dr[2].ToString() + "; ";
                //}
                //frmReport frm = new frmReport(acc, dt, "rpt_PCManagerItem.rpt", strLoai, strMamay, strTen, strNuocSX, strMaKP, strChiTiet, strDongia, strVitri, ngaybdsd, cauhinh);
                //frm.Show();
            }
            catch { }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try {
                DataTable dt = _MySQL.get_DanhSachFrom_Ngay_Dot( "",dTKTuNgay.Value, cbLoai.SelectedValue.ToString() ).Tables[0];
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_QuanLyTheBHYT.rpt", "rpt_QuanLyTheBHYT.rpt");
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
            if (MessageBox.Show("Xác nhận xóa thiết bị?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _MySQL.DeleteDanhMuc("tiepnhanyc", txtMaBN.Text);
              
                
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
            
            f_nhapDanhMucYC("dmhinhthucyc", "DANH MỤC HÌNH THỨC");
            f_LoadListHinhThucYC();
        }

        private void btLoadCD_Click(object sender, EventArgs e)
        {
            try
            { DataSet ds=new DataSet();

                if (txtMaMay.Text.Length == 8)
                {
                    CThanhToanBHYTOracle dataOra=new CThanhToanBHYTOracle();

                    ds = dataOra.f_loadChiDinh(txtMaMay.Text, dNgayCD.Value);

                    cbChiDinh.DataSource=ds.Tables[0];
                    cbChiDinh.DisplayMember="Tenvp";
                    cbChiDinh.ValueMember="ID";;
                    
                }
                else
                {
                    cbChiDinh.DataSource=ds.Tables[0];
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
            }
            catch { }
        }

        private void btThemCD_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoiDung.Text = txtNoiDung.Text + "\n -Chỉ định ngày "+dNgayCD.Value.ToShortDateString()+" Tên chỉ định: " + cbChiDinh.Text;
            }
            catch { }
        }

        private void btRefesh_Click(object sender, EventArgs e)
        {
            f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoiDung.Text = txtNoiDung.Text + "- " + cbTinhTrang.Text +"\n";
            }
            catch { }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btDotBanGiao_Click(object sender, EventArgs e)
        {
            f_nhapDanhMucYC("dmloaiyeucau", "DANH MỤC ĐỢT NHẬN TRẢ THẺ");
           
        }

        private void toolStripBtTaoDot_Click(object sender, EventArgs e)
        {

        }

        private void đánhDấuĐãGiaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Enable = "0";
                string id;
               
                    id = cbLoai.SelectedValue.ToString();
                    _MySQL.UpdateDanhMuc("dmloaiyeucau",id,0,false);    
                    f_LoadListLoai();
           }catch{}

                
        }
  
        private void khởiTạoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string Enable = "0";
                string id;

                id = cbLoai.SelectedValue.ToString();
                _MySQL.UpdateDanhMuc("dmloaiyeucau", id, 1, true);
                f_LoadListLoai();
            }
            catch { }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = _MySQL.get_DanhSach("", dTKTuNgay.Value, dTKDenNgay.Value,"0").Tables[0];
                CThanhToanBHYTOracle dOracle=new CThanhToanBHYTOracle();
                
                string SoThe="";
                foreach (DataRow dr in dt.Rows)
                {
                    int Tong = 0;
                    int BNtra=0;
                    int BHYTtra=0;
                    Tong = dOracle.f_loadTongTien_v_chidinh(dr["MaBN"].ToString(), dTKTuNgay.Value);
                    if(Tong>f_getMucDongChiTra())
                    {
                        SoThe=dr["SoThe"].ToString();
                        BNtra = Tong * (100 - TinhTiLeBHYTthanhtoan(SoThe, 0))/100;
                    }
                    BHYTtra=Tong-BNtra;
                    dr["NoiDung"] = Tong.ToString() + "^" + BHYTtra.ToString();

                }
                AccessData acc = new AccessData();
                frmReport frm = new frmReport(acc, dt, "rpt_QuanLyTheBHYT_chuanhan.rpt", "rpt_QuanLyTheBHYT_chuanhan.rpt");
                frm.Show();

            }
            catch { }
        }
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.F3)
                {
                    //MessageBox.Show("New");
                    //if (toolNew.Enabled == true)
                       // f_new();
                    
                    txttimkiem.Text="";
                    txttimkiem.Select();
                }

                // Ctrl + E
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.E))
                {
                    //MessageBox.Show("Edit");
                    //if (toolEdit.Enabled == true && txtma)
                       // f_edit();
                }

                // Ctrl + S
                if (e.KeyCode == Keys.F5)
                {
                    //MessageBox.Show("Save");
                    if (toolSave.Enabled == true)
                        f_save2();
                }
                if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.C))
                {
                    //MessageBox.Show("Save");
                   // if (toolCancel.Enabled == true)
                       // f_cancel();
                }



            }
            catch { }
        }
        private void f_save()
        {
            try
            {

                string id = txtMaBN.Text;
                string done = "0";
                DateTime TGTra = DateTime.Now;
                foreach (DataGridViewRow row in dview.Rows)
                {
                    id = row.Cells["ID"].Value.ToString();
                    done = row.Cells["Done"].Value.ToString();
                    _MySQL.UpdateTiepNhan_Done(id, done, TGTra);

                }





            }
            catch { };
            f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
        }
        private void f_save2()
        {
            try
            {

                string id = txtMaBN.Text;
                string done = "1";
                DateTime TGTra = DateTime.Now;
                if (dview.Rows.Count == 1)
                {
                    foreach (DataGridViewRow row in dview.Rows)
                    {
                        id = row.Cells["ID"].Value.ToString();
                        //done = "1";
                        _MySQL.UpdateTiepNhan_Done(id, done, TGTra);

                    }

                }




            }
            catch { };
            f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);
        }
        private int TinhTiLeBHYTthanhtoan(string mathe, int traituyen)
        {
            int BNTra = -1;
            string the100 = "1239";
            string the95 = "45";
            string the80 = "67";
            string kytu3 = mathe.Substring(2, 1);
            if (traituyen == 1)
            {
                BNTra = 30;

            }
            else
            {
                if (the100.IndexOf(kytu3) >= 0)
                {
                    BNTra = 100;
                }
                else if (the95.IndexOf(kytu3) >= 0)
                {
                    BNTra = 95;
                }
                else
                {
                    BNTra = 80;
                }

            }
            return BNTra;
        }
        
        private int f_getMucDongChiTra()
        {
            try
            {
                CXml cxml = new CXml();
                string MucDongChiTra = cxml.ReadXML(3, "config.xml");
                return int.Parse(MucDongChiTra);
            }
            catch
            {
                return 0;
            }
        }

        private void dview_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dview.IsCurrentCellDirty)
            //{
            //    if (dview.CurrentCell.ColumnIndex == 0)
            //        dview.CurrentCell.Value = (dview.CurrentCell.Value.ToString() == "1" ? 0 : 1);
            //}
        }

        private void txttimkiem_Leave(object sender, EventArgs e)
        {
            
            if (ckLuu.Checked == true&& txttimkiem.Text.Length==8)

            {
                f_LoadDanhSachMay(txttimkiem.Text, dTKTuNgay.Value, dTKDenNgay.Value);

                SendKeys.Send("{F5}");
                SendKeys.Send("{F3}");
            }
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dview.Rows)
            {
                item.Cells["Done"].Value = ckAll.Checked;

            }
        }
}
}