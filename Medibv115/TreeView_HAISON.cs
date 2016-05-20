using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report2009
{
    public partial class TreeView_HAISON : UserControl
    {
        private string strMa = "", strTen = "", stooltip = "";
        private bool bCo;
        public TreeView_HAISON()
        {
            InitializeComponent();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                e.Node.ForeColor = e.Node.Checked ? Color.Blue : Color.Black;

                treeView1.ExpandAll();
            }
            treeView1.ExpandAll();
        }
        private void f_Set_Check(TreeNode v_node, bool v_bool)
        {
            try
            {
                foreach (TreeNode anode in v_node.Nodes)
                {
                    anode.Checked = v_bool;
                    anode.ForeColor = anode.Checked ? Color.Blue : Color.Black;
                    if (anode.Nodes.Count > 0)
                    {
                        f_Set_Check(anode, v_bool);
                    }
                }
            }
            catch
            {
            }
        }
        private void f_Set_CheckID(TreeView v_tree, bool v_b)
        {
            try
            {
                for (int i = 0; i < v_tree.Nodes.Count; i++)
                {                   
                    v_tree.Nodes[i].Checked = v_b;
                    v_tree.Nodes[i].ForeColor = v_tree.Nodes[i].Checked ? Color.Blue : Color.Black;
                }
            }
            catch
            {
            }
        }
        public bool set_CheckBox
        {
            set
            {
                chkTitle.Checked = value;
            }
        }
        /// <summary>
        /// Hàm load dữ liệu
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="strDisplayMember">Tên thể hiện</param>
        /// <param name="strValuaMember">Giá trị </param>
        /// <param name="strTieude">Tên thể cho cho giá trị chuyền vào Control</param>
        /// <param name="b_dk">
        ///  - Nếu true thì khi lấy giá trị là _'05','05'
        ///  - Nếu false thì khi lấy giá trị là_ 05,05
        /// </param>
        /// <param name="s_tooltip">Mô tả thể hiện giá trị đó trên control</param>
        public void setDataSource(DataTable dt, string strDisplayMember, string strValuaMember, string strTieude,bool b_dk,string s_tooltip)
        {
            chkTitle.Text = strTieude.ToUpper();
            treeView1.Nodes.Clear();
            bCo = b_dk;
            stooltip = s_tooltip;
            TreeNode anode;            
            foreach (DataRow r in dt.Rows)
            {
                anode = new TreeNode(r[strDisplayMember].ToString());
                anode.Tag = r[strValuaMember].ToString();               
                treeView1.Nodes.Add(anode);
                anode.ExpandAll();
            }            
        }       
        /// <summary>
        /// Hàm lấy nhưng mục chọn
        /// </summary>
        public string get_Ma
        {
            get
            {
                f_Get_CheckID();
                return strMa;
            }
        }
        /// <summary>
        /// Hàm lấy tên những mục chọn
        /// </summary>
        public string get_Ten
        {
            get
            {
                f_Get_CheckID();
                return strTen;
            }
        }

        private void f_Get_CheckID()
        {
            try
            {
                strMa = ""; strTen = "";
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    if (treeView1.Nodes[i].Checked)
                    {
                        if (bCo) strMa += "'" + treeView1.Nodes[i].Tag.ToString() + "',";
                        else strMa += treeView1.Nodes[i].Tag.ToString() + ",";
                        strTen += treeView1.Nodes[i].Text + "^";
                    }
                }
            }
            catch
            {               
            }
        }
        private void chkTitle_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(chkTitle, stooltip);
        }

        private void chkTitle_CheckedChanged(object sender, EventArgs e)
        {            
            f_Set_CheckID(treeView1, chkTitle.Checked);
        }

        public void f_set_CheckTreeView(DataSet ads, string s_sosanh)
        {
            foreach (DataRow r in ads.Tables[0].Select("done=1"))
            {
                for (int i = 0; i < treeView1.Nodes.Count; i++)
                {
                    if (r[s_sosanh].ToString() == treeView1.Nodes[i].Tag.ToString()) 
                    {                       
                        treeView1.Nodes[i].Checked = true;
                        treeView1.Nodes[i].ForeColor = treeView1.Nodes[i].Checked ? Color.Blue : Color.Black;
                    }
                }
            }
        }
    }
}
