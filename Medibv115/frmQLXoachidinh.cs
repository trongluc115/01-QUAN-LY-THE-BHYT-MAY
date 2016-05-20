using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Data;
using DataOracle;
using DataMySQL;
using LibBaocao;
using Entity;

namespace MediIT115
{
    public partial class frmQLXoachidinh : Form
    {
        private string _IDYC = "";
        private string _title = "";
        private DateTime _ngay;
        CTiepNhanYC _MySQLCMay;
        bool flagIsNew = false;
        public frmQLXoachidinh()
        {
            InitializeComponent();
        }
        public frmQLXoachidinh(string ID,DateTime Ngay, string title)
        {
            _IDYC = ID;
            _title = title;
            _ngay = Ngay;
            InitializeComponent();
        }
       
        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            txtToolStrip.Text = _title;
            dNgayCD.Value = _ngay;
            _MySQLCMay = new CTiepNhanYC();
            f_LoadListUser();
      
            panel1.Enabled = true;
        }
      
        private void f_LoadListUser()
        {
            try
            {
                CThanhToanBHYTOracle dataOracle = new CThanhToanBHYTOracle();
                cbUser.DataSource = dataOracle.f_loadUser_v_chidinh(dNgayCD.Value).Tables[0];
                cbUser.DisplayMember = "Ten";
                cbUser.ValueMember = "ID";
            }
            catch { }
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                f_save();
                f_loadchidinh();
                f_loadcChiDinhDaXoaFromMySQL();

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
                    f_setThongTin("", "","0");
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

        private void toolNew_Click(object sender, EventArgs e)
        {
            controlToolStrip("new");
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
            controlToolStrip("edit");
        }

        private void dDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { 
                int row=dDanhMuc.CurrentCell.RowIndex;
                f_setThongTin(dDanhMuc.Rows[row].Cells[0].Value.ToString(), dDanhMuc.Rows[row].Cells[1].Value.ToString(), dDanhMuc.Rows[row].Cells[2].Value.ToString());
            }
            catch { }
        }
        private void f_setThongTin(string id, string ten,string Enable)
        {
            try {
               
                cbUser.SelectedIndex = int.Parse(Enable);
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void f_loadchidinh()
        {
            try
            {
                DataSet ds = new DataSet();
                dDanhMuc.Rows.Clear();
               
                    CThanhToanBHYTOracle dataOra = new CThanhToanBHYTOracle();
                    string iddaduyet = f_getListIDFrom_v_Chidinh(dNgayCD.Value);
                   ds = dataOra.f_loadChiDinh_v_chidinh_user(cbUser.SelectedValue.ToString(), dNgayCD.Value,iddaduyet);

                    try
                    {
                        
                        DataTable dt =ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            dDanhMuc.Rows.Add(dr.ItemArray);
                        }
                    }
                    catch { }
                    

                
                


            }
            catch { }
        }

        private void dNgayCD_Leave(object sender, EventArgs e)
        {
            f_LoadListUser();
        }

        private void cbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            f_loadchidinh();
            f_loadcChiDinhDaXoaFromMySQL();
        }
        private void f_loadcChiDinhDaXoaFromMySQL()
        {

            try
            {
                DataSet ds = new DataSet();
                dChiDinh.Rows.Clear();

                CTiepNhanYC dataMySQL=new CTiepNhanYC();

                ds = dataMySQL.get_DanhSachXoaChiDinhFromUser(cbUser.SelectedValue.ToString(), dNgayCD.Value);

                try
                {

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        dChiDinh.Rows.Add(dr.ItemArray);
                    }
                }
                catch { }






            }
            catch { }
        }
        private void f_save()
        {
            
                foreach (DataGridViewRow row in dDanhMuc.Rows)
                {
                    try{
                    if(row.Cells["Chon"].Value.ToString()=="True")
                    {
                        string HotenBN = row.Cells["HotenBN"].Value.ToString();
                        string id = row.Cells["ID_chidinh"].Value.ToString();
                        string stt = _IDYC;
                        string strMaBN = row.Cells["MaBN"].Value.ToString();
                        string strLoai = "1";
                        string strMaVP = row.Cells["MaVP"].Value.ToString();
                        string strTenVP =row.Cells["TenVP"].Value.ToString();
                        string strMaKP = row.Cells["MaKP"].Value.ToString();
                        DateTime ngayxoa = dNgayCD.Value;
                        string strNguoiXoa =row.Cells["nguoixoa"].Value.ToString();
                        string strUserID = row.Cells["userid"].Value.ToString();
                        string strNgayCD =f_changeFormatNgay( row.Cells["Ngay"].Value.ToString());
                        string strSoLuong = row.Cells["SL"].Value.ToString();
                        f_insertBenhNhan(strMaBN, HotenBN);
                        _MySQLCMay.InsertDanhMuc_v_chidinh(id,stt,strMaBN,strLoai,strMaVP,strTenVP,strMaKP,ngayxoa,strNguoiXoa,strUserID,strNgayCD,strSoLuong);
                    }
                
                    }catch{}
                }
                    

        }
        private void f_insertBenhNhan(string mabn,string hoten)
        {
            
            _MySQLCMay.InsertDanhMuc("btdbn", mabn,hoten,"1");
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dChiDinh.Rows)
                {
                    if (row.Cells["Chon_chidinh"].Value.ToString()=="True")
                    {
                        string id = row.Cells["IDchidinh"].Value.ToString();
                        _MySQLCMay.DeleteDanhMuc("v_chidinh", id);
                        
                        
                    }
                 
                }
                f_loadchidinh();
                f_loadcChiDinhDaXoaFromMySQL();
               

            }
        }
        private string f_getListIDFrom_v_Chidinh(DateTime ngay)
        {
            string kq = "0";
            

                DataSet ds = new DataSet();
                ds = _MySQLCMay.get_DanhSachXoaChiDinhFromUser(dNgayCD.Value);

                try
                {
                    
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        kq +=","+ dr["ID"].ToString() ;
                        
                    }
                    
                }
                catch { }
                return kq;






           
        }
        private string f_changeFormatNgay(string ngay)
        {
            string kq = "";
            try {
                kq = ngay.Substring(3, 2) + "/" + ngay.Substring(0, 2) + ngay.Substring(5, ngay.Length - 5);
            }
            catch { }
            return kq;
        }
        private string f_getNoiDung_v_Chidinh(string IDYC)
        {
            string kq = "";


            DataSet ds = new DataSet();
            ds = _MySQLCMay.get_NoiDungXoaChiDinhFromIDYC(_IDYC);

            try
            {

                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    kq += "- " + dr["MaBN"].ToString() + " (" + dr["Ngaychidinh"].ToString() + ") \n  ." + dr["Tenvp"].ToString() + " \n"; //dr["TenNGuoixoa"].ToString() +"\n";


                }
                if (dt.Rows.Count > 0)
                {
                    kq = "Tổng số: " + dt.Rows.Count.ToString() + " chỉ định.\n==============================\n" + kq;
                }


            }
            catch { }
            return kq;







        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _MySQLCMay.UpdateNoiDung(_IDYC, f_getNoiDung_v_Chidinh(_IDYC));
        }
    }
}