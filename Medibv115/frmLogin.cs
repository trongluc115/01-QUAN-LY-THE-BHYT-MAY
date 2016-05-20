using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Data;
using DataMySQL;
using Entity;

namespace MediIT115
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            //CUserDAO dangnhap = new CUserDAO();
            CTiepNhanYC tn = new CTiepNhanYC();
            CXml xml = new CXml();
            
            string ID = tn.get_RightDangNhap(txtUsername.Text, txtPassword.Text);
            if (ID!="-1")
            {

                xml.WriteXML(1, ID, "ConfigMenu.xml");
                this.Close();


            }
            else {
                MessageBox.Show("Đăng nhập không hợp lệ!");
                xml.WriteXML(1, "", "ConfigMenu.xml");
                Application.Exit();
            }
        }
        public void f_Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                //MessageBox.Show(sender.ToString());
                //MessageBox.Show(e.KeyCode.ToString());
               
                if (e.KeyCode == Keys.Enter)
                {
                    //MessageBox.Show(e.KeyCode.ToString());
                    //SendKeys.Send("{Tab}{F4}");
                    if (e.Handled == false)
                    {
                        SendKeys.Send("{Tab}");
                        e.Handled = true;
                    }
                }
                else
                    if ((sender.GetType().ToString() == "System.Windows.Forms.ComboBox"))
                    {
                        //MessageBox.Show(sender.ToString());
                        System.Windows.Forms.ComboBox tmp = (System.Windows.Forms.ComboBox)(sender);
                        if (tmp.SelectedIndex < 0)
                        {
                            tmp.SelectedIndex = 0;
                        }
                        //SendKeys.Send("{F4}");
                    }
            }
            catch
            {
                //MessageBox.Show(ex.ToString());
            }

        }
        

        private void frmLogin_Load(object sender, EventArgs e)
        {
           
        }
    }
}