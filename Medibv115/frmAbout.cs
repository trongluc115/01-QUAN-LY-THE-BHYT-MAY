using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
namespace MediIT115
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            CXml xml = new CXml();
            lbServer.Text = xml.ReadXML(2, "config.xml");
            dview.DataSource = xml.ReadXML("config.xml").Tables[0];
        }
    }
}