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
    public partial class frmchidinh : Form
    {
        private string _tableName = "";
        private string _title = "";
        CMay _MySQLCMay;
        bool flagIsNew = false;
        public frmchidinh()
        {
            InitializeComponent();
        }
        public frmchidinh(string tablename, string title)
        {
            _tableName = tablename;
            _title = title;
            InitializeComponent();
        }
        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            txtToolStrip.Text = _title;
            _MySQLCMay = new CMay();
            f_loadDanhSach();
            panel1.Enabled = true;
        }
        private void f_loadDanhSach()
        {
            try
            {
                dDanhMuc.Rows.Clear();
                DataTable dt = _MySQLCMay.get_DanhMuc(_tableName).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dDanhMuc.Rows.Add(dr.ItemArray);
                }
            }
            catch { }

            
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                controlToolStrip("save");
                string Enable = cbEnable.SelectedIndex.ToString();
                string id ;
                if (flagIsNew == true)
                {
                    id = _MySQLCMay.getID(_tableName);
                    txtMaBN.Text = id;
                    _MySQLCMay.InsertDanhMuc(_tableName, id, txtTen.Text,Enable);
                }
                else {
                    id = txtMaBN.Text;
                    _MySQLCMay.UpdateDanhMuc(_tableName, id, txtTen.Text,Enable);
                }
                f_loadDanhSach();
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
                txtMaBN.Text = id;
                txtTen.Text = ten;
                cbEnable.SelectedIndex = int.Parse(Enable);
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtMaBN.Text.Length == 8)
                {
                    CBenhNhan item = new CBenhNhan();
                    CBenhNhanOracle dataOra = new CBenhNhanOracle();
                    item = dataOra.getBenhNhan(txtMaBN.Text);
                    txtTen.Text = item.HoTen ;
                    txtNamSinh.Text = item.NamSinh.ToString();

                }
                else
                {
                    txtTen.Text = "";
                }


            }
            catch { }
        }
        private void f_loadchidinh()
        {
            try
            {
                DataSet ds = new DataSet();
                dDanhMuc.Rows.Clear();
                if (txtMaBN.Text.Length == 8)
                {
                    CThanhToanBHYTOracle dataOra = new CThanhToanBHYTOracle();

                    ds = dataOra.f_loadChiDinh_v_chidinh(txtMaBN.Text, dNgayCD.Value);

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
                


            }
            catch { }
        }

        private void dNgayCD_Leave(object sender, EventArgs e)
        {
            f_loadchidinh();
        }

        private void btThem_Click(object sender, EventArgs e)
        {

        }
    }
}