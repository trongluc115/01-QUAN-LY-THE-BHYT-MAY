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
    public partial class frmTiepNhanKB : Form
    {
        
        
        bool flagIsNew = false;
        public frmTiepNhanKB()
        {
            InitializeComponent();
            
           
        }
        private void frmQLThietBi_Load(object sender, EventArgs e)
        {
           
            controlToolStrip("cancel");
           
        }
        #region LoadCombobox
      
      
       
     
        private void f_LoadListNguoiNhanYC()
        {
            try
            {
                //cbNguoiNhanYC.DataSource = _MySQL.get_DanhMuc("dmUser", "1").Tables[0];
                //cbNguoiNhanYC.DisplayMember = "Ten";
                //cbNguoiNhanYC.ValueMember = "ID";
            }
            catch { }
        }
        
        
        #endregion
        private void init_form()
        {
            try
            {
            }
            catch { }
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

      
        
        

        private void controlToolStrip(string code)
        {
            switch (code)
            {
                case "new":
                    toolDelete.Enabled = false;
                    toolEdit.Enabled = false;
              
                    toolCancel.Enabled = true;
                    toolSave.Enabled = true;
                    
                    toolNew.Enabled = false;
                    flagIsNew = true;
                    
                    break;
                case "edit":
                    toolDelete.Enabled = false;
                    toolNew.Enabled = false;
                    toolSave.Enabled = true;
                   
                    toolCancel.Enabled = true;
                    toolEdit.Enabled = false;
              
                    flagIsNew = false;
                    break;
                case "cancel":
                    toolDelete.Enabled = true;
                    toolNew.Enabled = true;
                   
                    toolCancel.Enabled = false;
                    toolEdit.Enabled = true;
                    toolSave.Enabled = false;
                    
                    break;
                case "save":
                    toolDelete.Enabled = true;
                    toolNew.Enabled = true;
                  
                    toolCancel.Enabled = false;
                    toolEdit.Enabled = true;
                    toolSave.Enabled = false;
                    
                    
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

       
      


        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            try {

                if (txtMaBN.Text.Length == 8)
                {
                    CBenhNhan item = new CBenhNhan();
                    CBenhNhanOracle dataOra = new CBenhNhanOracle();
                    item = dataOra.getBenhNhan(txtMaBN.Text);
                    txtHoten.Text=item.HoTen ;
                    txtNamsinh.Text = item.NamSinh.ToString();
                    CThanhToanBHYTOracle dataBHYTOra = new CThanhToanBHYTOracle();
                    CThanhToanBHYT bhyt = new CThanhToanBHYT();
                    bhyt = dataBHYTOra.f_loadTT_BHYT_TiepNhan(txtMaBN.Text,DateTime.Today);
                    txtSothe.Text = bhyt.SoTheBHYT ;
                    //txtHSD.Text = string.Format("{0:dd/MM/yyyy}", bhyt.HSD);
                    //setList(cbPhuongThuc, bhyt.TraiTuyen.ToString());
                    //if(ckLuu.Checked==true)
                    //{
                    //    if (toolSave.Enabled == true)
                    //    {
                            
                    //        SendKeys.Send("{F5}");
                    //    }
                    //}


                }
                else {
                    txtHoten.Text = "";
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
      
       
       

     
      
        private void toolCancel_Click(object sender, EventArgs e)
        {
            f_cancel();
        }

       

       

       
       

      
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }


        private void toolStripBtTaoDot_Click(object sender, EventArgs e)
        {

        }

     
       
     
        #region control

        private void f_new()
        {
            init_form();
            
            controlToolStrip("new");
            txtMaBN.Select();
        }
        private void f_edit()
        {
            controlToolStrip("edit");
        }
        private void f_save()
        {
         
        }
        private void f_cancel()
        {
            controlToolStrip("cancel");
        }
        #endregion 
        private string HexAsciiConvert(string hex)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= hex.Length - 2; i += 2)
            {

                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),

                System.Globalization.NumberStyles.HexNumber))));

            }
            
            
            return sb.ToString();

        }

        /*
            1.	Số thẻ BHYT
            2.	Họ và tên bệnh nhân
            3.	Giới tính
            4.	Ngày sinh
            5.	Địa chỉ
            6.	Mã bệnh viện đăng ký BHYT
            7.	Thời hạn sử dụng (từ ngày )
            8.	Thời hạn sử dụng (từ đến)
            9.	Ngày cấp thẻ BHYT
            10.	Số VSSID của thẻ BHYT

         */

        private void txtBHYTCode_Leave(object sender, EventArgs e)
        {
            string hex = txtBHYTCode.Text;

          //  txtHoten.Text = HexAsciiConvert(hex);
            CTheBHYT thebhyt = new CTheBHYT();
            thebhyt.sothe = txtBHYTCode.Text.Split('|')[0];
            thebhyt.hoten = txtBHYTCode.Text.Split('|')[1];
            thebhyt.gioitinh = txtBHYTCode.Text.Split('|')[2];
            thebhyt.ngaysinh = txtBHYTCode.Text.Split('|')[3];
            thebhyt.diachi = txtBHYTCode.Text.Split('|')[4];
            thebhyt.mabv = txtBHYTCode.Text.Split('|')[5];
            thebhyt.tungay = txtBHYTCode.Text.Split('|')[6];
            thebhyt.denngay = txtBHYTCode.Text.Split('|')[7];
            thebhyt.ngaycap = txtBHYTCode.Text.Split('|')[8];
            thebhyt.VSSID = txtBHYTCode.Text.Split('|')[9];

            txtSothe.Text = thebhyt.sothe + thebhyt.mabv;
            txtHoten.Text = HexAsciiConvert(thebhyt.hoten);

            int nam,thang,ngay;
            ngay=int.Parse(thebhyt.tungay.Substring(0,2));
            thang=int.Parse(thebhyt.tungay.Substring(3,2));
            nam=int.Parse(thebhyt.tungay.Substring(6,4));

            dTungay.Value = new DateTime(nam, thang, ngay);

            ngay = int.Parse(thebhyt.denngay.Substring(0, 2));
            thang = int.Parse(thebhyt.denngay.Substring(3, 2));
            nam = int.Parse(thebhyt.denngay.Substring(6,4
                ));

            dDenngay.Value =new  DateTime(nam, thang, ngay);




        }

       

        
    }
}