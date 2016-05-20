using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataMySQL;
namespace MediIT115
{
    public partial class frmBaoTri : Form
    {
        private string _tableName = "";
        private string _title = "";
        private string _mamay = "";
        CMay _MySQLCMay;
        bool flagIsNew = false;
        public frmBaoTri()
        {
            InitializeComponent();
        }
        public frmBaoTri(string tablename,string title,string mamay)
        {
            _tableName = tablename;
            _title = title;
            _mamay = mamay;
            InitializeComponent();
        }
        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            txtToolStrip.Text = _title;
            _MySQLCMay = new CMay();
            f_loadDanhSach();
            panel1.Enabled = false;
        }
        private void f_loadDanhSach()
        {
            try
            {
                dDanhMuc.Rows.Clear();
                DataTable dt = _MySQLCMay.get_BaoTri(_tableName,_mamay).Tables[0];
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
                string id ;
                if (flagIsNew == true)
                {
                    id = _MySQLCMay.getID(_tableName);
                    txtMaBN.Text = id;
                    _MySQLCMay.InsertBaoTri(id, dateN1.Value, _mamay, txtNoiDung.Text);
                }
                else {
                    id = txtMaBN.Text;
                    _MySQLCMay.UpdateBaoTri(id, txtNoiDung.Text, dateN1.Value
                        );
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
                    f_setThongTin("", "",DateTime.Today);
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
                f_setThongTin(dDanhMuc.Rows[row].Cells[0].Value.ToString(), dDanhMuc.Rows[row].Cells[4].Value.ToString(),DateTime.Parse(dDanhMuc.Rows[row].Cells[1].Value.ToString()));
            }
            catch { }
        }
        private void f_setThongTin(string id, string ten,DateTime ngay)
        {
            try {
                txtMaBN.Text = id;
                txtNoiDung.Text = ten;
                dateN1.Value = ngay;
            }
            catch { }
        }

        private void toolDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _MySQLCMay.DeleteDanhMuc("baotri", txtMaBN.Text);
                f_loadDanhSach();
            }
            catch { }
        }

        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            txtNoiDung.Text="{Kiểm kê "+dateN1.Value.Year.ToString()+"}";
        }

        
    }
}