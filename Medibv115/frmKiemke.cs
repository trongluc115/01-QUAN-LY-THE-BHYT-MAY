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
    public partial class frmKiemke : Form
    {
        private string s_id = "";
        private string s_kiemke = "";
        CMay _MySQLCMay;
        public frmKiemke()
        {
            InitializeComponent();
        }
        public frmKiemke(string S_id, string S_kiemke)
        {
            s_id = S_id;
            s_kiemke = S_kiemke;
            InitializeComponent();
        }

        private void frmKiemke_Load(object sender, EventArgs e)
        {
            _MySQLCMay = new CMay();
            if (s_kiemke == "1")
            {
                lbKiemke.Text = "ĐÃ KIỂM KÊ";
                btYes.Select();
                
            }
            else {
                lbKiemke.Text = "CHƯA KIỂM KÊ";
                lbKiemke.ForeColor = Color.Red;
                btYes.Select();
            }

        }

        private void btYes_Click(object sender, EventArgs e)
        {
            _MySQLCMay.UpdateKiemKe(s_id, "1");
            this.Close();

        }

        private void btNo_Click(object sender, EventArgs e)
        {
            _MySQLCMay.UpdateKiemKe(s_id, "0");
            this.Close();
        }
        

      
    }
}