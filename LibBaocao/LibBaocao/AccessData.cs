using System;
using System.Xml;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using System.Windows.Forms;


namespace LibBaocao
{
    public class AccessData
    {
        public string Msg = "Báo cáo 2007";
        private int iRownum = 1;
        public const string links_userid = "links", links_pass = "link7155019s20";
        string xxxxx = "Ð§Ì©Î«³²°Ô£";
        string sConn = "Server=192.168.1.14;Port=5432;User Id=medisoft;Password=links1920;Database=medisoft;Encoding=UNICODE;Pooling=true;";
        NpgsqlDataAdapter dest;
        NpgsqlConnection con;
        NpgsqlCommand cmd;        
        string sComputer = null;
        string sql = "",  owner = "medisoft", password = "links1920", userid = "medibv", database = "medisoft";
        private const string sformat = "dd/mm/yyyy";
        private DataSet ds = null;

        private string vsql = "";
        private DataSet vds = null;
        private DataRow r1, r2;

        public string Xq = "02";
        public string Sa = "01";
        public string Ns = "03";
        public string Dtim = "04";
        public string CTSCan = "05";
        public string MRI = "06";
        public string Diennao = "07";
        public string CTG = "08";
        public string Luuhuyetnao = "09";

        public string insert = "ins", delete = "del", dutru = "dutru", duyet = "duyet";

        public AccessData()
        {
            sComputer = System.Environment.MachineName.Trim().ToUpper();
            if (Maincode("Con") != "") sConn = Maincode("Con");
            if (Maincode("User") != "") userid = Maincode("User");
            if (Maincode("UserID") != "") owner = Maincode("UserID");
            if (Maincode("Password") != "") password = Maincode("Password");
            if (Maincode("Database") != "") database = Maincode("Database");
            if (Maincode("xxxxx") == "*****") password = decode(xxxxx).ToLower();
            
            sComputer = System.Environment.MachineName.Trim().ToUpper();
            sConn = "Server=" + Maincode("Ip") + ";Port=" + Maincode("Post") + ";User Id=" + owner + ";Password=" + password + ";Database=" + database + ";Encoding=UNICODE;Pooling=true;";
            upd_dmcomputer(sComputer);
            ds = get_data("select id,computer from " + userid + ".dmcomputer");
            DataRow r = getrowbyid(ds.Tables[0], "computer='" + sComputer + "'");
            if (r != null) iRownum = int.Parse(r["id"].ToString());
        }

        public string s_ngayin
        {
            get
            {
                return "Ngày " + DateTime.Now.Day.ToString().PadLeft(2, '0') + " tháng " + DateTime.Now.Month.ToString().PadLeft(2, '0') + " năm " + DateTime.Now.Year.ToString() + "";
            }
        }

        public string s_ThieuReport(string s_loi)
        {
            string s = "";
            if (s_loi.ToLower().Split('^')[0] == "load report failed.")
                s = "Thiếu report " + s_loi.ToLower().Split('^')[1] + "";
            else
                s = s_loi;
            return s;
        }
        public string s_FormulaFields(string s_loi)
        {
            return "Thiếu FormulaFields  " + s_loi.ToLower().Split('^')[0] + "  trong report " + s_loi.ToLower().Split('^')[1] + "";
        }
        /// <summary>
        /// Tên file không đính kèm đuôi .XML
        /// </summary>
        /// <param name="fileXML">Tên file XML cần write(không nhập .xml mà chỉ nhập tên file)</param>
        /// <returns>Đường dẫn lưu file xml đó</returns>
        public string s_getDirec_XML(string fileXML)
        {
            string s = System.IO.Directory.GetCurrentDirectory();
            string ss = s.Substring(0, s.Length - 10) + "\\DataXml\\" + fileXML + ".xml";
            return ss;
        }

        public string s_title(string s_tu, string s_den)
        {
            string s_title = "";
            if (s_tu == s_den) s_title = "Ngày " + s_tu.Substring(0, 2) + " tháng " + s_tu.Substring(3, 2) + " năm " + s_tu.Substring(6, 4) + "";
            else s_title = "Từ ngày " + s_tu.Substring(0,10) + " đến ngày " + s_den.Substring(0,10);
            return s_title;
        }

        #region Thongso
        public string Giamdoc(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=7 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public string Phutrach(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=8 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public string Thongke(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=9 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public string Thukho(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=16 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public string Ketoan(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=15 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public string Ketoantruong(int nhom)
        {
            ds = get_data("select ten from medibv.d_thongso where id=32 and nhom=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
            else return "";
        }
        public bool bSolieu
        {
            get
            {
                return int.Parse(Thongso("c22")) == 1;
            }
        }
        public string Thongso(string tenfile, string cot)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\..\\xml\\" + tenfile + ".xml");
                XmlNodeList nodeLst = doc.GetElementsByTagName(cot);
                return nodeLst.Item(0).InnerText;
            }
            catch { return ""; }
        }
        public string Thongso(string sql)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\..\\xml\\thongso.xml");
                XmlNodeList nodeLst = doc.GetElementsByTagName(sql);
                return nodeLst.Item(0).InnerText;
            }
            catch
            {
                return "";
            }
        }
        public bool bGiaban_theodot(int d_nhom)
        {
            if (Mabv.Substring(0, 3) == "701") return false;
            else
            {
                ds = get_data("select ten from " + user + ".d_thongso where id=126 and nhom=" + d_nhom);
                if (ds.Tables[0].Rows.Count == 0) return false;
                return ds.Tables[0].Rows[0][0].ToString().Trim() == "1";
            }
        }
        public bool bSovaovien_ylenh
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=167");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bYlenh_nhathuoc
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=181");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bThekho_congdon(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=113 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public int get_nhomkho
        {
            get
            {
                try { return int.Parse(Thongso("d_nhomkholog", "NHOMKHO").ToString()); }
                catch { return 1; }
            }
        }
        public bool bThekho_sophieu(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=91 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bDongia_thekho(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=98 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public int iTreem6tuoi
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=147");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                catch { return 0; }
            }
        }
        public int d_dongia_le(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=58 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 2;
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public bool bGiaban(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=14 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bQuanlyhandung(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=19 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public string format_sotien(int d_nhom)
        {
            string ret = "###,###,###,##0";
            int n = d_thanhtien_le(d_nhom);
            if (n > 0) ret += ".";
            for (int i = 0; i < n; i++) ret += "0";
            return ret;
        }
        public string format_soluong(int d_nhom)
        {
            string ret = "###,###,###,##0";
            int n = d_soluong_le(d_nhom);
            if (n > 0) ret += ".";
            for (int i = 0; i < n; i++) ret += "0";
            return ret;
        }
        public string format_giaban(int d_nhom)
        {
            string ret = "###,###,###,##0";
            int n = d_giaban_le(d_nhom);
            if (n > 0) ret += ".";
            for (int i = 0; i < n; i++) ret += "0";
            return ret;
        }
        public string format_dongia(int d_nhom)
        {
            string ret = "###,###,###,##0";
            int n = d_dongia_le(d_nhom);
            if (n > 0) ret += ".";
            for (int i = 0; i < n; i++) ret += "0";
            return ret;
        }
        public int d_soluong_le(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=57 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 2;
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public int d_giaban_le(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=79 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 0;
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public int d_thanhtien_le(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=59 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 2;
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public bool bQuanlylosx(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=20 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bQuanlynhomcc(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=18 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public string ma13_ngtru(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=48 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString().Trim();
        }
        public string ma16_ngtru(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=49 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString().Trim();
        }
        public decimal ma13_st(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=50 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 20000;
            return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public decimal ma16_st(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=51 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return 20000;
            return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public string ma13_vitri
        {
            get
            {
                ds = get_data("select ten from " + user + ".d_thongso where id=52");
                if (ds.Tables[0].Rows.Count == 0) return "5,1";
                return ds.Tables[0].Rows[0][0].ToString().Trim();
            }
        }
        public string ma16_vitri
        {
            get
            {
                ds = get_data("select ten from " + user + ".d_thongso where id=53");
                if (ds.Tables[0].Rows.Count == 0) return "5,2";
                return ds.Tables[0].Rows[0][0].ToString().Trim();
            }
        }
        public bool bXuatban_user(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=90 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bInPhieuxuatban(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=103 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return true;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bChonloaiphieu_xuat(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=129 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString().Trim() == "1";
        }
        public bool bDonthuoc_cachdung
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=156");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bChieu_sang
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=155");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bChuyenkho_inrieng(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=73 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bCongdon
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=200");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bSotien_bhyt(int d_nhom)
        {
            return true;
        }
        public bool bIncstt(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=145 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bKiemke_c14(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=114 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public int Ngaylv_Ngayht
        {
            get
            {
                return int.Parse(Thongso("c14"));
            }
        }
        public int get_loai(int nhom)
        {
            ds = get_data("select loai from " + user + ".d_dmnhomkho where id=" + nhom);
            if (ds.Tables[0].Rows.Count > 0) return int.Parse(ds.Tables[0].Rows[0]["loai"].ToString());
            else return 1;
        }
        public bool bTru_tonao(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=119 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString().Trim() == "1";
        }
        public bool bGiaban_nguon(int d_nhom)
        {
            ds = get_data("select * from " + user + ".d_dmnguon where loai=1 and nhom=" + d_nhom);
            return ds.Tables[0].Rows.Count > 0;
        }
        public bool bHoatchat { get { return Thongso("c23") == "1"; } }
        public bool bKhoaso(int d_nhom, string d_mmyy)
        {
            return get_data("select * from " + user + ".d_khoaso where nhom=" + d_nhom + " and mmyy='" + d_mmyy + "'").Tables[0].Rows.Count > 0;
        }
        public decimal Round(decimal so, int le)
        {
            return Math.Round(so + decimal.Parse("0.00000000001"), le);
        }
        public string for_num_ngay(string ngay)
        {
            if (ngay.IndexOf("/") >= 0) return for_num_ngay_yyyymmdd(ngay);
            //return "to_number(to_char(" + ngay + ", '" + f_ngay_yyyymmdd + "'))";
            return "date(to_char(" + ngay + ", '" + f_ngay_yyyymmdd + "'))";
        }
        public string for_num_ngay_yyyymmdd(string ngay)
        {
            return "date('" + for_ngay_yyyymmdd(ngay) + "')";
            //return "date(to_char(" + ngay + ", '" + f_ngay_yyyymmdd + "'))";

        }
        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }
        public string f_ngay_yyyymmdd = "yyyymmdd";
        public bool bICDNguyennhan
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=100");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bNgayra_ngayvao_1
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=278");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public bool bCapcuu_noitru
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=150");
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1;
                }
                catch { return false; }
            }
        }
        public long get_maql_phongluu(string ngay, long maql)
        {
            long mavaovien = 0;
            ds = get_data("select mavaovien from " + user + ".benhandt where maql=" + maql);
            if (ds.Tables[0].Rows.Count > 0) mavaovien = long.Parse(ds.Tables[0].Rows[0]["mavaovien"].ToString());
            if (bMmyy(ngay))
            {
                ds = get_data("select maql from " + user + mmyy(ngay) + ".benhancc where mavaovien=" + mavaovien);
                if (ds.Tables[0].Rows.Count > 0) return long.Parse(ds.Tables[0].Rows[0]["maql"].ToString());
                else return 0;
            }
            else return 0;
        }

        #endregion

        #region Cap ID
        public long get_id_dlogin { get { return get_capid1(-30, "capid"); } }
        private long get_capid1(int m_ma)
        {
            sql = "update " + user + ".d_capid set id=id+1 where ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + ".d_capid(ma,ten,id,computer) values (:m_ma,:m_ten,1,:m_computer)";
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                cmd.Parameters.Add("m_ten", NpgsqlDbType.Varchar, 20).Value = sComputer;
                cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = sComputer;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            sql = "select id from " + user + ".d_capid where ma=" + m_ma;
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dest = new NpgsqlDataAdapter(cmd);
            ds = new DataSet();
            dest.Fill(ds);
            cmd.Dispose();
            con.Close(); con.Dispose();
            return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public long get_capid1(int m_ma, string m_table)
        {
            sql = "update " + user + "." + m_table + " set id=id+1 where ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + "." + m_table + "(ma,yy,loai,id) values (:m_ma,'??',:m_loai,1)";
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                cmd.Parameters.Add("m_loai", NpgsqlDbType.Varchar, 20).Value = sComputer;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            sql = "select id from " + user + "." + m_table + " where ma=" + m_ma;
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dest = new NpgsqlDataAdapter(cmd);
            ds = new DataSet();
            dest.Fill(ds);
            cmd.Dispose();
            con.Close(); con.Dispose();
            return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public string get_madstt(string tendstt)
        {
            if (tendstt == "") return "";
            try
            {
                sql = "select mabv from " + user + ".dstt where trim(tenbv)=:tendstt";
                con = new NpgsqlConnection(sConn);
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("tendstt", NpgsqlDbType.Text).Value = tendstt.Trim();
                dest = new NpgsqlDataAdapter(cmd);
                ds = new DataSet();
                dest.Fill(ds);
                cmd.Dispose();
                con.Close(); con.Dispose();
                return ds.Tables[0].Rows[0][0].ToString().Trim();
            }
            catch { return ""; };
        }
        public long get_id_phieuxuat(string d_ngay, int d_makp, int d_nhom, string d_loai, string d_phieu, string d_kho, string d_mmyy)
        {
            sql = "select id from " + user + d_mmyy + ".d_phieuxuat where makp=" + d_makp + " and nhom=" + d_nhom;
            if (d_loai != "") sql += " and loai='" + d_loai + "'";
            if (d_phieu != "") sql += " and phieu='" + d_phieu + "'";
            if (d_kho != "") sql += " and kho='" + d_kho + "'";
            sql += " and to_char(ngay,'dd/mm/yyyy')='" + d_ngay.Substring(0, 10) + "'";
            ds = get_data(sql);
            if (ds.Tables[0].Rows.Count == 0) return 0;
            else return long.Parse(ds.Tables[0].Rows[0]["id"].ToString());
        }        
        public long get_capid(int m_ma, string m_yy)
        {
            sql = "update " + user + ".capid set id=id+1 where ma=:m_ma and yy=:m_yy";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
            cmd.Parameters.Add("m_yy", NpgsqlDbType.Varchar).Value = m_yy;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + ".capid(ma,yy,loai,id) values (:m_ma,:m_yy,'?',1)";
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                cmd.Parameters.Add("m_yy", NpgsqlDbType.Varchar).Value = m_yy;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            con.Close(); con.Dispose();
            return long.Parse(get_data("select id from " + user + ".capid where yy='" + m_yy + "'" + " and ma=" + m_ma).Tables[0].Rows[0][0].ToString());
        }
        public long get_id_phieuxuat() { return long.Parse(iRownum.ToString().PadLeft(3, '0') + get_capid_may(17, sComputer).ToString().PadLeft(9, '0')); }
        private long get_capid_may(int m_ma, string m_computer)
        {
            sql = "update " + user + ".d_capid set id=id+1 where ma=:m_ma and computer=:m_computer";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
            cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = m_computer;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + ".d_capid(ma,ten,id,computer) values (:m_ma,:m_ten,1,:m_computer)";
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                cmd.Parameters.Add("m_ten", NpgsqlDbType.Varchar, 20).Value = m_computer;
                cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = m_computer;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            sql = "select id from " + user + ".d_capid where ma=" + m_ma + " and computer='" + m_computer + "'";
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dest = new NpgsqlDataAdapter(cmd);
            ds = new DataSet();
            dest.Fill(ds);
            cmd.Dispose();
            con.Close(); con.Dispose();
            return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public long get_capid(int m_ma)
        {
            sql = "update " + user + ".capid set id=id+1 where ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + ".capid(ma,yy,loai,id) values (:m_ma,'??',:m_loai,1)";
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                cmd.Parameters.Add("m_loai", NpgsqlDbType.Varchar, 20).Value = sComputer;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            sql = "select id from " + user + ".capid where ma=" + m_ma;
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            dest = new NpgsqlDataAdapter(cmd);
            ds = new DataSet();
            dest.Fill(ds);
            cmd.Dispose();
            con.Close(); con.Dispose();
            return long.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        #endregion

        #region encode
        public string encode(string s)
        {
            string s1 = "";
            char c;
            byte b;
            s = s.Trim();
            for (int i = 0; i < s.Length; i++)
            {
                c = Convert.ToChar(s.Substring(i, 1).ToUpper());
                b = (byte)(c);
                s1 = s1 + Convert.ToChar((b % 2 == 0) ? b + 128 : b + 96);
            }
            return s1;
        }

        public string decode(string s)
        {
            string s1 = "";
            char c;
            byte b;
            s = s.Trim();
            for (int i = 0; i < s.Length; i++)
            {
                c = Convert.ToChar(s.Substring(i, 1));
                b = (byte)(c);
                s1 = s1 + Convert.ToChar((b % 2 == 0) ? b - 128 : b - 96);
            }
            return s1;
        }
        #endregion

        #region Khác
        public string user { get { return userid; } }
        public bool bMahoa
        {
            get
            {
                ds = get_data("select ten from " + user + ".thongso where id=-13");
                if (ds.Tables[0].Rows.Count == 0) return false;
                return ds.Tables[0].Rows[0][0].ToString() == "1";
            }
        }
        public bool bBHYT(string madoituong )
        {
            try{
                return get_data("select donvi from " + user + ".doituong where madoituong="+madoituong).Tables[0].Rows[0][0].ToString() == "1"?true:false;
            }
            catch{
                return false;
            }
            
        }
        public string NhomvpCongkham
        {
            get
            {
                try
                {
                    return get_data("select ten from " + user + ".bhyt_thongso where id=10").Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    return "";
                }
            }

        }
        public string TenNhomvpCongkham
        {
            get
            {
                try
                {
                    return get_data("select ten from " + user + ".v_nhomvp where ma="+NhomvpCongkham).Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    return "";
                }
            }

        }
        public int iNgaykiemke
        {
            get
            {
                ds = get_data("select ten from " + user + ".d_thongso where id=105");
                if (ds.Tables[0].Rows.Count > 0) return int.Parse(ds.Tables[0].Rows[0]["ten"].ToString());
                else return 7;
            }
        }
        public bool bNoiNgoai_Hang(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=55 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bNoiNgoai_Nuoc(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=123 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString().Trim() == "1";
        }
        public void ins_thongso(int d_nhom, int d_rec)
        {
            for (int i = 1; i < d_rec; i++)
                if (get_data("select * from " + user + ".d_thongso where nhom=" + d_nhom + " and id=" + i).Tables[0].Rows.Count == 0) upd_thongso(d_nhom, i, "", "0");
        }
        public void ins_thongso(int d_nhom, int tu, int den)
        {
            for (int i = tu; i <= den; i++)
                if (get_data("select * from " + user + ".d_thongso where nhom=" + d_nhom + " and id=" + i).Tables[0].Rows.Count == 0) upd_thongso(d_nhom, i, "", "0");
        }
        public bool bQuanlynguon(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=17 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public bool bDutrumua_thangtruoc(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=33 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public string Mmyy_truoc(string d_mmyy)
        {
            int t_mm, t_yy;
            string mm = d_mmyy.Substring(0, 2), yy = d_mmyy.Substring(2, 2);
            if (mm == "01")
            {
                t_mm = 12; t_yy = int.Parse(yy) - 1;
            }
            else
            {
                t_mm = int.Parse(mm) - 1; t_yy = int.Parse(yy);
            }
            return t_mm.ToString().PadLeft(2, '0') + t_yy.ToString().PadLeft(2, '0');
        }
        public string Mmyy_sau(string d_mmyy)
        {
            int t_mm, t_yy;
            string mm = d_mmyy.Substring(0, 2), yy = d_mmyy.Substring(2, 2);
            if (mm == "12")
            {
                t_mm = 01; t_yy = int.Parse(yy) + 1;
            }
            else
            {
                t_mm = int.Parse(mm) + 1; t_yy = int.Parse(yy);
            }
            return t_mm.ToString().PadLeft(2, '0') + t_yy.ToString().PadLeft(2, '0');
        }
        public int get_stt(DataTable dt, string cot)
        {
            try
            {
                if (dt.Rows.Count == 0) return 1;
                else return int.Parse(dt.Rows[dt.Rows.Count - 1][cot].ToString()) + 1;
            }
            catch { return 1; }
        }
        public int get_stt(DataTable dt)
        {
            if (dt.Rows.Count == 0) return 1;
            else return int.Parse(dt.Rows[dt.Rows.Count - 1]["stt"].ToString()) + 1;
        }
        public DataSet get_sum(DataSet m_ds, string[] m_id, string[] m_col)
        {
            DataSet ds = m_ds.Clone();
            foreach (DataColumn c in ds.Tables[0].Columns) c.DefaultValue = 0;
            foreach (DataColumn c in m_ds.Tables[0].Columns) c.DefaultValue = 0;
            DataRow r1;
            string sql = "";
            foreach (DataRow r in m_ds.Tables[0].Rows)
            {
                sql = "";
                foreach (string s_col in m_id)
                {
                    if (sql == "")
                        sql += (s_col + "='" + r[s_col].ToString() + "'");
                    else
                        sql += (" and " + (s_col + "='" + r[s_col].ToString() + "'"));
                }
                r1 = getrowbyid(ds.Tables[0], sql);
                if (r1 != null)
                {
                    foreach (string col in m_col)
                    {
                        r1[col] = decimal.Parse(r1[col].ToString()) + decimal.Parse(r[col].ToString());
                    }
                }
                else
                {
                    r1 = ds.Tables[0].NewRow();
                    foreach (DataColumn c in ds.Tables[0].Columns) r1[c.ColumnName] = r[c.ColumnName];
                    ds.Tables[0].Rows.Add(r1);
                }
            }
            return ds;
        }
    
        public string title(string tu, string den)
        {
            tu = tu.PadLeft(2, '0'); den = den.PadLeft(2, '0');
            if (tu == den) return "Tháng " + tu;
            else if (tu == "01")
            {
                if (den == "03") return "Quí 1";
                else if (den == "06") return "6 tháng đầu";
                else if (den == "12") return "";
                else return "Từ tháng " + tu + " đến " + den;
            }
            else if (tu == "04" && den == "06") return "Quí 2";
            else if (tu == "07" && den == "09") return "Quí 3";
            else if (tu == "10" && den == "12") return "Quí 4";
            else if (tu == "06" && den == "12") return "6 tháng sau";
            else return "Từ tháng " + tu + " đến " + den;
        }
        #endregion

        #region kiem tra so
        public void MaskDigit(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8) e.Handled = false;
            else e.Handled = true;
        }
        public void MaskDecimal(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8) e.Handled = false;
            else e.Handled = true;
        }

        public string so_chu(string s)
        {
            string ret = "0";
            int i = 0, l = s.Length;
            while (i < l)
            {
                if (Char.IsDigit(Convert.ToChar(s.Substring(i, 1)))) ret += s.Substring(i, 1);
                else break;
                i++;
            }
            return ret;
        }
        #endregion

        #region Hàm lấy mmyy
        public bool bMmyy(string mmyy)
        {
            return get_data("select * from " + user + ".table where mmyy='" + mmyy + "'").Tables[0].Rows.Count > 0;
        }
        public string get_mmyy(string v_ngay)
        {
 
            return v_ngay.Substring(3, 2) + v_ngay.Substring(8, 2);
      
        }
        public string mmyy(string ngay)
        {
            if (ngay.Length == 4) return ngay;
            else return ngay.Substring(3, 2) + ngay.Substring(8, 2);
        }
        #endregion

        #region Hàm lấy ngày giờ
        public string onlyso(string s)
        {
            string ret = "", s1 = " 0123456789";
            for (int i = 0; i < s.Length; i++)
                if (s1.IndexOf(s.Substring(i, 1)) != -1) ret += s.Substring(i, 1);
            return ret;
        }
        public string DateToString(string format, System.DateTime date)
        {
            if (date.Equals(null)) return "";
            else return date.ToString(format, System.Globalization.DateTimeFormatInfo.CurrentInfo);
        }
        public System.DateTime StringToDate(string s)
        {
            s = (s == "") ? ngayhienhanh_server.Substring(0, 10) : s;
            string[] format ={ "dd/MM/yyyy" };
            return System.DateTime.ParseExact(s.ToString().Substring(0, 10), format, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
        }
        public Int64 songay(DateTime d1, DateTime d2, int congthem)
        {
            try
            {
                return Convert.ToInt64(d1.ToOADate() - d2.ToOADate() + congthem);
            }
            catch { return 0; }
        }
        public string DateToString(System.DateTime date)
        {
            string format = "dd/MM/yyyy";
            return date.ToString(format, System.Globalization.DateTimeFormatInfo.CurrentInfo);
        }
        public string ngayhienhanh_server
        {
            get
            {
                return get_data("select to_char(now(),'dd/mm/yyyy hh24:mi')").Tables[0].Rows[0][0].ToString();
            }
        }
        public string f_ngay
        {
            get
            {
                ds = get_data("select ten from " + user + ".thongso where id=-2");
                if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
                else return "dd/mm/yyyy";
            }
        }
        public string f_ngaygio
        {
            get
            {
                ds = get_data("select ten from " + user + ".thongso where id=-3");
                if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["ten"].ToString().Trim();
                else return "dd/mm/yyyy hh24:mi";
            }
        }
        public string for_ngay(string ngay, string time)
        {
            time = time.Replace("'", "");            
            return "to_date(to_char(" + ngay + ", '" + time + "'), '" + time + "')";
        }
        public string for_num_ngay(string ngay, string time)
        {
            return "to_date(to_char(" + ngay + ", " + time + "), " + time + ")";
        }
        public string sGiobaocao
        {
            get
            {
                try
                {
                    DataSet ds = get_data("select ten from " + user + ".thongso where id=138");
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                catch { return "00:00"; }
            }
        }
        public bool bNgay(string ngayvao, string ngaysinh)
        {
            int d1 = DateTime.Now.Day;
            int m1 = DateTime.Now.Month;
            int y1 = DateTime.Now.Year;
            if (ngayvao != "")
            {
                y1 = int.Parse(ngayvao.Substring(6, 4));
                m1 = int.Parse(ngayvao.Substring(3, 2));
                d1 = int.Parse(ngayvao.Substring(0, 2));
            }
            int d2 = int.Parse(ngaysinh.Substring(0, 2));
            int m2 = int.Parse(ngaysinh.Substring(3, 2));
            int y2 = int.Parse(ngaysinh.Substring(6, 4));

            if (y2 > y1) return false;
            else if (y2 < y1) return true;
            if (m2 > m1) return false;
            else if (m2 < m1) return true;
            if (d2 > d1) return false;
            return true;
        }
        public bool bNgay(string ngay)
        {
            try
            {
                if (ngay.IndexOf("_") != -1) return false;
                int len = ngay.Length;
                if (len == 0) return false;
                string dd = ngay.Substring(0, 2), mm = ngay.Substring(3, 2), yyyy = ngay.Substring(6, 4);
                string s31 = "01+03+05+07+08+10+12+", s30 = "04+06+09+11+", s2829 = (int.Parse(yyyy) % 4 == 0) ? "29" : "28";
                if (int.Parse(yyyy.Substring(0, 1)) < 1) return false;
                if (int.Parse(mm) < 1 || int.Parse(mm) > 12) return false;
                if (s31.IndexOf(mm + "+") > -1)
                {
                    if (int.Parse(dd) < 0 || int.Parse(dd) > 31) return false;
                }
                else if (s30.IndexOf(mm + "+") > -1)
                {
                    if (int.Parse(dd) < 0 || int.Parse(dd) > 30) return false;
                }
                else if (int.Parse(dd) < 0 || int.Parse(dd) > int.Parse(s2829)) return false;
                if (len > 10)
                {
                    string hh = ngay.Substring(11, 2), MM = ngay.Substring(14, 2);
                    if (int.Parse(hh) > 23) return false;
                    if (int.Parse(MM) > 59) return false;
                }
                return true;
            }
            catch { return false; };
        }
        public string Ktngaygio(string s, int len)
        {
            try
            {
                string s1 = onlyso(s);
                if (len == 10)
                    return s1.Substring(0, 2).Trim().PadLeft(2, '0') + "/" + s1.Substring(2, 2).Trim().PadLeft(2, '0') + "/" + s1.Substring(4).Trim().PadLeft(4, '0');
                else
                    return s1.Substring(0, 2).Trim().PadLeft(2, '0') + "/" + s1.Substring(2, 2).Trim().PadLeft(2, '0') + "/" + s1.Substring(4, 4).Trim().PadLeft(4, '0') + " " + s1.Substring(9, 2).Trim().PadLeft(2, '0') + ":" + s1.Substring(11, 2).Trim().PadLeft(2, '0');
            }
            catch { return s; }
        }
        public string Ngay_hethong { get { return ngayhienhanh_server.Substring(0, 10); } }
        public string Ngay_hethong_gio { get { return ngayhienhanh_server; } }
        public System.DateTime StringToDateTime(string s)
        {
            string[] format1 ={ "dd/MM/yyyy" }, format2 ={ "dd/MM/yyyy HH:mm" };
            return System.DateTime.ParseExact(s.ToString(), (s.Length == 10) ? format1 : format2, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
        }
        public bool ngay(DateTime d1, DateTime d2, int so)
        {
            return (Math.Abs(songay(d1, StringToDate(d2.Day.ToString().PadLeft(2, '0') + "/" + d2.Month.ToString().PadLeft(2, '0') + "/" + d2.Year.ToString()), 0)) <= so);
        }

        #endregion

        #region Read XML
        public string Maincode(string sql)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("..\\..\\..\\xml\\maincode.xml");
            XmlNodeList nodeLst = doc.GetElementsByTagName(sql);
            return nodeLst.Item(0).InnerText;
        }
        public string Syte
        {
            get
            {
                try
                {
                    return get_data("select ten from medibv.thongso where id=4").Tables[0].Rows[0]["Ten"].ToString();
                }
                catch
                {
                    return Maincode("Syte");
                }
            }
        }
        public string Tenbv
        {
            get
            {
                try
                {
                    return get_data("select ten from medibv.thongso where id=3").Tables[0].Rows[0]["Ten"].ToString();
                }
                catch
                {
                    return Maincode("Tenbv");
                }
            }
        }
        public string Mabv
        {
            get
            {
                try
                {
                    return get_data("select ten from medibv.thongso where id=2").Tables[0].Rows[0]["Ten"].ToString();
                }
                catch
                {
                    return Maincode("Mabv");
                }
            }
        }
        public int Mabv_so
        {
            get
            {
                string s = "";
                for (int i = 0; i < Mabv.Length; i++)
                    s += (Mabv.Substring(i, 1) == ".") ? "" : Mabv.Substring(i, 1);
                return int.Parse(s);
            }
        }
        public string Diachi
        {
            get
            {
                try
                {
                    return get_data("select ten from medibv.thongso where id=5").Tables[0].Rows[0]["Ten"].ToString();
                }
                catch
                {
                    return Maincode("Diachi");
                }
            }
        }
        #endregion ReadXMl

        #region Cập nhật dữ liệu
        public int get_dmcomputer()
        {
            sql = "update " + user + ".dmcomputer set computer=:m_computer where computer=:m_computer";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = sComputer;
            int irec = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close(); con.Dispose();
            if (irec == 0)
            {
                sql = "insert into " + user + ".dmcomputer(id,computer) values (" + get_id_dmcomputer + ",'" + sComputer + "')";
                execute_data(sql);
            }
            return int.Parse(get_data("select id from " + user + ".dmcomputer where computer='" + sComputer + "'").Tables[0].Rows[0][0].ToString());
        }
        public bool upd_thongso(int d_nhom, int d_id, string d_loai, string d_ten)
        {
            sql = "update " + user + ".d_thongso set loai=:d_loai,ten=:d_ten where nhom=:d_nhom and id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_loai", NpgsqlDbType.Text).Value = d_loai;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".d_thongso (id,nhom,loai,ten) values (:d_id,:d_nhom,:d_loai,:d_ten)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                    cmd.Parameters.Add("d_loai", NpgsqlDbType.Text).Value = d_loai;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "d_thongso");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_bhyt_thongso(int d_nhom, int d_id, string d_loai, string d_ten)
        {
            sql = "update " + user + ".bhyt_thongso set loai=:d_loai,ten=:d_ten where nhom=:d_nhom and id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_loai", NpgsqlDbType.Text).Value = d_loai;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bhyt_thongso (id,nhom,loai,ten) values (:d_id,:d_nhom,:d_loai,:d_ten)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                    cmd.Parameters.Add("d_loai", NpgsqlDbType.Text).Value = d_loai;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bhyt_thongso");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_bc_bv_Right(string id, string id_menu, string id_cha, decimal stt, string ten)
        {
            sql = "update " + user + ".bc_bv_right set id_menu=:id_menu,id_cha=:id_cha, stt=:stt,ten=:ten where id=:id ";
            con = new NpgsqlConnection(sConn);
            try
            {
                try
                {
                    con.Open();
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("id", NpgsqlDbType.Varchar).Value = id;
                    cmd.Parameters.Add("id_menu", NpgsqlDbType.Varchar).Value = id_menu;
                    cmd.Parameters.Add("id_cha", NpgsqlDbType.Varchar).Value = id_cha;
                    cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = stt;
                    cmd.Parameters.Add("ten", NpgsqlDbType.Text).Value = ten;
                    int num = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (num == 0)
                    {
                        sql = "insert into " + user + ".bc_bv_right(id,id_menu,id_cha,stt,ten) values (:id,:id_menu,:id_cha,:stt,:ten)";
                        cmd = new NpgsqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("id", NpgsqlDbType.Varchar).Value = id;
                        cmd.Parameters.Add("id_menu", NpgsqlDbType.Varchar).Value = id_menu;
                        cmd.Parameters.Add("id_cha", NpgsqlDbType.Varchar).Value = id_cha;
                        cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = stt;
                        cmd.Parameters.Add("ten", NpgsqlDbType.Text).Value = ten;
                        num = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
                catch (NpgsqlException exception)
                {
                    upd_error(exception.Message, sComputer, "bc_bv_right");
                    return false;
                }
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return true;
        }
        private int get_id_dmcomputer
        {
            get
            {
                ds = get_data("select max(id) as id from " + user + ".dmcomputer");
                if (ds.Tables[0].Rows[0]["id"].ToString() == "") return 1;
                else return int.Parse(ds.Tables[0].Rows[0]["id"].ToString()) + 1;
            }
        }
        public void upd_dmcomputer(string m_computer)
        {
            sql = "update " + user + ".dmcomputer set computer=:m_computer where computer=:m_computer";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = m_computer;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close(); con.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".dmcomputer(id,computer) values (" + get_id_dmcomputer + ",'" + m_computer + "')";
                    execute_data(sql);
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "dmcomputer");
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
        }
        public void upd_error(string m_message, string m_computer, string m_table)
        {
            if (con != null)
            {
                con.Close(); con.Dispose();
            }
            sql = "insert into " + user + ".error(message,computer,tables) values (:m_message,:m_computer,:m_table)";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_message", NpgsqlDbType.Text).Value = m_message;
                cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = m_computer;
                cmd.Parameters.Add("m_table", NpgsqlDbType.Varchar, 20).Value = m_table;
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                cmd.Dispose();
                con.Close(); 
                con.Dispose();                
            }
        }
        public void upd_error(string m_mmyy, string m_message, string m_computer, string m_table)
        {
            if (con != null)
            {
                con.Close(); con.Dispose();
            }
            sql = "insert into " + user + m_mmyy + ".d_error(message,computer,tables) values (:m_message,:m_computer,:m_table)";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_message", NpgsqlDbType.Text).Value = m_message;
                cmd.Parameters.Add("m_computer", NpgsqlDbType.Varchar, 20).Value = m_computer;
                cmd.Parameters.Add("m_table", NpgsqlDbType.Varchar, 20).Value = m_table;
                cmd.ExecuteNonQuery();
            }
            catch { }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
        }
        public bool upd_bc_dmbv(string d_mabv, decimal d_stt, string d_tenbv, string d_ten_menu, string d_id_menu)
        {
            sql = "update " + user + ".bc_dmbv set tenbv=:d_tenbv,ten_menu=:d_ten_menu,id_menu=:d_id_menu";
            sql += " where mabv=:d_mabv and stt=:d_stt";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_mabv", NpgsqlDbType.Varchar, 10).Value = d_mabv;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_tenbv", NpgsqlDbType.Text).Value = d_tenbv;
                cmd.Parameters.Add("d_ten_menu", NpgsqlDbType.Text).Value = d_ten_menu;
                cmd.Parameters.Add("d_id_menu", NpgsqlDbType.Varchar, 4).Value = d_id_menu;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_dmbv (mabv,stt,tenbv,ten_menu,id_menu)";
                    sql += " values (:d_mabv,:d_stt,:d_tenbv,:d_ten_menu,:d_id_menu)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_mabv", NpgsqlDbType.Varchar, 10).Value = d_mabv;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.Parameters.Add("d_tenbv", NpgsqlDbType.Text).Value = d_tenbv;
                    cmd.Parameters.Add("d_ten_menu", NpgsqlDbType.Text).Value = d_ten_menu;
                    cmd.Parameters.Add("d_id_menu", NpgsqlDbType.Varchar, 4).Value = d_id_menu;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_dmbv");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public void updrec_right(DataTable dt, long id, string right)
        {
            string exp = "id=" + id;
            DataRow r = getrowbyid(dt, exp);
            if (r == null)
            {
                DataRow nrow = dt.NewRow();
                nrow[0] = id;
                nrow[4] = right;
                dt.Rows.Add(nrow);
            }
            else
            {
                DataRow[] dr = dt.Select(exp);
                dr[0][4] = right;
            }
        }
        public bool upd_bc_dlogin_right(long d_id, string d_right_)
        {
            sql = "update " + user + ".bc_dlogin set right_=:d_right_";
            sql += " where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_right_", NpgsqlDbType.Text).Value = d_right_;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "d_dlogin");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_bc_dlogin(long d_id, string d_hoten, string d_userid, string d_password_, string d_right_, int d_nhomkho, string d_makho, string d_makp, int d_tao, int d_admin, string d_manhom, string d_loaint, string d_loaikhac, string d_loaivp, string d_loaicdha)
        {
            sql = "update " + user + ".bc_dlogin set hoten=:d_hoten,userid=:d_userid,password_=:d_password_,";
            sql += "right_=:d_right_,nhomkho=:d_nhomkho,makho=:d_makho,makp=:d_makp,tao=:d_tao,admin=:d_admin,";
            sql += "manhom=:d_manhom,loaint=:d_loaint,loaikhac=:d_loaikhac,loaivp=:d_loaivp,loaicdha=:d_loaicdha";
            sql += " where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_hoten", NpgsqlDbType.Text).Value = d_hoten;
                cmd.Parameters.Add("d_userid", NpgsqlDbType.Varchar, 20).Value = d_userid;
                cmd.Parameters.Add("d_password_", NpgsqlDbType.Varchar, 20).Value = d_password_;
                cmd.Parameters.Add("d_right_", NpgsqlDbType.Text).Value = d_right_;
                cmd.Parameters.Add("d_nhomkho", NpgsqlDbType.Numeric).Value = d_nhomkho;
                cmd.Parameters.Add("d_makho", NpgsqlDbType.Text).Value = d_makho;
                cmd.Parameters.Add("d_makp", NpgsqlDbType.Text).Value = d_makp;
                cmd.Parameters.Add("d_tao", NpgsqlDbType.Numeric).Value = d_tao;
                cmd.Parameters.Add("d_admin", NpgsqlDbType.Numeric).Value = d_admin;
                cmd.Parameters.Add("d_manhom", NpgsqlDbType.Text).Value = d_manhom;
                cmd.Parameters.Add("d_loaint", NpgsqlDbType.Text).Value = d_loaint;
                cmd.Parameters.Add("d_loaikhac", NpgsqlDbType.Text).Value = d_loaikhac;
                cmd.Parameters.Add("d_loaivp", NpgsqlDbType.Text).Value = d_loaivp;
                cmd.Parameters.Add("d_loaicdha", NpgsqlDbType.Text).Value = d_loaicdha;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;

                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_dlogin (id,hoten,userid,password_,right_,nhomkho,makho,makp,tao,admin,manhom,loaint,loaikhac,loaivp,loaicdha) values (:d_id,:d_hoten,:d_userid,:d_password_,:d_right_,:d_nhomkho,:d_makho,:d_makp,:d_tao,:d_admin,:d_manhom,:d_loaint,:d_loaikhac,:d_loaivp,:d_loaicdha)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_hoten", NpgsqlDbType.Text).Value = d_hoten;
                    cmd.Parameters.Add("d_userid", NpgsqlDbType.Varchar, 20).Value = d_userid;
                    cmd.Parameters.Add("d_password_", NpgsqlDbType.Varchar, 20).Value = d_password_;
                    cmd.Parameters.Add("d_right_", NpgsqlDbType.Text).Value = d_right_;
                    cmd.Parameters.Add("d_nhomkho", NpgsqlDbType.Numeric).Value = d_nhomkho;
                    cmd.Parameters.Add("d_makho", NpgsqlDbType.Text).Value = d_makho;
                    cmd.Parameters.Add("d_makp", NpgsqlDbType.Text).Value = d_makp;
                    cmd.Parameters.Add("d_tao", NpgsqlDbType.Numeric).Value = d_tao;
                    cmd.Parameters.Add("d_admin", NpgsqlDbType.Numeric).Value = d_admin;
                    cmd.Parameters.Add("d_manhom", NpgsqlDbType.Text).Value = d_manhom;
                    cmd.Parameters.Add("d_loaint", NpgsqlDbType.Text).Value = d_loaint;
                    cmd.Parameters.Add("d_loaikhac", NpgsqlDbType.Text).Value = d_loaikhac;
                    cmd.Parameters.Add("d_loaivp", NpgsqlDbType.Text).Value = d_loaivp;
                    cmd.Parameters.Add("d_loaicdha", NpgsqlDbType.Text).Value = d_loaicdha;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_dlogin");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public void updrec(DataTable dt, long id, string userid, string password, string right, int nhomkho, string makho, string makp, string loaivp, string loaicdha)
        {
            string exp = "id=" + id;
            DataRow r = getrowbyid(dt, exp);
            if (r == null)
            {
                DataRow nrow = dt.NewRow();
                nrow["id"] = id;
                nrow["userid"] = userid;
                nrow["password_"] = password;
                nrow["right_"] = right;
                nrow["nhomkho"] = nhomkho;
                nrow["makho"] = makho;
                nrow["makp"] = makp;
                nrow["loaivp"] = loaivp;
                nrow["loaicdha"] = loaicdha;
                dt.Rows.Add(nrow);
            }
            else
            {
                DataRow[] dr = dt.Select(exp);
                dr[0]["right_"] = right;
            }
        }
        public void delrec(DataTable dt, string exp)
        {
            try
            {
                DataRow[] r = dt.Select(exp);
                for (int i = 0; i < r.Length; i++) r[i].Delete();
            }
            catch { }
        }
        public bool upd_bc_Nhomkp(long v_id, decimal v_stt, string v_ten, string v_makp)
        {
            sql = "update " + user + ".bc_nhomkp set stt=:v_stt,ten=:v_ten,makp=:v_makp where id=:v_id";
            con = new NpgsqlConnection(sConn);
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
            cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
            cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
            cmd.Parameters.Add("v_makp", NpgsqlDbType.Varchar, 100).Value = v_makp;

            try
            {
                con.Open();
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_nhomkp(id,stt,ten,makp) values(:v_id,:v_stt,:v_ten,:v_makp)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
                    cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
                    cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
                    cmd.Parameters.Add("v_makp", NpgsqlDbType.Varchar, 100).Value = v_makp;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_nhomkp");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_maubaocao(long v_id, string v_ma, string v_ten, string v_loai)
        {
            sql = "update " + user + ".bc_maubaocao set ma=:v_ma,ten=:v_ten,loai=:v_loai where id=:v_id";
            con = new NpgsqlConnection(sConn);
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
            cmd.Parameters.Add("v_ma", NpgsqlDbType.Varchar, 50).Value = v_ma;
            cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 254).Value = v_ten;
            cmd.Parameters.Add("v_loai", NpgsqlDbType.Varchar, 100).Value = v_loai;

            try
            {
                con.Open();
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_maubaocao(id,ma,ten,loai) values(:v_id,:v_ma,:v_ten,:v_loai)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
                    cmd.Parameters.Add("v_ma", NpgsqlDbType.Varchar, 50).Value = v_ma;
                    cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 254).Value = v_ten;
                    cmd.Parameters.Add("v_loai", NpgsqlDbType.Varchar, 100).Value = v_loai;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_maubaocao");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_dmkhac(long v_id, string v_id_khac)
        {
            sql = "update " + user + ".bc_dmkhac set id_khac=:v_id_khac where id=:v_id";
            con = new NpgsqlConnection(sConn);
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
            cmd.Parameters.Add("v_id_khac", NpgsqlDbType.Varchar, 100).Value = v_id_khac;

            try
            {
                con.Open();
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_dmkhac(id,id_khac) values(:v_id,:v_id_khac)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
                    cmd.Parameters.Add("v_id_khac", NpgsqlDbType.Varchar, 100).Value = v_id_khac;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_dmkhac");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_loainhanvien(long v_id, decimal v_stt, string v_ten, string v_mabs)
        {
            sql = "update " + user + ".bc_loainhanvien set stt=:v_stt,ten=:v_ten,mabs=:v_mabs where id=:v_id";
            con = new NpgsqlConnection(sConn);
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
            cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
            cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
            cmd.Parameters.Add("v_mabs", NpgsqlDbType.Varchar, 2000).Value = v_mabs;

            try
            {
                con.Open();
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_loainhanvien(id,stt,ten,mabs) values(:v_id,:v_stt,:v_ten,:v_mabs)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
                    cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
                    cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
                    cmd.Parameters.Add("v_mabs", NpgsqlDbType.Varchar, 2000).Value = v_mabs;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_loainhanvien");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_bc_Nhomdoituong(long v_id, decimal v_stt, string v_ten, string v_madoituong)
        {
            sql = "update " + user + ".bc_nhomdoituong set stt=:v_stt,ten=:v_ten,madoituong=:v_madoituong where id=:v_id";
            con = new NpgsqlConnection(sConn);
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
            cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
            cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
            cmd.Parameters.Add("v_madoituong", NpgsqlDbType.Varchar, 100).Value = v_madoituong;

            try
            {
                con.Open();
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_nhomdoituong(id,stt,ten,madoituong) values(:v_id,:v_stt,:v_ten,:v_madoituong)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("v_id", NpgsqlDbType.Numeric).Value = v_id;
                    cmd.Parameters.Add("v_stt", NpgsqlDbType.Numeric).Value = v_stt;
                    cmd.Parameters.Add("v_ten", NpgsqlDbType.Varchar, 100).Value = v_ten;
                    cmd.Parameters.Add("v_madoituong", NpgsqlDbType.Varchar, 100).Value = v_madoituong;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_nhomkp");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_dutrukho(int d_manguon, int d_makho, string d_mmyy, int d_mabd, decimal d_dau, decimal d_nhap, decimal d_xuat, decimal d_ton, decimal d_tutruc, decimal d_tc, decimal d_dongia, decimal d_l1, decimal d_d1, decimal d_l2, decimal d_d2, string d_nhacc, string d_donvi)
        {

            sql = "update medibv.d_dutrukho set nhap=:d_nhap,xuat=:d_xuat,ton=:d_ton,tutruc=:d_tutruc,tc=:d_tc,l1=:d_l1,d1=:d_d1,l2=:d_l2,d2=:d_d2,nhacc=:d_nhacc,donvi=:d_donvi,manguon=:d_manguon where makho=:d_makho and mmyy=:d_mmyy and mabd=:d_mabd";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_manguon", NpgsqlDbType.Numeric).Value = d_manguon;
                cmd.Parameters.Add("d_makho", NpgsqlDbType.Numeric).Value = d_makho;
                cmd.Parameters.Add("d_mmyy", NpgsqlDbType.Varchar, 4).Value = d_mmyy;
                cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                cmd.Parameters.Add("d_dau", NpgsqlDbType.Numeric).Value = d_dau;
                cmd.Parameters.Add("d_nhap", NpgsqlDbType.Numeric).Value = d_nhap;
                cmd.Parameters.Add("d_xuat", NpgsqlDbType.Numeric).Value = d_xuat;
                cmd.Parameters.Add("d_ton", NpgsqlDbType.Numeric).Value = d_ton;
                cmd.Parameters.Add("d_tutruc", NpgsqlDbType.Numeric).Value = d_tutruc;
                cmd.Parameters.Add("d_tc", NpgsqlDbType.Numeric).Value = d_tc;
                cmd.Parameters.Add("d_dongia", NpgsqlDbType.Numeric).Value = d_dongia;
                cmd.Parameters.Add("d_l1", NpgsqlDbType.Numeric).Value = d_l1;
                cmd.Parameters.Add("d_d1", NpgsqlDbType.Numeric).Value = d_d1;
                cmd.Parameters.Add("d_l2", NpgsqlDbType.Numeric).Value = d_l2;
                cmd.Parameters.Add("d_d2", NpgsqlDbType.Numeric).Value = d_d2;
                cmd.Parameters.Add("d_nhacc", NpgsqlDbType.Text).Value = d_nhacc;
                cmd.Parameters.Add("d_donvi", NpgsqlDbType.Text).Value = d_donvi;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (i == 0)
                {
                    sql = " insert into medibv.d_dutrukho(makho,mmyy,mabd,nhap,xuat,ton,tutruc,tc,l1,d1,l2,d2,nhacc,donvi,manguon) values (:d_makho,:d_mmyy,:d_mabd,:d_nhap,:d_xuat,:d_ton,:d_tutruc,:d_tc,:d_l1,:d_d1,:d_l2,:d_d2,:d_nhacc,:d_donvi,:d_manguon)";
                    //con.Open();
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_manguon", NpgsqlDbType.Numeric).Value = d_manguon;
                    cmd.Parameters.Add("d_makho", NpgsqlDbType.Numeric).Value = d_makho;
                    cmd.Parameters.Add("d_mmyy", NpgsqlDbType.Varchar, 4).Value = d_mmyy;
                    cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                    cmd.Parameters.Add("d_dau", NpgsqlDbType.Numeric).Value = d_dau;
                    cmd.Parameters.Add("d_nhap", NpgsqlDbType.Numeric).Value = d_nhap;
                    cmd.Parameters.Add("d_xuat", NpgsqlDbType.Numeric).Value = d_xuat;
                    cmd.Parameters.Add("d_ton", NpgsqlDbType.Numeric).Value = d_ton;
                    cmd.Parameters.Add("d_tutruc", NpgsqlDbType.Numeric).Value = d_tutruc;
                    cmd.Parameters.Add("d_tc", NpgsqlDbType.Numeric).Value = d_tc;
                    cmd.Parameters.Add("d_dongia", NpgsqlDbType.Numeric).Value = d_dongia;
                    cmd.Parameters.Add("d_l1", NpgsqlDbType.Numeric).Value = d_l1;
                    cmd.Parameters.Add("d_d1", NpgsqlDbType.Numeric).Value = d_d1;
                    cmd.Parameters.Add("d_l2", NpgsqlDbType.Numeric).Value = d_l2;
                    cmd.Parameters.Add("d_d2", NpgsqlDbType.Numeric).Value = d_d2;
                    cmd.Parameters.Add("d_nhacc", NpgsqlDbType.Text).Value = d_nhacc;
                    cmd.Parameters.Add("d_donvi", NpgsqlDbType.Text).Value = d_donvi;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "d_dutrukho");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_bc_nhombd(long d_id, string d_ten, int d_id_nhom, int d_stt, int d_ycu)
        {
            sql = "update " + user + ".bc_nhombd set ten=:d_ten,stt=:d_stt,ycu=:d_ycu where id=:d_id and id_nhom=:d_id_nhom";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_id_nhom", NpgsqlDbType.Numeric).Value = d_id_nhom;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.Parameters.Add("d_ycu", NpgsqlDbType.Numeric).Value = d_ycu;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_nhombd (id,ten,id_nhom,stt,ycu) values (:d_id,:d_ten,:d_id_nhom,:d_stt,:d_ycu)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_id_nhom", NpgsqlDbType.Numeric).Value = d_id_nhom;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_ycu", NpgsqlDbType.Numeric).Value = d_ycu;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_nhombd");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_bc_dmnhomnguon(long d_id, string d_ten, int d_id_nguon, int d_stt)
        {
            sql = "update " + user + ".bc_dmnhomnguon set ten=:d_ten,stt=:d_stt where id=:d_id and id_nguon=:d_id_nguon";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_id_nguon", NpgsqlDbType.Numeric).Value = d_id_nguon;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;

                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_dmnhomnguon (id,ten,id_nguon,stt) values (:d_id,:d_ten,:d_id_nguon,:d_stt)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_id_nguon", NpgsqlDbType.Numeric).Value = d_id_nguon;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_dmnhomnguon");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public void upd_bcbhyt(int l_id, int i_stt, string s_ten, int i_idnhomvp, string i_idloaivp)
        {
            string sql_upd = "", sql_int = "";
            int i = 0;
            sql_upd = "update medibv.dmbaocao_bhyt set stt=:i_stt,ten=:s_ten,idnhomvp=:i_idnhomvp,idloaivp=:i_idloaivp where id=:l_id";
            sql_int = "insert into medibv.dmbaocao_bhyt(id,stt,ten,idnhomvp,idloaivp) values(:l_id,:i_stt,:s_ten,:i_idnhomvp,:i_idloaivp)";
            NpgsqlConnection con = new NpgsqlConnection(sConn);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql_upd, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("l_id", NpgsqlDbType.Numeric, 3).Value = l_id;
            cmd.Parameters.Add("i_stt", NpgsqlDbType.Numeric, 3).Value = i_stt;
            cmd.Parameters.Add("s_ten", NpgsqlDbType.Text, 100).Value = s_ten;
            cmd.Parameters.Add("i_idnhomvp", NpgsqlDbType.Numeric, 7).Value = i_idnhomvp;
            cmd.Parameters.Add("i_idloaivp", NpgsqlDbType.Varchar, 80).Value = i_idloaivp;
            i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                cmd.Dispose();
                cmd = new NpgsqlCommand(sql_int, con);
                cmd.Parameters.Add("l_id", NpgsqlDbType.Numeric, 3).Value = l_id;
                cmd.Parameters.Add("i_stt", NpgsqlDbType.Numeric, 3).Value = i_stt;
                cmd.Parameters.Add("s_ten", NpgsqlDbType.Text, 100).Value = s_ten;
                cmd.Parameters.Add("i_idnhomvp", NpgsqlDbType.Numeric, 7).Value = i_idnhomvp;
                cmd.Parameters.Add("i_idloaivp", NpgsqlDbType.Varchar, 80).Value = i_idloaivp;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException exx)
                {
                    upd_error(exx.ToString(), sComputer, "dmbaocao_bhyt");
                }
            }
            cmd.Dispose();
            con.Dispose();
        }
        public bool upd_phieuxuat(string d_mmyy, long d_id, string d_ngay, int d_makp, int d_nhom, string d_loai, string d_phieu, string d_kho, decimal d_sotien, int d_userid)
        {
            string sql = user + d_mmyy + ".upd_phieuxuat";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.Parameters.Add("d_ngay", NpgsqlDbType.Varchar, 10).Value = d_ngay;
                cmd.Parameters.Add("d_makp", NpgsqlDbType.Numeric).Value = d_makp;
                cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                cmd.Parameters.Add("d_loai", NpgsqlDbType.Varchar, 10).Value = d_loai;
                cmd.Parameters.Add("d_phieu", NpgsqlDbType.Varchar, 20).Value = d_phieu;
                cmd.Parameters.Add("d_kho", NpgsqlDbType.Varchar, 20).Value = d_kho;
                cmd.Parameters.Add("d_sotien", NpgsqlDbType.Numeric).Value = d_sotien;
                cmd.Parameters.Add("d_userid", NpgsqlDbType.Numeric).Value = d_userid;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(d_mmyy, ex.Message, sComputer, "d_phieuxuat");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }

        public bool upd_phieuxuat(string d_mmyy, long d_id, string d_soct, string d_ngay, int d_makp, int d_nhom, string d_loai, string d_phieu, string d_kho, decimal d_sotien, string d_no, string d_co, string d_diengiai, int d_userid)
        {
            string sql = user + d_mmyy + ".upd_phieuxuat";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.Parameters.Add("d_soct", NpgsqlDbType.Varchar, 10).Value = d_soct;
                cmd.Parameters.Add("d_ngay", NpgsqlDbType.Varchar, 10).Value = d_ngay;
                cmd.Parameters.Add("d_makp", NpgsqlDbType.Numeric).Value = d_makp;
                cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                cmd.Parameters.Add("d_loai", NpgsqlDbType.Varchar, 10).Value = d_loai;
                cmd.Parameters.Add("d_phieu", NpgsqlDbType.Varchar, 20).Value = d_phieu;
                cmd.Parameters.Add("d_kho", NpgsqlDbType.Varchar, 20).Value = d_kho;
                cmd.Parameters.Add("d_sotien", NpgsqlDbType.Numeric).Value = d_sotien;
                cmd.Parameters.Add("d_no", NpgsqlDbType.Varchar, 20).Value = d_no;
                cmd.Parameters.Add("d_co", NpgsqlDbType.Varchar, 20).Value = d_co;
                cmd.Parameters.Add("d_diengiai", NpgsqlDbType.Text).Value = d_diengiai;
                cmd.Parameters.Add("d_userid", NpgsqlDbType.Numeric).Value = d_userid;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(d_mmyy, ex.Message, sComputer, "d_phieuxuat");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public void updrec_thekho(DataTable dt, int mabd, string ma, string ten, string tenhc, string dang, string tenhang, decimal soluong, int stt)
        {
            string exp = "mabd=" + mabd;
            DataRow r = getrowbyid(dt, exp);
            if (r == null)
            {
                DataRow nrow = dt.NewRow();
                nrow["mabd"] = mabd;
                nrow["ma"] = ma;
                nrow["ten"] = ten;
                nrow["tenhc"] = tenhc;
                nrow["dang"] = dang;
                nrow["tenhang"] = tenhang;
                nrow["toncuoi"] = soluong;
                nrow["stt"] = stt;
                dt.Rows.Add(nrow);
            }
            else
            {
                DataRow[] dr = dt.Select(exp);
                if (dr.Length > 0) dr[0]["toncuoi"] = decimal.Parse(dr[0]["toncuoi"].ToString()) + soluong;
            }
        }
        public DataSet ins_items(DataSet dsxml, DataRow[] dr, int i, decimal soluong, decimal sotien)
        {
            DataRow r2;
            r2 = dsxml.Tables[0].NewRow();
            r2["yymmdd"] = dr[i]["yymmdd"].ToString();
            r2["ngay"] = dr[i]["ngay"].ToString();
            r2["sonhap"] = dr[i]["sonhap"].ToString();
            r2["soxuat"] = dr[i]["soxuat"].ToString();
            r2["mabd"] = dr[i]["mabd"].ToString();
            r2["ma"] = dr[i]["ma"].ToString();
            r2["ten"] = dr[i]["ten"].ToString().Trim();
            r2["tenhc"] = dr[i]["tenhc"].ToString();
            r2["dang"] = dr[i]["dang"].ToString();
            r2["tenhang"] = dr[i]["tenhang"].ToString();
            r2["tennuoc"] = dr[i]["tennuoc"].ToString();
            r2["diengiai"] = dr[i]["diengiai"].ToString();
            r2["tondau"] = dr[i]["tondau"].ToString();
            r2["sttondau"] = dr[i]["sttondau"].ToString();
            r2["slnhap"] = dr[i]["slnhap"].ToString();
            r2["stnhap"] = dr[i]["stnhap"].ToString();
            r2["slxuat"] = dr[i]["slxuat"].ToString();
            r2["stxuat"] = dr[i]["stxuat"].ToString();
            if (dr[i]["dongia"].ToString() != "") r2["dongia"] = dr[i]["dongia"].ToString();
            r2["handung"] = dr[i]["handung"].ToString();
            r2["losx"] = dr[i]["losx"].ToString();
            r2["toncuoi"] = soluong;
            r2["sttoncuoi"] = sotien;
            dsxml.Tables[0].Rows.Add(r2);
            return dsxml;
        }
        public void updrec_07(DataTable dt, int ma, int col, long so)
        {
            DataRow[] dr = dt.Select("ma=" + ma);
            dr[0][col] = so;
        }
        public bool upd_bieu07(long m_id, int m_ma, string m_ngay, Decimal m_soluong, int m_userid)
        {
            sql = "update " + user + ".bieu_07 set ngay=:m_ngay,soluong=:m_soluong,";
            sql += "userid=:m_userid where id=:m_id and ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                cmd.Parameters.Add("m_soluong", NpgsqlDbType.Numeric).Value = m_soluong;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bieu_07(id,ma,ngay,soluong,userid,ngayud) values ";
                    sql += "(:m_id,:m_ma,:m_ngay,:m_soluong,:m_userid,now())";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                    cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                    cmd.Parameters.Add("m_soluong", NpgsqlDbType.Numeric).Value = m_soluong;
                    cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bieu_07");
                return false;
            }
            finally
            {

                con.Close(); con.Dispose();
            }
            return true;
        }
        public DataSet get_sotien_dbv(int i_nhom, string tu, string den)
        {
            string stime = "'" + f_ngay + "'", usr = user;
            sql = "select case when d.stt is null then 0 else d.stt end as stt,sum(b.soluong*f.giamua) as sotien ";
            sql += "from xxx.d_xuatsdll a inner join xxx.d_thucxuat b on a.id=b.id ";
            sql += " inner join " + usr + ".d_dmbd c on b.mabd=c.id ";
            sql += " inner join xxx.d_theodoi f on b.sttt=f.id ";
            sql += " left join " + usr + ".d_nhombo d on c.nhombo=d.id ";
            sql += " where a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai<>3 and a.nhom=" + i_nhom;
            sql += " group by case when d.stt is null then 0 else d.stt end ";
            sql += " union all select case when d.stt is null then 0 else d.stt end as stt,-sum(b.soluong*f.giamua) as sotien ";
            sql += "from xxx.d_xuatsdll a inner join xxx.d_thucxuat b on a.id=b.id ";
            sql += " inner join " + usr + ".d_dmbd c on b.mabd=c.id ";
            sql += " inner join xxx.d_theodoi f on b.sttt=f.id ";
            sql += " left join " + usr + ".d_nhombo d on c.nhombo=d.id ";
            sql += " where a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai=3 and a.nhom=" + i_nhom;
            sql += " group by case when d.stt is null then 0 else d.stt end";
            sql += " union all select case when d.stt is null then 0 else d.stt end as stt,sum(b.soluong*f.giamua) as sotien ";
            sql += " from xxx.d_xuatll a inner join xxx.d_xuatct b on a.id=b.id ";
            sql += " inner join " + usr + ".d_dmbd c on b.mabd=c.id ";
            sql += " inner join xxx.d_theodoi f on b.sttt=f.id ";
            sql += " left join " + usr + ".d_nhombo d on c.nhombo=d.id";
            sql += " where a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai='XK' and a.nhom=" + i_nhom;
            sql += " group by case when d.stt is null then 0 else d.stt end";
            sql += " union all select case when d.stt is null then 0 else d.stt end as stt,sum(b.soluong*f.giamua) as sotien ";
            sql += " from xxx.d_ngtrull a inner join xxx.d_ngtruct b on a.id=b.id ";
            sql += " inner join " + usr + ".d_dmbd c on b.mabd=c.id ";
            sql += " inner join xxx.d_theodoi f on b.sttt=f.id ";
            sql += " inner join " + usr + ".d_nhombo d on c.nhombo=d.id ";
            sql += " where a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " group by case when d.stt is null then 0 else d.stt end";
            sql += " union all select case when d.stt is null then 0 else d.stt end as stt,sum(b.soluong*f.giamua) as sotien ";
            sql += " from xxx.bhytkb a inner join xxx.bhytthuoc b on a.id=b.id ";
            sql += " inner join " + usr + ".d_dmbd c on b.mabd=c.id ";
            sql += " inner join xxx.d_theodoi f on b.sttt=f.id ";
            sql += " left join " + usr + ".d_nhombo d on c.nhombo=d.id ";
            sql += " where a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " group by case when d.stt is null then 0 else d.stt end";
            return get_data_mmyy(sql,tu,den,false);
        }

        public DataSet get_sothuoc_dbv(int i_nhom, string tu, string den)
        {
            string stime = "'" + f_ngay + "'", usr = user;
            DataSet dsmabd = new DataSet();
            dsmabd.ReadXml("..\\..\\..\\xml\\d_truyvan.xml");
            DataRow r1, r2;
            string table = bNoiNgoai_Hang(i_nhom) ? "," + usr + ".d_dmhang g," + usr + ".d_nhomhang h" : "," + usr + ".d_dmnuoc g," + usr + ".d_nhomnuoc h";
            string dk = bNoiNgoai_Hang(i_nhom) ? " and c.mahang=g.id and g.loai=h.id" : " and c.manuoc=g.id and g.loai=h.id";
            DataTable dt = bNoiNgoai_Hang(i_nhom) ? get_data("select * from " + usr + ".d_nhomhang where nhom=" + i_nhom).Tables[0] : get_data("select * from " + usr + ".d_nhomnuoc where nhom=" + i_nhom).Tables[0];
            sql = "select distinct h.stt,b.mabd from xxx.d_xuatsdll a,xxx.d_thucxuat b," + usr + ".d_dmbd c,xxx.d_theodoi f";
            sql += table;
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id";
            sql += dk;
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " union all select distinct h.stt,b.mabd from xxx.d_xuatll a,xxx.d_xuatct b," + usr + ".d_dmbd c,xxx.d_theodoi f";
            sql += table;
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id";
            sql += dk;
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai='XK' and a.nhom=" + i_nhom;
            sql += " union all select distinct h.stt,b.mabd from xxx.d_ngtrull a,xxx.d_ngtruct b," + usr + ".d_dmbd c,xxx.d_theodoi f";
            sql += table;
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id";
            sql += dk;
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " union all select distinct h.stt,b.mabd from xxx.bhytkb a,xxx.bhytthuoc b," + usr + ".d_dmbd c,xxx.d_theodoi f";
            sql += table;
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id";
            sql += dk;
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            foreach (DataRow r in get_data_mmyy(sql, tu, den, false).Tables[0].Rows)
            {
                sql = "id=" + int.Parse(r["stt"].ToString()) + " and n_so=" + int.Parse(r["mabd"].ToString());
                r1 = getrowbyid(dsmabd.Tables[0], sql);
                if (r1 == null)
                {
                    r2 = dsmabd.Tables[0].NewRow();
                    r2["id"] = r["stt"].ToString();
                    r2["n_so"] = r["mabd"].ToString();
                    dsmabd.Tables[0].Rows.Add(r2);
                }
            }
            return dsmabd;
        }
        public DataSet get_soluong_dichtruyen(int i_nhom, string tu, string den)
        {
            string stime = "'" + f_ngay + "'", usr = user;
            sql = "select d.stt,c.hamluong,sum(b.soluong) as soluong from xxx.d_xuatsdll a,xxx.d_thucxuat b," + usr + ".d_dmbd c,xxx.d_theodoi f," + usr + ".d_nhombo d";
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id and c.nhombo=d.id";
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai<>3 and a.nhom=" + i_nhom;
            sql += " and d.stt in (12,13,14) and c.hamluong is not null";
            sql += " group by d.stt,c.hamluong";
            sql += " union all select d.stt,c.hamluong,-sum(b.soluong) as soluong from xxx.d_xuatsdll a,xxx.d_thucxuat b," + usr + ".d_dmbd c,xxx.d_theodoi f," + usr + ".d_nhombo d";
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id and c.nhombo=d.id";
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai=3 and a.nhom=" + i_nhom;
            sql += " and d.stt in (12,13,14) and c.hamluong is not null";
            sql += " group by d.stt,c.hamluong";
            sql += " union all select d.stt,c.hamluong,sum(b.soluong) as soluong from xxx.d_xuatll a,xxx.d_xuatct b," + usr + ".d_dmbd c,xxx.d_theodoi f," + usr + ".d_nhombo d";
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id and c.nhombo=d.id";
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.loai='XK' and a.nhom=" + i_nhom;
            sql += " and d.stt in (12,13,14) and c.hamluong is not null";
            sql += " group by d.stt,c.hamluong";
            sql += " union all select d.stt,c.hamluong,sum(b.soluong) as soluong from xxx.d_ngtrull a,xxx.d_ngtruct b," + usr + ".d_dmbd c,xxx.d_theodoi f," + usr + ".d_nhombo d";
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id and c.nhombo=d.id";
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " and d.stt in (12,13,14)";
            sql += " group by d.stt,c.hamluong";
            sql += " union all select d.stt,c.hamluong,sum(b.soluong) as soluong from xxx.bhytkb a,xxx.bhytthuoc b," + usr + ".d_dmbd c,xxx.d_theodoi f," + usr + ".d_nhombo d";
            sql += " where a.id=b.id and b.mabd=c.id and b.sttt=f.id and c.nhombo=d.id";
            sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            sql += " and a.nhom=" + i_nhom;
            sql += " and d.stt in (12,13,14) and c.hamluong is not null";
            sql += " group by d.stt,c.hamluong";
            return get_data_mmyy(sql, tu, den, false);
        }
        public bool upd_mabd(int d_mabd)
        {
            sql = "update " + user + ".d_mabd set mabd=:d_mabd where mabd=:d_mabd";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".d_mabd(mabd) values (:d_mabd)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "d_mabd");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_danhmuc(string d_table, long d_id, string d_ma, string d_ten, int d_nhom, int d_stt)
        {
            sql = "update " + user + "." + d_table + " set ma=:d_ma,ten=:d_ten,nhom=:d_nhom,stt=:d_stt where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_ma", NpgsqlDbType.Text).Value = d_ma;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + "." + d_table + " (id,ma,ten,nhom,stt) values (:d_id,:d_ma,:d_ten,:d_nhom,:d_stt)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_ma", NpgsqlDbType.Text).Value = d_ma;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_nhom", NpgsqlDbType.Numeric).Value = d_nhom;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, d_table);
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_danhmuc(string d_table, long d_id, string d_ten, int d_stt)
        {
            sql = "update " + user + "." + d_table + " set ten=:d_ten,stt=:d_stt where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + "." + d_table + " (id,ten,stt) values (:d_id,:d_ten,:d_stt)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, d_table);
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public void upd_tonkhoct_xuat(string tt, string mmyy, long stt, int makho, int manguon, int nhomcc, int mabd, string handung, string losx, decimal soluong, decimal sotien, decimal giaban, decimal giamua)
        {
            if (tt == delete)
                execute_data("update " + user + mmyy + ".d_tonkhoct set slxuat=slxuat-" + soluong + " where makho=" + makho + " and stt=" + stt);
            else if (tt == insert)
                upd_tonkhoct(mmyy, makho, stt, mabd, soluong, "slxuat");
            upd_tonkhoth(tt, mmyy, makho, mabd, manguon, soluong, "slxuat");
        }
        public void upd_tonkhoth_dutru(string tt, int nhom, string mmyy, int makho, int manguon, int mabd, decimal soluong)
        {
            if (bTru_tonao(nhom))
            {
                if (tt == dutru || tt == insert) execute_data("update " + user + mmyy + ".d_tonkhoth set slyeucau=slyeucau+" + soluong + " where makho=" + makho + " and manguon=" + manguon + " and mabd=" + mabd);
                else if (tt == duyet || tt == delete) execute_data("update " + user + mmyy + ".d_tonkhoth set slyeucau=slyeucau-" + soluong + " where makho=" + makho + " and manguon=" + manguon + " and mabd=" + mabd);
            }
        }
        public void upd_tonkhoth(string tt, string mmyy, int makho, int mabd, int manguon, decimal soluong, string fie)
        {
            if (tt == delete) execute_data("update " + user + mmyy + ".d_tonkhoth set " + fie + "=" + fie + "-" + soluong + " where makho=" + makho + " and mabd=" + mabd + " and manguon=" + manguon);
            else if (tt == insert) upd_tonkhoth(mmyy, makho, manguon, mabd, soluong, fie);
        }
        public bool upd_tonkhoth(string mmyy, int makho, int manguon, int mabd, decimal soluong, string fie)
        {
            sql = "update " + user + mmyy + ".d_tonkhoth set " + fie + "=" + fie + "+:soluong where makho=:makho and manguon=:manguon and mabd=:mabd";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("soluong", NpgsqlDbType.Numeric).Value = soluong;
                cmd.Parameters.Add("makho", NpgsqlDbType.Numeric).Value = makho;
                cmd.Parameters.Add("manguon", NpgsqlDbType.Numeric).Value = manguon;
                cmd.Parameters.Add("mabd", NpgsqlDbType.Numeric).Value = mabd;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + mmyy + ".d_tonkhoth(makho,manguon,mabd," + fie + ") values ";
                    sql += "(:makho,:manguon,:mabd,:soluong)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("makho", NpgsqlDbType.Numeric).Value = makho;
                    cmd.Parameters.Add("manguon", NpgsqlDbType.Numeric).Value = manguon;
                    cmd.Parameters.Add("mabd", NpgsqlDbType.Numeric).Value = mabd;
                    cmd.Parameters.Add("soluong", NpgsqlDbType.Numeric).Value = soluong;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(mmyy, ex.Message, sComputer, "d_tonkhoth");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_tonkhoct(string mmyy, int makho, long stt, int mabd, decimal soluong, string fie1)
        {
            sql = "update " + user + mmyy + ".d_tonkhoct set " + fie1 + "=" + fie1 + "+:soluong,";
            sql += "mabd=:mabd where makho=:makho and stt=:stt";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("soluong", NpgsqlDbType.Numeric).Value = soluong;
                cmd.Parameters.Add("mabd", NpgsqlDbType.Numeric).Value = mabd;
                cmd.Parameters.Add("makho", NpgsqlDbType.Numeric).Value = makho;
                cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = stt;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + mmyy + ".d_tonkhoct(stt,mabd,makho," + fie1 + ",idn,sttn) values ";
                    sql += "(:stt,:mabd,:makho,:soluong,0,0)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = stt;
                    cmd.Parameters.Add("mabd", NpgsqlDbType.Numeric).Value = mabd;
                    cmd.Parameters.Add("makho", NpgsqlDbType.Numeric).Value = makho;
                    cmd.Parameters.Add("soluong", NpgsqlDbType.Numeric).Value = soluong;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(mmyy, ex.Message, sComputer, "d_tonkhoct");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_tonkhoct(string d_mmyy, int d_makho, long d_stt, int d_mabd, decimal d_tondau)
        {
            sql = user + mmyy(d_mmyy) + ".upd_tonkhoct";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("d_makho", NpgsqlDbType.Numeric).Value = d_makho;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_mabd", NpgsqlDbType.Numeric).Value = d_mabd;
                cmd.Parameters.Add("d_tondau", NpgsqlDbType.Numeric).Value = d_tondau;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(d_mmyy, ex.Message, sComputer, "d_tonkhoct");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }
        public bool upd_kh1451(long m_id, int m_ma, string m_ngay, int m_c01, int m_c02,
            int m_c03, int m_c04, int m_c05, int m_c06, int m_c07, int m_c08, int m_c09,
            int m_c10, int m_c11, int m_c12, int m_c13, int m_c14, int m_c15, int m_c16,
            int m_c17, int m_c18, int m_c19, int m_c20, int m_userid)
        {
            sql = "update " + user + ".kh_bieu_1451 set ngay=:m_ngay,c01=:m_c01,c02=:m_c02,c03=:m_c03,c04=:m_c04,";
            sql += "c05=:m_c05,c06=:m_c06,c07=:m_c07,c08=:m_c08,c09=:m_c09,c10=:m_c10,";
            sql += "c11=:m_c11,c12=:m_c12,c13=:m_c13,c14=:m_c14,c15=:m_c15,c16=:m_c16,c17=:m_c17,";
            sql += "c18=:m_c18,c19=:m_c19,c20=:m_c20,userid=:m_userid where id=:m_id and ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                cmd.Parameters.Add("m_c03", NpgsqlDbType.Numeric).Value = m_c03;
                cmd.Parameters.Add("m_c04", NpgsqlDbType.Numeric).Value = m_c04;
                cmd.Parameters.Add("m_c05", NpgsqlDbType.Numeric).Value = m_c05;
                cmd.Parameters.Add("m_c06", NpgsqlDbType.Numeric).Value = m_c06;
                cmd.Parameters.Add("m_c07", NpgsqlDbType.Numeric).Value = m_c07;
                cmd.Parameters.Add("m_c08", NpgsqlDbType.Numeric).Value = m_c08;
                cmd.Parameters.Add("m_c09", NpgsqlDbType.Numeric).Value = m_c09;
                cmd.Parameters.Add("m_c10", NpgsqlDbType.Numeric).Value = m_c10;
                cmd.Parameters.Add("m_c11", NpgsqlDbType.Numeric).Value = m_c11;
                cmd.Parameters.Add("m_c12", NpgsqlDbType.Numeric).Value = m_c12;
                cmd.Parameters.Add("m_c13", NpgsqlDbType.Numeric).Value = m_c13;
                cmd.Parameters.Add("m_c14", NpgsqlDbType.Numeric).Value = m_c14;
                cmd.Parameters.Add("m_c15", NpgsqlDbType.Numeric).Value = m_c15;
                cmd.Parameters.Add("m_c16", NpgsqlDbType.Numeric).Value = m_c16;
                cmd.Parameters.Add("m_c17", NpgsqlDbType.Numeric).Value = m_c17;
                cmd.Parameters.Add("m_c18", NpgsqlDbType.Numeric).Value = m_c18;
                cmd.Parameters.Add("m_c19", NpgsqlDbType.Numeric).Value = m_c19;
                cmd.Parameters.Add("m_c20", NpgsqlDbType.Numeric).Value = m_c20;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".kh_bieu_1451(id,ma,ngay,c01,c02,c03,c04,c05,c06,c07,c08,c09,c10,c11,c12,c13,c14,c15,c16,c17,c18,c19,c20,userid,ngayud) values ";
                    sql += "(:m_id,:m_ma,:m_ngay,:m_c01,:m_c02,:m_c03,:m_c04,:m_c05,:m_c06,:m_c07,:m_c08,:m_c09,:m_c10,:m_c11,:m_c12,:m_c13,:m_c14,:m_c15,:m_c16,:m_c17,:m_c18,:m_c19,:m_c20,:m_userid,now())";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                    cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                    cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                    cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                    cmd.Parameters.Add("m_c03", NpgsqlDbType.Numeric).Value = m_c03;
                    cmd.Parameters.Add("m_c04", NpgsqlDbType.Numeric).Value = m_c04;
                    cmd.Parameters.Add("m_c05", NpgsqlDbType.Numeric).Value = m_c05;
                    cmd.Parameters.Add("m_c06", NpgsqlDbType.Numeric).Value = m_c06;
                    cmd.Parameters.Add("m_c07", NpgsqlDbType.Numeric).Value = m_c07;
                    cmd.Parameters.Add("m_c08", NpgsqlDbType.Numeric).Value = m_c08;
                    cmd.Parameters.Add("m_c09", NpgsqlDbType.Numeric).Value = m_c09;
                    cmd.Parameters.Add("m_c10", NpgsqlDbType.Numeric).Value = m_c10;
                    cmd.Parameters.Add("m_c11", NpgsqlDbType.Numeric).Value = m_c11;
                    cmd.Parameters.Add("m_c12", NpgsqlDbType.Numeric).Value = m_c12;
                    cmd.Parameters.Add("m_c13", NpgsqlDbType.Numeric).Value = m_c13;
                    cmd.Parameters.Add("m_c14", NpgsqlDbType.Numeric).Value = m_c14;
                    cmd.Parameters.Add("m_c15", NpgsqlDbType.Numeric).Value = m_c15;
                    cmd.Parameters.Add("m_c16", NpgsqlDbType.Numeric).Value = m_c16;
                    cmd.Parameters.Add("m_c17", NpgsqlDbType.Numeric).Value = m_c17;
                    cmd.Parameters.Add("m_c18", NpgsqlDbType.Numeric).Value = m_c18;
                    cmd.Parameters.Add("m_c19", NpgsqlDbType.Numeric).Value = m_c19;
                    cmd.Parameters.Add("m_c20", NpgsqlDbType.Numeric).Value = m_c20;
                    cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "kh_bieu_1451");
                return false;
            }
            finally
            {

                con.Close(); con.Dispose();
            }
            return true;
        }

        public bool upd_kh1452(long m_id, int m_ma, string m_ngay, int m_c01, int m_c02,
            int m_c03, int m_userid)
        {
            sql = "update " + user + ".kh_bieu_1452 set ngay=:m_ngay,c01=:m_c01,c02=:m_c02,c03=:m_c03,";
            sql += "userid=:m_userid where id=:m_id and ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                cmd.Parameters.Add("m_c03", NpgsqlDbType.Numeric).Value = m_c03;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".kh_bieu_1452(id,ma,ngay,c01,c02,c03,userid,ngayud) values ";
                    sql += "(:m_id,:m_ma,:m_ngay,:m_c01,:m_c02,:m_c03,:m_userid,now())";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                    cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                    cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                    cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                    cmd.Parameters.Add("m_c03", NpgsqlDbType.Numeric).Value = m_c03;
                    cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "kh_bieu_1452");
                return false;
            }
            finally
            {

                con.Close(); con.Dispose();
            }
            return true;
        }
        public void updrec_145(DataTable dt, int stt, int col, Decimal so)
        {
            DataRow[] dr = dt.Select("ma=" + stt);
            dr[0][col] = so;
        }
        public bool upd_bc_nhomicd10(int d_id, string d_ten, int d_stt)
        {
            sql = " update " + user + ".bc_nhomicd10 set ten=:d_ten,stt=:d_stt where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;                
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;               
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_nhomicd10(id,ten,stt) values (:d_id,:d_ten,:d_stt)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_nhomicd10");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_bc_theonhomicd(string s_ma, string s_ten, int i_idnhom,int i_stt)
        {
            sql = " update " + user + ".bc_theonhomicd set ma=:s_ma,ten=:s_ten where idnhom=:i_idnhom and stt=:i_stt";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("s_ma", NpgsqlDbType.Varchar,9).Value = s_ma;
                cmd.Parameters.Add("s_ten", NpgsqlDbType.Text).Value = s_ten;
                cmd.Parameters.Add("i_idnhom", NpgsqlDbType.Numeric).Value = i_idnhom;
                cmd.Parameters.Add("i_stt", NpgsqlDbType.Numeric).Value = i_stt;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_theonhomicd(ma,ten,idnhom,stt) values (:s_ma,:s_ten,:i_idnhom,:i_stt)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("s_ma", NpgsqlDbType.Varchar, 9).Value = s_ma;
                    cmd.Parameters.Add("s_ten", NpgsqlDbType.Text).Value = s_ten;
                    cmd.Parameters.Add("i_idnhom", NpgsqlDbType.Numeric).Value = i_idnhom;
                    cmd.Parameters.Add("i_stt", NpgsqlDbType.Numeric).Value = i_stt;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_theonhomicd");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public bool upd_v_nhomkhoabvll(int d_id, string d_ma, string d_ten, int d_stt, string d_makp)
        {
            sql = " update " + user + ".bc_nhomkhoabvll set ma=:d_ma,ten=:d_ten,stt=:d_stt,makp=:d_makp where id=:d_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.Parameters.Add("d_ma", NpgsqlDbType.Text).Value = d_ma;
                cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                cmd.Parameters.Add("d_makp", NpgsqlDbType.Text).Value = d_makp;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bc_nhomkhoabvll(id,ma,ten,stt,makp) values (:d_id,:d_ma,:d_ten,:d_stt,:d_makp)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_ma", NpgsqlDbType.Text).Value = d_ma;
                    cmd.Parameters.Add("d_ten", NpgsqlDbType.Text).Value = d_ten;
                    cmd.Parameters.Add("d_stt", NpgsqlDbType.Numeric).Value = d_stt;
                    cmd.Parameters.Add("d_makp", NpgsqlDbType.Text).Value = d_makp;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bc_nhomkhoabvll");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
            return true;
        }
        public void upd_v_nhomkhoabvct(int d_id, string d_makp, string d_tenkp)
        {
            sql = " update " + user + ".v_nhomkhoabvct set tenkp=:d_tenkp where id=:d_id and makp=:d_makp";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                cmd.Parameters.Add("d_makp", NpgsqlDbType.Varchar, 3).Value = d_makp;
                cmd.Parameters.Add("d_tenkp", NpgsqlDbType.Text).Value = d_tenkp;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".v_nhomkhoabvct(id,makp,tenkp) values (:d_id,:d_makp,:d_tenkp)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("d_id", NpgsqlDbType.Numeric).Value = d_id;
                    cmd.Parameters.Add("d_makp", NpgsqlDbType.Varchar, 3).Value = d_makp;
                    cmd.Parameters.Add("d_tenkp", NpgsqlDbType.Text).Value = d_tenkp;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "v_nhomkhoabvct");
            }
            finally
            {
                cmd.Dispose();
                con.Dispose();
            }
        }
        public void updrec_bieu(DataTable dt, long ma, string ten, long so)
        {
            string exp = "ma=" + ma;
            DataRow r = getrowbyid(dt, exp);
            if (r == null)
            {
                DataRow nrow = dt.NewRow();
                nrow["ma"] = ma;
                nrow["stt"] = get_stt(dt);
                nrow["ten"] = ten;
                for (int i = 1; i <= so; i++) nrow["c" + i.ToString().PadLeft(2, '0')] = 0;
                dt.Rows.Add(nrow);
            }
        }
        public void updrec_02(DataTable dt, string stt, int col, Decimal so)
        {
            DataRow[] dr = dt.Select("stt='" + stt + "'");
            dr[0][col] = so;
        }

        #endregion

        #region get data
        public DataSet merge(DataSet ds1, DataSet ds2)
        {
            DataRow r1;
            foreach (DataRow r in ds2.Tables[0].Rows)
            {
                r1 = ds1.Tables[0].NewRow();
                for (int i = 0; i < ds2.Tables[0].Columns.Count; i++) r1[i] = r[i].ToString();
                ds1.Tables[0].Rows.Add(r1);
            }
            return ds1;
        }

        public string get_mapt(string tenpt)
        {
            if (tenpt == "") return "";
            try
            {
                sql = "select loaipt,mapt from " + user + ".dmpttt where trim(noi_dung)=:tenpt";
                con = new NpgsqlConnection(sConn);
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("tenpt", NpgsqlDbType.Text).Value = tenpt.Trim();
                dest = new NpgsqlDataAdapter(cmd);
                ds = new DataSet();
                dest.Fill(ds);
                cmd.Dispose();
                con.Close(); con.Dispose();
                return ds.Tables[0].Rows[0][0].ToString().Trim() + ds.Tables[0].Rows[0][1].ToString();
            }
            catch { return ""; };
        }
        public DataSet get_data_mmyy(string str, string tu, string den, bool khoangcach)
        {           
            DataSet tmp = new DataSet();
            try
            {
                DateTime dt1 = (khoangcach) ? StringToDate(tu).AddDays(-iNgaykiemke) : StringToDate(tu);
                DateTime dt2 = (khoangcach) ? StringToDate(den).AddDays(iNgaykiemke) : StringToDate(den);
                int y1 = dt1.Year, m1 = dt1.Month;
                int y2 = dt2.Year, m2 = dt2.Month;
                int itu, iden;
                string mmyy = "";
                bool be = true;
                for (int i = y1; i <= y2; i++)
                {
                    itu = (i == y1) ? m1 : 1;
                    iden = (i == y2) ? m2 : 12;
                    for (int j = itu; j <= iden; j++)
                    {
                        mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                        if (bMmyy(mmyy))
                        {
                            sql = str.Replace("xxx", user + mmyy);
                            if (be)
                            {
                                tmp = get_data(sql);
                                be = false;
                            }
                            else tmp.Merge(get_data(sql));
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                upd_error(ex.Message + sql, sComputer, "");
                return null;
            }
            return tmp;
        }
        public DataSet get_data(string sql)
        {
            try
            {
                if (con != null)
                {
                    con.Close(); con.Dispose();
                }
                ds = new DataSet();
                con = new NpgsqlConnection(sConn);
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                dest = new NpgsqlDataAdapter(cmd);
                dest.Fill(ds);
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message.ToString().Trim()+sql, sComputer, "?");
            } 
            
            return ds;
            
        }
        public DataSet get_data_mmyy(string v_mmyy, string v_sql)
        {
            v_sql = v_sql.Replace("xxx", user + v_mmyy);
            v_sql = v_sql.Replace("medibv", user);
            return get_data(v_sql);
        }
        public DataSet get_xuat_tungay(DataSet ds, DataTable dt, DataTable dtkp, DataTable dtloaint, DataTable dtkhac, DataTable dtkho, string d_tu, string d_den, int d_makho, int d_manguon)
        {
            DataTable dtvay = get_data("select * from " + user + ".d_dmvay").Tables[0];
            DataRow r1, r2, r3;
            string s_diengiai, xxx, stime = "'" + f_ngay + "'", usr = user;

            DateTime dt1 = StringToDate(d_tu).AddDays(-iNgaykiemke);
            DateTime dt2 = StringToDate(d_den).AddDays(iNgaykiemke);
            int y1 = dt1.Year, m1 = dt1.Month;
            int y2 = dt2.Year, m2 = dt2.Month;
            int itu, iden;
            string d_mmyy = "";
            for (int i = y1; i <= y2; i++)
            {
                itu = (i == y1) ? m1 : 1;
                iden = (i == y2) ? m2 : 12;
                for (int j = itu; j <= iden; j++)
                {
                    d_mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                    if (bMmyy(d_mmyy))
                    {
                        xxx = usr + d_mmyy;
                        sql = "select a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,b.soluong,b.soluong*t.giamua as sotien";
                        sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi t ";
                        sql += " where a.id=b.id and b.sttt=t.id and a.loai in ('CK','BS','XK','VA') and a.khox=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) order by a.ngay";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["so"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                switch (r["loai"].ToString())
                                {
                                    case "CK":
                                        s_diengiai = "Xuất đến :";
                                        r3 = getrowbyid(dtkho, "id=" + int.Parse(r["khon"].ToString()));
                                        break;
                                    case "BS":
                                        s_diengiai = "Bổ sung tủ trực :";
                                        r3 = getrowbyid(dtkp, "id=" + int.Parse(r["khon"].ToString()));
                                        break;
                                    case "VA":
                                        s_diengiai = "Vay :";
                                        r3 = getrowbyid(dtvay, "id=" + int.Parse(r["khon"].ToString()));
                                        break;
                                    default:
                                        s_diengiai = r["lydo"].ToString().Trim();
                                        s_diengiai += (s_diengiai != "") ? ":" : "";
                                        r3 = getrowbyid(dtkhac, "id=" + int.Parse(r["khon"].ToString()));
                                        break;
                                }
                                r2["diengiai"] = s_diengiai;
                                r2["diengiai"] += (r3 != null) ? r3["ten"].ToString() : "";
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                        sql = "select to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                        sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi t where a.id=b.id and b.sttt=t.id and a.loai in (1,4) and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.ngay,a.makp,b.mabd";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["ngay"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                r2["diengiai"] = "Xuất :" + r3["ten"].ToString();
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }

                        sql = "select to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien,bn.hoten ||'-'|| bn.mabn as oi";
                        sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucbucstt b," + xxx + ".d_theodoi t,"+user+".btdbn bn where a.mabn=bn.mabn and a.id=b.id and b.sttt=t.id and a.loai=2 and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.ngay,a.makp,b.mabd,bn.hoten ||'-'|| bn.mabn";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["ngay"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                r2["diengiai"] = "Xuất tử trực : " + r["oi"].ToString() + " "  + r3["ten"].ToString();
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                        sql = "select to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.mabn,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                        sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi t where a.id=b.id and b.sttt=t.id and a.loai in (1,4) and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.ngay,a.makp,b.mabd,a.mabn";
                        sql += " union all ";
                        sql += "select to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.mabn,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                        sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucbucstt b," + xxx + ".d_theodoi t where a.id=b.id and b.sttt=t.id and a.loai=2 and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.ngay,a.makp,b.mabd,a.mabn";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["mabn"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                r2["diengiai"] = "Xuất :" + r3["ten"].ToString();
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                        sql = "select a.mabn,a.hoten,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.loai,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                        sql += " from " + xxx + ".d_ngtrull a," + xxx + ".d_ngtruct b," + xxx + ".d_theodoi t where a.id=b.id and b.sttt=t.id and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.mabn,a.hoten,a.ngay,a.loai,b.mabd";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtloaint, "id=" + int.Parse(r["loai"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["mabn"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                r2["diengiai"] = "Xuất ngoại trú :" + r3["ten"].ToString().Trim() + "[" + r["hoten"].ToString().Trim() + "]";
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                        sql = "select a.id,a.sothe,c.hoten,to_char(a.ngay,'dd/mm/yyyy') as ngay,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                        sql += " from " + xxx + ".bhytkb a," + xxx + ".bhytthuoc b," + xxx + ".bhytds c," + xxx + ".d_theodoi t where a.id=b.id and b.sttt=t.id and a.mabn=c.mabn and b.makho=" + d_makho;
                        sql += " and a.ngay between to_date('" + d_tu + "'," + stime + ") and to_date('" + d_den + "'," + stime + ")";
                        if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) group by a.id,a.sothe,c.hoten,a.ngay,b.mabd";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = "";
                                r2["soxuat"] = r["sothe"].ToString();
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                                r2["diengiai"] = "BHYT Ngoại trú [" + r["hoten"].ToString().Trim() + "]";
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = 0;
                                r2["stnhap"] = 0;
                                r2["slxuat"] = r["soluong"].ToString();
                                r2["stxuat"] = r["sotien"].ToString();
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }
       
        #endregion

        #region PTTT
        public DataSet get_data_PTTT(string str, string tu, string den)
        {
            DataSet tmp = new DataSet();

            DateTime dt1 = StringToDate(tu);
            DateTime dt2 = StringToDate(den);
            int y1 = dt1.Year, m1 = dt1.Month;
            int y2 = dt2.Year, m2 = dt2.Month;
            int itu, iden;
            string mmyy = "";
            bool be = true;
            for (int i = y1; i <= y2; i++)
            {
                itu = (i == y1) ? m1 : 1;
                iden = (i == y2) ? m2 : 12;
                for (int j = itu; j <= iden; j++)
                {
                    mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                    if (bMmyy(mmyy))
                    {
                        sql = " select '" + mmyy + "' as mmyy , " + str.Replace("xxx", user + mmyy);
                        if (be)
                        {
                            tmp = get_data(sql);
                            be = false;
                        }
                        else tmp.Merge(get_data(sql));
                    }
                }
            }
            return tmp;
        }
        public bool upd_pttt(string s_mmyy, long m_id, string m_ptv, string m_phu1, string m_phu2, string m_bsgayme, string m_ktvgayme,string m_hoisuc, string m_dungcu)
        {
            sql = "update " + user + s_mmyy + ".pttt_duyetmo set ptv=:m_ptv,phu1=:m_phu1,phu2=:m_phu2,bsgayme=:m_bsgayme,ktvgayme=:m_ktvgayme,hoisuc=:m_hoisuc,dungcu=:m_dungcu where id=:m_id";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_ptv", NpgsqlDbType.Varchar, 4).Value = m_ptv;
                cmd.Parameters.Add("m_phu1", NpgsqlDbType.Varchar, 4).Value = m_phu1;
                cmd.Parameters.Add("m_phu2", NpgsqlDbType.Varchar, 4).Value = m_phu2;
                cmd.Parameters.Add("m_bsgayme", NpgsqlDbType.Varchar, 4).Value = m_bsgayme;
                cmd.Parameters.Add("m_ktvgayme", NpgsqlDbType.Varchar, 4).Value = m_ktvgayme;
                cmd.Parameters.Add("m_hoisuc", NpgsqlDbType.Varchar, 4).Value = m_hoisuc;
                cmd.Parameters.Add("m_dungcu", NpgsqlDbType.Varchar, 4).Value = m_dungcu;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + s_mmyy + ".pttt_duyetmo(id,ptv,phu1,phu2,bsgayme,ktvgayme,hoisuc,dungcu)";
                    sql += " values (:m_id,:m_ptv,:m_phu1,:m_phu2,:m_bsgayme,:m_ktvgayme,:m_hoisuc,:m_dungcu)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_ptv", NpgsqlDbType.Varchar, 4).Value = m_ptv;
                    cmd.Parameters.Add("m_phu1", NpgsqlDbType.Varchar, 4).Value = m_phu1;
                    cmd.Parameters.Add("m_phu2", NpgsqlDbType.Varchar, 4).Value = m_phu2;
                    cmd.Parameters.Add("m_bsgayme", NpgsqlDbType.Varchar, 4).Value = m_bsgayme;
                    cmd.Parameters.Add("m_ktvgayme", NpgsqlDbType.Varchar, 4).Value = m_ktvgayme;
                    cmd.Parameters.Add("m_hoisuc", NpgsqlDbType.Varchar, 4).Value = m_hoisuc;
                    cmd.Parameters.Add("m_dungcu", NpgsqlDbType.Varchar, 4).Value = m_dungcu;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, "", "pttt " + m_id.ToString());
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return true;
        }
        public bool upd_pttt_bsduyetmo(long m_id, int m_loai, string m_mabs)
        {
            sql = "update " + user + ".pttt_bs_duyetmo set loai=:m_loai where id=:m_id and mabs=:m_mabs";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_loai", NpgsqlDbType.Numeric).Value = m_loai;
                cmd.Parameters.Add("m_mabs", NpgsqlDbType.Varchar).Value = m_mabs;

                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".pttt_bs_duyetmo(id,loai,mabs)";
                    sql += " values (:m_id,:m_loai,:m_mabs)";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_loai", NpgsqlDbType.Numeric).Value = m_loai;
                    cmd.Parameters.Add("m_mabs", NpgsqlDbType.Varchar).Value = m_mabs;

                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, "", "pttt_bs_duyetmo" + m_id.ToString());
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return true;
        }
        public void upd_boiduong_pttt(string b_pttt, string b_loai, string b_tenloai, decimal b_ptv, decimal b_phu1,
      decimal b_phu2, decimal b_bsgayme, decimal b_ktvgayme, decimal b_hoisuc, decimal b_dungcu)
        {
            string sql_upd = "", sql_int = "";
            int i = 0;
            sql_upd = "update " + user + ".boiduong_pttt set ptv=:b_ptv,tenloai=:b_tenloai";
            sql_upd += ",phu1=:b_phu1,phu2=:b_phu2,bsgayme=:b_bsgayme,ktvgayme=:b_ktvgayme ";
            sql_upd += ",hoisuc=:b_hoisuc,dungcu=:b_dungcu  ";
            sql_upd += " where pttt=:b_pttt and loai=:b_loai";
            sql_int = "insert into " + user + ".boiduong_pttt(pttt,loai,tenloai,ptv,phu1,phu2,bsgayme,ktvgayme,hoisuc,dungcu) " +
                " values('" + b_pttt + "','" + b_loai + "',:b_tenloai," + b_ptv + "," + b_phu1 + "," + b_phu2 + "," + b_bsgayme + "," + b_ktvgayme + "," + b_hoisuc + "," + b_dungcu + ")";
            NpgsqlConnection con = new NpgsqlConnection(sConn);
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql_upd, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("b_tenloai", NpgsqlDbType.Text).Value = b_tenloai;
            cmd.Parameters.Add("b_ptv", NpgsqlDbType.Numeric).Value = b_ptv;
            cmd.Parameters.Add("b_phu1", NpgsqlDbType.Numeric).Value = b_phu1;
            cmd.Parameters.Add("b_phu2", NpgsqlDbType.Numeric).Value = b_phu2;
            cmd.Parameters.Add("b_bsgayme", NpgsqlDbType.Numeric).Value = b_bsgayme;
            cmd.Parameters.Add("b_ktvgayme", NpgsqlDbType.Numeric).Value = b_ktvgayme;
            cmd.Parameters.Add("b_hoisuc", NpgsqlDbType.Numeric).Value = b_hoisuc;
            cmd.Parameters.Add("b_dungcu", NpgsqlDbType.Numeric).Value = b_dungcu;
            cmd.Parameters.Add("b_pttt", NpgsqlDbType.Varchar, 1).Value = b_pttt;
            cmd.Parameters.Add("b_loai", NpgsqlDbType.Varchar, 10).Value = b_loai;
            i = cmd.ExecuteNonQuery();
            if (i == 0)
            {
                cmd.Dispose();
                cmd = new NpgsqlCommand(sql_int, con);
                cmd.Parameters.Add("b_tenloai", NpgsqlDbType.Text).Value = b_tenloai;

                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException exx)
                {
                    upd_error(exx.ToString(), "", "boiduong_pttt");
                }
            }
            cmd.Dispose();
            con.Dispose();
        }
        #endregion

        #region f_Get_Baocao
        public DataSet F_Get_Baocao(string tu, string den, string mabn, string hoten, string phai, string mann, string matt, string maquan, string maphuongxa, string madantoc, string makp, string mabsdt, string makt, string maktv, string userid, string tronggio, string loaibn, string giobaocao, string madoituong, string loaicdha, string dotuoi, string nhomkt, string bacsidoc)
        {
            string sql = "", m_dotuoi = "";

            DataSet dsbc = new DataSet();
            try
            {
                hoten = Hoten_khongdau(hoten);

                sql = "select distinct a.id as id,(to_char(a.ngay,'dd/mm/yyyy hh24:mi'))  as ngay,h.mabn,h.hoten,decode(h.phai,1,'Nữ','Nam') as phai,nvl(to_char(h.ngaysinh,'dd/mm/yyyy'),h.namsinh) ngaysinh,(h.sonha|| ' ' ||h.thon|| ' ' ||i.tenpxa|| ' ' ||j.tenquan|| ' ' ||k.tentt) as  diachi,g.tenkp,b.hoten as bsdt,c.ten as ktv,p.ten bacsidoc,";
                sql += "d.doituong,(case when d1.madoituong is null then d.doituong  else d1.doituong end) as doituong_chidinh,f.hoten as userid,a.chandoan,a.ketluan,kt.makt, kt.tenkt,count(ct.id) as soca,ct.solan ";
                sql += ",case when bh.sothe is null then bh1.sothe else bh.sothe end as sothe,case when bh.denngay is null then to_char(bh1.denngay,'dd/mm/yyyy')  else to_char(bh.denngay,'dd/mm/yyyy') end as denngay,case when bh.tungay is null then to_char(bh1.tungay,'dd/mm/yyyy')  else to_char(bh.tungay,'dd/mm/yyyy') end as tungay";
                sql += ",sum(p1.soluong) as soluong,tp.thuocphim";
                sql += " from xxx.cdha_bnll a left join " + user + ".dmbs b on a.bsdt=b.ma";
                sql += " inner join xxx.cdha_bnct ct on a.id=ct.id";
                sql += " left join " + user + ".cdha_dsktv c on a.ktv=c.maktv ";
                sql += " left join " + user + ".cdha_dsktv p on a.bsdoc=p.maktv ";
                sql += " left join " + user + ".doituong d on a.madoituong=d.madoituong";
                sql += " left join " + user + ".cdha_dlogin f on a.userid=f.id";
                sql += " left join " + user + ".btdkp_bv g on a.makp=g.makp ";
                sql += " left join " + user + ".btdbn h on a.mabn=h.mabn ";
                sql += " left join " + user + ".btdpxa i on h.maphuongxa=i.maphuongxa";
                sql += " left join " + user + ".btdquan j on h.maqu=j.maqu";
                sql += " left join " + user + ".btdtt k on h.matt=k.matt";
                sql += " left join " + user + ".btdnn_bv l on  h.mann=l.mann  ";
                sql += " left join " + user + ".btddt m on h.madantoc=m.madantoc";
                sql += " left join " + user + ".cdha_kythuat kt on ct.makt=kt.makt ";
                sql += " left join xxx.cdha_kqvp kq on a.id=kq.id  and a.id_loai=kq.id_loaicdha  and ct.id=kq.id and kt.id_vp=kq.mavp";
                sql += " left join " + user + ".doituong d1 on kq.madoituong=d1.madoituong";
                sql += " left join xxx.bhyt bh on a.maql=bh.maql";
                sql += " left join medibv.bhyt bh1 on a.maql=bh1.maql";
                sql += " left join xxx.cdha_ctphimxq p1 on ct.id=p1.id and ct.makt=p1.makt";
                sql += " left join medibv.cdha_thuocphim tp on p1.loai=tp.loai and p1.idtp=tp.id_tp";
                sql += " where  ";
                if (giobaocao != "") sql += "  to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')  between to_date('" + tu + " " + giobaocao + "','dd/mm/yyyy hh24:mi') and to_date('" + den + " " + giobaocao + "','dd/mm/yyyy hh24:mi')";
                else sql += " to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') between to_date('" + tu + "','dd/mm/yyyy') and to_date('" + den + "','dd/mm/yyyy')";

                if (mabn != "") sql += " and a.mabn='" + mabn + "'";
                if (hoten != "") sql += " and h.hotenkdau='" + hoten + "'";
                if (phai != "") sql += " and h.phai='" + phai + "'";
                if (mann != "") sql += " and h.mann='" + mann + "'";
                if (madantoc != "") sql += " and h.madantoc='" + madantoc + "'";
                if (matt != "") sql += " and h.matt='" + matt + "'";
                if (maquan != "") sql += " and h.maqu='" + maquan + "'";
                if (maphuongxa != "") sql += " and h.maphuongxa='" + maphuongxa + "'";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                if (userid != "") sql += " and a.userid=" + userid + "";
                if (mabsdt != "") sql += " and a.bsdt='" + mabsdt + "' ";
                if (maktv != "") sql += " and a.ktv='" + maktv + "'";
                if (bacsidoc != "") sql += " and a.bsdoc='" + bacsidoc + "'";
                if (makt != "") sql += " and ct.makt in " + makt + "";
                if (tronggio != "") sql += " and a.ngoaigio='" + tronggio + "'";
                if (loaibn != "") sql += " and a.loaibn='" + loaibn + "'";
                if (madoituong != "") sql += " and case when kq.madoituong is null then a.madoituong in " + madoituong + " else kq.madoituong in " + madoituong + "  end ";
                sql += "and  a.id_loai='" + loaicdha + "'";
                if (dotuoi != "")
                {
                    if (dotuoi.IndexOf(">") != -1)
                    {
                        m_dotuoi = dotuoi.Substring(1);
                        sql += " and to_number(to_char(sysdate,'yyyy'))-to_number(h.namsinh)>" + m_dotuoi + "";
                    }
                    else
                    {
                        m_dotuoi = dotuoi.Substring(2);
                        sql += " and to_number(to_char(sysdate,'yyyy'))-to_number(h.namsinh)<=" + m_dotuoi + "";
                    }
                }
                if (nhomkt != "") sql += " and kt.id_nhom='" + nhomkt + "'";
                sql += " group by a.id,(to_char(a.ngay,'dd/mm/yyyy hh24:mi')),h.mabn,h.hoten,decode(h.phai,1,'Nữ','Nam'),nvl(to_char(h.ngaysinh,'dd/mm/yyyy'),h.namsinh),(h.sonha|| ' ' ||h.thon|| ' ' ||i.tenpxa|| ' ' ||j.tenquan|| ' ' ||k.tentt),g.tenkp,b.hoten,c.ten,p.ten,";
                sql += " d.doituong,(case when d1.madoituong is null then d.doituong  else d1.doituong end),f.hoten,a.chandoan,a.ketluan,kt.makt, kt.tenkt,ct.solan";
                sql += " ,case when bh.sothe is null then bh1.sothe else bh.sothe end,case when bh.denngay is null then to_char(bh1.denngay,'dd/mm/yyyy')  else to_char(bh.denngay,'dd/mm/yyyy') end,case when bh.tungay is null then to_char(bh1.tungay,'dd/mm/yyyy')  else to_char(bh.tungay,'dd/mm/yyyy') end,tp.thuocphim";

                dsbc = get_data_mmyy(sql, tu, den, true);


            }
            catch
            {
                return null;
            }
            return dsbc;
        }
        #endregion

        #region Lay du lieu
        public string get_phieuxuat(string d_mmyy, int d_nhom, string d_kho)
        {
            int ret = 1;
            sql = "select max(to_number(substr(soct,1,4),'9999')) as so from " + user + d_mmyy + ".d_phieuxuat where nhom=" + d_nhom;
            if (d_kho != "") sql += " and kho='" + d_kho + "'";
            ds = get_data(sql);
            if (ds.Tables[0].Rows[0]["so"].ToString() != "") ret = int.Parse(ds.Tables[0].Rows[0]["so"].ToString()) + 1;
            return ret.ToString().PadLeft(4, '0');
        }
        public string get_phieuxuat(string d_mmyy, long d_id)
        {
            sql = "select soct from " + user + d_mmyy + ".d_phieuxuat where id=" + d_id;
            ds = get_data(sql);
            return ds.Tables[0].Rows[0]["soct"].ToString();
        }
        public string get_vviet(string maicd)
        {
            if (maicd == "") return "";
            try
            {
                return get_data("select vviet from medibv.icd10 where trim(cicd10)='" + maicd.Trim() + "'").Tables[0].Rows[0][0].ToString();
            }
            catch { return ""; }
        }
        #endregion

        #region Excecute data
        public void execute_data_mmyy(string str, string tu, string den, bool khoangcach)
        {
            DateTime dt1 = (khoangcach) ? StringToDate(tu).AddDays(-iNgaykiemke) : StringToDate(tu);
            DateTime dt2 = (khoangcach) ? StringToDate(den).AddDays(iNgaykiemke) : StringToDate(den);
            int y1 = dt1.Year, m1 = dt1.Month;
            int y2 = dt2.Year, m2 = dt2.Month;
            int itu, iden;
            string mmyy = "";
            for (int i = y1; i <= y2; i++)
            {
                itu = (i == y1) ? m1 : 1;
                iden = (i == y2) ? m2 : 12;
                for (int j = itu; j <= iden; j++)
                {
                    mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                    if (bMmyy(mmyy))
                    {
                        sql = str.Replace("xxx", user + mmyy);
                        execute_data(sql);
                    }
                }
            }
        }

        public void execute_data(string sql)
        {
            try
            {
                if (con != null)
                {
                    con.Close(); con.Dispose();
                }
                con = new NpgsqlConnection(sConn);
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message.ToString().Trim(), sComputer, "?");
            }
        }
        #endregion

        #region Getrowbyid
        public DataRow getrowbyid(DataTable dt, string exp)
        {
            try
            {
                DataRow[] r = dt.Select(exp);
                return r[0];
            }
            catch { return null; }
        }
        #endregion

        #region tao moi data bao cao
        public void f_Create_Data_Baocao(string mmyy)
        {
            execute_data("CREATE TABLE medibv.bc_dlogin(id numeric(5) NOT NULL DEFAULT 0,hoten text,userid varchar(20),password_ varchar(20),right_ text,nhomkho numeric(2) DEFAULT 0,makho text,makp text,tao numeric(1) DEFAULT 0,admin numeric(1) DEFAULT 0,manhom text,loaint text,loaikhac text,ngayud timestamp DEFAULT now(),thuhoi numeric(1) DEFAULT 0,loaivp text,loaicdha text,CONSTRAINT pk_bc_dlogin PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("CREATE TABLE medibv.bc_dmbv(mabv varchar(10) NOT NULL,stt numeric(3) NOT NULL DEFAULT 0,tenbv text,ten_menu text,id_menu varchar(4),CONSTRAINT pk_bc_dmbv PRIMARY KEY (mabv, stt) USING INDEX TABLESPACE medi_index) WITH OIDS;");

            execute_data("CREATE TABLE medibv.bc_thongso(mabv varchar(10) NOT NULL,loai numeric(2)  DEFAULT 0,id_bc varchar(50) ,stt numeric(3)  DEFAULT 0, ten nvarchar2(254),giatri numeric(1)  DEFAULT 0, diengiai varchar(254),CONSTRAINT pk_bc_thongso PRIMARY KEY (mabv,loai,id_bc,stt) USING INDEX TABLESPACE medi_index) WITH OIDS;");

            execute_data("CREATE TABLE medibv.bc_maubaocao(id numeric(5)  DEFAULT 0,ma varchar(50) ,ten varchar(254), loai varchar(100),CONSTRAINT pk_bc_maubaocao PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            
            execute_data("CREATE TABLE medibv.bc_dmkhac(id numeric(2)  DEFAULT 0,id_khac varchar(200) ,CONSTRAINT pk_bc_dmkhac PRIMARY KEY (id))");
            
            execute_data("CREATE TABLE medibv.bc_nhomkhoabvll(id numeric(3) NOT NULL DEFAULT 0,ma varchar(10),ten text,  stt numeric(3) DEFAULT 0,makp text,ngayud timestamp DEFAULT now(), CONSTRAINT pk_v_nhomkhoabvll PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("CREATE TABLE medibv.bc_nhomkhoabvct(id numeric(3) NOT NULL DEFAULT 0,makp varchar(3),tenkp text,CONSTRAINT pk_v_nhomkhoabvct PRIMARY KEY (id,makp) USING INDEX TABLESPACE medi_index) ");
            execute_data("CREATE TABLE medibv.bc_loainhanvien(id numeric(2)  DEFAULT 0,stt numeric(2)  DEFAULT 0,ten varchar(100),mabs varchar(2000) ,CONSTRAINT pk_bc_loainhanvien PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("CREATE TABLE medibv.bc_nhomdoituong(id numeric(2)  DEFAULT 0,stt numeric(2)  DEFAULT 0,ten varchar(100),madoituong varchar(254) ,CONSTRAINT pk_bc_nhomdoituong PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");            
            execute_data("CREATE TABLE " + user + ".bhyt_maubaocao (id numeric(10), maloai varchar(64),tenloai text, ten text, filereport varchar(64), used numeric(1) default (1), CONSTRAINT pk_bhyt_maubaocao PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("alter table " + user + ".bhyt_maubaocao add stt numeric(7)");
            execute_data("CREATE TABLE " + user + ".doituong_bhyt(ma_old varchar(2), ma13 text, ma_dt varchar(2), ten_dt text, ty_le numeric(3), ghichu text, tien1 numeric(10), tien2 numeric(10), loai numeric(1), tenth varchar(20), tonghop numeric(1), readonly numeric(1), stt numeric(3), viettat text, stt_inth numeric(3) ) WITH OIDS;");
            execute_data("CREATE TABLE " + user + ".dmnhomdv_bhyt(manhom numeric(3) NOT NULL, tennhom text, mabv varchar(8), tenbv text, stt numeric(3) NOT NULL, CONSTRAINT pk_dmnhomdv_bhyt PRIMARY KEY (manhom, stt) ) WITH OIDS;");
            execute_data("CREATE TABLE " + user + ".dmbaocao_bhyt (id numeric(3) NOT NULL, stt numeric(3), ten text, idnhomvp numeric(3), idloaivp varchar(80), CONSTRAINT pk_dmbaocao_bhyt PRIMARY KEY (id) ) WITH OIDS;");
            execute_data("CREATE TABLE medibv.bhyt_thongso(id numeric(3) NOT NULL DEFAULT 0,loai text,ten text,nhom numeric(2) NOT NULL DEFAULT 0,ngayud timestamp DEFAULT now(),CONSTRAINT pk_bhyt_thongso PRIMARY KEY (id, nhom) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("CREATE TABLE " + user + ".boiduong_pttt(  pttt text NOT NULL, loai varchar(10) NOT NULL, tenloai text, ptv numeric(10), phu1 numeric(10), phu2 numeric(10), bsgayme numeric(10), ktvgayme numeric(10), hoisuc numeric(10),  dungcu numeric(10), CONSTRAINT pk_boiduong_pttt PRIMARY KEY (pttt, loai)) WITHOUT OIDS;");
            execute_data("CREATE TABLE " + user + ".pttt_bs_duyetmo( id numeric(18) NOT NULL DEFAULT 0, loai numeric(2) DEFAULT 0, mabs varchar(4) NOT NULL, CONSTRAINT pk_pttt_bs_duyetmo PRIMARY KEY (id, mabs)) WITH OIDS;");
            execute_data("alter table " + user + ". pttt_bs_duyetmo alter column id type numeric(18);");
            execute_data("update " + user + ".boiduong_pttt set loai='1' where loai=0");
            execute_data(" CREATE TABLE " + user + mmyy + ".pttt_duyetmo(id numeric(18) NOT NULL DEFAULT 0, ptv varchar(4), phu1 varchar(4), phu2 varchar(4), bsgayme varchar(4), ktvgayme varchar(4), hoisuc varchar(4), dungcu varchar(4), CONSTRAINT pk_pttt_duyetmo PRIMARY KEY (id) USING INDEX TABLESPACE medi_index) WITH OIDS;");
            execute_data("alter table " + user + mmyy + ".pttt_duyetmo alter column id type numeric(18);");
            execute_data("alter table " + user + ".bhyt_maubaocao add loai  numeric(1) default 0;");
            execute_data("alter table " + user + ".bhyt_maubaocao add tonghop numeric(2) default 0;");
            execute_data("alter table " + user + ".bc_bv_right add soluong numeric(3) default 1;");
        }

        #endregion tao moi data bao cao

        #region XÁC ĐỊNH FILE
        public string f_modify(string tenfile)
        {
            return System.IO.File.GetLastWriteTime(tenfile).ToString("dd/MM/yyyy HH:mm");
        }
        public string f_size(string tenfile)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(tenfile);
            return (fi.Length / 1024).ToString();
        }
        public string file_exe(string tenfilegoc)
        {
            string ret = "";
            string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory());
            for (int i = 0; i < files.GetLength(0); i++)
            {
                if (files[i].ToString().ToUpper().IndexOf(".EXE") != -1 && tenfile_goc(files[i].ToString()) == tenfilegoc.ToLower())
                {
                    ret = files[i].ToString();
                    break;
                }
            }
            return ret;
        }
        public string file_exe(string path, string tenfilegoc)
        {
            string[] files = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < files.GetLength(0); i++)
            {
                if ((files[i].ToString().ToUpper().IndexOf(".EXE") != -1) && (tenfile_goc(files[i].ToString()) == tenfilegoc.ToLower()))
                {
                    return files[i].ToString();
                }
            }
            return "";
        }
        public string tenfile_goc(string tenfile)
        {
            System.Reflection.Assembly asm = null;
            asm = System.Reflection.Assembly.LoadFrom(tenfile);
            return asm.GetName().Name.ToString().ToLower();
        }
        public string path_medisofthis()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            int num = 0;
            int length = currentDirectory.Length;
            while (length > 0)
            {
                if (currentDirectory.Substring(length - 1, 1) == @"\")
                {
                    num++;
                }
                if (num == 3)
                {
                    break;
                }
                length--;
            }
            return currentDirectory.Substring(0, length - 1);
        }
        public void writeXml(string tenfile, string cot, string s)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(@"..\..\..\xml\" + tenfile + ".xml");
                ds.Tables[0].Rows[0][cot] = s;
            }
            catch
            {
                DataColumn column = new DataColumn();
                column.ColumnName = cot;
                column.DataType = Type.GetType("System.String");
                ds.Tables[0].Columns.Add(column);
                ds.Tables[0].Rows[0][cot] = s;
            }
            ds.WriteXml(@"..\..\..\xml\" + tenfile + ".xml");
        }
        public bool bAutoupdate
        {
            get
            {
                try
                {
                    return (int.Parse(get_data("select ten from " + user + ".thongso where id=340").Tables[0].Rows[0][0].ToString()) == 1);
                }
                catch
                {
                    return false;
                }
            }
        }
        public string Path_medisoft
        {
            get
            {
                try
                {
                    return get_data("select ten from " + user + ".thongso where id=341").Tables[0].Rows[0][0].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        public bool bUpdate(string p_localhost, string p_server, string file)
        {
            bool flag = f_modify(file_exe(p_localhost, file)) == f_modify(file_exe(p_server, file));
            bool flag2 = f_size(file_exe(p_localhost, file)) == f_size(file_exe(p_server, file));
            return (flag && flag2);
        }
        #endregion

        #region excel
        public bool check_open_Excel()
        {
            Process[] processes = Process.GetProcesses();
            bool ret = false;
            if (processes.Length > 1)
            {
                for (int i = 0; i <= processes.Length - 1; i++)
                {
                    if (((Process)processes[i]).ProcessName == "EXCEL")
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }
        public void check_process_Excel()
        {
            Process[] processes = Process.GetProcesses();

            if (processes.Length > 1)
            {
                int i = 0;
                for (int n = 0; n <= processes.Length - 1; n++)
                {
                    if (((Process)processes[n]).ProcessName == "EXCEL")
                    {
                        i++;
                        ((Process)processes[n]).Kill();
                    }
                }
            }
        }
        public string getIndex(int i)
        {
            string[] map = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
							   "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
							   "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
							   "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
							   "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ",
							   "EA", "EB", "EC", "ED", "EE", "EF", "EG", "EH", "EI", "EJ", "EK", "EL", "EM", "EN", "EO", "EP", "EQ", "ER", "ES", "ET", "EU", "EV", "EW", "EX", "EY", "EZ",
							   "FA", "FB", "FC", "FD", "FE", "FF", "FG", "FH", "FI", "FJ", "FK", "FL", "FM", "FN", "FO", "FP", "FQ", "FR", "FS", "FT", "FU", "FV", "FW", "FX", "FY", "FZ",
							   "GA", "GB", "GC", "GD", "GE", "GF", "GG", "GH", "GI", "GJ", "GK", "GL", "GM", "GN", "GO", "GP", "GQ", "GR", "GS", "GT", "GU", "GV", "GW", "GX", "GY", "GZ",
							   "HA", "HB", "HC", "HD", "HE", "HF", "HG", "HH", "HI", "HJ", "HK", "HL", "HM", "HN", "HO", "HP", "HQ", "HR", "HS", "HT", "HU", "HV", "HW", "HX", "HY", "HZ",
							   "IA", "IB", "IC", "ID", "IE", "IF", "IG", "IH", "II", "IJ", "IK", "IL", "IM", "IN", "IO", "IP", "IQ", "IR", "IS", "IT", "IU", "IV", "IW", "IX", "IY", "IZ",
							   "JA", "JB", "JC", "JD", "JE", "JF", "JG", "JH", "JI", "JJ", "JK", "JL", "JM", "JN", "JO", "JP", "JQ", "JR", "JS", "JT", "JU", "JV", "JW", "JX", "JY", "JZ",
							   "KA", "KB", "KC", "KD", "KE", "KF", "KG", "KH", "KI", "KJ", "KK", "KL", "KM", "KN", "KO", "KP", "KQ", "KR", "KS", "KT", "KU", "KV", "KW", "KX", "KY", "KZ",
							   "LA", "LB", "LC", "LD", "LE", "LF", "LG", "LH", "LI", "LJ", "LK", "LL", "LM", "LN", "LO", "LP", "LQ", "LR", "LS", "LT", "LU", "LV", "LW", "LX", "LY", "LZ",
							   "MA", "MB", "MC", "MD", "ME", "MF", "MG", "MH", "MI", "MJ", "MK", "ML", "MM", "MN", "MO", "MP", "MQ", "MR", "MS", "MT", "MU", "MV", "MW", "MX", "MY", "MZ",
							   "NA", "NB", "NC", "ND", "NE", "NF", "NG", "NH", "NI", "NJ", "NK", "NL", "NM", "NN", "NO", "NP", "NQ", "NR", "NS", "NT", "NU", "NV", "NW", "NX", "NY", "NZ",
							   "OA", "OB", "OC", "OD", "OE", "OF", "OG", "OH", "OI", "OJ", "OK", "OL", "OM", "ON", "OO", "OP", "OQ", "OR", "OS", "OT", "OU", "OV", "OW", "OX", "OY", "OZ",
							   "PA", "PB", "PC", "PD", "PE", "PF", "PG", "PH", "PI", "PJ", "PK", "PL", "PM", "PN", "PO", "PP", "PQ", "PR", "PS", "PT", "PU", "PV", "PW", "PX", "PY", "PZ",
							   "QA", "QB", "QC", "QD", "QE", "QF", "QG", "QH", "QI", "QJ", "QK", "QL", "QM", "QN", "QO", "QP", "QQ", "QR", "QS", "QT", "QU", "QV", "QW", "QX", "QY", "QZ",
							   "RA", "RB", "RC", "RD", "RE", "RF", "RG", "RH", "RI", "RJ", "RK", "RL", "RM", "RN", "RO", "RP", "RQ", "RR", "RS", "RT", "RU", "RV", "RW", "RX", "RY", "RZ",
							   "SA", "SB", "SC", "SD", "SE", "SF", "SG", "SH", "SI", "SJ", "SK", "SL", "SM", "SN", "SO", "SP", "SQ", "SR", "SS", "ST", "SU", "SV", "SW", "SX", "SY", "SZ",
							   "TA", "TB", "TC", "TD", "TE", "TF", "TG", "TH", "TI", "TJ", "TK", "TL", "TM", "TN", "TO", "TP", "TQ", "TR", "TS", "TT", "TU", "TV", "TW", "TX", "TY", "TZ",
							   "UA", "UB", "UC", "UD", "UE", "UF", "UG", "UH", "UI", "UJ", "UK", "UL", "UM", "UN", "UO", "UP", "UQ", "UR", "US", "UT", "UU", "UV", "UW", "UX", "UY", "UZ",
							   "VA", "VB", "VC", "VD", "VE", "VF", "VG", "VH", "VI", "VJ", "VK", "VL", "VM", "VN", "VO", "VP", "VQ", "VR", "VS", "VT", "VU", "VV", "VW", "VX", "VY", "VZ",
							   "WA", "WB", "WC", "WD", "WE", "WF", "WG", "WH", "WI", "WJ", "WK", "WL", "WM", "WN", "WO", "WP", "WQ", "WR", "WS", "WT", "WU", "WV", "WW", "WX", "WY", "WZ",
							   "XA", "XB", "XC", "XD", "XE", "XF", "XG", "XH", "XI", "XJ", "XK", "XL", "XM", "XN", "XO", "XP", "XQ", "XR", "XS", "XT", "XU", "XV", "XW", "XX", "XY", "XZ",
							   "YA", "YB", "YC", "YD", "YE", "YF", "YG", "YH", "YI", "YJ", "YK", "YL", "YM", "YN", "YO", "YP", "YQ", "YR", "YS", "YT", "YU", "YV", "YW", "YX", "YY", "YZ",
							   "ZA", "ZB", "ZC", "ZD", "ZE", "ZF", "ZG", "ZH", "ZI", "ZJ", "ZK", "ZL", "ZM", "ZN", "ZO", "ZP", "ZQ", "ZR", "ZS", "ZT", "ZU", "ZV", "ZW", "ZX", "ZY", "ZZ"};

            return map[i];
        }
        public string Export_Excel(DataSet dset, string tenfile)
        {
            try
            {
                string dirPath = AppDomain.CurrentDomain.BaseDirectory + "Excel";
                string filePath = dirPath + "\\" + tenfile + ".xls";
                if (!System.IO.Directory.Exists(dirPath)) System.IO.Directory.CreateDirectory(dirPath);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.Unicode);
                string astr = "";
                astr = "<Table>";//"<Table border=1>";
                astr = astr + "<tr>";
                for (int i = 0; i < dset.Tables[0].Columns.Count; i++)
                {
                    astr = astr + "<th>";
                    astr = astr + dset.Tables[0].Columns[i].ColumnName;
                    astr = astr + "</th>";
                }
                astr = astr + "</tr>";
                sw.Write(astr);
                for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
                {
                    astr = "<tr>";
                    for (int j = 0; j < dset.Tables[0].Columns.Count; j++)
                    {
                        astr = astr + "<td>";
                        astr = astr + dset.Tables[0].Rows[i][j].ToString();
                        astr = astr + "</td>";
                    }
                    astr = astr + "</tr>";
                    sw.Write(astr);
                }
                astr = "</Table>";
                sw.Write(astr);
                sw.Close();
                return filePath;
            }
            catch (Exception ex)
            {
                upd_error(ex.Message, sComputer, tenfile);
                return "";
            }
        }
        public string Export_Excel(DataTable dt, string tenfile)
        {
            try
            {
                string dirPath = AppDomain.CurrentDomain.BaseDirectory + "Excel";
                string filePath = dirPath + "\\" + tenfile + ".xls";
                if (!System.IO.Directory.Exists(dirPath)) System.IO.Directory.CreateDirectory(dirPath);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.Unicode);
                string astr = "";
                astr = "<Table>";//"<Table border=1>";
                astr = astr + "<tr>";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    astr = astr + "<th>";
                    astr = astr + dt.Columns[i].ColumnName;
                    astr = astr + "</th>";
                }
                astr = astr + "</tr>";
                sw.Write(astr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    astr = "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        astr = astr + "<td>";
                        astr = astr + dt.Rows[i][j].ToString();
                        astr = astr + "</td>";
                    }
                    astr = astr + "</tr>";
                    sw.Write(astr);
                }
                astr = "</Table>";
                sw.Write(astr);
                sw.Close();
                return filePath;
            }
            catch (Exception ex)
            {
                upd_error(ex.Message, sComputer, tenfile);
                return "";
            }
        }
        
        #endregion

        #region BHYT
        public string thetunguyen(int d_nhom)
        {
            ds = get_data("select ten from " + user + ".d_thongso where id=120 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count == 0) return "SS,RR,TT";
            return ds.Tables[0].Rows[0][0].ToString();
        }
        public string thetunguyen_vitri(int d_nhom)
        {
            string ret = "4,2";
            ds = get_data("select ten from " + user + ".d_thongso where id=121 and nhom=" + d_nhom);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ret = ds.Tables[0].Rows[0][0].ToString().Trim();
                int pos = ret.IndexOf(",");
                if (pos != -1)
                {
                    int p1 = int.Parse(ret.Substring(0, pos)) - 1;
                    int p2 = int.Parse(ret.Substring(pos + 1));
                    ret = p1.ToString() + "," + p2.ToString();
                }
            }
            return ret;
        }
        public int bGiabhyt
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=13 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return 0;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public bool sothe_doituong(int madoituong)
        {
            return get_data("select * from medibv.doituong where sothe>0 and madoituong=" + madoituong).Tables[0].Rows.Count > 0;
        }
        public string thetrongtinh()
        {
            ds = get_data("select ten from medibv.d_thongso where id=83 and nhom=1");
            if (ds.Tables[0].Rows.Count == 0) return "50";
            return ds.Tables[0].Rows[0][0].ToString();
        }
        public int Ngay_toa_bhyt()
        {
            ds = get_data("select ten from medibv.d_thongso where id=99 and nhom=1");
            if (ds.Tables[0].Rows.Count > 0) return int.Parse(ds.Tables[0].Rows[0]["ten"].ToString());
            else return 1;
        }
        public string thetrongtinh_vitri_old
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=5 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return "1,2";
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string sothemoi()
        {
            ds = get_data("select ten from medibv.thongso where id=50");
            if (ds.Tables[0].Rows.Count == 0) return "";
            return ds.Tables[0].Rows[0][0].ToString();
        }
        public string vitrithe_13()
        {
            string ret = "0,2";
            ds = get_data("select ten from medibv.d_thongso where id=52 and nhom=1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ret = ds.Tables[0].Rows[0][0].ToString().Trim();
                int pos = ret.IndexOf(",");
                if (pos != -1)
                {
                    int p1 = int.Parse(ret.Substring(0, pos)) - 1;
                    int p2 = int.Parse(ret.Substring(pos + 1));
                    ret = p1.ToString() + "," + p2.ToString();
                }
            }
            return ret;
        }
        public string vitrithe_moi()
        {
            string ret = "5,2";
            ds = get_data("select ten from medibv.d_thongso where id=53 and nhom=1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ret = ds.Tables[0].Rows[0][0].ToString().Trim();
                int pos = ret.IndexOf(",");
                if (pos != -1)
                {
                    int p1 = int.Parse(ret.Substring(0, pos)) - 1;
                    int p2 = int.Parse(ret.Substring(pos + 1));
                    ret = p1.ToString() + "," + p2.ToString();
                }
            }
            return ret;
        }
        public bool bcongkham_bhyt()
        {
            ds = get_data("select ten from medibv.d_thongso where id=82 and nhom=1");
            if (ds.Tables[0].Rows.Count == 0) return false;
            return ds.Tables[0].Rows[0][0].ToString() == "1";
        }
        public decimal themoi_chitra()
        {
            //			ds=get_data("select ten from medibv.d_thongso where id=49 and nhom=1");
            //			if (ds.Tables[0].Rows.Count==0) return 0;
            //			return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
            return 80;
        }
        public decimal themoi_sotien()
        {
            ds = get_data("select ten from medibv.d_thongso where id=51 and nhom=1");
            if (ds.Tables[0].Rows.Count == 0) return 0;
            return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public int iKhambenh
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=10 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return 0;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public int iNhombaocao
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=9 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return 0;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public string sothe(int madoituong)
        {
            ds = get_data("select sothe,ngay,mabv,mien from medibv.doituong where madoituong=" + madoituong);
            if (ds.Tables[0].Rows.Count > 0) return ds.Tables[0].Rows[0]["sothe"].ToString().Trim().PadLeft(2, '0') + ds.Tables[0].Rows[0]["ngay"].ToString().Trim() + ds.Tables[0].Rows[0]["mabv"].ToString().Trim() + ds.Tables[0].Rows[0]["mien"].ToString().Trim();
            else return "00000";
        }
        public bool sothe(DataSet dsdt,int madoituong, string sothe)
        {
            int i = 0; bool ret = false;
            string s1 = "", s2 = "", s3 = "'", dau = ",+;.", so = "0123456789";
            DataRow[] r = dsdt.Tables[0].Select("madoituong=" + madoituong);

            if (r!=null) s1 = dsdt.Tables[0].Rows[0]["dai"].ToString().Trim();
            if (s1 != "")
            {
                while (i < s1.Length)
                {
                    if (dau.IndexOf(s1.Substring(i, 1)) != -1)
                    {
                        if (s2 != "") s3 += s2.PadLeft(2, '0') + "','";
                        s2 = "";
                    }
                    else if (so.IndexOf(s1.Substring(i, 1)) != -1) s2 += s1.Substring(i, 1);
                    i++;
                }
                if (s2 != "") s3 += s2.PadLeft(2, '0') + "','";
                s3 = (s3.Length > 1) ? s3.Substring(0, s3.Length - 2) : "";
                ret = s3.IndexOf("'" + sothe.Length.ToString().PadLeft(2, '0') + "'") != -1;
            }
            return ret;
        }
        public decimal Congkham()
        {
            ds = get_data("select ten from medibv.d_thongso where id=47 and nhom=1");
            if (ds.Tables[0].Rows.Count == 0) return 3000;
            return decimal.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        public string BHXH_VN
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=1 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Syte;
                return ds.Tables[0].Rows[0][0].ToString();

            }
        }
        public string BHXH_TINH
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=2 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Tenbv;
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string DV_KCB
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=3 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Tenbv;
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string MABHXH
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=4 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Tenbv;
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string MABV_BHYT
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=7 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Tenbv;
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public string MABV_KCB
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=6 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return Tenbv;
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        public int TRE_EM_DUOI_6
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=8 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return 6;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public int tutrucpk 
        {
            get
            {
                ds = get_data("select ten from medibv.bhyt_thongso where id=12 and nhom=0");
                if (ds.Tables[0].Rows.Count == 0) return 0;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
        }
        public void upd_bhyt_mabaocao(long id, string maloai, string tenloai, string tenmau, string filereport, int iused,int istt, int i_loai,int i_tonghop)
        {
            int i_rec = 0;
            cmd.Dispose();
            con.Close();
            sql = "update " + user + ".bhyt_maubaocao set tenloai=:tenloai, ten=:tenmau, filereport=:filereport, stt=:stt,loai=:i_loai,tonghop=:i_tonghop ";
            sql += " where id=:id";
            con = new NpgsqlConnection(sConn);
            con.Open();
            cmd = new NpgsqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("tenloai", NpgsqlDbType.Text).Value = tenloai;
            cmd.Parameters.Add("tenmau", NpgsqlDbType.Text).Value = tenmau;
            cmd.Parameters.Add("filereport", NpgsqlDbType.Varchar, 64).Value = filereport;            
            cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = istt;
            cmd.Parameters.Add("i_loai", NpgsqlDbType.Numeric).Value = i_loai;
            cmd.Parameters.Add("i_tonghop", NpgsqlDbType.Numeric).Value = i_tonghop;
            cmd.Parameters.Add("id", NpgsqlDbType.Numeric).Value = id;

            i_rec = cmd.ExecuteNonQuery();
            if (i_rec <= 0)
            {
                sql = "insert into " + user + ".bhyt_maubaocao(id,maloai,tenloai,ten,filereport,stt,loai,tonghop) values (:id,:maloai,:tenloai,:tenmau,:filereport,:stt,:i_loai,:i_tonghop)";
                con = new NpgsqlConnection(sConn);
                try
                {
                    con.Open();
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("id", NpgsqlDbType.Numeric).Value = id;
                    cmd.Parameters.Add("maloai", NpgsqlDbType.Varchar, 64).Value = maloai;
                    cmd.Parameters.Add("tenloai", NpgsqlDbType.Text).Value = tenloai;
                    cmd.Parameters.Add("tenmau", NpgsqlDbType.Text).Value = tenmau;
                    cmd.Parameters.Add("filereport", NpgsqlDbType.Varchar, 64).Value = filereport;
                    cmd.Parameters.Add("stt", NpgsqlDbType.Numeric).Value = istt;
                    cmd.Parameters.Add("i_loai", NpgsqlDbType.Numeric).Value = i_loai;
                    cmd.Parameters.Add("i_tonghop", NpgsqlDbType.Numeric).Value = i_tonghop;

                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }
            }
        }
        public long get_id_bhyt_maubaocao()
        {
            long i_id;
            try
            {
                sql = "select nvl(max(id),0) as id from " + user + ".bhyt_maubaocao";
                i_id = long.Parse(get_data(sql).Tables[0].Rows[0]["id"].ToString()) + 1;
            }
            catch { i_id = 1; }
            return i_id;
        }
        public void upd_v_nhombhyt_stt(int id, int stt, string idnhomvp)
        {
            sql = " update " + user + ".v_nhombhyt set stt=" + stt + " where id=" + id.ToString();
            execute_data(sql);
            //
            sql = "update " + user + ".v_nhomvp set idnhombhyt=0 where idnhombhyt=" + id.ToString();
            execute_data(sql);
            //
            string[] a_nhomvp;
            a_nhomvp = idnhomvp.Split(',');
            for (int i = 0; i < a_nhomvp.Length - 1; i++)
            {
                sql = "update " + user + ".v_nhomvp set idnhombhyt=" + id + " where ma=" + a_nhomvp[i];
                execute_data(sql);
            }
        }
        public DataTable get_dmnhombc_bhyt(int i_nhombc)
        {
            if (i_nhombc == 0)
            {
                sql = "select a.ten, a.id as idnhomvp, ' ' as idloaivp, a.id as stt from " + user + ".v_nhombhyt a order by stt ";
            }
            else
            {
                sql = "select * from " + user + ".dmbaocao_bhyt order by stt ";
            }
            return get_data(sql).Tables[0];
        }
        public string get_stt_nhombc(System.Data.DataTable dt, string s_idnhom, string s_idloai)
        {
            string s_stt = "";
            string s_exp = " idnhomvp=" + s_idnhom;
            string s_tmp = "";
            foreach (DataRow r in dt.Select(s_exp, ""))
            {
                s_stt = r["stt"].ToString();
                s_tmp = "," + r["idloaivp"].ToString();
                if (s_tmp.IndexOf("," + s_idloai.Trim() + ",") >= 0)
                {
                    s_stt = r["stt"].ToString();
                    break;
                }
            }
            return s_stt;
        }
        public bool sothe(int madoituong, string sothe)
        {
            int i = 0; bool ret = false;
            string s1 = "", s2 = "", s3 = "'", dau = ",+;.", so = "0123456789";
            ds = get_data("select dai from " + user + ".doituong where madoituong=" + madoituong);
            if (ds.Tables[0].Rows.Count > 0)
                s1 = ds.Tables[0].Rows[0]["dai"].ToString().Trim();
            if (s1 != "")
            {
                while (i < s1.Length)
                {
                    if (dau.IndexOf(s1.Substring(i, 1)) != -1)
                    {
                        if (s2 != "") s3 += s2.PadLeft(2, '0') + "','";
                        s2 = "";
                    }
                    else if (so.IndexOf(s1.Substring(i, 1)) != -1) s2 += s1.Substring(i, 1);
                    i++;
                }
                if (s2 != "") s3 += s2.PadLeft(2, '0') + "','";
                s3 = (s3.Length > 1) ? s3.Substring(0, s3.Length - 2) : "";
                ret = s3.IndexOf("'" + sothe.Length.ToString().PadLeft(2, '0') + "'") != -1;
            }
            return ret;
        }
        #endregion

        #region PTTT
        public string get_tenpt(string mapt)
        {
            if (mapt == "") return "";
            try
            {
                DataTable dt = get_data("select loaipt,noi_dung from " + user + ".dmpttt where trim(mapt)='" + mapt.Trim() + "'").Tables[0];
                return dt.Rows[0][0].ToString().Trim() + dt.Rows[0][1].ToString();
            }
            catch { return ""; }
        }

        #endregion

        #region The kho
        public DataSet get_tondau(DataSet ds, DataTable dt, string d_mmyy, int d_makho, int d_manguon, int d_nhom)
        {
            DataRow r1, r2, r3;
            DataRow[] dr;
            string s_khokhongin = "", usr = user, xxx = usr + d_mmyy;
            if (d_makho == -1)
            {
                foreach (DataRow r in get_data("select * from " + usr + ".d_dmkho where nhom=" + d_nhom + " and ketoan=1").Tables[0].Rows)
                    s_khokhongin += r["id"].ToString().Trim() + ",";
            }
            sql = "select a.mabd,sum(a.tondau) as tondau,sum(a.tondau*b.giamua) as sttondau from " + xxx + ".d_tonkhoct a," + xxx + ".d_theodoi b";
            sql += " where a.stt=b.id and a.tondau<>0";
            if (d_makho != -1) sql += " and a.makho=" + d_makho;
            else if (s_khokhongin != "") sql += " and a.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
            if (d_manguon != -1) sql += " and b.manguon=" + d_manguon;
            sql += " and a.mabd in (select mabd from " + usr + ".d_mabd) group by a.mabd";
            if (d_makho == -1)
            {
                sql += " union all ";
                sql += "select a.mabd,sum(a.tondau) as tondau,sum(a.tondau*b.giamua) as sttondau from " + xxx + ".d_tutrucct a," + xxx + ".d_theodoi b";
                sql += " where a.stt=b.id and a.tondau<>0";
                if (s_khokhongin != "") sql += " and a.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                if (d_manguon != -1) sql += " and b.manguon=" + d_manguon;
                sql += " and a.mabd in (select mabd from " + usr + ".d_mabd) group by a.mabd";
            }
            bool bCongdon = bThekho_congdon(get_nhomkho);
            foreach (DataRow r in get_data(sql).Tables[0].Rows)
            {
                r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                if (r1 != null)
                {
                    r3 = getrowbyid(ds.Tables[0], "mabd=" + int.Parse(r["mabd"].ToString()));
                    if (r3 == null)
                    {
                        r2 = ds.Tables[0].NewRow();
                        r2["yymmdd"] = "";
                        r2["ngay"] = "";
                        r2["sonhap"] = "";
                        r2["soxuat"] = "";
                        r2["mabd"] = r["mabd"].ToString();
                        r2["ma"] = r1["ma"].ToString();
                        r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                        r2["tenhc"] = r1["tenhc"].ToString();
                        r2["dang"] = r1["dang"].ToString();
                        if (bCongdon)
                        {
                            r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                            r2["tennuoc"] = r1["tennhom"].ToString();
                        }
                        else
                        {
                            r2["tenhang"] = r1["tenhang"].ToString();
                            r2["tennuoc"] = r1["tennuoc"].ToString();
                        }
                        r2["diengiai"] = "Tồn đầu";
                        r2["tondau"] = r["tondau"].ToString();
                        r2["sttondau"] = r["sttondau"].ToString();
                        r2["slnhap"] = 0;
                        r2["stnhap"] = 0;
                        r2["slxuat"] = 0;
                        r2["stxuat"] = 0;
                        r2["toncuoi"] = r["tondau"].ToString();
                        r2["sttoncuoi"] = r["sttondau"].ToString();
                        ds.Tables[0].Rows.Add(r2);
                    }
                    else
                    {
                        dr = ds.Tables[0].Select("mabd=" + int.Parse(r["mabd"].ToString()));
                        if (dr.Length > 0)
                        {
                            dr[0]["tondau"] = decimal.Parse(dr[0]["tondau"].ToString()) + decimal.Parse(r["tondau"].ToString());
                            dr[0]["sttondau"] = decimal.Parse(dr[0]["sttondau"].ToString()) + decimal.Parse(r["sttondau"].ToString());
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }

        public DataSet get_nhap(DataSet ds, DataTable dt, DataTable dtkp, DataTable dtkho, string d_tu, string d_den, string d_yy, int d_makho, int d_manguon, int d_nhom)
        {
            string s_khokhongin = "", usr = user, xxx;
            if (d_makho == -1)
            {
                foreach (DataRow r in get_data("select * from " + usr + ".d_dmkho where nhom=" + d_nhom + " and ketoan=1").Tables[0].Rows)
                    s_khokhongin += r["id"].ToString().Trim() + ",";
            }
            bool bSophieu = bThekho_sophieu(d_nhom), bDongia = bDongia_thekho(d_nhom);
            bool bCongdon = bThekho_congdon(d_nhom);
            DataRow r1, r2, r3;
            string s_diengiai = "", d_mmyy;
            int i_tu = int.Parse(d_tu), i_den = int.Parse(d_den);
            for (int i = i_tu; i <= i_den; i++)
            {
                d_mmyy = i.ToString().PadLeft(2, '0') + d_yy;
                if (bMmyy(d_mmyy))
                {
                    xxx = usr + d_mmyy;
                    sql = "select ";
                    if (bSophieu) sql += "to_char(a.ngaysp,'dd/mm/yyyy') as ngay,a.sophieu as so,";
                    else sql += "to_char(a.ngayhd,'dd/mm/yyyy') as ngay,a.sohd as so,";
                    sql += "b.mabd,b.handung,b.soluong,b.sotien+b.sotien*b.vat/100+b.cuocvc+b.chaythu as sotien,a.loai,c.ten as diengiai,b.giamua,a.nguoigiao ";
                    sql += " from " + xxx + ".d_nhapll a inner join " + xxx + ".d_nhapct b on a.id=b.id ";
                    sql += " left join " + user + ".d_dmnx c on a.madv=c.id";
                    sql += " where b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    if (d_makho != -1) sql += " and a.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and a.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and a.manguon=" + d_manguon;
                    sql += " order by a.ngayhd";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = r["so"].ToString();
                            r2["soxuat"] = "";
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            switch (r["loai"].ToString())
                            {
                                case "M": r2["diengiai"] = "Nhập mới: ";
                                    break;
                                case "T": r2["diengiai"] = "Tái nhập: ";
                                    break;
                                default: r2["diengiai"] = "Hoàn trả : ";
                                    break;
                            }
                            r2["diengiai"] += (r["loai"].ToString() == "N") ? r["nguoigiao"].ToString() : r["diengiai"].ToString();
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = r["soluong"].ToString();
                            r2["stnhap"] = r["sotien"].ToString();
                            r2["slxuat"] = 0;
                            r2["stxuat"] = 0;
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["handung"] = r["handung"].ToString();
                            if (bDongia) r2["dongia"] = r["giamua"].ToString();
                            else r2["dongia"] = 0;
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    if (d_makho != -1)
                    {
                        sql = "select a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,b.soluong,b.soluong*c.giamua as sotien,c.giamua,c.handung ";
                        sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi c ";
                        sql += " where a.id=b.id and b.sttt=c.id and a.loai in ('CK','TH','HT') and a.khon=" + d_makho;
                        if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) order by a.ngay";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtkp, "id=" + int.Parse(r["khox"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = r["so"].ToString();
                                r2["soxuat"] = "";
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                if (bCongdon)
                                {
                                    r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                    r2["tennuoc"] = r1["tennhom"].ToString();
                                }
                                else
                                {
                                    r2["tenhang"] = r1["tenhang"].ToString();
                                    r2["tennuoc"] = r1["tennuoc"].ToString();
                                }
                                switch (r["loai"].ToString())
                                {
                                    case "TH":
                                        s_diengiai = "Thu hồi :" + r3["ten"].ToString();
                                        break;
                                    case "HT":
                                        s_diengiai = r3["ten"].ToString() + ": Hoàn trả ";
                                        break;
                                    default:
                                        r3 = getrowbyid(dtkho, "id=" + int.Parse(r["khox"].ToString()));
                                        if (r3 != null) s_diengiai = "Nhập từ :" + r3["ten"].ToString();
                                        break;
                                }
                                r2["diengiai"] = s_diengiai;
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = r["soluong"].ToString();
                                r2["stnhap"] = r["sotien"].ToString();
                                r2["slxuat"] = 0;
                                r2["stxuat"] = 0;
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                r2["handung"] = r["handung"].ToString();
                                if (bDongia) r2["dongia"] = r["giamua"].ToString();
                                else r2["dongia"] = 0;
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                    }
                    else if (s_khokhongin != "")
                    {
                        sql = "select a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,b.soluong,b.soluong*c.giamua as sotien,c.giamua,c.handung ";
                        sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi c ";
                        sql += " where a.id=b.id and b.sttt=c.id and a.loai in ('CK')";
                        sql += " and a.khox in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                        sql += " and a.khon not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                        if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) order by a.ngay";
                        foreach (DataRow r in get_data(sql).Tables[0].Rows)
                        {
                            r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                            if (r1 != null)
                            {
                                r3 = getrowbyid(dtkp, "id=" + int.Parse(r["khox"].ToString()));
                                r2 = ds.Tables[0].NewRow();
                                r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                                r2["ngay"] = r["ngay"].ToString();
                                r2["sonhap"] = r["so"].ToString();
                                r2["soxuat"] = "";
                                r2["mabd"] = r["mabd"].ToString();
                                r2["ma"] = r1["ma"].ToString();
                                r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                                r2["tenhc"] = r1["tenhc"].ToString();
                                r2["dang"] = r1["dang"].ToString();
                                if (bCongdon)
                                {
                                    r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                    r2["tennuoc"] = r1["tennhom"].ToString();
                                }
                                else
                                {
                                    r2["tenhang"] = r1["tenhang"].ToString();
                                    r2["tennuoc"] = r1["tennuoc"].ToString();
                                }
                                r3 = getrowbyid(dtkho, "id=" + int.Parse(r["khox"].ToString()));
                                if (r3 != null) s_diengiai = "Nhập từ :" + r3["ten"].ToString();
                                r2["diengiai"] = s_diengiai;
                                r2["tondau"] = 0;
                                r2["sttondau"] = 0;
                                r2["slnhap"] = r["soluong"].ToString();
                                r2["stnhap"] = r["sotien"].ToString();
                                r2["slxuat"] = 0;
                                r2["stxuat"] = 0;
                                r2["toncuoi"] = r["soluong"].ToString();
                                r2["sttoncuoi"] = r["sotien"].ToString();
                                r2["handung"] = r["handung"].ToString();
                                if (bDongia) r2["dongia"] = r["giamua"].ToString();
                                else r2["dongia"] = 0;
                                ds.Tables[0].Rows.Add(r2);
                            }
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi c where a.id=b.id and b.sttt=c.id and a.loai=3";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "c.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = r["ngay"].ToString();
                            r2["soxuat"] = "";
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = r3["ten"].ToString().Trim() + ":" + "Hoàn trả ";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = r["soluong"].ToString();
                            r2["stnhap"] = r["sotien"].ToString();
                            r2["slxuat"] = 0;
                            r2["stxuat"] = 0;
                            r2["dongia"] = r["giamua"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }

        public DataSet get_xuat(DataSet ds, DataTable dt, DataTable dtkp, DataTable dtloaint, DataTable dtkhac, DataTable dtkho, string d_tu, string d_den, string d_yy, int d_makho, int d_manguon, int d_nhom)
        {
            string s_khokhongin = "", usr = user, xxx;
            if (d_makho == -1)
            {
                foreach (DataRow r in get_data("select * from " + usr + ".d_dmkho where nhom=" + d_nhom + " and ketoan=1").Tables[0].Rows)
                    s_khokhongin += r["id"].ToString().Trim() + ",";
            }
            DataTable dtvay = get_data("select * from " + usr + ".d_dmvay").Tables[0];
            DataTable dtnx = get_data("select * from " + usr + ".d_dmnx where nhom=" + d_nhom).Tables[0];
            DataRow r1, r2, r3;
            bool bDongia = bDongia_thekho(d_nhom), bCongdon = bThekho_congdon(d_nhom);
            string s_diengiai, d_mmyy;
            int i_tu = int.Parse(d_tu), i_den = int.Parse(d_den);
            for (int i = i_tu; i <= i_den; i++)
            {
                d_mmyy = i.ToString().PadLeft(2, '0') + d_yy;
                if (bMmyy(d_mmyy))
                {
                    xxx = usr + d_mmyy;
                    sql = "select a.idduyet,a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,c.handung,c.giamua,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien ";
                    sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi c ";
                    sql += " where a.id=b.id and b.sttt=c.id";
                    if (d_makho != -1) sql += " and a.loai in ('CK','BS','XK','VA') and a.khox=" + d_makho;
                    else
                    {
                        sql += " and a.loai in ('XK')";
                        if (s_khokhongin != "") sql += " and a.khox not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    }
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd)";
                    sql += " group by a.idduyet,a.sophieu,to_char(a.ngay,'dd/mm/yyyy'),a.lydo,a.loai,a.khox,a.khon,b.mabd,c.handung,c.giamua";
                    if (d_makho == -1 && s_khokhongin != "")
                    {
                        sql += " union all ";
                        sql += "select a.idduyet,a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,c.handung,c.giamua,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien ";
                        sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi c ";
                        sql += " where a.id=b.id and b.sttt=c.id";
                        sql += " and a.loai='CK'";
                        sql += " and a.khox not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                        sql += " and a.khon in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                        if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                        sql += " and b.mabd in (select mabd from " + usr + ".d_mabd)";
                        sql += " group by a.idduyet,a.sophieu,to_char(a.ngay,'dd/mm/yyyy'),a.lydo,a.loai,a.khox,a.khon,b.mabd,c.handung,c.giamua";
                    }
                    foreach (DataRow r in get_data(sql).Tables[0].Select("true", "ngay"))
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["so"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            switch (r["loai"].ToString())
                            {
                                case "CK":
                                    s_diengiai = "Xuất đến :";
                                    r3 = getrowbyid(dtkho, "id=" + int.Parse(r["khon"].ToString()));
                                    break;
                                case "BS":
                                    s_diengiai = "Bổ sung tủ trực :";
                                    r3 = getrowbyid(dtkp, "id=" + int.Parse(r["khon"].ToString()));
                                    break;
                                case "VA":
                                    s_diengiai = "Vay :";
                                    r3 = getrowbyid(dtvay, "id=" + int.Parse(r["khon"].ToString()));
                                    break;
                                default:
                                    if (int.Parse(r["khon"].ToString()) == 0)
                                    {
                                        s_diengiai = "Hoàn trả :";
                                        r3 = getrowbyid(dtnx, "id=" + int.Parse(r["idduyet"].ToString()));
                                    }
                                    else
                                    {
                                        s_diengiai = r["lydo"].ToString().Trim();
                                        s_diengiai += (s_diengiai != "") ? ":" : "";
                                        r3 = getrowbyid(dtkhac, "id=" + int.Parse(r["khon"].ToString()));
                                    }
                                    break;
                            }
                            r2["diengiai"] = s_diengiai;
                            r2["diengiai"] += (r3 != null) ? r3["ten"].ToString() : "";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["handung"] = r["handung"].ToString();
                            if (bDongia) r2["dongia"] = r["giamua"].ToString();
                            else r2["dongia"] = 0;
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.loai,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi c where a.id=b.id and b.sttt=c.id and a.loai in (1,4) ";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "c.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.loai";
                    sql += " union all ";
                    sql += "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.loai,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a,";
                    if (d_makho != -1) sql += xxx + ".d_thucbucstt b";
                    else sql += xxx + ".d_thucxuat b";
                    sql += "," + xxx + ".d_theodoi c";
                    sql += " where a.id=b.id and b.sttt=c.id and a.loai=2 ";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "c.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.loai";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            s_diengiai = (r["loai"].ToString() == "2" && d_makho == -1) ? "Tủ trực" : (r["loai"].ToString() == "1") ? "Lĩnh" : (r["loai"].ToString() == "2") ? "Bù trực" : "Hao phí";
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["ngay"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = "Xuất :" + r3["ten"].ToString() + "[" + s_diengiai + "]";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.mabn,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi c where a.id=b.id and b.sttt=c.id and a.loai in (1,4) ";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "c.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.mabn";
                    sql += " union all ";
                    sql += "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += "0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.mabn,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a,";
                    if (d_makho != -1) sql += xxx + ".d_thucbucstt b ";
                    else sql += xxx + ".d_thucxuat b ";
                    sql += "," + xxx + ".d_theodoi c ";
                    sql += "where a.id=b.id and b.sttt=c.id and a.loai=2 ";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd)";
                    sql += "group by ";
                    if (bDongia) sql += "c.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.mabn";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["mabn"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = "Xuất :" + r3["ten"].ToString();
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "c.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "a.mabn,a.hoten,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.loai,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*c.giamua) as sotien";
                    if (bCongdon) sql += ",c.handung";
                    else sql += ",'' as handung";
                    sql += " from " + xxx + ".d_ngtrull a," + xxx + ".d_ngtruct b," + xxx + ".d_theodoi c where a.id=b.id and b.sttt=c.id";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and c.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += " group by ";
                    if (bDongia) sql += "c.giamua,";
                    if (bCongdon) sql += "c.handung,";
                    sql += "a.mabn,a.hoten,to_char(a.ngay,'dd/mm/yyyy'),a.loai,b.mabd";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r3 = getrowbyid(dtloaint, "id=" + int.Parse(r["loai"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["mabn"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            if (bCongdon) r2["diengiai"] = "Xuất ngoại trú :" + r3["ten"].ToString().Trim();
                            else r2["diengiai"] = "Xuất ngoại trú :" + r3["ten"].ToString().Trim() + "[" + r["hoten"].ToString().Trim() + "]";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            r2["handung"] = r["handung"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "d.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "a.id,a.sothe,c.hoten,to_char(a.ngay,'dd/mm/yyyy') as ngay,b.mabd,sum(b.soluong) as soluong,sum(b.soluong*d.giamua) as sotien";
                    if (bCongdon) sql += ",d.handung";
                    else sql += ",'' as handung";
                    sql += " from " + xxx + ".bhytkb a," + xxx + ".bhytthuoc b," + xxx + ".bhytds c," + xxx + ".d_theodoi d where a.id=b.id and a.mabn=c.mabn and b.sttt=d.id";
                    if (d_makho != -1) sql += " and b.makho=" + d_makho;
                    else if (s_khokhongin != "") sql += " and b.makho not in (" + s_khokhongin.Substring(0, s_khokhongin.Length - 1) + ")";
                    if (d_manguon != -1) sql += " and d.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd)";
                    sql += "group by ";
                    if (bDongia) sql += "d.giamua,";
                    if (bCongdon) sql += "d.handung,";
                    sql += "a.id,a.sothe,c.hoten,to_char(a.ngay,'dd/mm/yyyy'),b.mabd";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["sothe"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            if (bCongdon) r2["diengiai"] = "BHYT Ngoại trú";
                            else r2["diengiai"] = "BHYT Ngoại trú [" + r["hoten"].ToString().Trim() + "]";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            r2["handung"] = r["handung"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }

        public DataSet get_tondau_cstt(DataSet ds, DataTable dt, string d_mmyy, int d_makp, int d_manguon)
        {
            DataRow r1, r2, r3;
            DataRow[] dr;
            string usr = user, xxx = usr + d_mmyy;
            sql = "select a.mabd,sum(a.tondau) as tondau,sum(a.tondau*t.giamua) as sttondau from " + xxx + ".d_tutrucct a," + xxx + ".d_theodoi t ";
            sql += " where a.stt=t.id and a.tondau<>0 and a.makp=" + d_makp;
            if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
            sql += " and a.mabd in (select mabd from " + usr + ".d_mabd) group by a.mabd";
            bool bCongdon = bThekho_congdon(get_nhomkho);
            foreach (DataRow r in get_data(sql).Tables[0].Rows)
            {
                r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                if (r1 != null)
                {
                    r3 = getrowbyid(ds.Tables[0], "mabd=" + int.Parse(r["mabd"].ToString()));
                    if (r3 == null)
                    {
                        r2 = ds.Tables[0].NewRow();
                        r2["yymmdd"] = "";
                        r2["ngay"] = "";
                        r2["sonhap"] = "";
                        r2["soxuat"] = "";
                        r2["mabd"] = r["mabd"].ToString();
                        r2["ma"] = r1["ma"].ToString();
                        r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                        r2["tenhc"] = r1["tenhc"].ToString();
                        r2["dang"] = r1["dang"].ToString();
                        if (bCongdon)
                        {
                            r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                            r2["tennuoc"] = r1["tennhom"].ToString();
                        }
                        else
                        {
                            r2["tenhang"] = r1["tenhang"].ToString();
                            r2["tennuoc"] = r1["tennuoc"].ToString();
                        }
                        r2["diengiai"] = "Tồn đầu";
                        r2["tondau"] = r["tondau"].ToString();
                        r2["sttondau"] = r["sttondau"].ToString();
                        r2["slnhap"] = 0;
                        r2["stnhap"] = 0;
                        r2["slxuat"] = 0;
                        r2["stxuat"] = 0;
                        r2["toncuoi"] = r["tondau"].ToString();
                        r2["sttoncuoi"] = r["sttondau"].ToString();
                        ds.Tables[0].Rows.Add(r2);
                    }
                    else
                    {
                        dr = ds.Tables[0].Select("mabd=" + int.Parse(r["mabd"].ToString()));
                        if (dr.Length > 0)
                        {
                            dr[0]["tondau"] = decimal.Parse(dr[0]["tondau"].ToString()) + decimal.Parse(r["tondau"].ToString());
                            dr[0]["sttondau"] = decimal.Parse(dr[0]["sttondau"].ToString()) + decimal.Parse(r["sttondau"].ToString());
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }

        public DataSet get_nhap_cstt(DataSet ds, DataTable dt, DataTable dtkp, DataTable dtkho, string d_tu, string d_den, string d_yy, int d_makp, int d_manguon, int d_nhom)
        {
            bool bSophieu = bThekho_sophieu(d_nhom), bDongia = bDongia_thekho(d_nhom);
            bool bCongdon = bThekho_congdon(d_nhom);
            DataRow r1, r2, r3;
            string s_diengiai = "", usr = user, d_mmyy, xxx;
            int i_tu = int.Parse(d_tu), i_den = int.Parse(d_den);
            for (int i = i_tu; i <= i_den; i++)
            {
                d_mmyy = i.ToString().PadLeft(2, '0') + d_yy;
                if (bMmyy(d_mmyy))
                {
                    xxx = usr + d_mmyy;
                    sql = "select a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,b.soluong,b.soluong*t.giamua as sotien,t.giamua ";
                    sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b ," + xxx + ".d_theodoi t ";
                    sql += " where a.id=b.id and b.sttt=t.id and a.loai in ('BS') ";
                    sql += " and a.khon=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) order by a.ngay";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["soxuat"] = "";
                            r2["sonhap"] = r["so"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            switch (r["loai"].ToString())
                            {
                                case "BS":
                                    s_diengiai = "Bổ sung tủ trực :";
                                    break;
                            }
                            r2["diengiai"] = s_diengiai;
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slxuat"] = 0;
                            r2["stxuat"] = 0;
                            r2["slnhap"] = r["soluong"].ToString();
                            r2["stnhap"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            if (bDongia) r2["dongia"] = r["giamua"].ToString();
                            else r2["dongia"] = 0;
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "t.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.loai,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucbucstt b," + xxx + ".d_theodoi t ";
                    sql += " where a.id=b.id and b.sttt=t.id and a.loai in (2) ";
                    sql += " and a.makp=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "t.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.loai";
                    sql += " union all ";
                    sql += "select ";
                    if (bDongia) sql += "t.giamua,";
                    else sql += "0,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.loai,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucbucstt b," + xxx + ".d_theodoi t ";
                    sql += "where a.id=b.id and b.sttt=t.id and a.loai=2 ";
                    sql += " and a.makp=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd)";
                    sql += "group by ";
                    if (bDongia) sql += "t.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.loai";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            s_diengiai = "Bù trực";
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["soxuat"] = "";
                            r2["sonhap"] = r["ngay"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = "Nhập :" + "[" + s_diengiai + "]";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slxuat"] = 0;
                            r2["stxuat"] = 0;
                            r2["slnhap"] = r["soluong"].ToString();
                            r2["stnhap"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }

        public DataSet get_xuat_cstt(DataSet ds, DataTable dt, DataTable dtkp, DataTable dtloaint, DataTable dtkhac, DataTable dtkho, string d_tu, string d_den, string d_yy, int d_makp, int d_manguon, int d_nhom)
        {
            string s_diengiai = "", d_mmyy, usr = user, xxx;
            DataTable dtvay = get_data("select * from " + usr + ".d_dmvay").Tables[0];
            DataRow r1, r2, r3;
            bool bDongia = bDongia_thekho(d_nhom), bCongdon = bThekho_congdon(d_nhom);
            int i_tu = int.Parse(d_tu), i_den = int.Parse(d_den);
            for (int i = i_tu; i <= i_den; i++)
            {
                d_mmyy = i.ToString().PadLeft(2, '0') + d_yy;
                if (bMmyy(d_mmyy))
                {
                    xxx = usr + d_mmyy;
                    sql = "select a.sophieu as so,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.lydo,a.loai,a.khox,a.khon,b.mabd,b.soluong,b.soluong*t.giamua as sotien,t.giamua ";
                    sql += " from " + xxx + ".d_xuatll a," + xxx + ".d_xuatct b," + xxx + ".d_theodoi t ";
                    sql += " where a.id=b.id and b.sttt=t.id and a.loai in ('TH','HT') and a.khox=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and b.mabd in (select mabd from " + usr + ".d_mabd) order by a.ngay";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["khox"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["soxuat"] = r["so"].ToString();
                            r2["sonhap"] = "";
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            switch (r["loai"].ToString())
                            {
                                case "TH":
                                    s_diengiai = "Thu hồi :" + r3["ten"].ToString();
                                    break;
                                case "HT":
                                    s_diengiai = r3["ten"].ToString() + ": Hoàn trả ";
                                    break;
                            }
                            r2["diengiai"] = s_diengiai;
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            if (bDongia) r2["dongia"] = r["giamua"].ToString();
                            else r2["dongia"] = 0;
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "t.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.loai,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi t ";
                    sql += " where a.id=b.id and b.sttt=t.id and a.loai in (2) ";
                    sql += " and a.makp=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and a.maql<>0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "t.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.loai";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            s_diengiai = "Tủ trực";
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["ngay"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = "Xuất :" + r3["ten"].ToString() + "[" + s_diengiai + "]";
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                    sql = "select ";
                    if (bDongia) sql += "t.giamua,";
                    else sql += " 0 as giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy') as ngay,a.makp,b.mabd,a.mabn,sum(b.soluong) as soluong,sum(b.soluong*t.giamua) as sotien";
                    sql += " from " + xxx + ".d_xuatsdll a," + xxx + ".d_thucxuat b," + xxx + ".d_theodoi t ";
                    sql += " where a.id=b.id and b.sttt=t.id and a.loai in (2) ";
                    sql += " and a.makp=" + d_makp;
                    if (d_manguon != -1) sql += " and t.manguon=" + d_manguon;
                    sql += " and a.maql=0 and b.mabd in (select mabd from " + usr + ".d_mabd) ";
                    sql += "group by ";
                    if (bDongia) sql += "t.giamua,";
                    sql += "to_char(a.ngay,'dd/mm/yyyy'),a.makp,b.mabd,a.mabn";
                    foreach (DataRow r in get_data(sql).Tables[0].Rows)
                    {
                        r1 = getrowbyid(dt, "id=" + int.Parse(r["mabd"].ToString()));
                        if (r1 != null)
                        {
                            r3 = getrowbyid(dtkp, "id=" + int.Parse(r["makp"].ToString()));
                            r2 = ds.Tables[0].NewRow();
                            r2["yymmdd"] = r["ngay"].ToString().Substring(8, 2) + r["ngay"].ToString().Substring(3, 2) + r["ngay"].ToString().Substring(0, 2);
                            r2["ngay"] = r["ngay"].ToString();
                            r2["sonhap"] = "";
                            r2["soxuat"] = r["mabn"].ToString();
                            r2["mabd"] = r["mabd"].ToString();
                            r2["ma"] = r1["ma"].ToString();
                            r2["ten"] = r1["ten"].ToString().Trim() + " " + r1["hamluong"].ToString();
                            r2["tenhc"] = r1["tenhc"].ToString();
                            r2["dang"] = r1["dang"].ToString();
                            if (bCongdon)
                            {
                                r2["tenhang"] = r1["tenhang"].ToString().Trim() + "-" + r1["tennuoc"].ToString();
                                r2["tennuoc"] = r1["tennhom"].ToString();
                            }
                            else
                            {
                                r2["tenhang"] = r1["tenhang"].ToString();
                                r2["tennuoc"] = r1["tennuoc"].ToString();
                            }
                            r2["diengiai"] = "Xuất :" + r3["ten"].ToString();
                            r2["tondau"] = 0;
                            r2["sttondau"] = 0;
                            r2["slnhap"] = 0;
                            r2["stnhap"] = 0;
                            r2["slxuat"] = r["soluong"].ToString();
                            r2["stxuat"] = r["sotien"].ToString();
                            r2["toncuoi"] = r["soluong"].ToString();
                            r2["sttoncuoi"] = r["sotien"].ToString();
                            r2["dongia"] = r["giamua"].ToString();
                            ds.Tables[0].Rows.Add(r2);
                        }
                    }
                }
            }
            ds.AcceptChanges();
            return ds;
        }


        #endregion

        #region Son Kinh Thêm
        public DataSet get_data_th(string tu, string den, string sFormat, string m_schemas, string makp,string mau)
        {
            DataSet ds1 = new DataSet();
            switch(mau)
            {
                case "01":
                    ds1 = f_get_data_Mau1(tu, den, m_schemas, makp);
                    break;
                case "02":
                    ds1 = get_data_Mau2(tu, den, m_schemas, makp);
                    break;
                case "03":
                    ds1 = get_data_Mau3(tu, den, sFormat, m_schemas, makp);
                    break;
                case "04":
                    ds1 = get_data_Mau4(tu, den, sFormat, m_schemas, makp);
                    break;
                case "06":
                    ds1 = get_data_Mau6(tu, den, sFormat, m_schemas, makp);
                    break;
                case "07":
                    ds1 = get_data_Mau7(tu, den, sFormat, m_schemas, makp);
                    break;
                default :
                    ds1 = f_get_data_Mau1(tu, den, m_schemas, makp);
                    break;
            }
            
            return ds1;
        }
        private DataSet f_get_data_Mau1(string vtungay, string vdenngay, string vschemas,string vmakp)
        {
            DataSet ads = new DataSet();
            sql = "select 1 as loai,a.mabn,b.hoten,b.namsinh,a.maql,to_char(a.ngay,'dd/mm/yyyy') as ngayvv,a.maicd as maicdvv,a.chandoan as chandoanvv,to_char(a.ngay,'dd/mm/yyyy') as ngayrv,a.ttlucrv,a.maicd as maicdrv,a.chandoan as chandoanrv,a.sovaovien as soluutru";
            sql += " from ((((xxx.benhanpk a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn)";
            sql += " left join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join xxx.xutrikbct d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong e on a.maql=e.maql)";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " union all ";
            sql += " select 3 as loai,a.mabn,b.hoten,b.namsinh,a.maql,to_char(a.ngay,'dd/mm/yyyy') as ngayvv,a.maicd as maicdvv,a.chandoan as chandoanvv,to_char(a.ngayrv,'dd/mm/yyyy') as ngayrv,a.ttlucrv,a.maicdrv,a.chandoanrv,a.soluutru";
            sql += " from ((((xxx.benhancc a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn)";
            sql += " left join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join xxx.xutrikbct d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong e on a.maql=e.maql)";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";

            ads.Merge(f_get_pttt(vtungay, vdenngay, vschemas, vmakp, 1));
            //sql = "select sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c01,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c011,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c012,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c02,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c021,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c022";
            //sql += " from ((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            //sql += " inner join xxx.pttt c on a.maql=c.maql)";
            //sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " where a.loaiba=1";//to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            //sql += " union all ";
            //sql += " select sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c01,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c011,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c012,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c02,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c021,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c022";
            //sql += " from (" + vschemas + ".benhanngtr a ";
            //sql += " inner join xxx.pttt c on a.maql=c.maql)";
            //sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " union all ";
            //sql += " select sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c01,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c011,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c012,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c02,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c021,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c022";
            //sql += " from (xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            //sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " union all ";
            //sql += " select sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c01,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c011,";
            //sql += " sum(case when substr(c.mapt,0,1)='P' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c012,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c02,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=2 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c021,";
            //sql += " sum(case when substr(c.mapt,0,1)='T' and c.makp in(" + vmakp + ") and c.tinhhinh=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c022";
            //sql += " from (xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            //sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //ads = get_data_mmyy(sql, vtungay, vdenngay, false);

            //sql = "select sum(case when d.ttlucrk=6 then 1 else 0 end) c03,";
            //sql += " sum(case when d.ttlucrk=6 then 1 else 0 end) c031,";
            //sql += " sum(0) c032,";
            //sql += " sum(case when d.ttlucrk=7 then 1 else 0 end) c04,";
            //sql += " sum(case when d.ttlucrk=7 and to_number(to_char(d.ngay,'yyyy'))-to_number(f.namsinh)>15 then 1 else 0 end) c041,";
            //sql += " sum(case when d.ttlucrk=7 and 5<=to_number(to_char(d.ngay,'yyyy'))-to_number(f.namsinh)<=15  then 1 else 0 end) c042,";
            //sql += " sum(case when d.ttlucrk=7 and to_number(to_char(d.ngay,'yyyy'))-to_number(f.namsinh)<5 then 1 else 0 end) c043,";
            //sql += " sum(case when d.ttlucrk=7 and to_date(to_char(d.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) c044";
            //sql += " from ((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            //sql += " inner join " + vschemas + ".xuatkhoa d on b.id=d.id)";
            //sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " where a.loaiba=1 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            //sql += " union all ";
            //sql += " select sum(case when a.ttlucrv=6 then 1 else 0 end) c03,sum(0) as c031,";
            //sql += " sum(case when a.ttlucrv=6 then 1 else 0 end) c032,";
            //sql += " sum(case when a.ttlucrv=7 then 1 else 0 end) c04,";
            //sql += " sum(case when a.ttlucrv=7 and to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)>15 then 1 else 0 end) c041,";
            //sql += " sum(case when a.ttlucrv=7 and 5<=to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)<=15  then 1 else 0 end) c042,";
            //sql += " sum(case when a.ttlucrv=7 and to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)<5 then 1 else 0 end) c043,";
            //sql += " sum(case when a.ttlucrv=7 and to_date(to_char(a.ngayrv,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) c044";
            //sql += " from " + vschemas + ".benhanngtr a inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            //ads.Merge(get_data(sql));
            ads.Merge(f_get_chuyenvien_th(vtungay, vdenngay, vschemas, vmakp, 2));

            //sql = " select sum(case when a.ttlucrv=6 then 1 else 0 end) c03,sum(0) as c031,";
            //sql += " sum(case when a.ttlucrv=6 then 1 else 0 end) c032,";
            //sql += " sum(case when a.ttlucrv=7 then 1 else 0 end) c04,";
            //sql += " sum(case when a.ttlucrv=7 and to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)>15 then 1 else 0 end) c041,";
            //sql += " sum(case when a.ttlucrv=7 and 5<=to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)<=15  then 1 else 0 end) c042,";
            //sql += " sum(case when a.ttlucrv=7 and to_number(to_char(a.ngayrv,'yyyy'))-to_number(f.namsinh)<5 then 1 else 0 end) c043,";
            //sql += " sum(case when a.ttlucrv=7 and to_date(to_char(a.ngayrv,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) c044";
            //sql += " from xxx.benhancc a inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            //sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            //ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            return ads;
        }
        /// <summary>
        /// Tổng hợp số liệu chuyển viện nội và ngoại trú
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_chuyenvien_th(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot)
        {
            int l_ivcot = vicot;
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when d.maql>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(0) as " + vcot.ToString() + "2,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or b.ttlucrv=7) and to_number(to_char(b.ngay,'yyyy'))-to_number(e.namsinh)>15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "1,";
            sql += " sum(case when (f.maql>0 or b.ttlucrv=7) and to_number(to_char(b.ngay,'yyyy'))-to_number(e.namsinh) between 5 and 15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "2,";
            sql += " sum(case when (f.maql>0 or b.ttlucrv=7) and to_number(to_char(b.ngay,'yyyy'))-to_number(e.namsinh)<5 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "3,";
            sql += " sum(case when (f.maql>0 or b.ttlucrv=7) and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when f.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot.ToString() + "";
            sql += " from (((" + vschemas + ".benhandt a inner join " + vschemas + ".xuatvien b on a.maql=b.maql)";
            sql += " left join " + vschemas + ".chuyenvien d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong f on a.maql=f.maql)";
            sql += " inner join " + vschemas + ".btdbn e on a.mabn=e.mabn";
            sql += " where a.loaiba=1 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and b.makp in(" + vmakp + ")";
            vicot = l_ivcot;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " union all ";
            sql += " select sum(0) as " + vcot.ToString() + "1,";
            sql += " sum(case when d.maql>0 then 1 else 0 end) as " + vcot.ToString() + "2,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh)>15 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "1,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh) between 5 and 15 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "2,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh)<5 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "3,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_date(to_char(a.ngayrv,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot.ToString() + "";
            sql += " from ((" + vschemas + ".benhanngtr a left join " + vschemas + ".chuyenvien d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong f on a.maql=f.maql)";
            sql += " inner join " + vschemas + ".btdbn e on a.mabn=e.mabn";
            sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            DataSet vds = get_data(sql);
            vicot = l_ivcot;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = " select sum(0) as " + vcot.ToString() + "1,";
            sql += " sum(case when d.maql>0 then 1 else 0 end) as " + vcot.ToString() + "2,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh)>15 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "1,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh) between 5 and 15 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "2,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_number(to_char(a.ngayrv,'yyyy'))-to_number(e.namsinh)<5 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "3,";
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or a.ttlucrv=7) and to_date(to_char(a.ngayrv,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot.ToString() + "";
            sql += " from ((xxx.benhancc a left join " + vschemas + ".chuyenvien d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong f on a.maql=f.maql)";
            sql += " inner join " + vschemas + ".btdbn e on a.mabn=e.mabn";
            sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            vicot = l_ivcot;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " union all ";
            sql += " select sum(0) as " + vcot.ToString() + "1,";
            sql += " sum(case when d.maql>0 then 1 else 0 end) as " + vcot.ToString() + "2,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when (f.maql>0 or b.xutri like '%07,') and to_number(to_char(a.ngay,'yyyy'))-to_number(e.namsinh)>15 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "1,";
            sql += " sum(case when (f.maql>0 or b.xutri like '%07,') and to_number(to_char(a.ngay,'yyyy'))-to_number(e.namsinh) between 5 and 15 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "2,";
            sql += " sum(case when (f.maql>0 or b.xutri like '%07,') and to_number(to_char(a.ngay,'yyyy'))-to_number(e.namsinh)<5 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + "3,";
            sql += " sum(case when (f.maql>0 or b.xutri like '%07,') and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) " + vcot.ToString() + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(0) as " + vcot.ToString() + "";
            sql += " from (((xxx.benhanpk a inner join xxx.xutrikbct b on a.maql=b.maql)";
            sql += " left join " + vschemas + ".chuyenvien d on a.maql=d.maql)";
            sql += " left join " + vschemas + ".tuvong f on a.maql=f.maql)";
            sql += " inner join " + vschemas + ".btdbn e on a.mabn=e.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            vds.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));
            return vds;
        }
        private DataSet get_data_Mau2(string vtungay, string vdenngay, string vschemas, string vmakp)
        {
            DataSet ads = new DataSet();
            string matt = "101";
            string m_dtkhamsuckhoe = "4,8";
            sql = "select sum(case when a.madoituong not in("+m_dtkhamsuckhoe+") then 1 else 0 end) as c01,sum(case when a.madoituong not in("+m_dtkhamsuckhoe+") then 1 else 0 end) as c011,";
            sql += " sum(case when a.madoituong not in("+m_dtkhamsuckhoe+") and a.nhantu='01' then 1 else 0 end) as c0111,sum(case when c.mannbo='01' then 1 else 0 end) as c0112,";
            sql += " sum(case when a.madoituong not in("+m_dtkhamsuckhoe+") and b.matt<>'" + matt + "' then 1 else 0 end) as c0113";
            sql += " from ((xxx.benhanpk a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn) inner join " + vschemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " union all ";
            sql += " select sum(case when a.maql>0 then 1 else 0 end) as c01,sum(case when a.maql>0 then 1 else 0 end) as c011,";
            sql += " sum(case when a.nhantu='01' then 1 else 0 end) as c0111,sum(case when c.mannbo='01' then 1 else 0 end) as c0112,";
            sql += " sum(case when b.matt<>'" + matt + "' then 1 else 0 end) as c0113";
            sql += " from ((xxx.benhancc a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn) inner join " + vschemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            ads = get_data_mmyy(sql, vtungay, vdenngay, false);

            sql = "select sum(case when a.ttlucrv=6 then 1 else 0 end) as c062,sum(case when a.ttlucrv=7 then 1 else 0 end) as c085,sum(case when a.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) as c095";
            sql += " from ((xxx.benhancc a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn) inner join " + vschemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            sql = "select count(distinct (a.mabn)) as c012,count(distinct (a.mabn)) as c022 from xxx.benhanpk a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn where madoituong in(" + m_dtkhamsuckhoe + ") and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') group by to_char(ngay,'dd/mm/yyyy')";
            ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            sql = "select count(*) as c021,sum(case when b.mannbo='01' then 1 else 0 end) as c0211,sum(case when b.matt<>'" + matt + "' then 1 else 0 end) as c0212";
            sql += " from (select a.mabn,b.matt,c.mannbo from (xxx.benhanpk a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn ) inner join " + vschemas + ".btdnn_bv c on b.mann=c.mann";
            sql += " where a.madoituong not in("+m_dtkhamsuckhoe+") and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') group by a.mabn,b.matt,c.mannbo,to_char(a.ngay,'dd/mm/yyyy')) as b";
            ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            sql = "select sum(case when a.ttlucrv>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c031,";
            sql += " sum(case when a.ttlucrv>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "')-to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as c032,";
            sql += " sum(case when a.ttlucrv=6 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c062,";
            sql += " sum(case when a.ttlucrv=7 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c085,";
            sql += " sum(case when a.ttlucrv=7 and to_number(to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi'))<1 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as c095";
            sql += " from " + vschemas + ".benhanngtr a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn";
            ads.Merge(get_data(sql));

            sql = "select sum(case when a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as c071,sum(case when a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as c072,sum(case when a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as c073,";
            sql += " sum(case when b.tuvong>0 then 1 else 0 end) as c0711,sum(case when b.mocapcuu=0 and (b.sonao=0 or b.cotsongco=0 or b.thuongkhac>0) then 1 else 0 end) as c0712,sum(case when b.mocapcuu=0 and (b.sonao=1 and b.cotsongco=1 and b.thuongkhac=0) then 1 else 0 end) as c0713";
            sql += " from (xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql) inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            string vcot = "c074";
            sql = " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (" + vschemas + ".benhanngtr a ";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            ads.Merge(get_data_mmyy(sql, vtungay, vdenngay, false));

            sql = "select sum(case when a.maql>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c04,";
            sql += " sum(case when b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c05,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=6 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c061,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)>15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c081,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh) between 5 and 15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c083,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<5 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c082,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c084,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)>15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c091,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and 5<=to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<=15 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c093,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<5 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c092,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) c094";
            sql += " from (" + vschemas + ".benhandt a left join " + vschemas + ".xuatvien b on a.maql=b.maql) inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            ads.Merge(get_data(sql));
            return ads;
        }
        private DataSet get_data_Mau3(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            DataSet ads = new DataSet();
            string m_dtkhamsuckhoe = "4,8";
            sql = "select sum(1) as a011,sum(case when b.makpbo='03' then 1 else 0 end) as a012,sum(case when b.makpbo='18' then 1 else 0 end) as a013,sum(case when b.makpbo='22' then 1 else 0 end) as a014,";
            sql += " sum(case when b.makpbo='17' then 1 else 0 end) as a015,sum(case when b.makpbo='23' then 1 else 0 end) as a016,sum(case when b.makpbo='13' then 1 else 0 end) as a017,sum(case when b.makpbo='25' then 1 else 0 end) as a018,";
            sql += " sum(case when c.xutri like '%05,%' then 1 else 0 end) as a019,sum(case when c.makp='10' and c.xutri like '%05,%' then 1 else 0 end) as a0191,sum(case when c.makp='02' and c.xutri like '%05,%' then 1 else 0 end) as a0192,sum(case when c.makp='03' and c.xutri like '%05,%' then 1 else 0 end) as a0193,";
            sql += " sum(case when c.xutri like '%07,%' then 1 else 0 end) as a020,sum(case when c.xutri like '%06,%' then 1 else 0 end) as a021,sum(case when a.nhantu=1 then 1 else 0 end) as a022";
            sql += " from (xxx.benhanpk a inner join "+m_schemas+".btdkp_bv b on a.makp=b.makp) inner join xxx.xutrikbct c on a.maql=c.maql";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by to_char(a.ngay,'dd/mm/yyyy')";
            ads=get_data_mmyy(sql, tu, den, false);

            sql = "select count(a.mabn) as a02,count(distinct (a.mabn)) as a0201 from ";
            sql += " xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn";
            sql += " where a.madoituong in(" + m_dtkhamsuckhoe + ") and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by to_char(a.ngay,'dd/mm/yyyy')";
            ads.Merge(get_data_mmyy(sql, tu, den, false));

            sql = "select count(a.mabn) as a021,count(distinct (a.mabn)) as a0211 from ";
            sql += " xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn";
            sql += " where a.madoituong in(4) and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by to_char(a.ngay,'dd/mm/yyyy')";
            ads.Merge(get_data_mmyy(sql, tu, den, false));

            sql = "select count(a.mabn) as a022,count(distinct (a.mabn)) as a0221 from ";
            sql += " xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn";
            sql += " where a.madoituong in(8) and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by to_char(a.ngay,'dd/mm/yyyy')";
            ads.Merge(get_data_mmyy(sql, tu, den, false));

            ads.Merge(f_get_tainan_solan_songuoi(m_schemas, tu, den, makp, 3));
            ads=zsum(ads);
            ads.Merge(f_get_data_noitruso_kotablename(m_schemas, tu, den, sformat));

            return ads;
        }

        private DataSet zsum(DataSet vds)
        {
            DataSet ads = vds.Clone();
            DataRow r1 = ads.Tables[0].NewRow();
            ads.Tables[0].Rows.Add(r1);
            decimal v_value = 0;
            string v_cot = "";
            foreach (DataColumn dc in vds.Tables[0].Columns)
            {
                v_value = 0;
                v_cot = "";
                foreach (DataRow r in vds.Tables[0].Rows)
                {
                    v_cot = r[dc.ToString()].ToString();
                    if (v_cot != "makp" || v_cot != "tenkp") v_value += (v_cot != "") ? decimal.Parse(v_cot) : 0;
                }
                ads.Tables[0].Rows[0][dc.ToString()] = v_value;
            }
            ads.Tables[0].AcceptChanges();
            return ads;
        }

        private DataSet f_get_tainan_solan_songuoi(string vschemas,string vtungay,string vdenngay,string vmakp,int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = " select count(a.mabn) as " + vcot.ToString() + "1,";
            sql += " count(distinct a.mabn) as " + vcot.ToString() + "1_1,";
            sql += " sum(case when a.nguyennhan not in(0,2) then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when a.nguyennhan=0 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when a.nguyennhan=2 then 1 else 0 end) as " + vcot.ToString() + "13";
            sql += " from ((xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join " + vschemas + ".benhandt e on a.maql=e.maql)";
            sql += " inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            if (vmakp != "") sql += " and e.makp in(" + vmakp + ")";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        private DataSet get_data_Mau4(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            DataSet ads = new DataSet();
            string matt = "101";

            ads.Merge(get_data_thngoaitru(tu, den, sFormat, m_schemas, makp,matt));
            ads.Merge(get_data_phongluu_chuyenvien_tuvong(tu,den,sFormat,m_schemas,makp));
            ads.Merge(get_data_ngoaitru_chuyenvien_tuvong(tu,den,sFormat,m_schemas,makp));
            ads.Merge(f_get_tainan(tu, den, sFormat, m_schemas, makp,4));
            ads.Merge(f_get_pttt(tu,den,m_schemas,makp,5));
            ads.Merge(get_data_noitru_chuyenvien_tuvong(tu,den,sFormat,m_schemas,makp));
            ads.Merge(get_data_xnghiem(tu,den,sFormat,m_schemas,makp));
            ads.Merge(f_get_data_cdha(tu, den, m_schemas, makp, 10));
            ads.Merge(get_data_noitru_ththbn(tu, den, sFormat, m_schemas, makp, 12, matt));
            ads.Merge(get_data_tkngaydtnoitru(tu, den, sFormat, m_schemas, makp, 16));
            
            return ads;
        }
        private DataSet get_data_thngoaitru(string tu, string den, string sFormat, string m_schemas, string makp,string matt)
        {
            DataSet ds1 = new DataSet();
            string m_dtkhamsuckhoe = "4,8";
            sql = "select sum(case when a.madoituong not in(" + m_dtkhamsuckhoe + ") then 1 else 0 end) as c01,sum(case when a.madoituong not in(" + m_dtkhamsuckhoe + ") then 1 else 0 end) as c011,";
            sql += " sum(case when a.madoituong not in(" + m_dtkhamsuckhoe + ") and a.nhantu='01' then 1 else 0 end) as c0111,sum(case when c.mannbo='01' then 1 else 0 end) as c0112,";
            sql += " sum(case when a.madoituong not in(" + m_dtkhamsuckhoe + ") and b.matt<>'" + matt + "' then 1 else 0 end) as c0113";
            sql += " from ((xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn) inner join " + m_schemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            sql += " union all ";
            sql += " select sum(case when a.maql>0 then 1 else 0 end) as c01,sum(case when a.maql>0 then 1 else 0 end) as c011,";
            sql += " sum(case when a.nhantu='01' then 1 else 0 end) as c0111,sum(case when c.mannbo='01' then 1 else 0 end) as c0112,";
            sql += " sum(case when b.matt<>'" + matt + "' then 1 else 0 end) as c0113";
            sql += " from ((xxx.benhancc a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn) inner join " + m_schemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            ds1=get_data_mmyy(sql, tu, den, false);

            sql = "select count(distinct (a.mabn)) as c012,count(distinct (a.mabn)) as c022 from xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn where madoituong in(" + m_dtkhamsuckhoe + ") and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by to_char(ngay,'dd/mm/yyyy')";
            ds1.Merge(get_data_mmyy(sql, tu, den, false));

            sql = "select count(*) as c021,sum(case when b.mannbo='01' then 1 else 0 end) as c0211,sum(case when b.matt='101' then 1 else 0 end) as c0212";
            sql += " from (select a.mabn,b.matt,c.mannbo from (xxx.benhanpk a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn ) inner join " + m_schemas + ".btdnn_bv c on b.mann=c.mann";
            sql += " where a.madoituong not in(" + m_dtkhamsuckhoe + ") and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') group by a.mabn,b.matt,c.mannbo,to_char(a.ngay,'dd/mm/yyyy')) as b";
            ds1.Merge(get_data_mmyy(sql, tu, den, false));

            return ds1;
        }
        private DataSet f_get_tainan(string tu, string den, string sFormat, string m_schemas, string makp,int vcot)
        {
            string cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql = " select sum(case when a.maql>0 then 1 else 0 end) as " + cot.ToString() + "0,";
            sql += " sum(case when a.maql>0 and c.phai=1 then 1 else 0 end) as " + cot.ToString() + "01,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "101,";
            sql += " sum(case when b.tuvong>0 then 1 else 0 end) as " + cot.ToString() + "11,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=0 or b.cotsongco=0 or b.thuongkhac>0) then 1 else 0 end) as " + cot.ToString() + "12,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=1 and b.cotsongco=1 and b.thuongkhac=0) then 1 else 0 end) as " + cot.ToString() + "13,";
            sql += " sum(case when a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "301,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "201";
            sql += " from ((xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join "+m_schemas+".benhandt e on a.maql=e.maql)";
            sql += " inner join " + m_schemas + ".btdbn c on a.mabn=c.mabn";
            if (makp != "") sql += " and e.makp in(" + makp + ")";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            sql += " union all ";
            sql += " select sum(case when a.maql>0 then 1 else 0 end) as " + cot.ToString() + "0,";
            sql += " sum(case when a.maql>0 and c.phai=1 then 1 else 0 end) as " + cot.ToString() + "01,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "101,";
            sql += " sum(case when b.tuvong>0 then 1 else 0 end) as " + cot.ToString() + "11,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=0 or b.cotsongco=0 or b.thuongkhac>0) then 1 else 0 end) as " + cot.ToString() + "12,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=1 and b.cotsongco=1 and b.thuongkhac=0) then 1 else 0 end) as " + cot.ToString() + "13,";
            sql += " sum(case when a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "301,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "201";
            sql += " from ((xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join " + m_schemas + ".benhanngtr e on a.maql=e.maql)";
            sql += " inner join " + m_schemas + ".btdbn c on a.mabn=c.mabn";
            if (makp != "") sql += " and e.makp in(" + makp + ")";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            sql += " union all ";
            sql += " select sum(case when a.maql>0 then 1 else 0 end) as " + cot.ToString() + "0,";
            sql += " sum(case when a.maql>0 and c.phai=1 then 1 else 0 end) as " + cot.ToString() + "01,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "101,";
            sql += " sum(case when b.tuvong>0 then 1 else 0 end) as " + cot.ToString() + "11,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=0 or b.cotsongco=0 or b.thuongkhac>0) then 1 else 0 end) as " + cot.ToString() + "12,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=1 and b.cotsongco=1 and b.thuongkhac=0) then 1 else 0 end) as " + cot.ToString() + "13,";
            sql += " sum(case when a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "301,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "201";
            sql += " from ((xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join xxx.benhancc e on a.maql=e.maql)";
            sql += " inner join " + m_schemas + ".btdbn c on a.mabn=c.mabn";
            if (makp != "") sql += " and e.makp in(" + makp + ")";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            sql += " union all ";
            sql += " select sum(case when a.maql>0 then 1 else 0 end) as " + cot.ToString() + "0,";
            sql += " sum(case when a.maql>0 and c.phai=1 then 1 else 0 end) as " + cot.ToString() + "01,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=0 then 1 else 0 end) as " + cot.ToString() + "101,";
            sql += " sum(case when b.tuvong>0 then 1 else 0 end) as " + cot.ToString() + "11,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=0 or b.cotsongco=0 or b.thuongkhac>0) then 1 else 0 end) as " + cot.ToString() + "12,";
            sql += " sum(case when b.mocapcuu=0 and (b.sonao=1 and b.cotsongco=1 and b.thuongkhac=0) then 1 else 0 end) as " + cot.ToString() + "13,";
            sql += " sum(case when a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan not in(0,2) then 1 else 0 end) as " + cot.ToString() + "301,";
            sql += " sum(case when a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when c.phai=1 and a.maql>0 and a.nguyennhan=2 then 1 else 0 end) as " + cot.ToString() + "201";
            sql += " from ((xxx.tainantt a left join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join xxx.benhanpk e on a.maql=e.maql)";
            sql += " inner join " + m_schemas + ".btdbn c on a.mabn=c.mabn";
            if (makp != "") sql += " and e.makp in(" + makp + ")";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            return get_data_mmyy(sql, tu, den, false);
        }
        private DataSet get_data_phongluu_chuyenvien_tuvong(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            sql = "select sum(case when a.ttlucrv=6 then 1 else 0 end) as c062,sum(case when a.ttlucrv=7 then 1 else 0 end) as c075,sum(case when a.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) as c085";
            sql += " from ((xxx.benhancc a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn) inner join " + m_schemas + ".btdnn_bv c on b.mann=c.mann) inner join xxx.xutrikbct d on a.maql=d.maql";
            sql += " where to_date(to_char(a.ngayrv,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            return get_data_mmyy(sql, tu, den, false);
        }
        private DataSet get_data_ngoaitru_chuyenvien_tuvong(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            sql = "select sum(case when a.ttlucrv>0 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as c031,";
            sql += " sum(case when a.ttlucrv>0 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then to_number(to_date(to_char(a.ngayrv,'" + sFormat + "'),'" + sFormat + "')-to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')) else 0 end) as c032,";
            sql += " sum(case when a.ttlucrv=6 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as c062,";
            sql += " sum(case when a.ttlucrv=7 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as c075,";
            sql += " sum(case when a.ttlucrv=7 and to_number(to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi'))<1 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as c085";
            sql += " from " + m_schemas + ".benhanngtr a inner join " + m_schemas + ".btdbn b on a.mabn=b.mabn";
            return get_data(sql);
        }
        private DataSet get_data_noitru_chuyenvien_tuvong(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            ///Chuyenvien va tuvong noitru
            sql = "select sum(case when b.maql>0 and b.ttlucrv=6 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c061,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)>15 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c071,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh) between 5 and 15 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c073,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<5 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c072,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c074,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)>15 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c081,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and 5<=to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<=15 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c083,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_number(to_char(b.ngay,'yyyy'))-to_number(c.namsinh)<5 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c082,";
            sql += " sum(case when b.maql>0 and b.ttlucrv=7 and to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(a.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) c084";
            sql += " from (" + m_schemas + ".benhandt a left join " + m_schemas + ".xuatvien b on a.maql=b.maql) inner join " + m_schemas + ".btdbn c on a.mabn=c.mabn";
            return get_data(sql);
        }
        /// <summary>
        /// Tinh PTTT tu ngay - den ngay
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_pttt(string vtungay, string vdenngay, string vschemas, string vmakp,int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from ((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (" + vschemas + ".benhanngtr a ";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "22";
            sql += " from (xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            return get_data_mmyy(sql,vtungay,vdenngay,false);
        }
        /// <summary>
        /// Tinh số ca pttt theo từng loại A,B,C
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <param name="vloaipttt"></param>
        /// <returns></returns>
        private DataSet f_get_pttt_loai(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot,string vloaipttt)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "111,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "112,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "121,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "122,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "211,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "212,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "22,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "221,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "222,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "31,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "311,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "312,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "32,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "321,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "322,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "41,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "411,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "412,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "42,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "421,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "422,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "51,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "512,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "52,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "521,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "522,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "61,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "611,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "612,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "62,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "621,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "622,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "71,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "711,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "712,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "72,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "721,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "722,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "81,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "811,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "812,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "82,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "821,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "822";
            sql += " from (((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".dmpttt e on trim(c.mapt)=trim(e.mapt))";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vloaipttt != "") sql += " and substr(c.mapt,0,1) in(" + vloaipttt + ")";
            sql += " union all ";
            sql += " select sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "111,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "112,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "121,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "122,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "211,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "212,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "22,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "221,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "222,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "31,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "311,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "312,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "32,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "321,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "322,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "41,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "411,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "412,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "42,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "421,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "422,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "51,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "512,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "52,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "521,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "522,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "61,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "611,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "612,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "62,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "621,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "622,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "71,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "711,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "712,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "72,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "721,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "722,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "81,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "811,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "812,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "82,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "821,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "822";
            sql += " from ((" + vschemas + ".benhanngtr a ";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".dmpttt e on trim(c.mapt)=trim(e.mapt))";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vloaipttt != "") sql += " and substr(c.mapt,0,1) in(" + vloaipttt + ")";
            sql += " union all ";
            sql += " select sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "111,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "112,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "121,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "122,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "211,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "212,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "22,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "221,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "222,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "31,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "311,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "312,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "32,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "321,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "322,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "41,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "411,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "412,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "42,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "421,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "422,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "51,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "512,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "52,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "521,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "522,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "61,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "611,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "612,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "62,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "621,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "622,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "71,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "711,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "712,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "72,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "721,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "722,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "81,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "811,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "812,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "82,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "821,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "822";
            sql += " from ((xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".dmpttt e on trim(c.mapt)=trim(e.mapt))";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vloaipttt != "") sql += " and substr(c.mapt,0,1) in(" + vloaipttt + ")";
            sql += " union all ";
            sql += " select sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "11,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "111,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "112,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "12,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "121,";
            sql += " sum(case when e.dacbiet='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "122,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "21,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "211,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "212,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "22,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "221,";
            sql += " sum(case when trim(e.loai1)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "222,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "31,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "311,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "312,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "32,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "321,";
            sql += " sum(case when trim(e.loai1)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "322,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "41,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "411,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "412,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "42,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "421,";
            sql += " sum(case when trim(e.loai1)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "422,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "51,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "512,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "52,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "521,";
            sql += " sum(case when trim(e.loai2)='A' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "522,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "61,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "611,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "612,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "62,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "621,";
            sql += " sum(case when trim(e.loai2)='B' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "622,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "71,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "711,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "712,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "72,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "721,";
            sql += " sum(case when trim(e.loai2)='C' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "722,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' then 1 else 0 end) as " + vcot.ToString() + "81,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "811,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='P' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "812,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' then 1 else 0 end) as " + vcot.ToString() + "82,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=2 then 1 else 0 end) as " + vcot.ToString() + "821,";
            sql += " sum(case when trim(e.loai3)='x' and substr(c.mapt,0,1)='T' and c.tinhhinh=1 then 1 else 0 end) as " + vcot.ToString() + "822";
            sql += " from ((xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".dmpttt e on trim(c.mapt)=trim(e.mapt))";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vloaipttt != "") sql += " and substr(c.mapt,0,1) in(" + vloaipttt + ")";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        private DataSet get_data_xnghiem(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            sql = "select nvl(sum(case when c.tieuban=0 then d.tieuban else c.tieuban end) ,0) c09";
            sql += " from " + m_schemas + ".xn_phieu b," + m_schemas + ".xn_ketqua a, " + m_schemas + ".xn_bv_chitiet c, " + m_schemas + ".xn_bv_ten d where b.id=a.id and a.id_ten=c.id and c.id_bv_ten=d.id";
            sql += " and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            return get_data(sql);
        }
        private DataSet f_get_data_cdha(string vtungay, string vdenngay, string vschemas, string vmakp,int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.idloaicdha in ('02') then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when a.idloaicdha in ('02') then b.solan else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when a.idloaicdha in ('01') then b.solan else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when a.idloaicdha in ('05') then b.solan else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when a.idloaicdha in ('06') then b.solan else 0 end) as " + vcot.ToString() + "5,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when a.idloaicdha in ('04') then b.solan else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when a.idloaicdha in ('07') then b.solan else 0 end) as " + vcot.ToString() + "2,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when a.idloaicdha in ('03') then b.solan else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when a.idloaicdha in ('08','09') then b.solan else 0 end) as " + vcot.ToString() + "4";
            sql += " from xxx.cdha_bnll a,xxx.cdha_bnct b," + vschemas + ".btdbn c";
            sql += " where a.count_cdha=b.count_cdha and a.mabn=c.mabn ";//and to_char(a.ngaycdha,'yyyy')='"+nam.ToString()+"'";
            sql += " and to_date(to_char(a.ngaycdha,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            return get_data_mmyy(sql,vtungay,vdenngay,false);
        }
        /// <summary>
        /// Thống kê theo loại cdha
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_data_cdha_tktheoloai(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.id_loai in ('02') then b.solan else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when a.id_loai in ('01') then b.solan else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when a.id_loai in ('05') then b.solan else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when a.id_loai in ('06') then b.solan else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when a.id_loai in ('04') then b.solan else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when a.id_loai in ('07') then b.solan else 0 end) as " + vcot.ToString() + "6,";
            sql += " sum(case when a.id_loai in ('03') then b.solan else 0 end) as " + vcot.ToString() + "7,";
            sql += " sum(case when a.id_loai in ('08','09') then b.solan else 0 end) as " + vcot.ToString() + "8";
            sql += " from xxx.cdha_bnll a,xxx.cdha_bnct b," + vschemas + ".btdbn c";
            sql += " where a.id=b.id and a.mabn=c.mabn ";//and to_char(a.ngaycdha,'yyyy')='"+nam.ToString()+"'";
            sql += " and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if(vmakp!="") sql += " and a.makp in(" + vmakp + ")";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        private DataSet get_data_noitru_ththbn(string tu, string den, string sFormat, string m_schemas, string makp,int i_cot,string s_matt)
        {
            string cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql = "select sum(case when (b.maql is null and to_date(to_char(a.ngay,'"+sFormat+"'),'"+sFormat+"')<to_date('"+tu+"','"+sFormat+"')) or (to_date(to_char(b.ngay,'"+sFormat+"'),'"+sFormat+"')>=to_date('"+tu+"','"+sFormat+"') and to_date(to_char(a.ngay,'"+sFormat+"'),'"+sFormat+"')<to_date('"+tu+"','"+sFormat+"')) then 1 else 0 end) as "+cot.ToString()+"1,";//bn dauky
            i_cot++;
            cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql += " sum(case when a.maql>0 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "1,";//BN Nhapvien
            sql += " sum(case when a.madoituong=1 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "2,";//Nhapvien doi tuong bhyt
            sql += " sum(case when a.madoituong<>1 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "3,";//Nhapvien doi tuong ko the
            sql += " sum(case when a.madoituong<>1 and c.mien=1 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "4,";//Nhapvien mien ko the
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(d.namsinh)<6 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "5,";//Nhapvien tre<6 tuoi
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(d.namsinh) between 6 and 14 and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "6,";//Nhapvien tre<6 tuoi
            sql += " sum(case when d.matt<>'"+s_matt+"' and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "7,";//Nhapvien ngoaitinh
            i_cot++;
            cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql += " sum(case when b.maql>0 and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then 1 else 0 end) as " + cot.ToString() + "1,";//Ra vien
            i_cot++;
            cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql += " sum(case when (b.maql is null and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + den + "','" + sFormat + "')) or (to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "')) then 1 else 0 end) as " + cot.ToString() + "1,";//Hienco
            sql += " sum(case when a.madoituong=1 and ((b.maql is null and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + den + "','" + sFormat + "')) or (to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "'))) then 1 else 0 end) as " + cot.ToString() + "2,";//Hienco bhyt
            sql += " sum(case when a.madoituong<>1 and ((b.maql is null and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + den + "','" + sFormat + "')) or (to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "'))) then 1 else 0 end) as " + cot.ToString() + "3,";//Hienco o bhyt
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(d.namsinh)<6 and ((b.maql is null and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + den + "','" + sFormat + "')) or (to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "'))) then 1 else 0 end) as " + cot.ToString() + "4,";//Hienco tre <6 tu
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(d.namsinh) between 6 and 15 and ((b.maql is null and to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + den + "','" + sFormat + "')) or (to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "'))) then 1 else 0 end) as " + cot.ToString() + "5,";//Hienco tre <15 tu
            i_cot=i_cot+2;
            cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql += " sum(case when to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "') then to_number(to_char(to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')-to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "1";
            sql += " from (((" + m_schemas + ".benhandt a left join " + m_schemas + ".xuatvien b on a.maql=b.maql)";
            sql += " inner join " + m_schemas + ".doituong c on a.madoituong=c.madoituong)";
            sql += " inner join " + m_schemas + ".btdbn d on a.mabn=d.mabn)";
            sql += " inner join " + m_schemas + ".btdnn_bv e on d.mann=e.mann";
            sql += " where a.loaiba=1";
            return get_data(sql);
        }
        private DataSet get_data_tkngaydtnoitru(string tu, string den, string sFormat, string m_schemas, string makp, int i_cot)
        {
            string cot = "c" + i_cot.ToString().PadLeft(2, '0');
            sql = " select sum(to_number(to_char(";
            sql += " case when (b.ngay is null or to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>to_date('" + den + "','" + sFormat + "')) then to_date('" + den + "','" + sFormat + "') else to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') end - ";
            sql += " case when to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + tu + "','" + sFormat + "') then to_date('" + tu + "','" + sFormat + "') else to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') end";
            sql += " ,'ddd'))+1) as " + cot.ToString() + "1";
            sql += " from (" + m_schemas + ".benhandt a left join " + m_schemas + ".xuatvien b on a.maql=b.maql)";
            sql += " inner join " + m_schemas + ".btdbn d on a.mabn=d.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + den + "','" + sFormat + "') and (b.ngay is null or to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + tu + "','" + sFormat + "'))";
            return get_data(sql);
        }
        private DataSet get_data_Mau6(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            sql = "select a.mabn,to_char(a.id) as id,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.id_loai,b.solan,decode(e.soluong,null,0,e.soluong) as soluong,decode(e.hu,null,0,e.hu) as phimhu,f.tenkt";
            sql += " from ((((xxx.cdha_bnll a inner join xxx.cdha_bnct b on a.id=b.id)";
            sql += " inner join " + m_schemas + ".cdha_loai c on a.id_loai=c.id)";
            sql += " inner join " + m_schemas + ".btdbn d on a.mabn=d.mabn)";
            sql += " left join xxx.cdha_ctphimxq e on a.id=e.id)";
            sql += " inner join "+m_schemas+".cdha_kythuat f on trim(b.makt)=trim(f.makt)";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            return get_data_mmyy(sql, tu, den, false);
        }
        private DataSet get_data_Mau7(string tu, string den, string sFormat, string m_schemas, string makp)
        {
            sql = "select a.mabn,to_char(a.id) as id,to_char(a.ngay,'dd/mm/yyyy') as ngay,a.id_loai,b.solan,case when e.soluong is null then 0 else e.soluong end as soluong,decode(e.hu,null,0,e.hu) as phimhu,f.tenkt as kythuat";
            sql += " from ((((xxx.cdha_bnll a inner join xxx.cdha_bnct b on a.id=b.id)";
            sql += " inner join " + m_schemas + ".cdha_loai c on a.id_loai=c.id)";
            sql += " inner join " + m_schemas + ".btdbn d on a.mabn=d.mabn)";
            sql += " left join xxx.cdha_ctphimxq e on a.id=e.id)";
            sql += " inner join " + m_schemas + ".cdha_kythuat f on trim(b.makt)=trim(f.makt)";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            DataSet ds1= get_data_mmyy(sql, tu, den, false);

            sql = "select b.mabn,to_char(b.id) as id,to_char(b.ngay,'dd/mm/yyyy') as ngay,to_char(0) as id_loai,nvl(case when c.tieuban=0 then d.tieuban else c.tieuban end,0) solan,nvl(case when c.tieuban=0 then d.tieuban else c.tieuban end,0) soluong,to_char(0) as phimhu,b.nhanxet as kythuat";
            sql += " from " + m_schemas + ".xn_phieu b," + m_schemas + ".xn_ketqua a, " + m_schemas + ".xn_bv_chitiet c, " + m_schemas + ".xn_bv_ten d where b.id=a.id and a.id_ten=c.id and c.id_bv_ten=d.id";
            sql += " and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + tu + "','" + sFormat + "') and to_date('" + den + "','" + sFormat + "')";
            ds1.Merge(get_data(sql));

            return ds1;
        }
        public string f_GetDateTime(DateTimePicker dtp)
        {
            return dtp.Value.Day.ToString().PadLeft(2, '0') + "/" + dtp.Value.Month.ToString().PadLeft(2, '0') + "/" + dtp.Value.Year.ToString().PadLeft(4, '0');
        }
        public int f_get_Max_DayofMonth(int m_thang,int m_nam)
        {
            int dMax = 0;
            switch (m_thang)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    dMax = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    dMax = 30;
                    break;
                case 2:
                    if (m_nam % 4 == 0)
                    {

                        dMax = 28;
                    }
                    else
                    {
                        dMax = 29;
                    }
                    break;
                default :
                    dMax = 0;
                    break;
            }
            return dMax;
        }
        public int i_treduoi6tuoi
        { 
            get
            {
                return int.Parse(get_data("select case when ten is null then '0' else to_char(ten) end from " + user + ".thongso where id=42").Tables[0].Rows[0][0].ToString());
            }
        }
        public int i_treduoi16tuoi
        {
            get
            {
                return int.Parse(get_data("select case when ten is null then '0' else to_char(ten) end from " + user + ".thongso where id=43").Tables[0].Rows[0][0].ToString());
            }
        }
        public string s_mathenguoingheo()
        {
            ds = new DataSet();
            DataTable dt = new DataTable("THENGUOINGHEO");
            try
            {
                ds.ReadXml("..\\..\\..\\xml\\m_thenguoingheo.xml", XmlReadMode.Auto);
            }
            catch
            {
                dt.Columns.Add(new DataColumn("ID",System.Type.GetType("System.Decimal")));
                dt.Columns.Add(new DataColumn("TEN", System.Type.GetType("System.String")));
                ds.Tables.Add(dt);
                DataRow r = ds.Tables[0].NewRow();
                r["id"] = 1;
                r["ten"] = "'JL','IS'";
                ds.Tables[0].Rows.Add(r);
                ds.AcceptChanges();
                if (!System.IO.Directory.Exists("..\\..\\..\\xml")) System.IO.Directory.CreateDirectory("..\\..\\..\\xml");
                ds.WriteXml("..\\..\\..\\xml\\m_thenguoingheo.xml", XmlWriteMode.WriteSchema);
            }
            string vgiatri = "";
            try
            {
                vgiatri = getrowbyid(ds.Tables[0], "id=1")["ten"].ToString();
            }
            catch
            {
                vgiatri = "";
            }
            return vgiatri;
        }
        public DataSet f_GetData_Hddt(string vschemas,string vmakp, string vtungay, string vdenngay,int vloaimau,bool vphongluu)
        {
            DataSet ads = new DataSet();
            DataSet vds_maicd = new DataSet();
            string vloaixn = "", vloaixquang = "", vloaisieuam = "", vchandoan="";
            DataRow r;
            if (vloaimau == 0 || vloaimau == 2)
            {
                try
                {
                    vds_maicd.ReadXml("..\\..\\..\\xml\\maicd_theocls.xml", XmlReadMode.Auto);
                }
                catch
                {
                    DataTable dt = new DataTable("ICDCLS");
                    dt.Columns.Add("ID", System.Type.GetType("System.Decimal"));
                    dt.Columns.Add("VALUE", System.Type.GetType("System.String"));
                    vds_maicd.Tables.Add(dt);
                    r= vds_maicd.Tables[0].NewRow();
                    r["id"] = 1;
                    r["value"] = "12";//XQuang
                    vds_maicd.Tables[0].Rows.Add(r);

                    r = vds_maicd.Tables[0].NewRow();
                    r["id"] = 2;
                    r["value"] = "10,11";//Sieuam
                    vds_maicd.Tables[0].Rows.Add(r);

                    r = vds_maicd.Tables[0].NewRow();
                    r["id"] = 3;//Xetnghiem
                    r["value"] = "4,5,6,8";
                    vds_maicd.Tables[0].Rows.Add(r);
                    vds_maicd.AcceptChanges();
                    if (!System.IO.Directory.Exists("..\\..\\..\\xml")) System.IO.Directory.CreateDirectory("..\\..\\..\\xml");
                    vds_maicd.WriteXml("..\\..\\..\\xml\\maicd_theocls.xml", XmlWriteMode.WriteSchema);
                }
                r = getrowbyid(vds_maicd.Tables[0], "id=1");
                if (r != null) vloaixquang = r["value"].ToString();
                r = getrowbyid(vds_maicd.Tables[0], "id=2");
                if (r != null) vloaisieuam = r["value"].ToString();
                r = getrowbyid(vds_maicd.Tables[0], "id=3");
                if (r != null) vloaixn = r["value"].ToString();
                vchandoan = vloaixquang + "^" + vloaisieuam + "^" + vloaixn;
            }
            if (vloaimau == 0) ads = fGetDataHddtMau1(vschemas, vmakp, vtungay, vdenngay,vchandoan);
            else if (vloaimau == 1) ads = fGetDataHddtMau2(vschemas, vmakp, vtungay, vdenngay);
            else if (vloaimau == 2) ads = fGetDataHddtMau3(vschemas, vmakp, vtungay, vdenngay, vphongluu,vchandoan);
            return ads;
        }
        private void f_zsum1(DataTable vdt)
        {
            DataTable adt = vdt.Clone();
            DataRow r1 = adt.NewRow();
            adt.Rows.Add(r1);
            decimal v_value = 0;
            string v_cot = "";
            foreach (DataColumn dc in adt.Columns)
            {
                v_value = 0;
                v_cot = "";
                foreach (DataRow r in adt.Rows)
                {
                    v_cot = r[dc.ToString()].ToString();
                    v_value += (v_cot != "") ? decimal.Parse(v_cot) : 0;
                }
                adt.Rows[0][dc.ToString()] = v_value;
            }
            adt.AcceptChanges();
        }
        private DataSet f_zsum(DataSet vds)
        {
            DataSet ads = vds.Clone();
            DataRow r1 = ads.Tables[0].NewRow();
            ads.Tables[0].Rows.Add(r1);
            decimal v_value = 0;
            string v_cot = "";
            foreach (DataColumn dc in vds.Tables[0].Columns)
            {
                v_value = 0;
                v_cot = "";
                foreach (DataRow r in vds.Tables[0].Rows)
                {
                    v_cot = r[dc.ToString()].ToString();
                    v_value += (v_cot != "") ? decimal.Parse(v_cot) : 0;
                }
                ads.Tables[0].Rows[0][dc.ToString()] = v_value;
            }
            ads.Tables[0].AcceptChanges();
            return ads;
        }
        private DataSet fGetDataHddtMau1(string vschemas,string vmakp, string vtungay, string vdenngay,string vchandoan)
        {
            DataSet adsHddt = new DataSet();
            adsHddt.Merge(f_get_giuongbenh(vschemas, vmakp, vtungay, vdenngay,1));
            adsHddt.Merge(f_get_noitru(vschemas, vmakp, vtungay, vdenngay, 2));
            adsHddt.Merge(f_get_tainan(vtungay, vdenngay, sformat, vschemas, vmakp, 7));
            adsHddt.Merge(f_get_noitru_ravien(vschemas, vmakp, vtungay, vdenngay, 8));
            adsHddt.Merge(f_get_pttt_loai(vtungay, vdenngay, vschemas, vmakp, 10,"'T'"));
            adsHddt.Merge(f_get_taibien_pttt(vtungay, vdenngay, vschemas, vmakp, 11));
            adsHddt.Merge(f_get_tuvong_tt(vtungay, vdenngay, vschemas, vmakp, 13));
            adsHddt.Merge(f_get_data_cdha_tktheoloai(vtungay, vdenngay, vschemas, vmakp, 14));
            adsHddt.Merge(f_get_hdcdt(vtungay, vdenngay, vschemas, 15));
            adsHddt.Merge(f_get_nckhoahoc(vtungay, vdenngay, vschemas, 16));
            adsHddt = f_zsum(adsHddt);
            adsHddt.Tables[0].Columns.Add("ID", System.Type.GetType("System.Decimal"));
            adsHddt.Tables[0].Rows[0]["id"] = 1;
            string vchandoanbenh = s_chandoanbenhtheokhoa();
            adsHddt.Merge(f_get_chandoan_ntcls(vtungay, vdenngay, vschemas, vmakp, vchandoanbenh,vchandoan));
            return adsHddt; 
        }
        /// <summary>
        /// Lấy chẩn đoán bệnh theo maxid
        /// </summary>
        /// <returns></returns>
        private string s_chandoanbenhtheokhoa()
        {
            DataSet vds = new DataSet();
            try
            {
                vds.ReadXml("..\\..\\..\\xml\\m_cdbenh_khoa.xml", XmlReadMode.Auto);
            }
            catch
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", System.Type.GetType("System.Decimal"));
                dt.Columns.Add("VALUE", System.Type.GetType("System.String"));
                vds.Tables.Add(dt);
                DataRow vr = vds.Tables[0].NewRow();
                vr["id"] = 1;
                vr["value"] = "Z01.4";
                vds.Tables[0].Rows.Add(vr);
                vds.WriteXml("..\\..\\..\\xml\\m_cdbenh_khoa.xml", XmlWriteMode.WriteSchema);
            }
            DataRow[] r = vds.Tables[0].Select("", "id desc");
            string vchandoanbenh = "";
            if (r.Length > 0) vchandoanbenh = r[0]["value"].ToString();
            return vchandoanbenh;
        }
        private DataSet fGetDataHddtMau2(string vschemas, string vmakp, string vtungay, string vdenngay)
        {
            DataSet adsHddt = new DataSet();
            return adsHddt;
        }
        private DataSet fGetDataHddtMau3(string vschemas, string vmakp, string vtungay, string vdenngay,bool vphongluu,string vchandoan)
        {
            DataSet adsHddt = new DataSet();
            adsHddt.Merge(f_get_hdkhambenh(vtungay, vdenngay, vschemas, vmakp, 1, vphongluu));
            adsHddt.Merge(f_get_hddtngtru(vtungay, vdenngay, vschemas, vmakp, 7));
            adsHddt.Merge(f_get_pttt_loai(vtungay, vdenngay, vschemas, vmakp, 9,"'T'"));
            adsHddt.Merge(f_get_taibien_pttt(vtungay, vdenngay, vschemas, vmakp, 10));
            adsHddt.Merge(f_get_tuvong_tt(vtungay, vdenngay, vschemas, vmakp, 11));
            adsHddt.Merge(f_get_data_cdha_tktheoloai(vtungay, vdenngay, vschemas, vmakp, 12));
            adsHddt.Merge(f_get_hdcdt(vtungay, vdenngay, vschemas, 13));
            adsHddt.Merge(f_get_nckhoahoc(vtungay, vdenngay, vschemas, 14));
            adsHddt = f_zsum(adsHddt);
            adsHddt.Tables[0].Columns.Add("ID", System.Type.GetType("System.Decimal"));
            adsHddt.Tables[0].Rows[0]["id"] = 1;
            string vchandoanbenh = s_chandoanbenhtheokhoa();
            adsHddt.Merge(f_get_chandoan_kbcls(vtungay, vdenngay, vschemas, vmakp, vchandoanbenh, vchandoan, vphongluu));
            return adsHddt;
        }
        /// <summary>
        /// Lấy số giường bệnh theo thực kê và theo kế hoạch (nếu không có nhập tổng hợp --> tính theo khai báo theo btdkp_bv)
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <returns></returns>
        private DataSet f_get_giuongbenh(string vschemas,string vmakp,string vtungay,string vdenngay,int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select c01 as " + vcot.ToString() + "1 from " + vschemas + ".bieu_031 where ma=1 ";
            sql += " and to_date(to_char(ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " order by ngay";
            DataSet ads1 = get_data(sql);
            decimal ttt = 0;
            try
            {
                ttt = decimal.Parse(ads1.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                ttt = 0;
            }
            if (ttt == 0)
            {
                sql = "select sum(kehoach) as " + vcot.ToString() + "1,sum(thucke) as " + vcot.ToString() + "2 from " + vschemas + ".btdkp_bv a where a.makp is not null";
                if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
                ads1 = get_data(sql);
            }
            return ads1;
        }
        /// <summary>
        /// Thống kê số bn ra viện và còn lại cuối ngày của khoa
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vcot"></param>
        /// <returns></returns>
        private DataSet f_get_noitru_ravien(string vschemas, string vmakp, string vtungay, string vdenngay, int vcot)
        {
            string cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql = "select sum(case when c.id>0 and c.ttlucrk<>5 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when d.phai=1 and c.id>0 and c.ttlucrk<>5 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "101,";
            vcot++;
            cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql += " sum(case when (c.id is null and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<=to_date('" + vdenngay + "','" + sformat + "')) or (to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')>to_date('" + vdenngay + "','" + sformat + "') and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<=to_date('" + vdenngay + "','" + sformat + "')) then 1 else 0 end) as " + cot.ToString() + "2,";//Hienco
            sql += " sum(case when (c.id is null and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<=to_date('" + vdenngay + "','" + sformat + "')) or (to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')>to_date('" + vdenngay + "','" + sformat + "') and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<=to_date('" + vdenngay + "','" + sformat + "')) then 1 else 0 end) as " + cot.ToString() + "3";//Hienco nu
            sql += " from ((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql)";
            sql += " left join " + vschemas + ".xuatkhoa c on b.id=c.id)";
            sql += " inner join " + vschemas + ".btdbn d on a.mabn=d.mabn"; 
            sql += " where d.mabn is not null";
            if (vmakp != "") sql += " and b.makp in(" + vmakp + ")";
            return get_data(sql);
        }
        /// <summary>
        /// Lấy số liệu nội trú, thông tin tử vong và chuyển viện.
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vcot"></param>
        /// <returns></returns>
        private DataSet f_get_noitru(string vschemas, string vmakp, string vtungay, string vdenngay,int vcot)
        {
            string vmathenguoingheo = s_mathenguoingheo();
            int vtreduoi6tuoi = i_treduoi6tuoi;
            int vtreduoi16tuoi = i_treduoi16tuoi;
            int vtren60tuoi = 60;
            string cot = "c" + vcot.ToString().PadLeft(2, '0');
            string vmabv = Mabv;
            string vmabv1 = vmabv.ToString().Split('.')[0];
            string vmabv2 = vmabv.ToString().Split('.')[1];
            string vmabv3 = vmabv.ToString().Split('.')[2];
            sql = "select sum(case when (to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<to_date('" + vtungay + "','" + sformat + "')) and (c.id is null or to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')>=to_date('" + vtungay + "','" + sformat + "')) then 1 else 0 end) as " + cot.ToString() + ",";//bn dauky
            sql += " sum(case when d.phai=1 and (to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "')<to_date('" + vtungay + "','" + sformat + "')) and (c.id is null or to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')>=to_date('" + vtungay + "','" + sformat + "')) then 1 else 0 end) as " + cot.ToString() + "1,";//bn dauky, nu
            vcot++;
            cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql += " sum(case when b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + ",";
            sql += " sum(case when d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when f.maql>0 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when f.maql>0 and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "21,";
            sql += " sum(case when f.maql>0 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when f.maql>0 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ")and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "31,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi16tuoi.ToString() + " and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "4,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi16tuoi.ToString() + " and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "41,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "5,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "51,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)>" + vtren60tuoi.ToString() + " and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "6,";
            sql += " sum(case when to_number(to_char(b.ngay,'yyyy'))-to_number(d.namsinh)>" + vtren60tuoi.ToString() + " and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "61,";
            sql += " sum(case when a.nhantu=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "7,";
            sql += " sum(case when a.nhantu=1 and d.phai=1 and b.maql>0 and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "71,";
            vcot++;
            cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql += " sum(case when to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + ",";
            sql += " sum(case when d.phai=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when f.maql>0 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when f.maql>0 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") and d.phai=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "21,";
            sql += " sum(case when to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and d.phai=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "31,";
            sql += " sum(case when to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)>60 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "4,";
            sql += " sum(case when to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)>60 and d.phai=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "')-to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "'),'ddd'))+1 else 0 end) as " + cot.ToString() + "41,";
            vcot++;
            cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql += " sum(case when c.ttlucrk=7 then 1 else 0 end) as " + cot.ToString() + ",";
            sql += " sum(case when c.ttlucrk=7 and d.phai=1 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when c.ttlucrk=7 and to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when c.ttlucrk=7 and d.phai=1 and to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + cot.ToString() + "21,";
            sql += " sum(case when c.ttlucrk=7 and to_date(to_char(c.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when c.ttlucrk=7 and to_date(to_char(c.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')-to_date(to_char(b.ngay,'dd/mm/yyyy hh24:mi'),'dd/mm/yyyy hh24:mi')<1 and d.phai=1 then 1 else 0 end) as " + cot.ToString() + "31,";
            vcot++;
            cot = "c" + vcot.ToString().PadLeft(2, '0');
            sql += " sum(case when substr(h.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr('" + vmabv.ToString() + "',3,2))-to_number(substr(h.mabv,3,2))=1 then 1 else 0 end) as " + cot.ToString() + "1,";
            sql += " sum(case when substr(h.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr('" + vmabv.ToString() + "',3,2))-to_number(substr(h.mabv,3,2))=1 and d.phai=1 then 1 else 0 end) as " + cot.ToString() + "11,";
            sql += " sum(case when substr(h.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr('" + vmabv.ToString() + "',3,2))-to_number(substr(h.mabv,3,2))>1 then 1 else 0 end) as " + cot.ToString() + "2,";
            sql += " sum(case when substr(h.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr('" + vmabv.ToString() + "',3,2))-to_number(substr(h.mabv,3,2))>1 and d.phai=1 then 1 else 0 end) as " + cot.ToString() + "21,";
            sql += " sum(case when b.maql>0 and b.khoachuyen<>'01' and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "3,";
            sql += " sum(case when d.phai=1 and b.maql>0 and b.khoachuyen<>'01' and to_date(to_char(b.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "31,";
            sql += " sum(case when c.id>0 and c.ttlucrk=5 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "4,";
            sql += " sum(case when d.phai=1 and c.id>0 and c.ttlucrk=5 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "41,";
            sql += " sum(case when c.id>0 and k.loaibv=1 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "5,";
            sql += " sum(case when d.phai=1 and c.id>0 and k.loaibv=1 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "51,";
            sql += " sum(case when f.maql>0 and c.id>0 and k.loaibv=1 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "6,";
            sql += " sum(case when f.maql>0 and d.phai=1 and c.id>0 and k.loaibv=1 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "61,";
            sql += " sum(case when k.loaibv=1 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") and f.maql>0 and c.id>0 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "7,";
            sql += " sum(case when k.loaibv=1 and substr(f.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") and f.maql>0 and d.phai=1 and c.id>0 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "71,";
            sql += " sum(case when k.loaibv=1 and to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and c.id>0 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "8,";
            sql += " sum(case when k.loaibv=1 and to_number(to_char(c.ngay,'yyyy'))-to_number(d.namsinh)<" + vtreduoi6tuoi.ToString() + " and d.phai=1 and c.id>0 and c.ttlucrk=6 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + cot.ToString() + "81";
            sql += " from (((((((" + vschemas.ToString() + ".benhandt a inner join " + vschemas.ToString() + ".nhapkhoa b on a.maql=b.maql)";
            sql += " left join " + vschemas.ToString() + ".xuatkhoa c on b.id=c.id)";
            sql += " inner join " + vschemas.ToString() + ".btdbn d on a.mabn=d.mabn)";
            sql += " inner join " + vschemas.ToString() + ".doituong e on a.madoituong=e.madoituong)";
            sql += " left join " + vschemas.ToString() + ".bhyt f on a.maql=f.maql)";
            sql += " left join " + vschemas.ToString() + ".noigioithieu g on a.maql=g.maql)";
            sql += " left join " + vschemas.ToString() + ".dstt h on g.mabv=h.mabv)";
            sql += " left join " + vschemas.ToString() + ".chuyenvien k on a.maql=k.maql";
            sql += " where b.maql>0";
            if (vmakp != "") sql += " and b.makp in(" + vmakp + ")";
            return get_data(sql);
        }
        /// <summary>
        /// Tai biến trong pttt
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_taibien_pttt(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when c.taibien=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when c.taibien=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when c.taibien=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when c.taibien=4 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from (((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".taibienpt e on c.taibien=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when c.taibien=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when c.taibien=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when c.taibien=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when c.taibien=4 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((" + vschemas + ".benhanngtr a ";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".taibienpt e on c.taibien=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when c.taibien=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when c.taibien=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when c.taibien=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when c.taibien=4 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".taibienpt e on c.taibien=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when c.taibien=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when c.taibien=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when c.taibien=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when c.taibien=4 then 1 else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".taibienpt e on c.taibien=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        /// <summary>
        /// Tử vong thủ thuật
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_tuvong_tt(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when substr(c.mapt,0,1)='T' and c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from (((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".tuvongpt e on c.tuvong=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='T' and c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((" + vschemas + ".benhanngtr a ";
            sql += " inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".tuvongpt e on c.tuvong=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='T' and c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((xxx.benhanpk a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".tuvongpt e on c.tuvong=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            sql += " union all ";
            sql += " select sum(case when substr(c.mapt,0,1)='T' and c.taibien<>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=2 then 1 else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and e.ma=3 then 1 else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when substr(c.mapt,0,1)='T' and c.taibien not in(0,1,2,3,4) then 1 else 0 end) as " + vcot.ToString() + "6";
            sql += " from ((xxx.benhancc a inner join xxx.pttt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".tuvongpt e on c.tuvong=e.ma)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(c.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        /// <summary>
        /// Lấy số liệu theo nhập tổng hợp trong biểu 09.1
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_hdcdt(string vtungay, string vdenngay, string vschemas, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.ma=1 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when a.ma=2 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when a.ma=3 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when a.ma=4 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "31,";
            sql += " sum(case when a.ma=5 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "32,";
            sql += " sum(case when a.ma=6 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when a.ma=7 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "5,";
            sql += " sum(case when a.ma=8 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "6,";
            sql += " sum(case when a.ma=9 and a.tongso>0 then a.tongso else 0 end) as " + vcot.ToString() + "7";
            sql += " from " + vschemas + ".bieu_091 a where a.id>0";
            sql += " and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " group by a.ma";
            return get_data(sql);
        }
        /// <summary>
        /// Hoat động nghiên cứu khoa học, số liệu lấy từ biểu 092: bieu_092
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_nckhoahoc(string vtungay, string vdenngay, string vschemas, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.ma>0 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when a.ma>0 then nvl(a.c01,0) else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when a.ma>0 then nvl(a.c02,0) else 0 end) as " + vcot.ToString() + "3,";
            sql += " sum(case when a.ma>0 then nvl(a.c03,0) else 0 end) as " + vcot.ToString() + "4,";
            sql += " sum(case when a.ma>0 then nvl(a.c04,0) else 0 end) as " + vcot.ToString() + "5";
            sql += " from " + vschemas + ".bieu_092 a where a.id>0";
            sql += " and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " group by a.ma";
            return get_data(sql);
        }
        /// <summary>
        /// Thống kê theo chẩn đoán bệnh
        /// </summary>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="vschemas"></param>
        /// <param name="vmakp"></param>
        /// <param name="vicot"></param>
        /// <returns></returns>
        private DataSet f_get_chandoan_ntcls(string vtungay, string vdenngay, string vschemas, string vmakp,string vmaicds,string vchandoan)
        {
            string vloaixq = "";
            try
            {
                vloaixq = vchandoan.Split('^')[0].ToString();
            }
            catch { vloaixq = ""; }

            string vloaisa = "";
            try
            {
                vloaisa = vchandoan.Split('^')[1].ToString();
            }
            catch { vloaisa = ""; }
            string vloaixn = "";
            try
            {
                vloaixn = vchandoan.Split('^')[2].ToString();
            }
            catch { vloaixn = ""; }
            string vloaivp = "";
            vloaivp = vloaixq + ",";
            vloaivp += (vloaisa != "") ? vloaisa + "," : "";
            vloaivp += (vloaixn != "") ? vloaixn + "," : "";
            vloaivp = vloaivp.TrimEnd(',');

            //int vicot = 30;
            //string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            DataSet vds = f_taotable_cdcls();
            DataRow r;
            //vmaicds = "";
            foreach (string vsmaicd in vmaicds.Split(','))
            {
                if (vsmaicd != "")
                {
                    r = vds.Tables[0].NewRow();
                    r["id"] = "0";
                    r["maicd"] = vsmaicd;
                    r["chandoan"] = "";
                    r["c301"] = 0;
                    r["c302"] = 0;
                    r["c303"] = 0;
                    r["c304"] = 0;
                    r["c305"] = 0;
                    vds.Tables[0].Rows.Add(r);
                }
            }
            
            sql = "select d.maicd,d.chandoan,sum(case when d.id>0 then 1 else 0 end) as songuoi,";
            sql += " sum(case when d.id>0 then to_number(to_char(to_date(to_char(d.ngay,'"+sformat+"'),'"+sformat+"')-to_date(to_char(c.ngay,'"+sformat+"'),'"+sformat+"'),'ddd'))+1 else 0 end) as songaydt";
            sql += " from (((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join " + vschemas + ".nhapkhoa c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".xuatkhoa d on c.id=d.id)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and d.ttlucrk<>5 and to_date(to_char(d.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vmaicds != "") sql += " and d.maicd in(" + vmaicds + ")";
            sql += " group by d.maicd,d.chandoan";

            foreach (DataRow r1 in get_data(sql).Tables[0].Rows)
            {
                r = getrowbyid(vds.Tables[0], "maicd='" + r1["maicd"].ToString() + "'");
                if (r != null)
                {
                    r["id"] = 1;
                    r["chandoan"] = r1["chandoan"].ToString();
                    r["c301"] = decimal.Parse(r["c301"].ToString()) + 1;
                    r["c302"] = decimal.Parse(r["c302"].ToString()) + decimal.Parse(r1["songaydt"].ToString());
                }
                else
                {
                    DataRow r5 = vds.Tables[0].NewRow();
                    r5["id"] = 1;
                    r5["maicd"] = r1["maicd"].ToString();
                    r5["chandoan"] = r1["chandoan"].ToString();
                    r5["c301"] = decimal.Parse(r1["songuoi"].ToString());
                    r5["c302"] = decimal.Parse(r1["songaydt"].ToString());
                    r5["c303"] = 0;
                    r5["c304"] = 0;
                    r5["c305"] = 0;
                    vds.Tables[0].Rows.Add(r5);
                }
            }

            sql = "select distinct d.maicd,g.id_loai,d.id";
            sql += " from (((((" + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql )";
            sql += " inner join " + vschemas + ".nhapkhoa c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".xuatkhoa d on c.id=d.id)";
            sql += " left join xxx.v_chidinh e on c.id=e.idkhoa)";
            sql += " left join " + vschemas + ".v_giavp g on e.mavp=g.id)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where a.loaiba=1 and d.ttlucrk<>5 and to_date(to_char(d.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and c.makp in(" + vmakp + ")";
            if (vmaicds != "") sql += " and d.maicd in(" + vmaicds + ")";
            if (vloaivp != "") sql += " and g.id_loai in(" + vloaivp + ")";
            
            DataSet vds2=get_data_mmyy(sql, vtungay, vdenngay, true);
            
            string vsql="";
            foreach (DataRow r1 in vds.Tables[0].Rows)
            {
                vsql = "maicd='" + r1["maicd"].ToString() + "'";
                DataRow[] r2 = vds2.Tables[0].Select(vsql);
                if (r2 != null)
                {
                    if (vloaixq != "")
                    {
                        try
                        {
                            r1["c303"] = decimal.Parse(r1["c303"].ToString()) + f_get_vloaicdha(vsql, vloaixq, vds2);
                        }
                        catch { }
                    }
                    if (vloaisa != "")
                    {
                        try
                        {
                            r1["c304"] = decimal.Parse(r1["c304"].ToString()) + f_get_vloaicdha(vsql, vloaisa, vds2);
                        }
                        catch { }
                    }
                    if (vloaixn != "")
                    {
                        try
                        {
                            r1["c305"] = decimal.Parse(r1["c305"].ToString()) + f_get_vloaicdha(vsql, vloaixn, vds2);
                        }
                        catch { }
                    }
                }
            }

            vds.AcceptChanges();
            return vds;
        }
        private decimal f_get_vloaicdha(string vsql,string vloaivp,DataSet vds2)
        {
            string vsid = "";
            decimal vsoloai = 0;
            vsql += " and id_loai in(" + vloaivp + ")";
            DataRow[] r3 = vds2.Tables[0].Select(vsql);
            if (r3 != null)
            {
                foreach (DataRow r4 in r3)
                {
                    if (vsid.IndexOf(r4["id"].ToString() + ",") < 0)
                    {
                        vsid = vsid + r4["id"].ToString() + ",";
                        vsoloai++;
                    }
                }
            }
            return vsoloai;
        }
        private DataSet f_taotable_cdcls()
        {
            DataSet vds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("MAICD", System.Type.GetType("System.String"));
            dt.Columns.Add("CHANDOAN", System.Type.GetType("System.String"));
            dt.Columns.Add("C301", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("C302", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("C303", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("C304", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("C305", System.Type.GetType("System.Decimal"));
            vds.Tables.Add(dt);
            return vds;
        }
        private DataSet f_get_hdkhambenh(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot,bool bphongluu)
        {
            string vmathenguoingheo = s_mathenguoingheo();
            int vtreduoi6tuoi = i_treduoi6tuoi;
            int vtreduoi16tuoi = i_treduoi16tuoi;
           // int vtren60tuoi = 60;
            string vmabv = Mabv;
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.maql>0 then 1 else 0 end) as " + vcot + ",";
            sql += " sum(case when a.madoituong=1 then 1 else 0 end) as " + vcot + "1,";
            sql += " sum(case when d.mien=1 and substr(c.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") then 1 else 0 end) as " + vcot + "2,";
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + vcot + "3,";
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)<" + vtreduoi6tuoi.ToString() + " then 1 else 0 end) as " + vcot + "4,";
            sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)>60 then 1 else 0 end) as " + vcot + "5,";
            sql += " sum(case when a.nhantu=1 then 1 else 0 end) as " + vcot + "6,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when d.mien<>1 then 1 else 0 end) as " + vcot + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when a.madoituong<>1 and d.mien=1 then 1 else 0 end) as " + vcot + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when b.xutri like '%07,%' and h.maql>0 then 1 else 0 end) as " + vcot + ",";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when b.makp is not null and b.xutri like '%05,%' then 1 else 0 end) as " + vcot + ",";
            sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and a.madoituong=1 then 1 else 0 end) as " + vcot + "1,";
            sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and substr(c.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") then 1 else 0 end) as " + vcot + "2,";
            sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + vcot + "3,";
            sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)>60 then 1 else 0 end) as " + vcot + "4,";
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(case when b.xutri like '%06,%' and k.loaibv=2 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))=1 then 1 else 0 end) as " + vcot.ToString() + "1,";
            sql += " sum(case when b.xutri like '%06,%' and k.loaibv=2 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))>1 then 1 else 0 end) as " + vcot.ToString() + "2,";
            sql += " sum(case when b.xutri like '%06,%' and k.loaibv=1 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))>1 then 1 else 0 end) as " + vcot.ToString() + "3";
            sql += " from (((((((xxx.benhanpk a inner join xxx.xutrikbct b on a.maql=b.maql)";
            sql += " left join xxx.bhyt c on a.maql=c.maql)";
            sql += " inner join " + vschemas + ".doituong d on a.madoituong=d.madoituong)";
            sql += " left join " + vschemas + ".noigioithieu e on a.maql=e.maql)";
            sql += " left join " + vschemas + ".dstt f on e.mabv=f.mabv)";
            sql += " inner join " + vschemas + ".btdbn g on a.mabn=g.mabn)";
            sql += " left join " + vschemas + ".tuvong h on a.maql=h.maql)";
            sql += " left join " + vschemas + ".chuyenvien k on a.maql=k.maql";
            sql += " where a.maql>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            if (bphongluu)
            {
                vicot=1;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " union all ";
                sql += " select sum(case when a.maql>0 then 1 else 0 end) as " + vcot + ",";
                sql += " sum(case when a.madoituong=1 then 1 else 0 end) as " + vcot + "1,";
                sql += " sum(case when d.mien=1 and substr(c.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") then 1 else 0 end) as " + vcot + "2,";
                sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + vcot + "3,";
                sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)<" + vtreduoi6tuoi.ToString() + " then 1 else 0 end) as " + vcot + "4,";
                sql += " sum(case when to_number(to_char(a.ngay,'yyyy'))-to_number(g.namsinh)>60 then 1 else 0 end) as " + vcot + "5,";
                sql += " sum(case when a.nhantu=1 then 1 else 0 end) as " + vcot + "6,";
                vicot++;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " sum(case when d.mien<>1 then 1 else 0 end) as " + vcot + ",";
                vicot++;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " sum(case when a.madoituong<>1 and d.mien=1 then 1 else 0 end) as " + vcot + ",";
                vicot++;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " sum(case when b.xutri like '%07,%' and h.maql>0 then 1 else 0 end) as " + vcot + ",";
                vicot++;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " sum(case when b.makp is not null and b.xutri like '%05,%' then 1 else 0 end) as " + vcot + ",";
                sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and a.madoituong=1 then 1 else 0 end) as " + vcot + "1,";
                sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and substr(c.sothe,12,2) in(" + vmathenguoingheo.ToString() + ") then 1 else 0 end) as " + vcot + "2,";
                sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and to_number(to_char(a.ngayrv,'yyyy'))-to_number(g.namsinh)<" + vtreduoi16tuoi.ToString() + " then 1 else 0 end) as " + vcot + "3,";
                sql += " sum(case when b.makp is not null and b.xutri like '%05,%' and to_number(to_char(a.ngayrv,'yyyy'))-to_number(g.namsinh)>60 then 1 else 0 end) as " + vcot + "4,";
                vicot++;
                vcot = "c" + vicot.ToString().PadLeft(2, '0');
                sql += " sum(case when b.xutri like '%06,%' and k.loaibv=2 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))=1 then 1 else 0 end) as " + vcot.ToString() + "1,";
                sql += " sum(case when b.xutri like '%06,%' and k.loaibv=2 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))>1 then 1 else 0 end) as " + vcot.ToString() + "2,";
                sql += " sum(case when b.xutri like '%06,%' and k.loaibv=1 and substr(k.mabv,0,3)='" + vmabv.Substring(0, 3) + "' and to_number(substr(k.mabv,3,2))-to_number(substr('" + vmabv.ToString() + "',3,2))>1 then 1 else 0 end) as " + vcot.ToString() + "3";
                sql += " from (((((((xxx.benhancc a inner join xxx.xutrikbct b on a.maql=b.maql)";
                sql += " left join xxx.bhyt c on a.maql=c.maql)";
                sql += " inner join " + vschemas + ".doituong d on a.madoituong=d.madoituong)";
                sql += " left join " + vschemas + ".noigioithieu e on a.maql=e.maql)";
                sql += " left join " + vschemas + ".dstt f on e.mabv=f.mabv)";
                sql += " inner join " + vschemas + ".btdbn g on a.mabn=g.mabn)";
                sql += " left join " + vschemas + ".tuvong h on a.maql=h.maql)";
                sql += " left join " + vschemas + ".chuyenvien k on a.maql=k.maql";
                sql += " where a.maql>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            }
            
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }
        private DataSet f_get_hddtngtru(string vtungay, string vdenngay, string vschemas, string vmakp, int vicot)
        {
            string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql = "select sum(case when a.maql>0 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot + "1,";//vao dieu tri ngoaitru
            sql += " sum(case when a.maql>0 and to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot + "2,";//xuat dtngtru
            sql += " sum(case when (a.ttlucrv is null or to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "')>to_date('" + vdenngay + "','" + sformat + "')) and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "')<=to_date('" + vdenngay + "','" + sformat + "') then 1 else 0 end) as " + vcot + "3,";//con lai cuoi ngay
            vicot++;
            vcot = "c" + vicot.ToString().PadLeft(2, '0');
            sql += " sum(to_number(to_char(";
            sql += " case when (a.ngayrv is null or to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "')>to_date('" + vdenngay + "','" + sformat + "')) then to_date('" + vdenngay + "','" + sformat + "') else to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') end - ";
            sql += " case when to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "')<to_date('" + vtungay + "','" + sformat + "') then to_date('" + vtungay + "','" + sformat + "') else to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') end";
            sql += " ,'ddd'))+1) as " + vcot.ToString() + "1,";//ngaytk
            sql += " sum(case when to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "') then to_number(to_char(to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "')-to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "'),'ddd')) else 0 end) as " + vcot.ToString() + "2";//ngaydt ra vien
            sql += " from " + vschemas + ".benhanngtr a inner join " + vschemas + ".btdbn b on a.mabn=b.mabn";
            sql += " where a.maql>0";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            return get_data(sql);
        }
        private DataSet f_get_chandoan_kbcls(string vtungay, string vdenngay, string vschemas, string vmakp, string vmaicds, string vchandoan,bool vbphongluu)
        {
            string vloaixq = "";
            try
            {
                vloaixq = vchandoan.Split('^')[0].ToString();
            }
            catch { vloaixq = ""; }

            string vloaisa = "";
            try
            {
                vloaisa = vchandoan.Split('^')[1].ToString();
            }
            catch { vloaisa = ""; }
            string vloaixn = "";
            try
            {
                vloaixn = vchandoan.Split('^')[2].ToString();
            }
            catch { vloaixn = ""; }
            string vloaivp = "";
            vloaivp = vloaixq + ",";
            vloaivp += (vloaisa != "") ? vloaisa + "," : "";
            vloaivp += (vloaixn != "") ? vloaixn + "," : "";
            vloaivp = vloaivp.TrimEnd(',');

            //int vicot = 30;
            //string vcot = "c" + vicot.ToString().PadLeft(2, '0');
            DataSet vds = f_taotable_cdcls();
            DataRow r;
            //vmaicds = "";
            foreach (string vsmaicd in vmaicds.Split(','))
            {
                if (vsmaicd != "")
                {
                    r = vds.Tables[0].NewRow();
                    r["id"] = "1";
                    r["maicd"] = vsmaicd;
                    r["chandoan"] = "";
                    r["c301"] = 0;
                    r["c302"] = 0;
                    r["c303"] = 0;
                    r["c304"] = 0;
                    r["c305"] = 0;
                    vds.Tables[0].Rows.Add(r);
                }
            }

            sql = "select a.maicd,a.chandoan,sum(case when a.maql>0 then 1 else 0 end) as songuoi,";
            sql += " sum(case when b.xutri like '%05,%' and b.makp is not null then 1 else 0 end) as songaydt";
            sql += " from (xxx.benhanpk a inner join xxx.xutrikbct b on a.maql=b.maql)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            if (vmaicds != "") sql += " and a.maicd in(" + vmaicds + ")";
            sql += " group by a.maicd,a.chandoan";
            if (vbphongluu)
            {
                sql += " union all ";
                sql += " select a.maicdrv as maicd,a.chandoanrv as chandoan,sum(case when a.ngayrv is not null then 1 else 0 end) as songuoi,";
                sql += " sum(case when b.xutri like '%05,%' and b.makp is not null then 1 else 0 end) as songaydt";
                sql += " from (xxx.benhancc a inner join xxx.xutrikbct b on a.maql=b.maql)";
                sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
                sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
                if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
                if (vmaicds != "") sql += " and a.maicdrv in(" + vmaicds + ")";
                sql += " group by a.maicdrv,a.chandoanrv";
            }
            foreach (DataRow r1 in get_data_mmyy(sql,vtungay,vdenngay,true).Tables[0].Rows)
            {
                r = getrowbyid(vds.Tables[0], "maicd='" + r1["maicd"].ToString() + "'");
                if (r != null)
                {
                    r["id"] = 1;
                    r["chandoan"] = r1["chandoan"].ToString();
                    r["c301"] = decimal.Parse(r["c301"].ToString()) + 1;
                    r["c302"] = decimal.Parse(r["c302"].ToString()) + decimal.Parse(r1["songaydt"].ToString());
                }
                else
                {
                    DataRow r5 = vds.Tables[0].NewRow();
                    r5["id"] = 1;
                    r5["maicd"] = r1["maicd"].ToString();
                    r5["chandoan"] = r1["chandoan"].ToString();
                    r5["c301"] = decimal.Parse(r1["songuoi"].ToString());
                    r5["c302"] = decimal.Parse(r1["songaydt"].ToString());
                    r5["c303"] = 0;
                    r5["c304"] = 0;
                    r5["c305"] = 0;
                    vds.Tables[0].Rows.Add(r5);
                }
            }

            sql = "select distinct a.maicd,g.id_loai,b.maql";
            sql += " from (((xxx.benhanpk a inner join xxx.xutrikbct b on a.maql=b.maql)";
            sql += " left join xxx.v_chidinh e on a.maql=e.maql)";
            sql += " left join " + vschemas + ".v_giavp g on e.mavp=g.id)";
            sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
            if (vmaicds != "") sql += " and a.maicd in(" + vmaicds + ")";
            if (vloaivp != "") sql += " and g.id_loai in(" + vloaivp + ")";
            if (vbphongluu)
            {
                sql += " union all ";
                sql += " select distinct a.maicdrv as maicd,g.id_loai,b.maql";
                sql += " from (((xxx.benhancc a inner join xxx.xutrikbct b on a.maql=b.maql)";
                sql += " left join xxx.v_chidinh e on a.maql=e.maql)";
                sql += " left join " + vschemas + ".v_giavp g on e.mavp=g.id)";
                sql += " inner join " + vschemas + ".btdbn f on a.mabn=f.mabn";
                sql += " where to_date(to_char(a.ngayrv,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
                if (vmakp != "") sql += " and a.makp in(" + vmakp + ")";
                if (vmaicds != "") sql += " and a.maicdrv in(" + vmaicds + ")";
                if (vloaivp != "") sql += " and g.id_loai in(" + vloaivp + ")";
            }

            DataSet vds2 = get_data_mmyy(sql, vtungay, vdenngay, true);

            string vsql = "";
            foreach (DataRow r1 in vds.Tables[0].Rows)
            {
                vsql = "maicd='" + r1["maicd"].ToString() + "'";
                DataRow[] r2 = vds2.Tables[0].Select(vsql);
                if (r2 != null)
                {
                    if (vloaixq != "")
                    {
                        try
                        {
                            r1["c303"] = decimal.Parse(r1["c303"].ToString()) + f_get_vloaicdha(vsql, vloaixq, vds2);
                        }
                        catch { }
                    }
                    if (vloaisa != "")
                    {
                        try
                        {
                            r1["c304"] = decimal.Parse(r1["c304"].ToString()) + f_get_vloaicdha(vsql, vloaisa, vds2);
                        }
                        catch { }
                    }
                    if (vloaixn != "")
                    {
                        try
                        {
                            r1["c305"] = decimal.Parse(r1["c305"].ToString()) + f_get_vloaicdha(vsql, vloaixn, vds2);
                        }
                        catch { }
                    }
                }
            }

            vds.AcceptChanges();
            return vds;
        }
        public DataSet f_get_tainantt_th(string vschemas, string vtungay, string vdenngay, DataSet vdsxml, DataSet vdsdk)
        {
            string vasql;
            string vasql_goc;
            string vcolumn;
            DataRow[] ar;
            DataSet vds = f_get_dstainantt_th(vschemas, vtungay, vdenngay);
            foreach (DataRow r in vdsxml.Tables[0].Rows)
            {
                vasql = "";
                vasql_goc = r["value"].ToString();
                vcolumn = "";
                if (vasql_goc != "")
                {
                    foreach (DataRow r1 in vdsdk.Tables[0].Rows)
                    {
                        vcolumn = r1["ten"].ToString();
                        vasql = vasql_goc + " and " + r1["value"].ToString();
                        ar = vds.Tables[0].Select(vasql);
                        if (ar.Length > 0)
                        {
                            r[vcolumn] = ar.Length;
                        }
                    }
                }
            }


            #region phongluu
            sql = " select e.mabn,4 as loaiba,e.nhantu,e.maql,to_number(to_char(e.ngay,'yyyy'))-to_number(c.namsinh) as tuoi,c.phai";
            sql += " from xxx.benhancc e inner join " + vschemas + ".btdbn c on e.mabn=c.mabn";
            sql += " where to_date(to_char(e.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') between to_date('" + vtungay + "','dd/mm/yyyy') and to_date('" + vdenngay + "','dd/mm/yyyy')";
            DataSet avds = this.get_data_mmyy(this.sql, vtungay, vdenngay, false);
            foreach (DataRow r in vdsxml.Tables[0].Rows)
            {
                vasql = "";
                vasql_goc = r["value"].ToString();
                vcolumn = "";
                if ((vasql_goc != "") && (r["id"].ToString() == "1"))
                {
                    foreach (DataRow r1 in vdsdk.Tables[0].Rows)
                    {
                        vcolumn = r1["ten"].ToString();
                        vasql = vasql_goc + " and " + r1["value"].ToString();
                        ar = avds.Tables[0].Select(vasql);
                        if (ar.Length > 0)
                        {
                            r[vcolumn] = ar.Length;
                        }
                    }
                }
            }

            #endregion
            vdsxml.AcceptChanges();
            return vdsxml;
        }
        private DataSet f_get_dstainantt_th(string vschemas, string vtungay, string vdenngay)
        {
            sql = " select a.mabn,1 as loaiba,e.nhantu,b.maql,to_number(to_char(a.ngay,'yyyy'))-to_number(c.namsinh) as tuoi,c.phai,b.sonao,";
            sql += " b.mubaohiem,b.cotsongco,b.mocapcuu,b.bivo,b.gaiquai,b.loaimu,b.ruoubia,b.mau,b.ptgaytn,b.nhapvien,b.tuvong,b.xinve,b.mabv";
            sql += " from ((xxx.tainantt a inner join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join " + vschemas + ".benhandt e on a.maql=e.maql)";
            sql += " inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " union all ";
            sql += " select a.mabn,2 as loaiba,e.nhantu,b.maql,to_number(to_char(a.ngay,'yyyy'))-to_number(c.namsinh) as tuoi,c.phai,b.sonao,";
            sql += " b.mubaohiem,b.cotsongco,b.mocapcuu,b.bivo,b.gaiquai,b.loaimu,b.ruoubia,b.mau,b.ptgaytn,b.nhapvien,b.tuvong,b.xinve,b.mabv";
            sql += " from ((xxx.tainantt a inner join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join " + vschemas + ".benhanngtr e on a.maql=e.maql)";
            sql += " inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " union all ";
            sql += " select a.mabn,4 as loaiba,e.nhantu,b.maql,to_number(to_char(a.ngay,'yyyy'))-to_number(c.namsinh) as tuoi,c.phai,b.sonao,";
            sql += " b.mubaohiem,b.cotsongco,b.mocapcuu,b.bivo,b.gaiquai,b.loaimu,b.ruoubia,b.mau,b.ptgaytn,b.nhapvien,b.tuvong,b.xinve,b.mabv";
            sql += " from ((xxx.tainantt a inner join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join xxx.benhancc e on a.maql=e.maql)";
            sql += " inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            sql += " union all ";
            sql += " select a.mabn,3 as loaiba,e.nhantu,b.maql,to_number(to_char(a.ngay,'yyyy'))-to_number(c.namsinh) as tuoi,c.phai,b.sonao,";
            sql += " b.mubaohiem,b.cotsongco,b.mocapcuu,b.bivo,b.gaiquai,b.loaimu,b.ruoubia,b.mau,b.ptgaytn,b.nhapvien,b.tuvong,b.xinve,b.mabv";
            sql += " from ((xxx.tainantt a inner join xxx.tainangt b on a.maql=b.maql)";
            sql += " inner join xxx.benhanpk e on a.maql=e.maql)";
            sql += " inner join " + vschemas + ".btdbn c on a.mabn=c.mabn";
            sql += " where to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay + "','" + sformat + "') and to_date('" + vdenngay + "','" + sformat + "')";
            return get_data_mmyy(sql, vtungay, vdenngay, false);
        }

        public bool upd_bieu06(long m_id, int m_ma, string m_ngay, int m_c01, int m_c02, int m_userid)
        {
            sql = "update " + user + ".bieu_06 set ngay=:m_ngay,c01=:m_c01,c02=:m_c02,";
            sql += "userid=:m_userid where id=:m_id and ma=:m_ma";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                int irec = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (irec == 0)
                {
                    sql = "insert into " + user + ".bieu_06(id,ma,ngay,c01,c02,userid,ngayud) values ";
                    sql += "(:m_id,:m_ma,:m_ngay,:m_c01,:m_c02,:m_userid,now())";
                    cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_id", NpgsqlDbType.Numeric).Value = m_id;
                    cmd.Parameters.Add("m_ma", NpgsqlDbType.Numeric).Value = m_ma;
                    cmd.Parameters.Add("m_ngay", NpgsqlDbType.Timestamp).Value = StringToDateTime(m_ngay);
                    cmd.Parameters.Add("m_c01", NpgsqlDbType.Numeric).Value = m_c01;
                    cmd.Parameters.Add("m_c02", NpgsqlDbType.Numeric).Value = m_c02;
                    cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                    irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "bieu_06");
                return false;
            }
            finally
            {

                con.Close(); con.Dispose();
            }
            return true;
        }

 

        public string Hoten_khongdau(string s)
        {
            ds = new DataSet();
            ds.ReadXml("..\\..\\..\\xml\\khongdau.xml");
            string s1 = s.Trim().ToUpper(), s2 = "";
            DataRow r;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].ToString().Trim() != "")
                {
                    r = getrowbyid(ds.Tables[0], "codau='" + s1[i].ToString() + "'");
                    if (r != null) s2 += r[1].ToString();
                    else s2 += s1[i];
                }
            }
            return s2;
        }
        public string khongdau(string s)
        {
            ds = new DataSet();
            ds.ReadXml("..\\..\\..\\xml\\khongdau.xml");
            string s1 = s.Trim().ToUpper(), s2 = "";
            DataRow r;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].ToString().Trim() != "")
                {
                    r = getrowbyid(ds.Tables[0], "codau='" + s1[i].ToString() + "'");
                    if (r != null) s2 += r[1].ToString();
                    else s2 += s1[i];
                }
                else s2 += s1[i];
            }
            return s2;
        }
        //Dinh Son 01/12/08  -Lay the BHYT
        public DataSet get_Vaovien(string tu, string den, string mabn, string hoten, string ngaysinh,
            int phai, string sonha, string thon, string mann, string madantoc, string matt, string maqu,
            string maphuongxa, string makp, string sovaovien, string maicd, string madoituong, string dentu,
            string nhantu, string dotuoi, bool icd, string soluutru, int userid, bool time)
        {
            string stime = (time) ? "'dd/mm/yyyy hh24:mi'" : "'dd/mm/yyyy'";
            if (time)
            {
                tu = tu + " " + sGiobaocao;
                den = den + " " + sGiobaocao;
            }
            hoten = Hoten_khongdau(hoten);
            sql = "select a.mabn,a.hoten,substr(k.tuoivao,1,3)||case when substr(k.tuoivao,4,1)=0 then 'TU' else case when substr(k.tuoivao,4,1)=1 then 'TH' else case when substr(k.tuoivao,4,1)=2 then 'NG' else 'GI' end end end as tuoi,";
            sql += "a.namsinh,a.sonha,a.thon,b.tenpxa,c.tenquan,d.tentt,a.phai,nvl(p.sothe,'') as sothe,q.hoten as bsnhanbenh,";
            sql += "g.tennn,to_char(f.ngay,'dd/mm/yyyy hh24:mi') as ngay,e.tenkp,h.doituong,";
            sql += "f.chandoan,f.maicd,f.sovaovien,i.ten as nhantu,j.ten as dentu, k.soluutru,u.hoten as tenuser ";
            sql += " from " + user + ".btdbn a inner join " + user + ".btdpxa b on a.maphuongxa=b.maphuongxa ";
            sql += " inner join " + user + ".btdquan c on a.maqu=c.maqu inner join " + user + ".btdtt d on a.matt=d.matt ";
            sql += " inner join " + user + ".benhandt f on a.mabn=f.mabn inner join " + user + ".btdkp_bv e on f.makp=e.makp ";
            sql += " inner join " + user + ".btdnn_bv g on a.mann=g.mann inner join " + user + ".doituong h on f.madoituong=h.madoituong ";
            sql += " inner join " + user + ".nhantu i on f.nhantu=i.ma inner join " + user + ".dentu j on f.dentu=j.ma ";
            sql += " inner join " + user + ".lienhe k on f.maql=k.maql left join " + user + ".dlogin u on f.userid=u.id ";
            sql += " left join " + user + ".bhyt p on f.maql=p.maql inner join " + user + ".dmbs q on f.mabs=q.ma";
            sql += " where ";
            sql += " f.loaiba=1 ";
            if (icd) sql += " and f.maicd in (select ma from " + user + ".dmicd10)";
            if (tu != "")
                sql += " and " + for_num_ngay("f.ngay", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            if (mabn != "") sql += " and a.mabn='" + mabn + "'";
            if (hoten != "") sql += " and a.hotenkdau like '%" + hoten + "%'";
            if (ngaysinh.Trim().Length == 10) sql += " and to_char(a.ngaysinh,'dd/mm/yyyy')='" + ngaysinh + "'";
            if (phai != -1) sql += " and a.phai=" + phai;
            if (sonha != "") sql += " and lower(trim(a.sonha))='" + sonha + "'";
            if (thon != "") sql += " and lower(trim(a.thon))='" + thon + "'";
            if (mann != "") sql += " and a.mann='" + mann + "'";
            if (madantoc != "") sql += " and a.madantoc='" + madantoc + "'";
            if (matt != "") sql += " and a.matt='" + matt + "'";
            if (maqu != "") sql += " and a.maqu='" + maqu + "'";
            if (maphuongxa != "") sql += " and a.maphuongxa='" + maphuongxa + "'";
            if (makp != "") sql += " and f.makp='" + makp + "'";
            if (sovaovien != "") sql += " and f.sovaovien='" + sovaovien + "'";
            if (maicd != "") sql += " and f.maicd='" + maicd + "'";
            if (madoituong != "") sql += " and f.madoituong=" + madoituong;
            if (dentu != "") sql += " and f.dentu=" + dentu;
            if (nhantu != "") sql += " and f.nhantu=" + nhantu;
            if (soluutru == "*") sql += " and k.soluutru is not null";
            else if (soluutru != "") sql += " and k.soluutru='" + soluutru + "'";
            if (userid != -1) sql += " and f.userid=" + userid;
            if (dotuoi != "")
            {
                if (dotuoi.IndexOf(">") != -1)
                    sql += " and to_number(to_char(now(),'yyyy'),'0000')-to_number(a.namsinh,'0000')>" + int.Parse(dotuoi.Substring(1));
                else
                {
                    int i1 = int.Parse(dotuoi.Substring(0, dotuoi.IndexOf("-")));
                    int i2 = int.Parse(dotuoi.Substring(dotuoi.IndexOf("-") + 1));
                    sql += " and to_number(to_char(now(),'yyyy'),'0000')-to_number(a.namsinh,'0000') between " + i1 + " and " + i2;
                }
            }
            sql += " order by f.ngay";
            return get_data(sql);
        }
        public DataSet get_Vaongoaitru(string tu, string den, string mabn, string hoten, string ngaysinh,
           int phai, string sonha, string thon, string mann, string madantoc, string matt, string maqu,
           string maphuongxa, string makp, string sovaovien, string maicd, string madoituong, string dentu,
           string nhantu, string dotuoi, bool icd, int userid, bool time)
        {
            try
            {
                string stime = (time) ? "'" + f_ngaygio + "'" : "'" + f_ngay + "'";
                if (time)
                {
                    tu = tu + " " + sGiobaocao;
                    den = den + " " + sGiobaocao;
                }
                hoten = Hoten_khongdau(hoten);
                sql = "select a.mabn,a.hoten,substr(k.tuoivao,1,3)||case when substr(k.tuoivao,4,1)=0 then 'TU' else case when substr(k.tuoivao,4,1)=1 then 'TH' else case when substr(k.tuoivao,4,1)=2 then 'NG' else 'GI' end end end as tuoi,";
                sql += "a.namsinh,nullif(a.sonha,' ') as sonha,nullif(a.thon,' ') as thon,b.tenpxa,c.tenquan,d.tentt,";
                sql += "g.tennn,to_char(f.ngay,'dd/mm/yyyy hh24:mi') as ngay,e.tenkp,h.doituong,";
                sql += "f.chandoan,f.maicd,f.sovaovien,i.ten as nhantu,j.ten as dentu,nullif(u.hoten,' ') as tenuser ";
                sql += " from " + user + ".btdbn a inner join " + user + ".btdpxa b on a.maphuongxa=b.maphuongxa";
                sql += " inner join " + user + ".btdquan c on a.maqu=c.maqu";
                sql += " inner join " + user + ".btdtt d on a.matt=d.matt";
                sql += " inner join " + user + ".benhanngtr f on a.mabn=f.mabn";
                sql += " inner join " + user + ".btdkp_bv e on f.makp=e.makp";
                sql += " inner join " + user + ".btdnn_bv g on a.mann=g.mann";
                sql += " inner join " + user + ".doituong h on f.madoituong=h.madoituong";
                sql += " inner join " + user + ".nhantu i on f.nhantu=i.ma";
                sql += " inner join " + user + ".dentu j on f.dentu=j.ma";
                sql += " inner join " + user + ".lienhe k on f.maql=k.maql";
                sql += " left join " + user + ".dlogin u on f.userid=u.id";
                sql += " where f.makp<>'01' and f.maql>0";
                if (tu != "")
                    sql += " and " + for_num_ngay("f.ngay", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
                if (makp != "") sql += " and f.makp='" + makp + "'";
                if (icd) sql += " and f.maicd in (select ma from " + user + ".dmicd10)";
                if (mabn != "") sql += " and a.mabn='" + mabn + "'";
                if (hoten != "") sql += " and a.hotenkdau='" + hoten + "'";
                if (ngaysinh.Trim().Length == 10) sql += " and to_char(a.ngaysinh,'dd/MM/yyyy')='" + ngaysinh + "'";
                if (phai != -1) sql += " and a.phai=" + phai;
                if (sonha != "") sql += " and lower(trim(a.sonha))='" + sonha.Trim().ToLower() + "'";
                if (thon != "") sql += " and lower(trim(a.thon))='" + thon.Trim().ToLower() + "'";
                if (mann != "") sql += " and a.mann='" + mann + "'";
                if (madantoc != "") sql += " and a.madantoc='" + madantoc + "'";
                if (matt != "") sql += " and a.matt='" + matt + "'";
                if (maqu != "") sql += " and a.maqu='" + maqu + "'";
                if (maphuongxa != "") sql += " and a.maphuongxa='" + maphuongxa + "'";
                if (madoituong != "") sql += " and f.madoituong=" + madoituong;
                if (sovaovien != "") sql += " and f.sovaovien='" + sovaovien + "'";
                if (maicd != "") sql += " and f.maicd='" + maicd + "'";
                if (madoituong != "") sql += " and f.madoituong=" + madoituong;
                if (dentu != "") sql += " and f.dentu=" + dentu;
                if (nhantu != "") sql += " and f.nhantu=" + nhantu;
                if (userid != -1) sql += " and f.userid=" + userid;
                if (dotuoi != "")
                {
                    if (dotuoi.IndexOf(">") != -1)
                        sql += " and to_number(to_char(now(),'yyyy'),'0000')-to_number(a.namsinh,'0000')>" + int.Parse(dotuoi.Substring(1));
                    else
                    {
                        int i1 = int.Parse(dotuoi.Substring(0, dotuoi.IndexOf("-")));
                        int i2 = int.Parse(dotuoi.Substring(dotuoi.IndexOf("-") + 1));
                        sql += " and to_number(to_char(now(),'yyyy'),'0000')-to_number(a.namsinh,'0000') between " + i1 + " and " + i2;
                    }
                }
                sql += " order by f.ngay";
                ds = get_data(sql);
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message.ToString().Trim(), sComputer, "Vaongtru");
            }
            return ds;
        }
        private DataSet f_get_data_ngtru_makpbo(string vschemas, string vtungay, string vdenngay, string sFormat)
        {
            DataSet ads = new DataSet();
            sql = "select sum(1) as a011,sum(case when b.makpbo in('03','04','05','06','07','08') then 1 else 0 end) as a012,sum(case when b.makpbo in('03','04','05','06','07','08') and a.nhantu=1 then 1 else 0 end) as a0121,";
            sql += " sum(case when b.makpbo='18' then 1 else 0 end) as a013,sum(case when b.makpbo='18' and a.nhantu=1 then 1 else 0 end) as a0131,";
            sql += " sum(case when b.makpbo='23' then 1 else 0 end) as a014,sum(case when b.makpbo='23' and a.nhantu=1 then 1 else 0 end) as a0141,";
            sql += " sum(case when b.makpbo='24' then 1 else 0 end) as a015,sum(case when b.makpbo='24' and a.nhantu=1 then 1 else 0 end) as a0151,";
            sql += " sum(case when b.makpbo='25' then 1 else 0 end) as a016,sum(case when b.makpbo='25' and a.nhantu=1 then 1 else 0 end) as a0161,";
            sql += " sum(case when b.makpbo='22' then 1 else 0 end) as a017,sum(case when b.makpbo='22' and a.nhantu=1 then 1 else 0 end) as a0171,";
            sql += " sum(case when b.makpbo='16' then 1 else 0 end) as a018,sum(case when b.makpbo='16' and a.nhantu=1 then 1 else 0 end) as a0181,";
            sql += " sum(case when b.makpbo='17' then 1 else 0 end) as a019,sum(case when b.makpbo='17' and a.nhantu=1 then 1 else 0 end) as a0191,";
            sql += " sum(case when b.makpbo='13' then 1 else 0 end) as a020,sum(case when b.makpbo='13' and a.nhantu=1 then 1 else 0 end) as a0201,";
            sql += " sum(0) as a021,sum(0) as a0211,sum(0) as a022,sum(0) as a0221,";
            sql += " sum(case when b.makpbo not in('03','04','05','06','07','08','18','22','24','16','17','23','13','25') then 1 else 0 end) as a023,";
            sql += " sum(case when b.makpbo not in('03','04','05','06','07','08','18','22','24','16','17','23','13','25') and a.nhantu=1 then 1 else 0 end) as a0231";
            sql += " from (xxx.benhanpk a inner join " + vschemas + ".btdkp_bv b on a.makp=b.makp) inner join xxx.xutrikbct c on a.maql=c.maql";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + vtungay + "','" + sFormat + "') and to_date('" + vdenngay + "','" + sFormat + "')";
            ads = get_data_mmyy(sql, vtungay, vdenngay, false);

            sql = "";
            sql += " from xxx.v_vienphill a inner join xxx.v_vienphict b on a.id=b.id";
            sql += " inner join "+vschemas+".v_giavp c on b.mavp=c.id";
            sql += " inner join "+vschemas+".v_loaivp d on c.id_loai=d.id";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + vtungay + "','" + sFormat + "') and to_date('" + vdenngay + "','" + sFormat + "')";
            return ads;
        }

        public DataSet f_get_data_hdcm_ngay(string vschemas, string vtungay, string vdenngay, bool vbgiobaocao,string vmau,int i_nhomkho)
        {
            string sFormat = (vbgiobaocao == true) ? sformat + " " + "hh24:mi" : sformat;
            if (vbgiobaocao)
            {
                vtungay += " " + sGiobaocao;
                vdenngay += " " + sGiobaocao;
            }
            DataSet vds = new DataSet();
            switch (vmau)
            {
                case "01":
                    vds.Merge(f_get_data_ngtru_makpbo(vschemas, vtungay, vdenngay, sFormat));
                    vds.Merge(f_get_data_noitruso(vschemas, vtungay, vdenngay, sFormat));
                    vds.Merge(f_get_data_vienphi(vschemas, vtungay, vdenngay, sFormat,i_nhomkho));
                    vds.Merge(f_get_pttt_khoa(vschemas, vtungay, vdenngay, sFormat));
                    vds.Merge(f_get_cls_khoa(vschemas, vtungay, vdenngay, sFormat));
                    f_zsum1(vds.Tables["Table"]);
                    break;
                case "02" :
                case "03" :
                case "04" :
                case "05" :
                    vds.Merge(f_get_data_noitrudanhsach(vschemas, vtungay, vdenngay, sFormat, vmau));
                    break;
                default :
                    vds.Merge(f_get_data_ngtru_makpbo(vschemas, vtungay, vdenngay, sFormat));
                    vds.Merge(f_get_data_noitruso(vschemas, vtungay, vdenngay, sFormat));
                    break;

            }
            return vds;
        }
        /// <summary>
        /// Lấy tổng số tiền thực thu : thutructiep+thutamung+thuthanhtoanrv+Xuatban
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="sFormat"></param>
        /// <returns></returns>
        private DataSet f_get_data_vienphi(string vschemas, string vtungay, string vdenngay, string sFormat,int i_vnhomkho)
        {
            DataSet vds = new DataSet("dtVienphi");
            //thutructiep
            sql = "select zs.loai, sum(zs.a051) as a051 from (select 1 as loai, nvl(sum(b.dongia*b.soluong-b.mien-b.thieu),0) as a051 ";
            sql += " from xxx.v_vienphill a inner join xxx.v_vienphict b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'"+sformat+"'),'"+sformat+"') between to_date('"+vtungay.ToString().Substring(0,10)+"','"+sformat+"') and to_date('"+vdenngay.ToString().Substring(0,10)+"','"+sformat+"')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += " union all ";
            //thuthanhtoanravien
            sql += " select 1 as loai,nvl(sum(a.sotien-a.tamung-a.bhyt-a.mien),0) as a051 ";
            sql += " from xxx.v_ttrvll a inner join xxx.v_ttrvds b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            //thutamung
            sql += " union all ";
            sql += " select 1 as loai,nvl(sum(a.sotien),0) as a051";
            sql += " from xxx.v_tamung a";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += ") zs group by zs.loai";
            vds = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10),false);
            vds.Merge(f_get_xuatban(vschemas, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), sFormat,i_vnhomkho));
            vds.Tables[0].TableName = "dtVienphi";
            vds.AcceptChanges();
            return vds;
        }
        /// <summary>
        /// Lấy số tiền xuất bán
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="sFormat"></param>
        /// <returns></returns>
        private DataSet f_get_xuatban(string vschemas, string vtungay, string vdenngay, string sFormat,int i_vnhomkho)
        {
           
            int i_nhom = i_vnhomkho;
            int i_Dongiale = d_dongia_le(i_nhom);
            sql = " select 2 as loai,sum(zs.a052) as a051 from (select sum(b.soluong*trunc(b.giaban," + i_Dongiale + ")) as a052";
            sql += " from xxx.d_ngtrull a,xxx.d_ngtruct b, " + vschemas + ".d_dmbd d,xxx.d_theodoi c";
            sql += " where a.id=b.id and b.mabd=d.id and a.nhom=" + i_nhom;
            sql += " and b.sttt=c.id and b.mabd=c.mabd";
            sql += " and to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') between to_date('" + vtungay.Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.Substring(0, 10) + "','" + sformat + "')";

            //trathuoc
            sql += " union all ";
            sql += " select sum(-1*b.soluong*trunc(b.giaban," + i_Dongiale + ")) as a052";
            sql += " from xxx.d_nhapll a,xxx.d_nhapct b," + user + ".d_dmbd c ";
            sql += " where a.id=b.id and a.loai='N' and b.mabd=c.id and a.nhom=" + i_nhom;
            sql += " and to_date(to_char(a.ngaysp,'dd/mm/yyyy'),'dd/mm/yyyy') between to_date('" + vtungay.Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.Substring(0, 10) + "','" + sformat + "')";
            sql += ") zs";

            return get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
        }
        private DataSet f_get_data_noitruso(string vschemas, string vtungay, string vdenngay, string sFormat)
        {
            DataSet vds = new DataSet("dtNoitru");
            string vsqlnhap = " and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            string vsqlxuat = " and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            sql = " select b.makp,d.tenkp,";
            sql += " sum(case when to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vtungay + "','" + sFormat + "') and (c.ngay is null or to_date(to_char(c.ngay, '" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "')) then 1 else 0 end) as a021,";
            sql += " sum(case when b.khoachuyen='01' " + vsqlnhap + " then 1 else 0 end) as a022,";
            sql += " sum(case when b.khoachuyen<>'01' " + vsqlnhap + " then 1 else 0 end) as a023,";
            sql += " sum(case when c.ttlucrk=5 " + vsqlxuat + " then 1 else 0 end) as a024,";
            sql += " sum(case when c.ttlucrk not in(5,6,7) " + vsqlxuat + " then 1 else 0 end) as a025,";
            sql += " sum(case when c.ttlucrk=6 " + vsqlxuat + " then 1 else 0 end) as a026,";
            sql += " sum(case when c.ttlucrk=7 " + vsqlxuat + " then 1 else 0 end) as a027,";
            sql += " sum(case when (c.ngay is null or to_date(to_char(c.ngay, '" + sFormat + "'),'" + sFormat + "')>to_date('" + vdenngay + "','" + sFormat + "')) and to_date(to_char(b.ngay,'"+sFormat+"'),'"+sFormat+"')<=to_date('"+vdenngay+"','"+sFormat+"') then 1 else 0 end) as a028";
            sql += " from " + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql";
            sql += " left join " + vschemas + ".xuatkhoa c on b.id=c.id";
            sql += " inner join " + vschemas + ".btdkp_bv d on b.makp=d.makp";
            sql += " where a.loaiba=1";
            sql += " group by b.makp,d.tenkp";
            vds= get_data(sql);
            vds.Tables[0].TableName = "dtNoitru";
            return vds;
        }

        private DataSet f_get_data_noitruso_kotablename(string vschemas, string vtungay, string vdenngay, string sFormat)
        {
            string vsqlnhap = " and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            string vsqlxuat = " and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            sql = " select b.makp,d.tenkp,";
            sql += " sum(case when to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vtungay + "','" + sFormat + "') and (c.ngay is null or to_date(to_char(c.ngay, '" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "')) then 1 else 0 end) as a051,";
            sql += " sum(case when b.id>0 " + vsqlnhap + " then 1 else 0 end) as a052,";         
            sql += " sum(case when c.ttlucrk not in(5,6,7) " + vsqlxuat + " then 1 else 0 end) as a053,";
            sql += " sum(case when c.ttlucrk=6 " + vsqlxuat + " then 1 else 0 end) as a054,";
            sql += " sum(case when c.ttlucrk=7 " + vsqlxuat + " then 1 else 0 end) as a055,";
            sql += " sum(case when (c.ngay is null or to_date(to_char(c.ngay, '" + sFormat + "'),'" + sFormat + "')>to_date('" + vdenngay + "','" + sFormat + "')) and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<=to_date('" + vdenngay + "','" + sFormat + "') then 1 else 0 end) as a056";
            sql += " from " + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql";
            sql += " left join " + vschemas + ".xuatkhoa c on b.id=c.id";
            sql += " inner join " + vschemas + ".btdkp_bv d on b.makp=d.makp";
            sql += " where a.loaiba=1";
            sql += " group by b.makp,d.tenkp";
            return get_data(sql);
        }
        private DataSet f_get_pttt_khoa(string vschemas, string vtungay, string vdenngay, string sFormat)
        {
            DataSet vds = new DataSet("dtPttt");

            #region taotable
            DataTable dt = new DataTable("dtPttt");
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("TEN", typeof(string));
            dt.Columns.Add("NOIDUNG", typeof(string));
            vds.Tables.Add(dt);
            vds.AcceptChanges();
            DataRow r = vds.Tables[0].NewRow();
            r["id"] = "loai1";
            r["ten"] = "* Loại I";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai2";
            r["ten"] = "* Loại II";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai3";
            r["ten"] = "* Loại III";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai4";
            r["ten"] = "* Loại IV(TT)";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai5";
            r["ten"] = "- PT Nội soi";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai6";
            r["ten"] = "- PT PHACO";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            vds.AcceptChanges();
            #endregion

            sql = "select substr(a.mapt,0,1) as loaipt,a.makp,c.tenkp,sum(case when b.loai1 in('A','B','C') then 1 else 0 end) as loai1,";
            sql += " sum(case when b.loai2 in('A','B','C') then 1 else 0 end) as loai2,";
            sql += " sum(case when b.loai3 in('x') then 1 else 0 end) as loai3,";
            sql += " sum(case when b.dacbiet in('x') then 1 else 0 end) as dacbiet,";
            sql += " sum(case when a.noisoi=1 then 1 else 0 end) as noisoi";
            sql += " from xxx.pttt a inner join " + vschemas + ".dmpttt b on a.mapt=b.mapt";
            sql += " inner join " + vschemas + ".btdkp_bv c on a.makp=c.makp";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + vtungay + "','" + sFormat + "') and to_date('" + vdenngay + "','" + sFormat + "')";
            sql += " group by substr(a.mapt,0,1),a.makp,c.tenkp";
            DataSet vds1 = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
            DataRow[] ar = vds1.Tables[0].Select("loaipt='P' and loai1>0");
            string snoidung = "";
            snoidung = "(";
            foreach (DataRow r1 in ar) snoidung += r1["tenkp"].ToString() + ":" + r1["loai1"].ToString() + "   " + ";";
            snoidung += ")";
            r = getrowbyid(vds.Tables[0], "id='loai1'");
            if(r!=null) r["noidung"] = snoidung;
            //loai2
            ar = vds1.Tables[0].Select("loaipt='P' and loai2>0");
            snoidung = "(";
            foreach (DataRow r1 in ar) snoidung += r1["tenkp"].ToString() + ":" + r1["loai2"].ToString() + "   " + ";";
            snoidung += ")";
            r = getrowbyid(vds.Tables[0], "id='loai2'");
            if (r != null) r["noidung"] = snoidung;
            //loai3
            ar = vds1.Tables[0].Select("loaipt='P' and loai3>0");
            snoidung = "(";
            foreach (DataRow r1 in ar) snoidung += r1["tenkp"].ToString() + ":" + r1["loai3"].ToString() + "   " + ";";
            snoidung += ")";
            r = getrowbyid(vds.Tables[0], "id='loai3'");
            if (r != null) r["noidung"] = snoidung;

            //loai5
            ar = vds1.Tables[0].Select("loaipt='P' and noisoi>0");
            snoidung = "(";
            foreach (DataRow r1 in ar) snoidung += r1["tenkp"].ToString() + ":" + r1["noisoi"].ToString() + "   " + ";";
            snoidung += ")";
            r = getrowbyid(vds.Tables[0], "id='loai4'");
            if (r != null) r["noidung"] = snoidung;

            vds.AcceptChanges();
            return vds;
        }
        private DataSet f_get_cls_khoa(string vschemas, string vtungay, string vdenngay, string sFormat)
        {
            DataSet vds = new DataSet("dtCLS");

            #region taotable
            DataTable dt = new DataTable("dtCLS");
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("TEN", typeof(string));
            dt.Columns.Add("NOIDUNG", typeof(string));
            vds.Tables.Add(dt);
            vds.AcceptChanges();
            DataRow r = vds.Tables[0].NewRow();
            r["id"] = "loai1";
            r["ten"] = "- XQuang";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai2";
            r["ten"] = "- Xét nghiệm";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai3";
            r["ten"] = "- Siêu âm tổng quát";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai4";
            r["ten"] = "- Siêu âm tim";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai5";
            r["ten"] = "- Điện tim";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai6";
            r["ten"] = "- Nội soi";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai7";
            r["ten"] = "- CO2";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            r = vds.Tables[0].NewRow();
            r["id"] = "loai8";
            r["ten"] = "- Đo thính lực";
            r["noidung"] = "";
            vds.Tables[0].Rows.Add(r);
            vds.AcceptChanges();
            #endregion 

            sql = "select d.loai,a.makp,d.tenkp,b.makt,c.tenkt,to_char(c.id_loaicdha) as idloai,count(*) as solan";
            sql += " from xxx.cdha_bnll a inner join xxx.cdha_bnct b on a.id=b.id";
            sql += " inner join " + vschemas + ".cdha_kythuat c on b.makt=c.makt";
            sql += " inner join " + vschemas + ".btdkp_bv d on a.makp=d.makp";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + vtungay + "','" + sFormat + "') and to_date('" + vdenngay + "','" + sFormat + "')";
            sql += " group by d.loai,a.makp,d.tenkp,b.makt,c.tenkt,c.id_loaicdha";
            sql += " order by d.loai,a.makp";
            DataSet vds1 = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
            vds1.WriteXml("..\\..\\Datareport\\tt.xml", XmlWriteMode.WriteSchema);
            DataRow[] ar = vds1.Tables[0].Select("idloai='2'");
            string snoidung = "";int ssoluong=0;
            foreach (DataRow r1 in ar) ssoluong += int.Parse(r1["solan"].ToString());
            snoidung +=ssoluong.ToString();
            snoidung += " NB,   ";
            ar = vds1.Tables[0].Select("idloai='5'");
            foreach (DataRow r1 in ar) ssoluong += int.Parse(r1["solan"].ToString());
            snoidung += ssoluong.ToString();
            snoidung += " NB,";
            r = getrowbyid(vds.Tables[0], "id='loai1'");
            if (r != null) r["noidung"] = snoidung;

            //loai3
            snoidung = "";
            string ssubnoidung = "";
            int ssubsoluong = 0;
            ar = vds1.Tables[0].Select("idloai='1' and makt='SA002'");
            
            foreach (DataRow r1 in ar)
            {
                if (r1["loai"].ToString() == "1") ssubsoluong += int.Parse(r1["solan"].ToString());
                else
                {
                    ssubnoidung += "" + r1["tenkp"].ToString() + ":  " + r1["solan"].ToString() + ";";
                }
                ssoluong += int.Parse(r1["solan"].ToString());
            }
            snoidung = "(PK:";
            snoidung += ssubsoluong.ToString()+"NB,";
            snoidung += ssubnoidung;
            snoidung += ")";
            r = getrowbyid(vds.Tables[0], "id='loai3'");
            if (r != null) r["noidung"] = snoidung;

            sql = " select e.id,e.ten,count(distinct(d.id)) as solan";
            sql += " from xxx.xn_phieu a inner join xxx.xn_ketqua b on a.id=b.id";
            sql += " inner join " + vschemas + ".xn_bv_chitiet c on b.id_ten=c.id";
            sql += " inner join medibv.xn_bv_ten d on d.id=c.id_bv_ten";
            sql += " inner join medibv.xn_bv_so e on d.id_bv_so=e.id";
            sql += " where to_date(to_char(a.ngay,'" + sFormat + "'),'" + sFormat + "') between to_date('" + vtungay + "','" + sFormat + "') and to_date('" + vdenngay + "','" + sFormat + "')";
            sql += " group by e.id,e.ten";
            vds1 = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
            vds.AcceptChanges();
            return vds;
        }
        /// <summary>
        /// Lấy danh sách bệnh nhân ra khoa
        /// </summary>
        /// <param name="vschemas"></param>
        /// <param name="vtungay"></param>
        /// <param name="vdenngay"></param>
        /// <param name="sFormat"></param>
        /// <returns></returns>
        private DataSet f_get_data_noitrudanhsach(string vschemas, string vtungay, string vdenngay, string sFormat,string vloai)
        {
            DataSet ads = new DataSet();
            string vsqlnhap = " and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(b.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            string vsqlxuat = " and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')>=to_date('" + vtungay + "','" + sFormat + "') and to_date(to_char(c.ngay,'" + sFormat + "'),'" + sFormat + "')<to_date('" + vdenngay + "','" + sFormat + "')";
            sql = " select b.makp,d.tenkp,e.hoten,e.namsinh,b.maicd as icdvaok,b.chandoan as chandoanvaok,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngayvaovien";
            if (vloai == "02")
            {
                sql += ",kc.tenkp as khoachuyen,to_char(b.ngay,'dd/mm/yyyy hh24:mi') as ngayvk";
            }
            else
            {
                sql += ",kd.tenkp as khoaden,to_char(c.ngay,'dd/mm/yyyy hh24:mi') as ngayrk,c.maicd as icdrak,c.chandoan as chandoanrak,k.tenbv";
            }
            sql += " from " + vschemas + ".benhandt a inner join " + vschemas + ".nhapkhoa b on a.maql=b.maql";
            if (vloai == "02")
            {
                sql += " inner join " + vschemas + ".btdkp_bv kc on b.khoachuyen=kc.makp";
            }
            else
            {
                sql += " inner join " + vschemas + ".xuatkhoa c on b.id=c.id";
                sql += " left join " + vschemas + ".chuyenkhoa f on c.id=f.id";
                sql += " left join " + vschemas + ".btdkp_bv kd on f.khoaden=kd.makp";
                sql += " left join " + vschemas + ".tuvong g on b.maql=g.maql";
                sql += " left join " + vschemas + ".chuyenvien h on b.maql=h.maql";
                sql += " left join " + vschemas + ".dstt k on h.mabv=k.mabv";
            }
            sql += " inner join " + vschemas + ".btdkp_bv d on b.makp=d.makp";
            sql += " inner join " + vschemas + ".btdbn e on a.mabn=e.mabn";
            sql += " where a.loaiba=1";
            if (vloai == "02")
            {
                sql += vsqlnhap;
                sql += " and b.khoachuyen<>'01'";
            }
            else
            {
                sql += vsqlxuat;
                if (vloai == "03") sql += " and c.ttlucrk=5";
                if (vloai == "04") sql += " and c.ttlucrk=6";
                if (vloai == "05") sql += " and c.ttlucrk=7";
            }
            ads = get_data(sql);
            
            return ads;
        }
        public DataSet f_get_data_vpkhoaphong_th(DataSet dsxml, string vschemas, string vtungay, string vdenngay, int i_vnhomkho)
        {
            string s_idvpkhambenh = "31";
            string s_idnhomvpxetnghiem = "4,5";
            string s_idnhomthuoc = "1";
            DataSet vds = f_get_vienphi_khoaphong(dsxml, vschemas, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), i_vnhomkho, s_idvpkhambenh,s_idnhomvpxetnghiem,s_idnhomthuoc);
            
            //thuthanhtoanravien
            sql += " select 1 as loai,nvl(sum(a.sotien-a.tamung-a.bhyt-a.mien),0) as a051 ";
            sql += " from xxx.v_ttrvll a inner join xxx.v_ttrvds b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            //thutamung
            sql += " union all ";
            sql += " select 1 as loai,nvl(sum(a.sotien),0) as a051";
            sql += " from xxx.v_tamung a";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += ") zs group by zs.loai";
            
            vds.AcceptChanges();
            return vds;
        }
        public DataSet get_Tainantt(string tu, string den, string mabn, string hoten, string ngaysinh,
           int phai, string thon, string mann, string madantoc, string matt, string maqu,
           string maphuongxa, string dotuoi, string diadiem, string bophan, string nguyennhan,
           string ngodoc, string dienbien, string xutri, int userid, bool time)
        {
            string m_dotuoi = "", stime = (time) ? "'dd/mm/yyyy hh24:mi'" : "'dd/mm/yyyy'";
            if (time)
            {
                tu = tu + " " + sGiobaocao;
                den = den + " " + sGiobaocao;
            }
            hoten = Hoten_khongdau(hoten);
            sql = "SELECT a.mabn,t.hoten,t.namsinh,c.tennn,d.ten as diadiem,e.ten as bophan, f.TEN as nguyennhan, g.TEN as ngodoc,h.ten as dienbien,i.TEN as xutri,a.thon,x.tentt,y.tenquan,z.tenpxa,trim(t.sonha)||' '||t.thon||' '||trim(z1.tenpxa)||','||trim(y1.tenquan)||','||x1.tentt as diachi,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngay,";
            sql += "a.lydo,a.chandoan,a.dieutri,a.lucvao,a.lucra";
            sql += " FROM xxx.TAINANTT a," + user + ".btdbn t," + user + ".BTDNN_BV c," + user + ".DMDIADIEM d," + user + ".DMBOPHAN e," + user + ".DMNGUYENNHAN f," + user + ".DMDIENBIEN h," + user + ".DMXUTRI i," + user + ".DMNGODOC g," + user + ".btdtt x," + user + ".btdquan y," + user + ".btdpxa z," + user + ".btdtt x1," + user + ".btdquan y1," + user + ".btdpxa z1 ";
            sql += " where a.mabn=t.mabn and t.MANN = c.MANN and a.DIADIEM = d.MA and a.BOPHAN = e.MA and a.NGUYENNHAN = f.MA and a.DIENBIEN = h.MA and a.XUTRI = i.MA and a.NGODOC = g.MA";
            sql += " and a.matt=x.matt and a.maqu=y.maqu and a.maphuongxa=z.maphuongxa and t.matt=x1.matt and t.maqu=y1.maqu and t.maphuongxa=z1.maphuongxa";
            if (tu != "")
            {
                if (time)
                    sql += " and a.ngay between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
                else
                    sql += " and " + for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
            }
            if (mabn != "") sql += " and t.mabn='" + mabn + "'";
            if (hoten != "") sql += " and t.hotenkdau='" + hoten + "'";
            if (ngaysinh.Trim().Length == 10) sql += " and to_char(t.ngaysinh,'dd/mm/yyyy')='" + ngaysinh + "'";
            if (phai != -1) sql += " and t.phai=" + phai;
            if (thon != "") sql += " and a.thon='" + thon + "'";
            if (mann != "") sql += " and t.mann='" + mann + "'";
            if (madantoc != "") sql += " and t.madantoc='" + madantoc + "'";
            if (matt != "") sql += " and a.matt='" + matt + "'";
            if (maqu != "") sql += " and a.maqu='" + maqu + "'";
            if (maphuongxa != "") sql += " and a.maphuongxa='" + maphuongxa + "'";
            if (diadiem != "") sql += " and a.diadiem='" + diadiem + "'";
            if (bophan != "") sql += " and a.bophan='" + bophan + "'";
            if (nguyennhan != "") sql += " and a.nguyennhan='" + nguyennhan + "'";
            if (ngodoc != "") sql += " and a.ngodoc='" + ngodoc + "'";
            if (dienbien != "") sql += " and a.dienbien='" + dienbien + "'";
            if (xutri != "") sql += " and a.xutri='" + xutri + "'";
            if (userid != -1) sql += " and a.userid='" + userid + "'";
            if (dotuoi != "")
            {
                if (dotuoi.IndexOf(">") != -1)
                {
                    m_dotuoi = dotuoi.Substring(1);
                    sql += " and to_number(to_char(now(),'yyyy'))-to_number(t.namsinh)>" + m_dotuoi;
                }
                else
                {
                    int i1 = int.Parse(dotuoi.Substring(0, dotuoi.IndexOf("-")));
                    int i2 = int.Parse(dotuoi.Substring(dotuoi.IndexOf("-") + 1));
                    sql += " and to_number(to_char(now(),'yyyy'),'0000')-to_number(t.namsinh,'0000') between " + i1 + " and " + i2;
                }
            }

            sql += " order by a.ngay";
            return get_data_mmyy(sql, tu, den, false);
        }

        #endregion

        #region Event Table
        public bool upd_eve_tables(int m_tableid, int m_userid, string m_command)
        {
            string ngay = Ngay_hethong_gio.Substring(0, 10);
            int icomputerid = get_dmcomputer(), m_ins = 0, m_upd = 0, m_del = 0;
            switch (m_command)
            {
                case "upd": m_upd = 1; break;
                case "del": m_del = 1; break;
                default: m_ins = 1; break;
            }
            sql = user + ".upd_eve_tables";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_tableid", NpgsqlDbType.Numeric).Value = m_tableid;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Varchar, 10).Value = ngay;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_computerid", NpgsqlDbType.Numeric).Value = icomputerid;
                cmd.Parameters.Add("m_ins", NpgsqlDbType.Numeric).Value = m_ins;
                cmd.Parameters.Add("m_upd", NpgsqlDbType.Numeric).Value = m_upd;
                cmd.Parameters.Add("m_del", NpgsqlDbType.Numeric).Value = m_del;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "eve_tables");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }

        public bool upd_eve_upd_del(int m_tableid, int m_userid, string m_command, string m_noidung)
        {
            int icomputerid = get_dmcomputer();
            sql = user + ".upd_eve_upd_del";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_tableid", NpgsqlDbType.Numeric).Value = m_tableid;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_computerid", NpgsqlDbType.Numeric).Value = icomputerid;
                cmd.Parameters.Add("m_command", NpgsqlDbType.Varchar, 3).Value = m_command;
                cmd.Parameters.Add("m_noidung", NpgsqlDbType.Text).Value = m_noidung;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(ex.Message, sComputer, "eve_upd_del");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }

        public bool upd_eve_tables(string m_mmyy, int m_tableid, int m_userid, string m_command)
        {
            string ngay = Ngay_hethong_gio.Substring(0, 10);
            int icomputerid = get_dmcomputer(), m_ins = 0, m_upd = 0, m_del = 0;
            switch (m_command)
            {
                case "upd": m_upd = 1; break;
                case "del": m_del = 1; break;
                default: m_ins = 1; break;
            }
            sql = user + m_mmyy + ".upd_eve_tables";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_tableid", NpgsqlDbType.Numeric).Value = m_tableid;
                cmd.Parameters.Add("m_ngay", NpgsqlDbType.Varchar, 10).Value = ngay;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_computerid", NpgsqlDbType.Numeric).Value = icomputerid;
                cmd.Parameters.Add("m_ins", NpgsqlDbType.Numeric).Value = m_ins;
                cmd.Parameters.Add("m_upd", NpgsqlDbType.Numeric).Value = m_upd;
                cmd.Parameters.Add("m_del", NpgsqlDbType.Numeric).Value = m_del;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(m_mmyy, ex.Message, sComputer, "eve_tables");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }

        public bool upd_eve_upd_del(string m_mmyy, int m_tableid, int m_userid, string m_command, string m_noidung)
        {
            int icomputerid = get_dmcomputer();
            sql = user + m_mmyy + ".upd_eve_upd_del";
            con = new NpgsqlConnection(sConn);
            try
            {
                con.Open();
                cmd = new NpgsqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("m_tableid", NpgsqlDbType.Numeric).Value = m_tableid;
                cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                cmd.Parameters.Add("m_computerid", NpgsqlDbType.Numeric).Value = icomputerid;
                cmd.Parameters.Add("m_command", NpgsqlDbType.Varchar, 3).Value = m_command;
                cmd.Parameters.Add("m_noidung", NpgsqlDbType.Text).Value = m_noidung;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                upd_error(m_mmyy, ex.Message, sComputer, "eve_upd_del");
                return false;
            }
            finally
            {
                cmd.Dispose();
                con.Close(); con.Dispose();
            }
            return true;
        }

        private int tableid()
        {
            ds = get_data("select max(id) as id from " + user + ".dmtables");
            if (ds.Tables[0].Rows[0]["id"].ToString() == "") return 1;
            else return int.Parse(ds.Tables[0].Rows[0]["id"].ToString()) + 1;
        }

        public int tableid(string mmyy, string tables)
        {
            int ret = 1;
            ds = get_data("select * from " + user + ".dmtables where tables='" + tables + "'");
            if (ds.Tables[0].Rows.Count > 0) ret = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
            else
            {
                ret = tableid();
                string fie = "";
                for (int i = 0; i < get_data("select * from " + user + mmyy + "." + tables + " where oid=-1").Tables[0].Columns.Count; i++) fie += ds.Tables[0].Columns[i].ColumnName.ToString().Trim() + "^";
                execute_data("insert into " + user + ".dmtables(id,tables,fields) values (" + ret + ",'" + tables + "','" + fie.Substring(0, fie.Length - 1) + "')");
            }
            return ret;
        }

        public string fields(string table, string dk)
        {
            string ret = "";
            DataTable dt = get_data("select * from " + table + " where " + dk).Tables[0];
            if (dt.Rows.Count > 0) for (int i = 0; i < dt.Columns.Count; i++) ret += dt.Rows[0][i].ToString().Trim() + "^";
            return (ret != "") ? ret.Substring(0, ret.Length - 1) : ret;
        }
        #endregion

        #region Vienphi
        public DataSet f_get_vienphi_khoaphong(DataSet dsxml, string vschemas, string vtungay, string vdenngay, int i_vnhomkho, string idloaikb, string idnhomxn, string idnhomthuoc)
        {
            string i_vitrithetunguyen = thetunguyen_vitri(i_vnhomkho);
            string s_thetunguyen = thetunguyen(i_vnhomkho);
            sql = "select b.makp,f.tenkp,b.madoituong,g.doituong,e.id as idloai,e.id_nhom as idnhom,e.ten as loaivp,sum(b.soluong) as soluong,sum(b.soluong*b.dongia-b.mien-b.thieu) as sotien";
            sql += " from xxx.v_vienphill a inner join xxx.v_vienphict b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " inner join " + vschemas + ".v_giavp d on b.mavp=d.id";
            sql += " inner join " + vschemas + ".v_loaivp e on d.id_loai=e.id";
            sql += " left join " + vschemas + ".btdkp_bv f on b.makp=f.makp";
            sql += " left join " + vschemas + ".doituong g on b.madoituong=g.madoituong";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += " group by b.makp,f.tenkp,b.madoituong,g.doituong,e.id,e.id_nhom,e.ten";
            vds = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);

            dsxml = f_insert_items(vds, dsxml, "idloai in(" + idloaikb.ToString() + ")", "dv", "bhyt", "tenkp", 1, "Ngoại trú");
            dsxml = f_insert_items(vds, dsxml, "idnhom in (" + idnhomxn.ToString() + ")", "dv", "bhyt", "loaivp", 2, "Cận lâm sàng");
            dsxml = f_insert_items(vds, dsxml, "idloai not in (" + idloaikb.ToString() + ") and idnhom not in (" + idnhomxn.ToString() + ")", "dv", "bhyt", "loaivp", 3, "Khác");

            //Lay bhyt ngoaitru
            sql = "select b.makp,f.tenkp,b.madoituong,g.doituong,count(*) as soluong,sum(b.soluong*b.dongia-b.mien-b.thieu) as sotien";
            sql += " from xxx.bhytkb a ";
            sql += " left join " + vschemas + ".btdkp_bv f on a.makp=f.makp";
            sql += " left join " + vschemas + ".doituong g on a.madoituong=g.madoituong";
            sql += " where a.loaiba<>1 and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += " group by b.makp,f.tenkp,b.madoituong,g.doituong";
            //vds = m_b.get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);

            sql = "select b.makp,f.tenkp,b.madoituong,g.doituong,e.id as idloai,e.id_nhom as idnhom,e.ten as loaivp,sum(b.soluong) as soluong,sum(b.soluong*b.dongia) as sotien";
            sql += " from xxx.v_ttrvll a inner join xxx.v_ttrvct b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " inner join " + vschemas + ".v_giavp d on b.mavp=d.id";
            sql += " inner join " + vschemas + ".v_loaivp e on d.id_loai=e.id";
            sql += " left join " + vschemas + ".btdkp_bv f on b.makp=f.makp";
            sql += " left join " + vschemas + ".doituong g on b.madoituong=g.madoituong";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += " and (a.bhyt=0 or (a.bhyt>0 and g.mien<>1)) and a.loaibn=1";
            sql += " group by b.makp,f.tenkp,b.madoituong,g.doituong,e.id,e.id_nhom,e.ten";
            vds = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
            dsxml = f_insert_items(vds, dsxml, "idnhom not in (" + idnhomthuoc.ToString() + ")", "dv", "bhyt", "tenkp", 4, "Nội trú");

            sql = "select b.makp,f.tenkp,b.madoituong,g.doituong,e.id as idloai,e.id_nhom as idnhom,e.ten as loaivp,sum(b.soluong) as soluong,sum(b.soluong*b.dongia) as sotien";
            sql += " from xxx.v_ttrvll a inner join xxx.v_ttrvct b on a.id=b.id";
            sql += " left join (select * from xxx.v_hoantra ht where to_date(to_char(ht.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')) c on a.userid=c.userid and a.quyenso=c.quyenso and a.sobienlai=c.sobienlai";
            sql += " inner join " + vschemas + ".v_giavp d on b.mavp=d.id";
            sql += " inner join " + vschemas + ".v_loaivp e on d.id_loai=e.id";
            sql += " left join " + vschemas + ".btdkp_bv f on b.makp=f.makp";
            sql += " left join " + vschemas + ".doituong g on b.madoituong=g.madoituong";
            sql += " where c.id is null and to_date(to_char(a.ngay,'" + sformat + "'),'" + sformat + "') between to_date('" + vtungay.ToString().Substring(0, 10) + "','" + sformat + "') and to_date('" + vdenngay.ToString().Substring(0, 10) + "','" + sformat + "')";
            sql += " and a.bhyt>0 and g.mien=1 and a.loaibn=1";
            sql += " group by b.makp,f.tenkp,b.madoituong,g.doituong,e.id,e.id_nhom,e.ten";
            vds = get_data_mmyy(sql, vtungay.Substring(0, 10), vdenngay.Substring(0, 10), false);
            dsxml = f_insert_items(vds, dsxml, "idnhom not in (" + idnhomthuoc.ToString() + ")", "bhyt", "dv", "tenkp", 4, "Nội trú");

            dsxml.AcceptChanges();
            dsxml.WriteXml("..\\..\\datareport\\t1.xml", XmlWriteMode.WriteSchema);
            return dsxml;
        }

        private DataSet f_insert_items(DataSet vds, DataSet dsxml, string qsql, string m_field, string m_field2, string m_field_sosanh, int i_loai, string m_ten)
        {
            DataRow[] ar = vds.Tables[0].Select(qsql);
            foreach (DataRow r in ar)
            {
                vsql = "loai=" + i_loai.ToString() + " and tenkp='" + r[m_field_sosanh].ToString() + "'";
                r1 = getrowbyid(dsxml.Tables[0], vsql);
                if (r1 == null)
                {
                    r2 = dsxml.Tables[0].NewRow();
                    r2["stt"] = i_loai;
                    r2["ten"] = m_ten;
                    r2["loai"] = i_loai;
                    r2["tenkp"] = r[m_field_sosanh].ToString();
                    r2[m_field] = m_field.ToString().ToUpper();
                    r2[m_field + "_soluong"] = r["soluong"].ToString();
                    r2[m_field + "_sotien"] = r["sotien"].ToString();
                    r2[m_field2] = m_field2.ToString().ToUpper();
                    r2[m_field2 + "_soluong"] = 0;
                    r2[m_field2 + "_sotien"] = 0;
                    dsxml.Tables[0].Rows.Add(r2);
                }
                else
                {
                    r1[m_field + "_soluong"] = decimal.Parse(r1[m_field + "_soluong"].ToString()) + decimal.Parse(r["soluong"].ToString());
                    r1[m_field + "_sotien"] = decimal.Parse(r1[m_field + "_sotien"].ToString()) + decimal.Parse(r["sotien"].ToString());
                }
            }
            dsxml.AcceptChanges();
            return dsxml;
        }
        private DataSet f_insert_loai2(DataSet vds, DataSet dsxml, string idnhomxn, string m_field)
        {
            DataRow[] ar = vds.Tables[0].Select("idnhom in (" + idnhomxn.ToString() + ")");
            foreach (DataRow r in ar)
            {
                vsql = "loai=2 and tenkp='" + r["loaivp"].ToString() + "'";
                r1 = getrowbyid(dsxml.Tables[0], vsql);
                if (r1 == null)
                {
                    r2 = dsxml.Tables[0].NewRow();
                    r2["stt"] = 2;
                    r2["loai"] = 2;
                    r2["tenkp"] = r["loaivp"].ToString();
                    r2[m_field] = "DV";
                    r2[m_field + "_soluong"] = r["soluong"].ToString();
                    r2[m_field + "_sotien"] = r["sotien"].ToString();
                    dsxml.Tables[0].Rows.Add(r2);
                }
                else
                {
                    r1[m_field + "_soluong"] = decimal.Parse(r1[m_field + "_soluong"].ToString()) + decimal.Parse(r["soluong"].ToString());
                    r1[m_field + "_sotien"] = decimal.Parse(r1[m_field + "_sotien"].ToString()) + decimal.Parse(r["sotien"].ToString());
                }
            }
            dsxml.AcceptChanges();
            return dsxml;
        }
        private DataSet f_insert_loai3(DataSet vds, DataSet dsxml, string idloaikb, string idnhomxn, string m_field)
        {
            DataRow[] ar = vds.Tables[0].Select("idnhom not in (" + idnhomxn.ToString() + ") and idloai not in(" + idloaikb.ToString() + ")");
            foreach (DataRow r in ar)
            {
                vsql = "loai=3 and tenkp='" + r["loaivp"].ToString() + "'";
                r1 = getrowbyid(dsxml.Tables[0], vsql);
                if (r1 == null)
                {
                    r2 = dsxml.Tables[0].NewRow();
                    r2["stt"] = 3;
                    r2["loai"] = 3;
                    r2["tenkp"] = r["loaivp"].ToString();
                    r2[m_field] = "DV";
                    r2[m_field + "_soluong"] = r["soluong"].ToString();
                    r2[m_field + "_sotien"] = r["sotien"].ToString();
                    dsxml.Tables[0].Rows.Add(r2);
                }
                else
                {
                    r1[m_field + "_soluong"] = decimal.Parse(r1[m_field + "_soluong"].ToString()) + decimal.Parse(r["soluong"].ToString());
                    r1[m_field + "_sotien"] = decimal.Parse(r1[m_field + "_sotien"].ToString()) + decimal.Parse(r["sotien"].ToString());
                }
            }
            dsxml.AcceptChanges();
            return dsxml;
        }
        #endregion

        #region Load Mau bao cao
        public void f_Load_DmMaubaocao(ComboBox cbmaubaocao,string maloai)
        {
            try
            {
                sql = " select filereport as ma, ten from " + user + ".bhyt_maubaocao where maloai='" + maloai + "'";
                cbmaubaocao.DataSource = get_data(sql).Tables[0];
            }
            catch
            {
            }
        }
        #endregion

        #region Bảng chuyển Font
        public void Taobangma_chuyen()
        {
            string s_abc = "", s_uni = "", s_vni = "";
            string[] s_array;
            s_abc = "a+¸+µ+¶+·+¹+¨+¾+»+¼+½+¨+©+Ê+Ç+È+Ë+b+c+d+®+e+Ð+Ì+Î+Ï+Ñ+ª+Õ+Ò+Ó+Ô+Ö+i+Ý+×+Ø+Ü+Þ+k+l+m+n+g+h+u+ó+ï+ñ+ò+ô+­+ø+õ+ö+÷+ù+o+«+è+å+æ+ç+é+¬+í+ê+ë+ì+î+y+ý+ú+û+ü+þ+p+v+t+ã+ß+á+â+ä";
            s_vni = "a+aù+aø+aû+aõ+aï+aê+aé+aè+aú+aü+aê+aâ+aá+aà+aå+aä+b+c+d+ñ+e+eù+eø+eû+eõ+eï+eâ+eá+eà+eå+eã+eä+i+í+ì+æ+ó+ò+k+l+m+n+g+h+u+uù+uø+uû+uõ+uï+ö+öù+öø+öû+öõ+öï+o+oâ+oá+oà+oå+oã+oä+ô+ôù+ôø+ôû+ôõ+ôï+y+yù+yø+yû+yõ+î+p+v+t+où+oø+oû+oõ+oï";
            s_uni = "a+á+à+ả+ã+ạ+ă+ắ+ằ+ẳ+ẵ+ă+â+ấ+ầ+ẩ+ậ+b+c+d+đ+e+é+è+ẻ+ẽ+ẹ+ê+ế+ề+ể+ễ+ệ+i+í+ì+ỉ+ĩ+ị+k+l+m+n+g+h+u+ú+ù+ủ+ũ+ụ+ư+ứ+ừ+ử+ữ+ự+o+ô+ố+ồ+ổ+ỗ+ộ+ơ+ớ+ờ+ở+ỡ+ợ+y+ý+ỳ+ỷ+ỹ+ỵ+p+v+t+ó+ò+ỏ+õ+ọ";
            DataSet ds = new DataSet();
            DataTable tmp = new DataTable();
            tmp.Columns.Add("id", typeof(int));
            tmp.Columns.Add("ten", typeof(string));
            ds.Tables.Add(tmp);
            DataSet dsABC = new DataSet();
            DataSet dsUNI = new DataSet();
            DataSet dsVNI = new DataSet();
            DataRow dr;
            dsABC = ds.Clone();
            s_array = s_abc.Split('+');
            if (s_array.Length > 0)
            {
                for (int i = 0; i < s_array.Length; i++)
                {
                    dr = dsABC.Tables[0].NewRow();
                    dr["id"] = i + 1;
                    dr["ten"] = s_array[i].ToString();
                    dsABC.Tables[0].Rows.Add(dr);
                }
            }
            dsABC.AcceptChanges();
            dsABC.WriteXml("..//FontXml//abc.xml", XmlWriteMode.WriteSchema);
            // tap uni.xml
            dsUNI = ds.Clone();
            s_array = s_uni.Split('+');
            if (s_array.Length > 0)
            {
                for (int i = 0; i < s_array.Length; i++)
                {
                    dr = dsUNI.Tables[0].NewRow();
                    dr["id"] = i + 1;
                    dr["ten"] = s_array[i].ToString();
                    dsUNI.Tables[0].Rows.Add(dr);
                }
            }
            dsUNI.AcceptChanges();
            dsUNI.WriteXml("..//FontXml//unicode.xml", XmlWriteMode.WriteSchema);
            // tao vni.xml
            dsVNI = ds.Clone();
            s_array = s_vni.Split('+');
            if (s_array.Length > 0)
            {
                for (int i = 0; i < s_array.Length; i++)
                {
                    dr = dsVNI.Tables[0].NewRow();
                    dr["id"] = i + 1;
                    dr["ten"] = s_array[i].ToString();
                    dsVNI.Tables[0].Rows.Add(dr);
                }
            }
            dsVNI.AcceptChanges();
            dsVNI.WriteXml("..//FontXml//vni.xml", XmlWriteMode.WriteSchema);
        }
        public string Unicode_TCVN3_ABC(string sUnicode)
        {
            DataSet dsUnicode = new DataSet();
            DataSet dsABC = new DataSet();
            if (!System.IO.File.Exists("..//FontXml//unicode.xml") || !System.IO.File.Exists("..//FontXml//abc.xml"))
            {
                Taobangma_chuyen();
            }
            dsUnicode.ReadXml("..//FontXml//unicode.xml", XmlReadMode.ReadSchema);
            dsABC.ReadXml("..//FontXml//abc.xml", XmlReadMode.ReadSchema);
            string s_Uni = sUnicode.ToLower().Trim();
            string stmp = "", s_ABC = "";
            int i_id;
            DataRow r, r1;
            for (int i = 0; i < s_Uni.Length; i++)
            {
                if (s_Uni[i].ToString() != " ")
                {
                    stmp = " ten ='" + s_Uni[i].ToString() + "'";
                    r = getrowbyid(dsUnicode.Tables[0], stmp);
                    if (r != null)
                    {
                        i_id = int.Parse(r["id"].ToString());
                        stmp = "id=" + i_id;
                        r1 = getrowbyid(dsABC.Tables[0], stmp);
                        if (r1 != null) s_ABC += r1["ten"].ToString();
                        else s_ABC += s_Uni[i].ToString();
                    }
                    else s_ABC += s_Uni[i].ToString();
                }
                else s_ABC += " ";
            }
            return s_ABC;
        }
        public string Unicode_VNI(string sUnicode)
        {
            DataSet dsUnicode = new DataSet();
            DataSet dsVNI = new DataSet();
            if (!System.IO.File.Exists("..//FontXml//unicode.xml") || !System.IO.File.Exists("..//FontXml//vni.xml"))
            {
                Taobangma_chuyen();
            }
            dsUnicode.ReadXml("..//FontXml//unicode.xml", XmlReadMode.ReadSchema);
            dsVNI.ReadXml("..//FontXml//vni.xml", XmlReadMode.ReadSchema);
            string s_Uni = sUnicode.ToLower().Trim();
            string stmp = "", s_VNI = "";
            int i_id;
            DataRow r, r1;
            for (int i = 0; i < s_Uni.Length; i++)
            {
                if (s_Uni[i].ToString() != " ")
                {
                    stmp = " ten ='" + s_Uni[i].ToString() + "'";
                    r = getrowbyid(dsUnicode.Tables[0], stmp);
                    if (r != null)
                    {
                        i_id = int.Parse(r["id"].ToString());
                        stmp = "id=" + i_id;
                        r1 = getrowbyid(dsVNI.Tables[0], stmp);
                        if (r1 != null) s_VNI += r1["ten"].ToString();
                        else s_VNI += s_Uni[i].ToString();
                    }
                    else s_VNI += s_Uni[i].ToString();
                }
                else s_VNI += " ";
            }
            return s_VNI;
        }
        public string VNI_TCVN3_ABC(string sVNI)
        {
            DataSet dsVNI = new DataSet();
            DataSet dsABC = new DataSet();
            if (!System.IO.File.Exists("..//FontXml//vni.xml") || !System.IO.File.Exists("..//FontXml//abc.xml"))
            {
                Taobangma_chuyen();
            }
            dsVNI.ReadXml("..//FontXml//vni.xml", XmlReadMode.ReadSchema);
            dsABC.ReadXml("..//FontXml//abc.xml", XmlReadMode.ReadSchema);
            string s_VNI = sVNI.ToLower().Trim();
            string stmp = "", s_ABC = "";
            int i_id;
            DataRow r, r1;
            for (int i = 0; i < s_VNI.Length; i++)
            {
                if (s_VNI[i].ToString() != " ")
                {
                    stmp = " ten ='" + s_VNI[i].ToString() + "'";
                    r = getrowbyid(dsVNI.Tables[0], stmp);
                    if (r != null)
                    {
                        i_id = int.Parse(r["id"].ToString());
                        stmp = "id=" + i_id;
                        r1 = getrowbyid(dsABC.Tables[0], stmp);
                        if (r1 != null) s_ABC += r1["ten"].ToString();
                        else s_ABC += s_VNI[i].ToString();
                    }
                    else s_ABC += s_VNI[i].ToString();
                }
                else s_ABC += " ";
            }
            return s_ABC;
        }
        public string TCVN3_ABC_VNI(string sABC)
        {
            DataSet dsVNI = new DataSet();
            DataSet dsABC = new DataSet();
            if (!System.IO.File.Exists("..//FontXml//vni.xml") || !System.IO.File.Exists("..//FontXml//abc.xml"))
            {
                Taobangma_chuyen();
            }
            dsVNI.ReadXml("..//FontXml//vni.xml", XmlReadMode.ReadSchema);
            dsABC.ReadXml("..//FontXml//abc.xml", XmlReadMode.ReadSchema);
            string s_ABC = sABC.ToLower().Trim();
            string stmp = "", s_VNI = "";
            int i_id;
            DataRow r, r1;
            for (int i = 0; i < s_ABC.Length; i++)
            {
                if (s_ABC[i].ToString() != " ")
                {
                    stmp = " ten ='" + s_ABC[i].ToString() + "'";
                    r = getrowbyid(dsABC.Tables[0], stmp);
                    if (r != null)
                    {
                        i_id = int.Parse(r["id"].ToString());
                        stmp = "id=" + i_id;
                        r1 = getrowbyid(dsVNI.Tables[0], stmp);
                        if (r1 != null) s_VNI += r1["ten"].ToString();
                        else s_VNI += s_ABC[i].ToString();
                    }
                    else s_VNI += s_ABC[i].ToString();
                }
                else s_VNI += " ";
            }
            return s_VNI;
        }
        public string TCVN3_ABC_Unicode(string sABC)
        {
            DataSet dsUnicode = new DataSet();
            DataSet dsABC = new DataSet();
            if (!System.IO.File.Exists("..//FontXml//unicode.xml") || !System.IO.File.Exists("..//FontXml//abc.xml"))
            {
                Taobangma_chuyen();
            }
            dsUnicode.ReadXml("..//FontXml//unicode.xml", XmlReadMode.ReadSchema);
            dsABC.ReadXml("..//FontXml//abc.xml", XmlReadMode.ReadSchema);
            string s_ABC = sABC.ToLower().Trim();
            string stmp = "", s_Uni = "";
            int i_id;
            DataRow r, r1;
            for (int i = 0; i < s_ABC.Length; i++)
            {
                if (s_ABC[i].ToString() != " ")
                {
                    stmp = " ten ='" + s_ABC[i].ToString() + "'";
                    r = getrowbyid(dsABC.Tables[0], stmp);
                    if (r != null)
                    {
                        i_id = int.Parse(r["id"].ToString());
                        stmp = "id=" + i_id;
                        r1 = getrowbyid(dsUnicode.Tables[0], stmp);
                        if (r1 != null) s_Uni += r1["ten"].ToString();
                        else s_Uni += s_ABC[i].ToString();
                    }
                    else s_Uni += s_ABC[i].ToString();
                }
                else s_Uni += " ";
            }
            return s_Uni;
        }
        #endregion
    }
}