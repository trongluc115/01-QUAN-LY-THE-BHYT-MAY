using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MediIT115
{
    public partial class haison : UserControl
    {
        private DataSet dsThang, dsQui;
        public haison()
        {
            InitializeComponent();
        }

        private void haison_Load(object sender, EventArgs e)
        {
            DataRow row;
            dsThang = new DataSet();
            dsThang.Tables.Add("Table");
            dsThang.Tables[0].Columns.Add(new DataColumn("ten", typeof(string)));
            for (int i = 1; i <= 12; i++)
            {
                row = dsThang.Tables[0].NewRow();
                row[0] = i.ToString().PadLeft(2, '0');
                dsThang.Tables[0].Rows.Add(row);
            }
            dsQui = new DataSet();
            dsQui.Tables.Add("Table");
            dsQui.Tables[0].Columns.Add(new DataColumn("ten", typeof(string)));
            for (int i = 1; i <= 4; i++)
            {
                row = dsQui.Tables[0].NewRow();
                row[0] = i.ToString().PadLeft(2, '0');
                dsQui.Tables[0].Rows.Add(row);
            }
            cbnam.Value = decimal.Parse(DateTime.Now.Year.ToString());
            cbbaocao.SelectedIndex = 0;
            cbthang.Enabled = false;
            cbnam.Enabled = false;
        }

        private void cbbaocao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0, st = 0, dqui = 0;
            string qui = "";
            string thang = DateTime.Now.Month.ToString().PadLeft(2,'0');
            string strnam = cbnam.Value.ToString();

            switch (thang)
            {
                case "01":
                case "02":
                case "03":
                    qui = "01";
                    break;
                case "04":
                case "05":
                case "06":
                    qui = "02";
                    break;
                case "07":
                case "08":
                case "09":
                    qui = "03";
                    break;
                case "10":
                case "11":
                case "12":
                    qui = "04";
                    break;
            }

            i = cbbaocao.SelectedIndex;

            switch (i)
            {
                case 0: //ngày
                    den.Enabled = true;
                    tu.Enabled = true;
                    cbthang.Enabled = false;
                    cbnam.Enabled = false;
                    tu.ResetText();
                    den.ResetText();
                    break;
                case 1: //thang
                    cbthang.DataSource = dsThang.Tables[0];
                    lblBc.Text = cbbaocao.Items[1].ToString() + " : ";
                    cbthang.DisplayMember = "ten";
                    cbthang.ValueMember = "ten";
                    cbthang.SelectedValue = thang;
                    cbnam.Value = decimal.Parse(strnam);
                    tu.Value = new DateTime(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString()), 1);
                    den.Value = new DateTime(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString()), DateTime.DaysInMonth(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString())));
                    den.Enabled = false;
                    tu.Enabled = false;
                    cbthang.Enabled = true;
                    cbnam.Enabled = true;
                    break;
                case 2: //qui
                    cbthang.DataSource = dsQui.Tables[0];
                    cbthang.DisplayMember = "ten";
                    cbthang.ValueMember = "ten";
                    cbthang.SelectedValue = qui;
                    cbnam.Value = decimal.Parse(strnam);
                    //
                    lblBc.Text = cbbaocao.Items[2].ToString() + " : ";
                    den.Enabled = false;
                    tu.Enabled = false;
                    cbthang.Enabled = true;
                    cbnam.Enabled = true;
                    st = int.Parse(cbthang.SelectedValue.ToString()) * 3;
                    dqui = st - 3 + 1;
                    tu.Value = new DateTime(int.Parse(strnam), dqui, 1);
                    den.Value = new DateTime(int.Parse(strnam), st, DateTime.DaysInMonth(int.Parse(strnam), st));
                    break;
                case 3: //nam
                    cbthang.SelectedIndex = -1;
                    cbthang.Enabled = false;
                    cbnam.Enabled = true;
                    cbnam.Value = decimal.Parse(strnam);
                    den.Enabled = false;
                    tu.Enabled = false;
                    tu.Value = new DateTime(int.Parse(cbnam.Value.ToString()), 1, 1);
                    den.Value = new DateTime(int.Parse(cbnam.Value.ToString()), 12, DateTime.DaysInMonth(int.Parse(cbnam.Value.ToString()), 12));
                    break;
            }
        }

        private void cbthang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != cbthang) return;
            int i = 0;
            int st = 0, dqui = 0;
            string thang = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string strnam = cbnam.Value.ToString();
            i = cbbaocao.SelectedIndex;
            switch (i)
            {
                case 0: //ngày
                    den.Enabled = true;
                    tu.Enabled = true;
                    cbthang.Enabled = false;
                    cbnam.Enabled = false;
                    tu.ResetText();
                    den.ResetText();
                    break;
                case 1: //thang
                    tu.Value = new DateTime(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString()), 1);
                    den.Value = new DateTime(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString()), DateTime.DaysInMonth(int.Parse(strnam), int.Parse(cbthang.SelectedValue.ToString())));
                    den.Enabled = false;
                    tu.Enabled = false;
                    cbthang.Enabled = true;
                    cbnam.Enabled = true;
                    break;
                case 2: //qui
                    den.Enabled = false;
                    tu.Enabled = false;
                    cbthang.Enabled = true;
                    cbnam.Enabled = true;
                    st = int.Parse(cbthang.SelectedValue.ToString()) * 3;
                    dqui = st - 3 + 1;
                    tu.Value = new DateTime(int.Parse(strnam), dqui, 1);
                    den.Value = new DateTime(int.Parse(strnam), st, DateTime.DaysInMonth(int.Parse(strnam), st));
                    break;
                case 3: //nam
                    cbthang.SelectedIndex = -1;
                    cbthang.Enabled = false;
                    cbnam.Enabled = true;
                    cbnam.Value = decimal.Parse(strnam);
                    den.Enabled = false;
                    tu.Enabled = false;
                    break;
            }
        }

        private void cbnam_ValueChanged(object sender, EventArgs e)
        {
            tu.Value = new DateTime(int.Parse(cbnam.Value.ToString()), 1, 1);
            den.Value = new DateTime(int.Parse(cbnam.Value.ToString()), 12, DateTime.DaysInMonth(int.Parse(cbnam.Value.ToString()), 12));
        }

        private void cbbaocao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }

        private void cbthang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }

        private void cbnam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }

        private void tu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }

        private void den_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{Tab}");
        }
        /// <summary>
        /// Ham lay tu ngay
        /// </summary>
        public string tungay
        {
            get
            {
                return tu.Text.Substring(0, 10);
            }
            
        }
        /// <summary>
        /// ham lay den ngay
        /// </summary>
        public string denngay
        {
            get
            {
                return den.Text.Substring(0, 10);
            }
        }
        public string s_title
        { 
            get
            {
                return tieude();
            }
        }
        private string tieude()
        {
            string s_title = "";
            if (cbbaocao.SelectedIndex == 0)
            {
                if (tu.Text == den.Text) s_title = "Báo cáo ngày " + tu.Text.Substring(0, 2) + " tháng " + tu.Text.Substring(3, 2) + " năm " + tu.Text.Substring(6, 4) + "";
                else s_title = "Từ ngày " + tu.Text + " đến ngày " + den.Text + "";
            }
            else if (cbbaocao.SelectedIndex == 1)
            {
                s_title = "Báo cáo tháng " + cbthang.Text + " năm " + cbnam.Value.ToString() + "";
            }
            else if (cbbaocao.SelectedIndex == 2)
            {
                s_title = "Báo cáo quý " + cbthang.Text + " năm " + cbnam.Value.ToString() + "";
            }
            else if (cbbaocao.SelectedIndex == 3)
            {
                s_title = "Báo cáo năm" + cbnam.Value.ToString() + "";
            }
            return s_title;
        }

        private void tu_Validated(object sender, EventArgs e)
        {

        }
        public bool kiemtra()
        {
            if (den.Value < tu.Value)
            {
                tu.Focus();
                return false;
            }
            return true;
        }
    }
}
