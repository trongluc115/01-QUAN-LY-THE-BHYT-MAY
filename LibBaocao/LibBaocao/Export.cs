using System;
using System.Data;
namespace LibBaocao
{
    public class Export
    {
        private DataColumn dc;
        private DataSet ds;
        private DataTable dt;
        private string sql, sql1;
        public Export()
        {
        }

        private void tongcong(LibBaocao.AccessData m, string exp, string stt, int k)
        {
            Decimal l_tong = 0;
            DataRow[] r1 = ds.Tables[0].Select(exp);
            Int16 iRec = Convert.ToInt16(r1.Length);
            for (int j = k; j < ds.Tables[0].Columns.Count; j++)
            {
                l_tong = 0;
                for (Int16 i = 0; i < iRec; i++) l_tong += Decimal.Parse(r1[i][j].ToString());
                m.updrec_02(ds.Tables[0], stt, j, l_tong);
            }
        }

        private void tongcong(LibBaocao.AccessData m, string exp, int ma, int k)
        {
            Decimal l_tong = 0;
            DataRow[] r1 = ds.Tables[0].Select(exp);
            Int16 iRec = Convert.ToInt16(r1.Length);
            for (int j = k; j < ds.Tables[0].Columns.Count; j++)
            {
                l_tong = 0;
                for (Int16 i = 0; i < iRec; i++) l_tong += Decimal.Parse(r1[i][j].ToString());
                m.updrec_145(ds.Tables[0], ma, j, l_tong);
            }
        }

        public DataSet bieu_02(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            ds = new DataSet();
            string user = m.user;
            DataRow[] dr;
            DataRow r1, r2;
            Int64 c02, c03, c04, c05, c06, c07, ma;
            ds = m.get_data("select * from " + user + ".dm_02 where ma<2 order by ma");
            DataSet ds1 = new DataSet();
            ds1 = m.get_data("select * from " + user + ".btdkp_bv order by makp");
            if (benhan)
            {
                sql = "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,3 as loaiba,";
                sql += "a.ngay as ngayrv,c.makpbo as ma,c.tenkp,d.mien";
                sql += " from xxx.benhanpk a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " union all ";
                sql += "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,4 as loaiba,";
                sql += "a.ngayrv as ngayrv,c.makp as ma,c.tenkp,d.mien";
                sql += " from xxx.benhancc a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";

                sql1 = "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,2 as loaiba,";
                sql1 += "a.ngayrv as ngayrv,c.makpbo as ma,c.tenkp,d.mien";
                sql1 += " from " + user + ".benhanngtr a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql1 += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql1 += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";

                DataSet tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                tmp.Merge(m.get_data(sql1));
                foreach (DataRow r in tmp.Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], int.Parse(r["ma"].ToString()), r["tenkp"].ToString(), 9);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c02 = (r["madoituong"].ToString() == "1") ? 1 : 0;
                        c03 = (r["madoituong"].ToString() != "1" && r["mien"].ToString() == "0") ? 1 : 0;
                        c04 = (c02 + c03 == 0) ? 1 : 0;
                        c05 = (r["nhantu"].ToString() == "1") ? 1 : 0;
                        c06 = (r["ttlucrv"].ToString() == "5") ? 1 : 0;
                        c07 = (r["ttlucrv"].ToString() == "6") ? 1 : 0;
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + 1;
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + c02;
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + c03;
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + c04;
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + c05;
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + c06;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + c07;
                        if (r["loaiba"].ToString() == "2")
                            dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + 1;
                    }
                }
                //ngaydt
                sql = "SELECT b.makpbo as makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + " ";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a,"+m.user+".btdkp_bv b where  a.makp=b.makp and ";
                sql += " (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') ";
                sql += "or " + m.for_ngay("a.ngayrv", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')) ";
                sql += " and a.makp is not null ";
                sql += "group by b.makpbo";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                sql = "SELECT b.makpbo as makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + "";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a,"+m.user+".btdkp_bv b where  a.makp=b.makp and ";
                sql += " " + m.for_num_ngay("a.ngay") + "<" + m.for_num_ngay(s_tu1) + "";
                sql += " and " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + "";
                sql += " and a.makp is not null ";
                sql += "group by b.makpbo";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                sql = "SELECT b.makpbo as makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + "";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a,"+m.user+".btdkp_bv b where a.makp=b.makp and ";
                sql += " " + m.for_num_ngay("a.ngay") + "<=" + m.for_num_ngay(s_den) + "";
                sql += " and a.ngayrv is null ";
                sql += " and a.makp is not null ";
                sql += "group by b.makpbo";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                tongcong(m,"ma>1", "A", 3);
            }
            #region 29102004

            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06,sum(a.c07) as c07,sum(a.c08) as c08,";
                sql += "sum(a.c09) as c09";
                sql += " from " + m.user + ".bieu_02 a where " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r1 = m.getrowbyid(ds1.Tables[0], "makp='" + ma.ToString().PadLeft(2, '0') + "'");
                        if (r1 != null) m.updrec_bieu(ds.Tables[0], int.Parse(r1["makp"].ToString()), r1["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + Decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                    }
                }
            }

            #endregion
            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09=0");
            return ds;
        }

        public DataSet bieu_02_bv(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            ds = new DataSet();
            string user = m.user;
            DataRow[] dr;
            DataRow r1, r2;
            Int64 c02, c03, c04, c05, c06, c07, ma;
            ds = m.get_data("select * from " + user + ".dm_02 where ma<2 order by ma");
            DataSet ds1 = new DataSet();
            ds1 = m.get_data("select * from " + user + ".btdkp_bv order by makp");
            if (benhan)
            {
                sql = "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,3 as loaiba,";
                sql += "a.ngay as ngayrv,c.makp as ma,c.tenkp,d.mien";
                sql += " from xxx.benhanpk a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " union all ";
                sql += "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,4 as loaiba,";
                sql += "a.ngayrv as ngayrv,c.makp as ma,c.tenkp,d.mien";
                sql += " from xxx.benhancc a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";

                sql1 = "SELECT a.ngay as ngayvv,a.madoituong,a.nhantu,a.ttlucrv,2 as loaiba,";
                sql1 += "a.ngayrv as ngayrv,c.makp as ma,c.tenkp,d.mien";
                sql1 += " from " + user + ".benhanngtr a," + user + ".btdkp_bv c," + user + ".doituong d ";
                sql1 += " where a.makp=c.makp and a.madoituong=d.madoituong ";
                sql1 += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";

                DataSet tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                tmp.Merge(m.get_data(sql1));
                foreach (DataRow r in tmp.Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], int.Parse(r["ma"].ToString()), r["tenkp"].ToString(), 9);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c02 = (r["madoituong"].ToString() == "1") ? 1 : 0;
                        c03 = (r["madoituong"].ToString() != "1" && r["mien"].ToString() == "0") ? 1 : 0;
                        c04 = (c02 + c03 == 0) ? 1 : 0;
                        c05 = (r["nhantu"].ToString() == "1") ? 1 : 0;
                        c06 = (r["ttlucrv"].ToString() == "5") ? 1 : 0;
                        c07 = (r["ttlucrv"].ToString() == "6") ? 1 : 0;
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + 1;
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + c02;
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + c03;
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + c04;
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + c05;
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + c06;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + c07;
                        if (r["loaiba"].ToString() == "2")
                            dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + 1;
                    }
                }
                //ngaydt
                sql = "SELECT a.makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + " ";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a where  ";
                sql += " (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') ";
                sql += "or " + m.for_ngay("a.ngayrv", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')) ";
                sql += " and a.makp is not null ";
                sql += "group by a.makp";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                sql = "SELECT a.makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + "";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a where  ";
                sql += " " + m.for_num_ngay("a.ngay") + "<" + m.for_num_ngay(s_tu1) + "";
                sql += " and " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + "";
                sql += " and a.makp is not null ";
                sql += "group by a.makp";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                sql = "SELECT a.makp,";
                sql += "sum(case when a.ngayrv is null or " + m.for_num_ngay("a.ngayrv") + ">" + m.for_num_ngay(s_den) + " ";
                sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                sql += "case when " + m.for_num_ngay("a.ngay") + ">" + m.for_num_ngay(s_tu1) + "";
                sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+1) as c15 ";
                sql += "FROM " + m.user + ".benhanngtr a where ";
                sql += " " + m.for_num_ngay("a.ngay") + "<=" + m.for_num_ngay(s_den) + "";
                sql += " and a.ngayrv is null ";
                sql += " and a.makp is not null ";
                sql += "group by a.makp";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null)
                            m.updrec_bieu(ds.Tables[0], int.Parse(r2["makp"].ToString()), r2["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
                tongcong(m,"ma>1", "A", 3);
            }
            #region 29102004

            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06,sum(a.c07) as c07,sum(a.c08) as c08,";
                sql += "sum(a.c09) as c09";
                sql += " from " + m.user + ".bieu_02 a where " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r1 = m.getrowbyid(ds1.Tables[0], "makp='" + ma.ToString().PadLeft(2, '0') + "'");
                        if (r1 != null) m.updrec_bieu(ds.Tables[0], int.Parse(r1["makp"].ToString()), r1["tenkp"].ToString(), 9);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + Decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                    }
                }
            }

            #endregion
            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09=0");
            return ds;
        }

        public DataSet bieu_031(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            DataRow[] dr;
            DataRow r1, r2;
            Int64 c08, c09, ma, i_makp;
            DataColumn dc;
            string user = m.user;
            ds = m.get_data("select * from " + user + "." + s_table + " order by ma");
            m.delrec(ds.Tables[0], "ma>3");
            ds.AcceptChanges();
            dc = new DataColumn();
            dc.ColumnName = "C12";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C13";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C14";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C15";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                r["c12"] = 0; r["c13"] = 0; r["c14"] = 0; r["c15"] = 0;
            }
            DataSet ds1 = new DataSet();
            ds1 = m.get_data("select * from " + user + ".btdkp order by makp");
            if (benhan)
            {
                sql = "SELECT a.khoachuyen,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngayvv,b.ttlucrk as ttlucrv,c.namsinh,";
                sql += "to_char(b.ngay,'dd/mm/yyyy hh24:mi') as ngayrv,d.makpbo as ma,c.phai,d.tenkp";
                sql += " FROM " + user + ".nhapkhoa a," + user + ".xuatkhoa b," + user + ".BTDBN c," + user + ".btdkp_bv d," + user + ".benhandt e ";
                sql += " where  a.id=b.id and a.mabn=c.mabn and a.makp=d.makp and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " and a.maba<20 and b.ttlucrk=7 and a.maql=e.maql and e.loaiba=1";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["ma"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c08 = (DateTime.Now.Year - int.Parse(r["namsinh"].ToString()) < 15) ? 1 : 0;
                        if (m.songay(m.StringToDate(r["ngayrv"].ToString().Substring(0, 10)), m.StringToDate(r["ngayvv"].ToString().Substring(0, 10)), 0) == 1 && int.Parse(r["ngayrv"].ToString().Substring(11, 2)) < int.Parse(r["ngayvv"].ToString().Substring(11, 2))) c09 = 1;
                        else c09 = (r["ngayvv"].ToString().Substring(0, 10) == r["ngayrv"].ToString().Substring(0, 10)) ? 1 : 0;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + 1;
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + c08;
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                    }
                }
                //phai=1
                sql = "SELECT a.khoachuyen,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngayvv,b.ttlucrk as ttlucrv,c.namsinh,";
                sql += "to_char(b.ngay,'dd/mm/yyyy hh24:mi') as ngayrv,d.makpbo as ma,c.phai";
                sql += " FROM " + user + ".nhapkhoa a," + user + ".xuatkhoa b," + user + ".BTDBN c," + user + ".btdkp_bv d," + user + ".benhandt e ";
                sql += " where  a.id=b.id and a.mabn=c.mabn and a.makp=d.makp and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " and a.maba<20 and b.ttlucrk=7 and a.maql=e.maql and e.loaiba=1 and c.phai=1";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["ma"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c08 = (DateTime.Now.Year - int.Parse(r["namsinh"].ToString()) < 15) ? 1 : 0;
                        if (m.songay(m.StringToDate(r["ngayrv"].ToString().Substring(0, 10)), m.StringToDate(r["ngayvv"].ToString().Substring(0, 10)), 0) == 1 && int.Parse(r["ngayrv"].ToString().Substring(11, 2)) < int.Parse(r["ngayvv"].ToString().Substring(11, 2))) c09 = 1;
                        else c09 = (r["ngayvv"].ToString().Substring(0, 10) == r["ngayrv"].ToString().Substring(0, 10)) ? 1 : 0;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + 1;
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + c08;
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                    }
                }
                //dau ky + cuoi ky
                sql = "SELECT c.MAKPbo as ma,sum(case when (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy') and (b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">=to_date('" + s_tu1 + "','dd/mm/yyyy'))) then 1 else 0 end) as C03,";
                sql += "sum(case when a.khoachuyen='01' and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end) as C04,";
                sql += "sum(case when a.khoachuyen<>'01' and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C05,";
                sql += "sum(case when a.khoachuyen='01' and to_number(to_char(now(),'yyyy'))-to_number(d.namsinh)<15 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C041,";
                sql += "sum(case when a.khoachuyen<>'01' and to_number(to_char(now(),'yyyy'))-to_number(d.namsinh)<15 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C042,";
                sql += "sum(case when a.khoachuyen='01' and e.nhantu=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C051,";
                sql += "sum(case when a.khoachuyen<>'01' and e.nhantu=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C052,";
                sql += "sum(case when a.khoachuyen='01' and e.madoituong=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C101,";
                sql += "sum(case when a.khoachuyen<>'01' and e.madoituong=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C102,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=1 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C06,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=2 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C07,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=3 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C08,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=4 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C09,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=5 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C10,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=6 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C11,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=7 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C12,";
                sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C13";
                sql += " FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".btdkp_bv c on a.makp=c.makp inner join " + user + ".btdbn d on a.mabn=d.mabn inner join " + user + ".benhandt e on a.maql=e.maql ";
                sql += " WHERE a.maba<20 and e.loaiba=1 and length(d.namsinh)=4";

                foreach (DataRow r in m.get_data(sql + "GROUP BY c.makpbo ORDER BY c.makpbo").Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select tenkp from " + user + ".btdkp where makp='" + r["ma"].ToString() + "'").Tables[0].Rows[0][0].ToString(), 15);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c02"] = Decimal.Parse(r["c03"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c041"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c051"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c101"].ToString());
                        dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c13"] = Decimal.Parse(dr[0]["c13"].ToString()) + Decimal.Parse(r["c042"].ToString());
                        dr[0]["c14"] = Decimal.Parse(dr[0]["c14"].ToString()) + Decimal.Parse(r["c052"].ToString());
                        dr[0]["c15"] = Decimal.Parse(dr[0]["c15"].ToString()) + Decimal.Parse(r["c102"].ToString());
                        dr[0]["c11"] = Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) + Decimal.Parse(r["c05"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c10"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                    }
                }
                //phai=1
                sql += " and d.phai=1 ";
                foreach (DataRow r in m.get_data(sql + "GROUP BY c.makpbo ORDER BY c.makpbo").Tables[0].Rows)
                {
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select tenkp from " + user + ".btdkp where makp='" + r["ma"].ToString() + "'").Tables[0].Rows[0][0].ToString(), 15);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c02"] = decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c041"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c051"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c101"].ToString());
                        dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c13"] = Decimal.Parse(dr[0]["c13"].ToString()) + Decimal.Parse(r["c042"].ToString());
                        dr[0]["c14"] = Decimal.Parse(dr[0]["c14"].ToString()) + Decimal.Parse(r["c052"].ToString());
                        dr[0]["c15"] = Decimal.Parse(dr[0]["c15"].ToString()) + Decimal.Parse(r["c102"].ToString());
                        dr[0]["c11"] = decimal.Parse(dr[0]["c11"].ToString()) + Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) + Decimal.Parse(r["c05"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c10"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                    }
                }
                //ngaydt linh 6/12/2007
                sql = "select e.makpbo as makp,sum(";
                //vao<tu and (den<ra or ra is null) => den-tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+1 else";
                //vao<tu and (ra<=den)              => ra - tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+ case when b.ttlucrk in (5,6,7) then 0 else 1 end else ";
                //vao>=tu and (den<ra or ra is null)=> den-vao
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+1 else ";
                //vao>=tu and (ra<=den)             => ra - vao
                sql += " case when to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')) + case when b.ttlucrk in(5,6,7) then 0 else 1 end end end end end) c15";
                //nu
                sql += ",sum(case when d.phai=1 then ";
                //vao<tu and (den<ra or ra is null) => den-tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+1 else";
                //vao<tu and (ra<=den)              => ra - tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+ case when b.ttlucrk in(5,6,7) then 0 else 1 end else ";
                //vao>=tu and (den<ra or ra is null)=> den-vao
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+1 else ";
                //vao>=tu and (ra<=den)             => ra - vao
                sql += " case when to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+ case when b.ttlucrk in (5,6,7) then 0 else 1 end end end end end else 0 end) c15_nu";
                sql += " from " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join " + user + ".btdbn d on a.mabn=d.mabn ";
                sql += " inner join " + user + ".btdkp_bv e on a.makp=e.makp where a.maba<20 and c.loaiba=1 and a.makp is not null ";
                sql += " and to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') <=to_date('" + s_den + "','dd/mm/yyyy') and to_date(to_char(coalesce(b.ngay,now),'dd/mm/yyyy'),'dd/mm/yyyy') >=to_date('" + s_tu + "','dd/mm/yyyy')";
                sql += " group by e.makpbo";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                    //nu
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15_nu"].ToString());
                }
                //ngaydt
                //sql = "SELECT d.makpbo as makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join "+user+".btdkp_bv d on a.makp=d.makp where  c.loaiba=1 and a.maba<20 ";
                //sql += "and (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += "or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy'))";
                //sql += " and a.makp is not null ";
                //sql += "group by d.makpbo";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT d.makpbo as makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join "+user+".btdkp_bv d on a.makp=d.makp where c.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += " and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and a.makp is not null ";
                //sql += "group by d.makpbo";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT d.makpbo as makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join "+user+".btdkp_bv d on a.makp=d.makp where c.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<=to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and b.ngay is null ";
                //sql += " and a.makp is not null ";
                //sql += "group by d.makpbo";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                ////ngaydt-nu
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".BTDBN c on a.mabn=c.mabn inner join " + user + ".benhandt d on a.maql=d.maql where  c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += "and (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += "or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy'))";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".BTDBN c on a.mabn=c.mabn inner join " + m.user + ".benhandt d on a.maql=d.maql where  c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += " and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".BTDBN c on a.mabn=c.mabn inner join " + m.user + ".benhandt d on a.mabn=d.mabn where c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<=to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and b.ngay is null ";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                int i_ma;
                foreach (DataRow r in m.get_data("select makpbo as makp,sum(kehoach) as kh from " + user + ".btdkp_bv group by makpbo order by makpbo").Tables[0].Rows)
                {
                    i_ma = int.Parse(r["makp"].ToString()) + 2;
                    r2 = m.getrowbyid(ds.Tables[0], "ma=" + i_ma);
                    if (r2 != null) r2["c01"] = r["kh"].ToString();
                }
                tongcong(m,"ma>2", "1", 3);
                r1 = m.getrowbyid(ds.Tables[0], "ma=1");
                if (r1 != null)
                {
                    r1["c12"] = 0; r1["c13"] = 0; r1["c14"] = 0; r1["c15"] = 0;
                }
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    r["c03"] = decimal.Parse(r["c03"].ToString()) + decimal.Parse(r["c12"].ToString());
                    r["c04"] = decimal.Parse(r["c04"].ToString()) + decimal.Parse(r["c13"].ToString());
                    r["c05"] = decimal.Parse(r["c05"].ToString()) + decimal.Parse(r["c14"].ToString());
                    r["c10"] = decimal.Parse(r["c10"].ToString()) + decimal.Parse(r["c15"].ToString());
                }
            }
            #region 29102004

            if (thongke)
            {
                sql = "select ma,sum(c01) as c01,sum(c02) as c02,sum(c03) as c03,sum(c04) as c04,";
                sql += "sum(c05) as c05,sum(c06) as c06,sum(c07) as c07,sum(c08) as c08,";
                sql += "sum(c09) as c09,sum(c10) as c10,sum(c11) as c11";
                sql += " from " + user + ".bieu_031 where " + m.for_ngay("ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    if (ma > 2)
                    {
                        i_makp = ma - 2;
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                        if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select b.tenkp from " + user + ".btdkp_bv a ," + user + ".btdkp b where a.makpbo=b.makp and a.makp='" + i_makp.ToString().PadLeft(2, '0') + "'").Tables[0].Rows[0][0].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        //dr[0]["c01"]=r["c01"].ToString();
                        //dr[0]["c02"]=Decimal.Parse(dr[0]["c02"].ToString())+Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c10"].ToString());
                        //dr[0]["c11"]=Decimal.Parse(dr[0]["c11"].ToString())+Decimal.Parse(r["c11"].ToString());
                    }
                }
                sql = "select id from " + user + ".bieu_031 where " + m.for_ngay("ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " order by ngay";
                ds1 = m.get_data(sql);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    long id1 = long.Parse(ds1.Tables[0].Rows[0][0].ToString());
                    long id2 = long.Parse(ds1.Tables[0].Rows[ds1.Tables[0].Rows.Count - 1][0].ToString());
                    foreach (DataRow r in m.get_data("select ma,c01,c02 from " + user + ".bieu_031 where id=" + id1).Tables[0].Rows)
                    {
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + int.Parse(r["ma"].ToString()));
                        if (r1 != null)
                        {
                            r1["c01"] = decimal.Parse(r1["c01"].ToString()) + decimal.Parse(r["c01"].ToString());
                            r1["c02"] = decimal.Parse(r1["c02"].ToString()) + decimal.Parse(r["c02"].ToString());
                        }
                    }
                    foreach (DataRow r in m.get_data("select ma,c11 from " + user + ".bieu_031 where id=" + id2).Tables[0].Rows)
                    {
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + int.Parse(r["ma"].ToString()));
                        if (r1 != null) r1["c11"] = decimal.Parse(r1["c11"].ToString()) + decimal.Parse(r["c11"].ToString());
                    }
                }
            }
            #endregion
            if (phatsinh) m.delrec(ds.Tables[0], "c02+c03+c04+c05+c06+c07+c08+c09+c10+c11=0");
            ds.AcceptChanges();
            return ds;
        }

        public DataSet bieu_031_bv(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            DataRow[] dr;
            DataRow r1, r2;
            Int64 c08, c09, ma, i_makp;
            DataColumn dc;
            string user = m.user;
            ds = m.get_data("select * from " + user+"."+s_table + " order by ma");
            m.delrec(ds.Tables[0], "ma>3");
            ds.AcceptChanges();
            dc = new DataColumn();
            dc.ColumnName = "C12";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C13";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C14";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C15";
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                r["c12"] = 0; r["c13"] = 0; r["c14"] = 0; r["c15"] = 0;
            }
            DataSet ds1 = new DataSet();
            ds1 = m.get_data("select * from " + user + ".btdkp_bv order by makp");
            if (benhan)
            {
                sql = "SELECT a.khoachuyen,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngayvv,b.ttlucrk as ttlucrv,c.namsinh,";
                sql += "to_char(b.ngay,'dd/mm/yyyy hh24:mi') as ngayrv,d.makp as ma,c.phai,d.tenkp";
                sql += " FROM " + user + ".nhapkhoa a," + user + ".xuatkhoa b," + user + ".BTDBN c," + user + ".btdkp_bv d," + user + ".benhandt e ";
                sql += " where  a.id=b.id and a.mabn=c.mabn and a.makp=d.makp and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " and a.maba<20 and b.ttlucrk=7 and a.maql=e.maql and e.loaiba=1";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["ma"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c08 = (DateTime.Now.Year - int.Parse(r["namsinh"].ToString()) < 15) ? 1 : 0;
                        if (m.songay(m.StringToDate(r["ngayrv"].ToString().Substring(0, 10)), m.StringToDate(r["ngayvv"].ToString().Substring(0, 10)), 0) == 1 && int.Parse(r["ngayrv"].ToString().Substring(11, 2)) < int.Parse(r["ngayvv"].ToString().Substring(11, 2))) c09 = 1;
                        else c09 = (r["ngayvv"].ToString().Substring(0, 10) == r["ngayrv"].ToString().Substring(0, 10)) ? 1 : 0;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + 1;
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + c08;
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                    }
                }
                //phai=1
                sql = "SELECT a.khoachuyen,to_char(a.ngay,'dd/mm/yyyy hh24:mi') as ngayvv,b.ttlucrk as ttlucrv,c.namsinh,";
                sql += "to_char(b.ngay,'dd/mm/yyyy hh24:mi') as ngayrv,d.makp as ma,c.phai";
                sql += " FROM " + user + ".nhapkhoa a," + user + ".xuatkhoa b," +  user + ".BTDBN c," + user + ".btdkp_bv d," + user + ".benhandt e ";
                sql += " where  a.id=b.id and a.mabn=c.mabn and a.makp=d.makp and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " and a.maba<20 and b.ttlucrk=7 and a.maql=e.maql and e.loaiba=1 and c.phai=1";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["ma"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c08 = (DateTime.Now.Year - int.Parse(r["namsinh"].ToString()) < 15) ? 1 : 0;
                        if (m.songay(m.StringToDate(r["ngayrv"].ToString().Substring(0, 10)), m.StringToDate(r["ngayvv"].ToString().Substring(0, 10)), 0) == 1 && int.Parse(r["ngayrv"].ToString().Substring(11, 2)) < int.Parse(r["ngayvv"].ToString().Substring(11, 2))) c09 = 1;
                        else c09 = (r["ngayvv"].ToString().Substring(0, 10) == r["ngayrv"].ToString().Substring(0, 10)) ? 1 : 0;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + 1;
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + c08;
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                    }
                }
                //dau ky + cuoi ky
                sql = "SELECT c.MAKP as ma,sum(case when (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy') and (b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">=to_date('" + s_tu1 + "','dd/mm/yyyy'))) then 1 else 0 end) as C03,";
                sql += "sum(case when a.khoachuyen='01' and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end) as C04,";
                sql += "sum(case when a.khoachuyen<>'01' and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C05,";
                sql += "sum(case when a.khoachuyen='01' and to_number(to_char(now(),'yyyy'))-to_number(d.namsinh)<15 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C041,";
                sql += "sum(case when a.khoachuyen<>'01' and to_number(to_char(now(),'yyyy'))-to_number(d.namsinh)<15 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C042,";
                sql += "sum(case when a.khoachuyen='01' and e.nhantu=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C051,";
                sql += "sum(case when a.khoachuyen<>'01' and e.nhantu=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C052,";
                sql += "sum(case when a.khoachuyen='01' and e.madoituong=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C101,";
                sql += "sum(case when a.khoachuyen<>'01' and e.madoituong=1 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " Between to_date('" + s_tu1 + "','dd/mm/yyyy') And to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C102,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=1 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C06,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=2 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C07,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=3 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C08,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=4 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C09,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=5 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C10,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=6 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C11,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=7 And " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end end)  as C12,";
                sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') then 1 else 0 end)  as C13";
                sql += " FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".btdkp_bv c on a.makp=c.makp inner join " + user + ".btdbn d on a.mabn=d.mabn inner join " + user + ".benhandt e on a.maql=e.maql ";
                sql += " WHERE a.maba<20 and e.loaiba=1 and length(d.namsinh)=4";
                foreach (DataRow r in m.get_data(sql + "GROUP BY c.makp ORDER BY c.makp").Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select tenkp from " + user + ".btdkp_bv where makp='" + r["ma"].ToString() + "'").Tables[0].Rows[0][0].ToString(), 15);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c02"] = Decimal.Parse(r["c03"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c041"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c051"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c101"].ToString());
                        dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c13"] = Decimal.Parse(dr[0]["c13"].ToString()) + Decimal.Parse(r["c042"].ToString());
                        dr[0]["c14"] = Decimal.Parse(dr[0]["c14"].ToString()) + Decimal.Parse(r["c052"].ToString());
                        dr[0]["c15"] = Decimal.Parse(dr[0]["c15"].ToString()) + Decimal.Parse(r["c102"].ToString());
                        dr[0]["c11"] = Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) + Decimal.Parse(r["c05"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c10"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                    }
                }
                //phai=1
                sql += " and d.phai=1 ";
                foreach (DataRow r in m.get_data(sql + "GROUP BY c.makp ORDER BY c.makp").Tables[0].Rows)
                {
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select tenkp from " + user + ".btdkp_bv where makp='" + r["ma"].ToString() + "'").Tables[0].Rows[0][0].ToString(), 15);
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        dr[0]["c02"] = decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c041"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c051"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c101"].ToString());
                        dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c13"] = Decimal.Parse(dr[0]["c13"].ToString()) + Decimal.Parse(r["c042"].ToString());
                        dr[0]["c14"] = Decimal.Parse(dr[0]["c14"].ToString()) + Decimal.Parse(r["c052"].ToString());
                        dr[0]["c15"] = Decimal.Parse(dr[0]["c15"].ToString()) + Decimal.Parse(r["c102"].ToString());
                        dr[0]["c11"] = decimal.Parse(dr[0]["c11"].ToString()) + Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) + Decimal.Parse(r["c05"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c10"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                    }
                }
                //ngaydt linh 6/12/2007
                sql = "select a.makp,sum(";
                //vao<tu and (den<ra or ra is null) => den-tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+1 else";
                //vao<tu and (ra<=den)              => ra - tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+ case when b.ttlucrk in (5,6,7) then 0 else 1 end else ";
                //vao>=tu and (den<ra or ra is null)=> den-vao
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+1 else ";
                //vao>=tu and (ra<=den)             => ra - vao
                sql += " case when to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')) + case when b.ttlucrk in(5,6,7) then 0 else 1 end end end end end) c15";
                //nu
                sql += ",sum(case when d.phai=1 then ";
                //vao<tu and (den<ra or ra is null) => den-tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+1 else";
                //vao<tu and (ra<=den)              => ra - tu
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')< to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date('" + s_tu + "','dd/mm/yyyy'))+ case when b.ttlucrk in(5,6,7) then 0 else 1 end else ";
                //vao>=tu and (den<ra or ra is null)=> den-vao
                sql += " case when to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and (to_date('" + s_den + "','dd/mm/yyyy')< to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') or b.ngay is null) then date_part('day',to_date('" + s_den + "','dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+1 else ";
                //vao>=tu and (ra<=den)             => ra - vao
                sql += " case when to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')>= to_date('" + s_tu + "','dd/mm/yyyy') ";
                sql += " and to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')<=to_date('" + s_den + "','dd/mm/yyyy') then date_part('day',to_date(to_char(b.ngay,'dd/mm/yyyy'),'dd/mm/yyyy')-to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy'))+ case when b.ttlucrk in (5,6,7) then 0 else 1 end end end end end else 0 end) c15_nu";
                sql += " from " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join " + user + ".btdbn d on a.mabn=d.mabn ";
                sql += " where a.maba<20 and c.loaiba=1 and a.makp is not null ";
                sql += " and to_date(to_char(a.ngay,'dd/mm/yyyy'),'dd/mm/yyyy') <=to_date('" + s_den + "','dd/mm/yyyy') and to_date(to_char(coalesce(b.ngay,now),'dd/mm/yyyy'),'dd/mm/yyyy') >=to_date('" + s_tu + "','dd/mm/yyyy')";
                sql += " group by a.makp";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["makp"].ToString()) + 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                    //nu
                    ma = 2;
                    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                    if (r1 == null)
                    {
                        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15_nu"].ToString());
                }
                //ngaydt
                //sql = "SELECT a.makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql where  c.loaiba=1 and a.maba<20 ";
                //sql += "and (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += "or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy'))";
                //sql += " and a.makp is not null ";
                //sql += "group by a.makp";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT a.makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql where c.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += " and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and a.makp is not null ";
                //sql += "group by a.makp";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT a.makp,";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql where c.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<=to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and b.ngay is null ";
                //sql += " and a.makp is not null ";
                //sql += "group by a.makp";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = int.Parse(r["makp"].ToString()) + 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0) dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                ////ngaydt-nu
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".BTDBN c on a.mabn=c.mabn inner join " + user + ".benhandt d on a.maql=d.maql where  c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += "and (" + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += "or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy'))";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".BTDBN c on a.mabn=c.mabn inner join " + m.user + ".benhandt d on a.maql=d.maql where  c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += " and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                //sql = "SELECT ";
                //sql += "sum(case when b.ngay is null or " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_den + "','dd/mm/yyyy') ";
                //sql += "then " + m.for_num_ngay(s_den) + " else " + m.for_num_ngay("b.ngay") + " end-";
                //sql += "case when " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + ">to_date('" + s_tu1 + "','dd/mm/yyyy')";
                //sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(s_tu1) + " end+to_number(case when a.khoachuyen='01' then 1 else 0 end))  as c15 ";
                //sql += "FROM " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " +  user + ".BTDBN c on a.mabn=c.mabn inner join " + m.user + ".benhandt d on a.mabn=d.mabn where c.phai=1 and d.loaiba=1 and a.maba<20 ";
                //sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + "<=to_date('" + s_den + "','dd/mm/yyyy')";
                //sql += " and b.ngay is null ";
                //sql += " and a.makp is not null ";
                //foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                //{
                //    ma = 2;
                //    r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                //    if (r1 == null)
                //    {
                //        r2 = m.getrowbyid(ds1.Tables[0], "makp='" + r["makp"].ToString() + "'");
                //        if (r2 != null) m.updrec_bieu(ds.Tables[0], ma, r2["tenkp"].ToString(), 15);
                //    }
                //    dr = ds.Tables[0].Select("ma=" + ma);
                //    if (dr.Length > 0 && r["c15"].ToString() != "") dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c15"].ToString());
                //}
                int i_ma;
                foreach (DataRow r in m.get_data("select makp,sum(kehoach) as kh from " + user + ".btdkp_bv group by makp order by makp").Tables[0].Rows)
                {
                    i_ma = int.Parse(r["makp"].ToString()) + 2;
                    r2 = m.getrowbyid(ds.Tables[0], "ma=" + i_ma);
                    if (r2 != null) r2["c01"] = r["kh"].ToString();
                }
                tongcong(m,"ma>2", "1", 3);
                r1 = m.getrowbyid(ds.Tables[0], "ma=1");
                if (r1 != null)
                {
                    r1["c12"] = 0; r1["c13"] = 0; r1["c14"] = 0; r1["c15"] = 0;
                }
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    r["c03"] = decimal.Parse(r["c03"].ToString()) + decimal.Parse(r["c12"].ToString());
                    r["c04"] = decimal.Parse(r["c04"].ToString()) + decimal.Parse(r["c13"].ToString());
                    r["c05"] = decimal.Parse(r["c05"].ToString()) + decimal.Parse(r["c14"].ToString());
                    r["c10"] = decimal.Parse(r["c10"].ToString()) + decimal.Parse(r["c15"].ToString());
                }
            }
            #region 29102004

            if (thongke)
            {
                sql = "select ma,sum(c01) as c01,sum(c02) as c02,sum(c03) as c03,sum(c04) as c04,";
                sql += "sum(c05) as c05,sum(c06) as c06,sum(c07) as c07,sum(c08) as c08,";
                sql += "sum(c09) as c09,sum(c10) as c10,sum(c11) as c11";
                sql += " from " + user + ".bieu_031 where " + m.for_ngay("ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    ma = int.Parse(r["ma"].ToString());
                    if (ma > 2)
                    {
                        i_makp = ma - 2;
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + ma);
                        if (r1 == null) m.updrec_bieu(ds.Tables[0], ma, m.get_data("select tenkp from " + m.user + ".btdkp_bv where makp='" + i_makp.ToString().PadLeft(2, '0') + "'").Tables[0].Rows[0][0].ToString(), 15);
                    }
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        //dr[0]["c01"]=r["c01"].ToString();
                        //dr[0]["c02"]=Decimal.Parse(dr[0]["c02"].ToString())+Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c10"].ToString());
                        //dr[0]["c11"]=Decimal.Parse(dr[0]["c11"].ToString())+Decimal.Parse(r["c11"].ToString());
                    }
                }
                sql = "select id from " + user + ".bieu_031 where " + m.for_ngay("ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " order by ngay";
                ds1 = m.get_data(sql);
                if (ds1.Tables[0].Rows.Count != 0)
                {
                    long id1 = long.Parse(ds1.Tables[0].Rows[0][0].ToString());
                    long id2 = long.Parse(ds1.Tables[0].Rows[ds1.Tables[0].Rows.Count - 1][0].ToString());
                    foreach (DataRow r in m.get_data("select ma,c01,c02 from " + user + ".bieu_031 where id=" + id1).Tables[0].Rows)
                    {
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + int.Parse(r["ma"].ToString()));
                        if (r1 != null)
                        {
                            r1["c01"] = decimal.Parse(r1["c01"].ToString()) + decimal.Parse(r["c01"].ToString());
                            r1["c02"] = decimal.Parse(r1["c02"].ToString()) + decimal.Parse(r["c02"].ToString());
                        }
                    }
                    foreach (DataRow r in m.get_data("select ma,c11 from " + user + ".bieu_031 where id=" + id2).Tables[0].Rows)
                    {
                        r1 = m.getrowbyid(ds.Tables[0], "ma=" + int.Parse(r["ma"].ToString()));
                        if (r1 != null) r1["c11"] = decimal.Parse(r1["c11"].ToString()) + decimal.Parse(r["c11"].ToString());
                    }
                }
            }
            #endregion
            if (phatsinh) m.delrec(ds.Tables[0], "c02+c03+c04+c05+c06+c07+c08+c09+c10+c11=0");
            ds.AcceptChanges();
            return ds;
        }

        public DataSet bieu_04(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            string user = m.user;
            DataRow[] dr;
            int c02, c03, c05, c06, c07, c09, c10, ma, so;
            ds = m.get_data("select * from " +user+"."+s_table + " order by ma");
            if (benhan)
            {
                sql = "SELECT a.mapt,b.loaipt,a.tinhhinh,a.taibien,a.tuvong";
                sql += " FROM xxx.PTTT a," + user + ".DMPTTT b where a.MAPT = b.MAPT";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                foreach (DataRow r in m.get_data_mmyy(sql, s_tu1, s_den, false).Tables[0].Rows)
                {
                    so = (r["mapt"].ToString().Substring(0, 1) == "P") ? 1 : 10;
                    ma = int.Parse(r["loaipt"].ToString()) + so;
                    dr = ds.Tables[0].Select("ma=" + ma);
                    if (dr.Length > 0)
                    {
                        c02 = (r["tinhhinh"].ToString() == "2") ? 1 : 0;
                        c03 = (r["tinhhinh"].ToString() != "2") ? 1 : 0;
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + 1;
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + c02;
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + c03;
                        if (r["taibien"].ToString() != "0")
                        {
                            c05 = (r["taibien"].ToString() == "2") ? 1 : 0;
                            c06 = (r["taibien"].ToString() == "3") ? 1 : 0;
                            c07 = (r["taibien"].ToString() != "2" || r["taibien"].ToString() != "3") ? 1 : 0;
                            dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + 1;
                            dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + c05;
                            dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + c06;
                            dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + c07;
                        }
                        if (r["tuvong"].ToString() != "0")
                        {
                            c09 = (r["tuvong"].ToString() == "1") ? 1 : 0;
                            c10 = (r["tuvong"].ToString() == "2") ? 1 : 0;
                            dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + 1;
                            dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                            dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + c10;
                        }
                    }
                }
                tongcong(m,"ma>1 and ma<10", "A", 3);
                tongcong(m,"ma>10", "B", 3);
            }

            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06,sum(a.c07) as c07,sum(a.c08) as c08,";
                sql += "sum(a.c09) as c09,sum(a.c10) as c10";
                sql += " from " + user + ".bieu_04 a," + user + ".dm_04 b where a.ma=b.ma ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("ma=" + int.Parse(r["ma"].ToString()));
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + Decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c10"].ToString());
                    }
                }
            }
            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09+c10=0");
            ds.AcceptChanges();
            return ds;
        }

        public DataSet bieu_04_khoa(LibBaocao.AccessData m, string s_makp, string s_tu, string s_tu1, string s_den, string s_table, int i_loaiba, bool time)
        {
            string user = m.user;
            string stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                s_tu1 = s_tu1 + " " + m.sGiobaocao;
                s_den = s_den + " " + m.sGiobaocao;
            }
            DataRow[] dr;
            int c02, c03, c05, c06, c07, c09, c10, ma, so;
            ds = m.get_data("select * from " + user+"."+s_table + " order by ma");
            sql = "";
            string sql_field = "select a.mapt,b.loaipt,a.tinhhinh,a.taibien,a.tuvong";
            sql_field += " from xxx.pttt a inner join " + user + ".dmpttt b on a.mapt = b.mapt";
            string sql_dk = "";
            if (s_tu1 != "") sql_dk += " and " + m.for_ngay("a.ngay", stime) + " between to_date('" + s_tu1 + "'," + stime + ") and to_date('" + s_den + "'," + stime + ")";
            if (s_makp != "") sql_dk += " and a.makp in (" + s_makp.Substring(0, s_makp.Length - 1) + ")";

            if (i_loaiba == 1)
            {
                sql += sql_field;
                sql += " inner join " + user + ".benhandt c on a.maql=c.maql";
                sql += " where (c.loaiba=1 or c.loaiba is null)";
                sql += sql_dk;
            }
            else
            {
                sql += sql_field;
                sql += " inner join " + user + ".benhanngtr c on a.maql=c.maql";
                sql += " and a.maql>0";
                sql += sql_dk;
                sql += " union all ";
                sql += sql_field;
                sql += " inner join xxx.benhancc c on a.maql=c.maql";
                sql += " and a.maql>0";
                sql += sql_dk;
                sql += " union all ";
                sql += sql_field;
                sql += " left join xxx.benhanpk c on a.maql=c.maql";
                sql += " and a.maql>0";
                sql += sql_dk;
            }

            foreach (DataRow r in m.get_data_mmyy(sql, s_tu1.Substring(0, 10), s_den.Substring(0, 10), false).Tables[0].Rows)
            {
                so = (r["mapt"].ToString().Substring(0, 1) == "P") ? 1 : 10;
                ma = int.Parse(r["loaipt"].ToString()) + so;
                dr = ds.Tables[0].Select("ma=" + ma);
                if (dr.Length > 0)
                {
                    c02 = (r["tinhhinh"].ToString() == "2") ? 1 : 0;
                    c03 = (r["tinhhinh"].ToString() != "2") ? 1 : 0;
                    dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + 1;
                    dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + c02;
                    dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + c03;
                    if (r["taibien"].ToString() != "0")
                    {
                        c05 = (r["taibien"].ToString() == "2") ? 1 : 0;
                        c06 = (r["taibien"].ToString() == "3") ? 1 : 0;
                        c07 = (r["taibien"].ToString() != "2" || r["taibien"].ToString() != "3") ? 1 : 0;
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + 1;
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + c05;
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + c06;
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + c07;
                    }
                    if (r["tuvong"].ToString() != "0")
                    {
                        c09 = (r["tuvong"].ToString() == "1") ? 1 : 0;
                        c10 = (r["tuvong"].ToString() == "2") ? 1 : 0;
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + 1;
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + c09;
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + c10;
                    }
                }
            }
            tongcong(m,"ma>1 and ma<10", "A", 3);
            tongcong(m,"ma>10", "B", 3);
            return ds;
        }

        public DataSet bieu_11(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            string user = m.user;
            DataRow[] dr;
            ds = m.get_data("select * from " + user+"."+s_table + " order by ma");
            DataSet tmp = null;
            if (benhan)
            {
                sql = "SELECT d.STT,sum(case when b.loaiba=1 then 0 else 1 end) as c01,sum(case when b.loaiba<>1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c02,";
                sql += "sum(case when b.loaiba<>1 and a.ttlucrv=7 then 1 else 0 end) as c03,";
                sql += "sum(case when b.loaiba=1 then 1 else 0 end) as c04,sum(case when b.loaiba=1 and c.phai=1 then 1 else 0 end) as c041,";
                sql += "sum(case when b.loaiba=1 and a.ttlucrv=7 then 1 else 0 end) as c05,";
                sql += "sum(case when b.loaiba=1 and c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c051,";
                sql += "sum(to_number(case when b.loaiba=1 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end)) as c06,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c07,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c08,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 and a.ttlucrv=7 then 1 else 0 end) as c09,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end) as c11,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end) as c12,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c15,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and c.phai=1 then 1 else 0 end) as c16,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c17,";
                sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c18";
                sql += " FROM " + user + ".XUATVIEN a," + user + ".BENHANDT b," + user + ".BTDBN c," + user + ".ICD10 d";
                sql += " where a.MAQL = b.MAQL and a.MABN = c.MABN and a.MAICD = d.CICD10 and length(trim(d.stt))>0 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += "group by d.stt";
                sql += " union all ";
                sql += "SELECT d.STT,sum(1) as c01,sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c02,";
                sql += "sum(case when a.ttlucrv=7 then 1 else 0 end) as c03,";
                sql += "sum(0) as c04,sum(0) as c041,";
                sql += "sum(0) as c05,";
                sql += "sum(0) as c051,";
                sql += "sum(0) as c06,";
                sql += "sum(0) as c07,";
                sql += "sum(0) as c08,";
                sql += "sum(0) as c09,";
                sql += "sum(0) as c10,";
                sql += "sum(0) as c11,";
                sql += "sum(0) as c12,";
                sql += "sum(0) as c15,";
                sql += "sum(0) as c16,";
                sql += "sum(0) as c17,";
                sql += "sum(0) as c18";
                sql += " FROM " + user + ".benhanngtr a," + user + ".BTDBN c," + user + ".ICD10 d";
                sql += " where a.MABN = c.MABN and a.MAICD = d.CICD10 and length(trim(d.stt))>0 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += "group by d.stt";

                tmp = m.get_data(sql);

                sql = "SELECT d.STT,sum(1) as c01,sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c02,";
                sql += "sum(case when a.ttlucrv=7 then 1 else 0 end) as c03,";
                sql += "sum(0) as c04,sum(0) as c041,";
                sql += "sum(0) as c05,";
                sql += "sum(0) as c051,";
                sql += "sum(0) as c06,";
                sql += "sum(0) as c07,";
                sql += "sum(0) as c08,";
                sql += "sum(0) as c09,";
                sql += "sum(0) as c10,";
                sql += "sum(0) as c11,";
                sql += "sum(0) as c12,";
                sql += "sum(0) as c15,";
                sql += "sum(0) as c16,";
                sql += "sum(0) as c17,";
                sql += "sum(0) as c18";
                sql += " FROM xxx.benhanpk a," + user + ".BTDBN c," + user + ".ICD10 d";
                sql += " where a.MABN = c.MABN and a.MAICD = d.CICD10 and length(trim(d.stt))>0 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += "group by d.stt";

                if (tmp != null) m.merge(tmp,m.get_data_mmyy(sql, s_tu1, s_den, false));
                else tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);

                sql = "SELECT d.STT,sum(1) as c01,sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c02,";
                sql += "sum(case when a.ttlucrv=7 then 1 else 0 end) as c03,";
                sql += "sum(0) as c04,sum(0) as c041,";
                sql += "sum(0) as c05,";
                sql += "sum(0) as c051,";
                sql += "sum(0) as c06,";
                sql += "sum(0) as c07,";
                sql += "sum(0) as c08,";
                sql += "sum(0) as c09,";
                sql += "sum(0) as c10,";
                sql += "sum(0) as c11,";
                sql += "sum(0) as c12,";
                sql += "sum(0) as c15,";
                sql += "sum(0) as c16,";
                sql += "sum(0) as c17,";
                sql += "sum(0) as c18";
                sql += " FROM xxx.benhancc a," + user + ".BTDBN c," + user + ".ICD10 d";
                sql += " where a.MABN = c.MABN and a.MAICD = d.CICD10 and length(trim(d.stt))>0 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += "group by d.stt";

                if (tmp != null) m.merge(tmp,m.get_data_mmyy(sql, s_tu1, s_den, false));
                else tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                tmp = m.get_sum(tmp, new string[] { "stt" }, new string[] { "c01", "c02", "c03", "c04", "c041", "c05", "c051", "c06", "c07", "c08", "c09", "c10", "c11", "c12", "c15", "c16", "c17", "c18" });
                foreach (DataRow r in tmp.Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = r["c01"].ToString();
                        dr[0]["c02"] = r["c02"].ToString();
                        dr[0]["c03"] = r["c03"].ToString();
                        dr[0]["c04"] = r["c04"].ToString();
                        dr[0]["c05"] = r["c05"].ToString();
                        dr[0]["c06"] = r["c06"].ToString();
                        dr[0]["c07"] = r["c07"].ToString();
                        dr[0]["c08"] = r["c08"].ToString();
                        dr[0]["c09"] = r["c09"].ToString();
                        dr[0]["c10"] = r["c10"].ToString();
                        dr[0]["c11"] = r["c11"].ToString();
                        dr[0]["c12"] = r["c12"].ToString();
                        if (m.Mabv.Substring(0, 3) == "701")
                        {
                            dr[0]["c041"] = r["c041"].ToString();
                            dr[0]["c051"] = r["c051"].ToString();
                            dr[0]["c15"] = r["c15"].ToString();
                            dr[0]["c16"] = r["c16"].ToString();
                            dr[0]["c17"] = r["c17"].ToString();
                            dr[0]["c18"] = r["c18"].ToString();
                        }
                    }
                }
                #region nguyennhan
                tmp = null;
                if (m.bICDNguyennhan)
                {
                    sql = "SELECT d.STT,sum(case when b.loaiba=3 then 0 else 1 end) as c04,";
                    sql += "sum(case when b.loaiba=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c07";
                    sql += " FROM " + user + ".XUATVIEN a," + user + ".BENHANDT b," + user + ".BTDBN c," + user + ".ICD10 d," + user + ".cdnguyennhan e";
                    sql += " where a.MAQL = b.MAQL and a.MABN = c.MABN and a.maql=e.maql and e.MAICD = d.CICD10 and d.stt is not null and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                    sql += "group by d.stt";
                    tmp = m.get_data(sql);
                    sql = "SELECT d.STT,sum(1) as c04,";
                    sql += "sum(0) as c07";
                    sql += " FROM " + user + ".benhanngtr a," + user + ".BTDBN c," + user + ".ICD10 d," + user + ".cdnguyennhan e";
                    sql += " where a.MABN = c.MABN and a.maql=e.maql and e.MAICD = d.CICD10 and d.stt is not null and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                    sql += "group by d.stt";
                    if (tmp != null) tmp.Merge(m.get_data(sql));
                    else tmp = m.get_data(sql);
                    sql = "SELECT d.STT,sum(1) as c04,";
                    sql += "sum(0) as c07";
                    sql += " FROM xxx.benhancc a," + user + ".BTDBN c," + user + ".ICD10 d," + user + ".cdnguyennhan e";
                    sql += " where a.MABN = c.MABN and a.maql=e.maql and e.MAICD = d.CICD10 and d.stt is not null and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                    sql += "group by d.stt";
                    if (tmp != null) tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                    else tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                    tmp = m.get_sum(tmp, new string[] { "stt" }, new string[] { "c04", "c07" });
                    foreach (DataRow r in tmp.Tables[0].Rows)
                    {
                        dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                        if (dr.Length > 0)
                        {
                            dr[0]["c04"] = decimal.Parse(dr[0]["c04"].ToString()) + decimal.Parse(r["c04"].ToString());
                            dr[0]["c07"] = decimal.Parse(dr[0]["c07"].ToString()) + decimal.Parse(r["c07"].ToString());
                        }
                    }
                }
                #endregion

                tongcong(m,"ma>=2 and ma<=58", "C01", 4);
                tongcong(m,"ma>=60 and ma<=98", "C02", 4);
                tongcong(m,"ma>=100 and ma<=103", "C03", 4);
                tongcong(m,"ma>=105 and ma<=115", "C04", 4);
                tongcong(m,"ma>=117 and ma<=124", "C05", 4);
                tongcong(m,"ma>=126 and ma<=135", "C06", 4);
                tongcong(m,"ma>=137 and ma<=146", "C07", 4);
                tongcong(m,"ma>=148 and ma<=150", "C08", 4);
                tongcong(m,"ma>=152 and ma<=173", "C09", 4);
                tongcong(m,"ma>=175 and ma<=189", "C10", 4);
                tongcong(m,"ma>=191 and ma<=208", "C11", 4);
                tongcong(m,"ma>=210 and ma<=211", "C12", 4);
                tongcong(m,"ma>=213 and ma<=223", "C13", 4);
                tongcong(m,"ma>=225 and ma<=247", "C14", 4);
                tongcong(m,"ma>=249 and ma<=259", "C15", 4);
                tongcong(m,"ma>=261 and ma<=269", "C16", 4);
                tongcong(m,"ma>=271 and ma<=283", "C17", 4);
                tongcong(m,"ma>=285 and ma<=288", "C18", 4);
                tongcong(m,"ma>=290 and ma<=308", "C19", 4);
                tongcong(m,"ma>=310 and ma<=323", "C20", 4);
                tongcong(m,"ma>=325 and ma<=333", "C21", 4);
            }

            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06,sum(a.c07) as c07,sum(a.c08) as c08,";
                sql += "sum(a.c09) as c09,sum(a.c10) as c10,sum(a.c11) as c11,sum(a.c12) as c12";
                if (m.Mabv.Substring(0, 3) == "701") sql += ",sum(a.c041) as c041,sum(a.c051) as c051,sum(a.c15) as c15,sum(a.c16) as c16,sum(a.c17) as c17,sum(a.c18) as c18";
                sql += " from " + user + ".bieu_11 a," + user + ".dm_11 b where a.ma=b.ma ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("ma=" + int.Parse(r["ma"].ToString()));
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + Decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(r["c09"].ToString());
                        dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString()) + Decimal.Parse(r["c10"].ToString());
                        dr[0]["c11"] = Decimal.Parse(dr[0]["c11"].ToString()) + Decimal.Parse(r["c11"].ToString());
                        dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString()) + Decimal.Parse(r["c12"].ToString());
                        if (m.Mabv.Substring(0, 3) == "701")
                        {
                            dr[0]["c041"] = Decimal.Parse(dr[0]["c041"].ToString()) + Decimal.Parse(r["c041"].ToString());
                            dr[0]["c051"] = Decimal.Parse(dr[0]["c051"].ToString()) + Decimal.Parse(r["c051"].ToString());
                            dr[0]["c15"] = Decimal.Parse(dr[0]["c15"].ToString()) + Decimal.Parse(r["c15"].ToString());
                            dr[0]["c16"] = Decimal.Parse(dr[0]["c16"].ToString()) + Decimal.Parse(r["c16"].ToString());
                            dr[0]["c17"] = Decimal.Parse(dr[0]["c17"].ToString()) + Decimal.Parse(r["c17"].ToString());
                            dr[0]["c18"] = Decimal.Parse(dr[0]["c18"].ToString()) + Decimal.Parse(r["c18"].ToString());
                        }
                    }
                }
            }
            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09+c10+c11+c12=0");
            ds.AcceptChanges();
            return ds;
        }

        public DataSet bieu_11_khoa(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, string s_makp, bool phatsinh, bool time)
        {
            string user = m.user;
            string stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                s_tu1 = s_tu1 + " " + m.sGiobaocao;
                s_den = s_den + " " + m.sGiobaocao;
            }
            DataRow[] dr;
            ds = m.get_data("select * from " + user+"."+s_table + " order by ma");
            sql = "select d.stt,sum(case when e.loaiba=1 then 0 else 1 end) as c01,sum(case when e.loaiba<>1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<15 then 1 else 0 end) as c02,";
            sql += "sum(case when e.loaiba<>1 and a.ttlucrk=7 then 1 else 0 end) as c03,";
            sql += "sum(case when e.loaiba=1 then 1 else 0 end) as c04,sum(case when e.loaiba=1 and c.phai=1 then 1 else 0 end) as c041,";
            sql += "sum(case when e.loaiba=1 and a.ttlucrk=7 then 1 else 0 end) as c05,";
            sql += "sum(case when e.loaiba=1 and c.phai=1 and a.ttlucrk=7 then 1 else 0 end) as c051,";
            sql += "sum(case when e.loaiba=1 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end ) as c06,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<15 then 1 else 0 end) as c07,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<=4 then 1 else 0 end) as c08,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<15 and a.ttlucrk=7 then 1 else 0 end) as c09,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<=4 and a.ttlucrk=7 then 1 else 0 end) as c10,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<15 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end) as c11,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<=4 then " + m.for_num_ngay("a.ngay") + "-" + m.for_num_ngay("b.ngay") + "+1 else 0 end) as c12,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')>60 then 1 else 0 end) as c15,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')>60 and c.phai=1 then 1 else 0 end) as c16,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')>60 and a.ttlucrk=7 then 1 else 0 end) as c17,";
            sql += "sum(case when e.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')>60 and c.phai=1 and a.ttlucrk=7 then 1 else 0 end) as c18";
            sql += " from " + user + ".xuatkhoa a inner join " + user + ".nhapkhoa b on a.id=b.id";
            sql += " inner join " + user + ".btdbn c on b.mabn=c.mabn";
            sql += " inner join " + user + ".icd10 d on a.maicd=d.cicd10";
            sql += " inner join " + user + ".benhandt e on b.maql=e.maql";
            sql += " where length(trim(d.stt))>0 ";

            if (s_tu1 != "")
                sql += " and " + m.for_ngay("a.ngay", stime) + " between to_date('" + s_tu1 + "'," + stime + ") and to_date('" + s_den + "'," + stime + ")";
            if (s_makp != "") sql += " and b.makp in (" + s_makp.Substring(0, s_makp.Length - 1) + ")";
            sql += "group by d.stt";
            foreach (DataRow r in m.get_data_mmyy(sql, s_tu1.Substring(0, 10), s_den.Substring(0, 10), false).Tables[0].Rows)
            {
                dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                if (dr.Length > 0)
                {
                    dr[0]["c01"] = r["c01"].ToString();
                    dr[0]["c02"] = r["c02"].ToString();
                    dr[0]["c03"] = r["c03"].ToString();
                    dr[0]["c04"] = r["c04"].ToString();
                    dr[0]["c05"] = r["c05"].ToString();
                    dr[0]["c06"] = r["c06"].ToString();
                    dr[0]["c07"] = r["c07"].ToString();
                    dr[0]["c08"] = r["c08"].ToString();
                    dr[0]["c09"] = r["c09"].ToString();
                    dr[0]["c10"] = r["c10"].ToString();
                    dr[0]["c11"] = r["c11"].ToString();
                    dr[0]["c12"] = r["c12"].ToString();
                    if (m.Mabv.Substring(0, 3) == "701")
                    {
                        dr[0]["c041"] = r["c041"].ToString();
                        dr[0]["c051"] = r["c051"].ToString();
                        dr[0]["c15"] = r["c15"].ToString();
                        dr[0]["c16"] = r["c16"].ToString();
                        dr[0]["c17"] = r["c17"].ToString();
                        dr[0]["c18"] = r["c18"].ToString();
                    }
                }
            }
            #region nguyennhan
            if (m.bICDNguyennhan)
            {
                sql = "select d.stt,sum(case when f.loaiba=3 then 0 else 1 end) as c04,";
                sql += "sum(case when f.loaiba=1 and to_number(to_char(now(),'yyyy'),'0000')-to_number(c.namsinh,'0000')<15 then 1 else 0 end) as c07";
                sql += " from " + user + ".xuatkhoa a inner join " + user + ".nhapkhoa b on a.id=b.id";
                sql += " inner join " + user + ".BTDBN c on b.mabn=c.mabn";
                sql += " inner join " + user + ".cdnguyennhan e on b.maql=e.maql";
                sql += " inner join " + user + ".ICD10 d on e.maicd=d.cicd10";
                sql += " inner join " + user + ".benhandt f on b.maql=f.maql";
                sql += " where d.stt is not null and " + m.for_ngay("a.ngay", stime) + " between to_date('" + s_tu1 + "'," + stime + ") and to_date('" + s_den + "'," + stime + ")";
                if (s_makp != "") sql += " and b.makp in (" + s_makp.Substring(0, s_makp.Length - 1) + ")";
                sql += "group by d.stt";
                foreach (DataRow r in m.get_data_mmyy(sql, s_tu1.Substring(0, 10), s_den.Substring(0, 10), false).Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["c04"] = decimal.Parse(dr[0]["c04"].ToString()) + decimal.Parse(r["c04"].ToString());
                        dr[0]["c07"] = decimal.Parse(dr[0]["c07"].ToString()) + decimal.Parse(r["c07"].ToString());
                    }
                }
            }
            #endregion

            tongcong(m,"ma>=2 and ma<=58", "C01", 4);
            tongcong(m,"ma>=60 and ma<=98", "C02", 4);
            tongcong(m,"ma>=100 and ma<=103", "C03", 4);
            tongcong(m,"ma>=105 and ma<=115", "C04", 4);
            tongcong(m,"ma>=117 and ma<=124", "C05", 4);
            tongcong(m,"ma>=126 and ma<=135", "C06", 4);
            tongcong(m,"ma>=137 and ma<=146", "C07", 4);
            tongcong(m,"ma>=148 and ma<=150", "C08", 4);
            tongcong(m,"ma>=152 and ma<=173", "C09", 4);
            tongcong(m,"ma>=175 and ma<=189", "C10", 4);
            tongcong(m,"ma>=191 and ma<=208", "C11", 4);
            tongcong(m,"ma>=210 and ma<=211", "C12", 4);
            tongcong(m,"ma>=213 and ma<=223", "C13", 4);
            tongcong(m,"ma>=225 and ma<=247", "C14", 4);
            tongcong(m,"ma>=249 and ma<=259", "C15", 4);
            tongcong(m,"ma>=261 and ma<=269", "C16", 4);
            tongcong(m,"ma>=271 and ma<=283", "C17", 4);
            tongcong(m,"ma>=285 and ma<=288", "C18", 4);
            tongcong(m,"ma>=290 and ma<=308", "C19", 4);
            tongcong(m,"ma>=310 and ma<=323", "C20", 4);
            tongcong(m,"ma>=325 and ma<=333", "C21", 4);

            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09+c10+c11+c12=0");
            ds.AcceptChanges();
            return ds;
        }

        public DataSet kh_bieu_15(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {
            DataRow[] dr;
            string user = m.user;
            ds = m.get_data("select * from " + user+"."+s_table + " order by ma");
            if (benhan)
            {
                sql = "SELECT d.STT,count(*) as c01,sum(to_number(case when a.ttlucrv=7 then 1 else 0 end)) as c02,";
                sql += "sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c03,";
                sql += "sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c04,";
                sql += "sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 and a.ttlucrv=7 then 1 else 0 end) as c05,";
                sql += "sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06";
                sql += " FROM " + user + ".XUATVIEN a," + user + ".BENHANDT b," + user + ".BTDBN c," + user + ".ICD10 d";
                sql += " where a.MAQL = b.MAQL and a.MABN = c.MABN and a.MAICD = d.CICD10 and b.loaiba=1 and length(trim(d.stt))>0 and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += "group by d.stt";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = r["c01"].ToString();
                        dr[0]["c02"] = r["c02"].ToString();
                        dr[0]["c03"] = r["c03"].ToString();
                        dr[0]["c04"] = r["c04"].ToString();
                        dr[0]["c05"] = r["c05"].ToString();
                        dr[0]["c06"] = r["c06"].ToString();
                    }
                }
                #region nguyennhan
                if (m.bICDNguyennhan)
                {
                    sql = "SELECT d.STT,count(*) as c01,sum(case when to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<15 then 1 else 0 end) as c03";
                    sql += " FROM " + user + ".XUATVIEN a," + user + ".BENHANDT b," + user + ".BTDBN c," + user + ".ICD10 d," + user + ".cdnguyennhan e";
                    sql += " where a.MAQL = b.MAQL and a.MABN = c.MABN and a.maql=e.maql and e.MAICD = d.CICD10 and d.stt is not null and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                    sql += "group by d.stt";
                    foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                    {
                        dr = ds.Tables[0].Select("stt='" + r["stt"].ToString() + "'");
                        if (dr.Length > 0)
                        {
                            dr[0]["c01"] = decimal.Parse(dr[0]["c01"].ToString()) + decimal.Parse(r["c01"].ToString());
                            dr[0]["c03"] = decimal.Parse(dr[0]["c03"].ToString()) + decimal.Parse(r["c03"].ToString());
                        }
                    }

                }
                #endregion
                tongcong(m,"ma>=2 and ma<=58", "C01", 4);
                tongcong(m,"ma>=60 and ma<=98", "C02", 4);
                tongcong(m,"ma>=100 and ma<=103", "C03", 4);
                tongcong(m,"ma>=105 and ma<=115", "C04", 4);
                tongcong(m,"ma>=117 and ma<=124", "C05", 4);
                tongcong(m,"ma>=126 and ma<=135", "C06", 4);
                tongcong(m,"ma>=137 and ma<=146", "C07", 4);
                tongcong(m,"ma>=148 and ma<=150", "C08", 4);
                tongcong(m,"ma>=152 and ma<=173", "C09", 4);
                tongcong(m,"ma>=175 and ma<=189", "C10", 4);
                tongcong(m,"ma>=191 and ma<=208", "C11", 4);
                tongcong(m,"ma>=210 and ma<=211", "C12", 4);
                tongcong(m,"ma>=213 and ma<=223", "C13", 4);
                tongcong(m,"ma>=225 and ma<=247", "C14", 4);
                tongcong(m,"ma>=249 and ma<=259", "C15", 4);
                tongcong(m,"ma>=261 and ma<=269", "C16", 4);
                tongcong(m,"ma>=271 and ma<=283", "C17", 4);
                tongcong(m,"ma>=285 and ma<=288", "C18", 4);
                tongcong(m,"ma>=290 and ma<=308", "C19", 4);
                tongcong(m,"ma>=310 and ma<=323", "C20", 4);
                tongcong(m,"ma>=325 and ma<=333", "C21", 4);
            }

            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06";
                sql += " from " + user + ".kh_bieu_15 a," + user + ".dm_11 b where a.ma=b.ma ";
                sql += " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    dr = ds.Tables[0].Select("ma=" + int.Parse(r["ma"].ToString()));
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = Decimal.Parse(dr[0]["c01"].ToString()) + Decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString()) + Decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString()) + Decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString()) + Decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(r["c06"].ToString());
                    }
                }
            }
            if (phatsinh) m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06=0");
            ds.AcceptChanges();
            return ds;
        }

       
        public DataSet kh_bieu_145(LibBaocao.AccessData m, string s_tu, string s_tu1, string s_den, string s_table, bool benhan, bool thongke, bool phatsinh)
        {

            string user = m.user;
            ds = m.get_data("select ma,stt,ten,0 as c25,0 as c26,c01,c02,c03,c04,c05,c06,c07,c08,c09,c10,c11,c12,0 as c21,0 as c22,0 as c23,0 as c24,c13,c14,c15,c16,c17,c18,c19,c20 from " + user + "." + s_table + " order by ma");
            DataSet tmp = new DataSet();
            if (benhan)
            {
                sql = "SELECT e.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".BTDNN_BV d on c.mann=d.mann inner join " + user + ".BTDNN e on d.mannbo=e.mann ";
                sql += " where ";
                sql += " " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by e.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
                
                sql = "SELECT e.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".benhanngtr a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".BTDNN_BV d on c.mann=d.mann inner join " + user + ".BTDNN e on d.mannbo=e.mann ";
                sql += " where ";
                sql += " " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by e.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT e.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANPK a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".BTDNN_BV d on c.mann=d.mann inner join " + user + ".BTDNN e on d.mannbo=e.mann ";
                sql += " where ";
                sql += " " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by e.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
             
                sql = "SELECT e.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANCC a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".BTDNN_BV d on c.mann=d.mann inner join " + user + ".BTDNN e on d.mannbo=e.mann ";
                sql += " where ";

                sql += " " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by e.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
              
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMDIADIEM d on b.diadiem=d.ma ";
                sql += " where ";
                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".BENHANNGTR a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMDIADIEM d on b.diadiem=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANPK a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMDIADIEM d on b.diadiem=d.ma ";
                sql += " where ";
                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANCC a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMDIADIEM d on b.diadiem=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMbophan d on b.bophan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".BENHANNGTR a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMbophan d on b.bophan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANPK a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMbophan d on b.bophan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.BENHANCC a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMbophan d on b.bophan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMnguyennhan d on b.nguyennhan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".benhanngtr a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMnguyennhan d on b.nguyennhan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhanpk a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMnguyennhan d on b.nguyennhan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }

                sql = "SELECT d.stt,sum(to_number(case when c.phai=0  and a.nhantu=1 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.nhantu=1 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19   and a.nhantu=1 and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.nhantu=1 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19   and a.nhantu=1 and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 and a.nhantu=1 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.nhantu=1 and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 and a.nhantu=1 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.nhantu=1 and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhancc a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMnguyennhan d on b.nguyennhan=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMngodoc d on b.ngodoc=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".benhanngtr a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMngodoc d on b.ngodoc=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhanpk a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMngodoc d on b.ngodoc=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhancc a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".DMngodoc d on b.ngodoc=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".XUATVIEN a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".dmxutri d on b.xutri=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20  and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join " + user + ".benhanngtr a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".dmxutri d on b.xutri=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
                
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhanpk a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".dmxutri d on b.xutri=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                sql = "SELECT d.stt,sum(to_number(case when c.phai=0 then 1 else 0 end)) as c01, sum(case when c.phai=0 and a.ttlucrv=7 then 1 else 0 end) as c02,";
                sql += "sum(to_number(case when c.phai=1 then 1 else 0 end)) as c03, sum(case when c.phai=1 and a.ttlucrv=7 then 1 else 0 end) as c04,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c05, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c06,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 then 1 else 0 end) as c07, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=4 and a.ttlucrv=7 then 1 else 0 end) as c08,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c09,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c10,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14 then 1 else 0 end) as c11,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>4 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=14  and a.ttlucrv=7 then 1 else 0 end) as c12,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c21,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c22,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19 then 1 else 0 end) as c23,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>14 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=19  and a.ttlucrv=7 then 1 else 0 end) as c24,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c13,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c14,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59 then 1 else 0 end) as c15,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>=20 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)<=59  and a.ttlucrv=7 then 1 else 0 end) as c16,";
                sql += "sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c17, sum(case when c.phai=0 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c18,";
                sql += "sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 then 1 else 0 end) as c19, sum(case when c.phai=1 and to_number(to_char(now(),'yyyy'))-to_number(c.namsinh)>60 and a.ttlucrv=7 then 1 else 0 end) as c20";

                sql += " FROM xxx.TAINANTT b inner join xxx.benhancc a on b.maql=a.maql inner join " + user + ".BTDBN c on b.mabn=c.mabn inner join " + user + ".dmxutri d on b.xutri=d.ma ";
                sql += " where ";

                sql += " d.stt<>0 and " + m.for_ngay("b.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu1 + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by d.stt";
                if (tmp == null)
                {
                    tmp = m.get_data_mmyy(sql, s_tu1, s_den, false);
                }
                else
                {
                    tmp.Merge(m.get_data_mmyy(sql, s_tu1, s_den, false));
                }
               
                tmp = m.get_sum(tmp, new string[] { "stt" }, new string[] { 
            "c01", "c02", "c03", "c04", "c05", "c06", "c07", "c08", "c09", "c10", "c11", "c12", "c13", "c14", "c15", "c16", 
            "c17", "c18", "c19", "c20"
         });
                upd_data(tmp);
                
                tongcong_145(m, "ma>=3 and ma<=9", 1, 3);
                tongcong_145(m, "ma>=3 and ma<=9", 2, 3);
                tongcong_145(m, "ma>=11 and ma<=17", 10, 3);
                tongcong_145(m, "ma>=19 and ma<=22", 0x12, 3);
                tongcong_145(m, "ma>=31 and ma<=36", 30, 3);
                tongcong_145(m, "ma in (24,25,26,27,28,29,30,37,38,39)", 0x17, 3);
                tongcong_145(m, "ma>=41 and ma<=48", 40, 3);
            }
            if (thongke)
            {
                sql = "select a.ma,sum(a.c01) as c01,sum(a.c02) as c02,sum(a.c03) as c03,sum(a.c04) as c04,";
                sql += "sum(a.c05) as c05,sum(a.c06) as c06,sum(a.c07) as c07,sum(a.c08) as c08,";
                sql += "sum(a.c09) as c09,sum(a.c10) as c10,sum(a.c11) as c11,sum(a.c12) as c12,";
                sql += "sum(a.c13) as c13,sum(a.c14) as c14,sum(a.c15) as c15,sum(a.c16) as c16,";
                sql += "sum(a.c17) as c17,sum(a.c18) as c18,sum(a.c19) as c19,sum(a.c20) as c20,";
                sql += "sum(0) as c21,sum(0) as c22,sum(0) as c23,sum(0) as c24";

                sql = " from " + user + ".kh_bieu_1451 a," + user + ".kh_dm_1451 b where a.ma=b.ma ";

                sql = " and " + m.for_ngay("a.ngay", "'dd/mm/yyyy'") + " between to_date('" + s_tu + "','dd/mm/yyyy') and to_date('" + s_den + "','dd/mm/yyyy')";
                sql += " group by a.ma";
                foreach (DataRow r in m.get_data(sql).Tables[0].Rows)
                {
                    DataRow[] dr = ds.Tables[0].Select("ma=" + int.Parse(r["ma"].ToString()));
                    if (dr.Length > 0)
                    {
                        dr[0]["c01"] = decimal.Parse(dr[0]["c01"].ToString()) + decimal.Parse(r["c01"].ToString());
                        dr[0]["c02"] = decimal.Parse(dr[0]["c02"].ToString()) + decimal.Parse(r["c02"].ToString());
                        dr[0]["c03"] = decimal.Parse(dr[0]["c03"].ToString()) + decimal.Parse(r["c03"].ToString());
                        dr[0]["c04"] = decimal.Parse(dr[0]["c04"].ToString()) + decimal.Parse(r["c04"].ToString());
                        dr[0]["c05"] = decimal.Parse(dr[0]["c05"].ToString()) + decimal.Parse(r["c05"].ToString());
                        dr[0]["c06"] = decimal.Parse(dr[0]["c06"].ToString()) + decimal.Parse(r["c06"].ToString());
                        dr[0]["c07"] = decimal.Parse(dr[0]["c07"].ToString()) + decimal.Parse(r["c07"].ToString());
                        dr[0]["c08"] = decimal.Parse(dr[0]["c08"].ToString()) + decimal.Parse(r["c08"].ToString());
                        dr[0]["c09"] = decimal.Parse(dr[0]["c09"].ToString()) + decimal.Parse(r["c09"].ToString());
                        dr[0]["c10"] = decimal.Parse(dr[0]["c10"].ToString()) + decimal.Parse(r["c10"].ToString());
                        dr[0]["c11"] = decimal.Parse(dr[0]["c11"].ToString()) + decimal.Parse(r["c11"].ToString());
                        dr[0]["c12"] = decimal.Parse(dr[0]["c12"].ToString()) + decimal.Parse(r["c12"].ToString());
                        dr[0]["c13"] = decimal.Parse(dr[0]["c13"].ToString()) + decimal.Parse(r["c13"].ToString());
                        dr[0]["c14"] = decimal.Parse(dr[0]["c14"].ToString()) + decimal.Parse(r["c14"].ToString());
                        dr[0]["c15"] = decimal.Parse(dr[0]["c15"].ToString()) + decimal.Parse(r["c15"].ToString());
                        dr[0]["c16"] = decimal.Parse(dr[0]["c16"].ToString()) + decimal.Parse(r["c16"].ToString());
                        dr[0]["c17"] = decimal.Parse(dr[0]["c17"].ToString()) + decimal.Parse(r["c17"].ToString());
                        dr[0]["c18"] = decimal.Parse(dr[0]["c18"].ToString()) + decimal.Parse(r["c18"].ToString());
                        dr[0]["c19"] = decimal.Parse(dr[0]["c19"].ToString()) + decimal.Parse(r["c19"].ToString());
                        dr[0]["c20"] = decimal.Parse(dr[0]["c20"].ToString()) + decimal.Parse(r["c20"].ToString());
                    }
                }
            }
            if (phatsinh)
            {
                m.delrec(ds.Tables[0], "c01+c02+c03+c04+c05+c06+c07+c08+c09+c10+c11+c12+c13+c14+c15+c16+c17+c18+c19+c20+c21+c22+c23+c24=0");
            }
            ds.AcceptChanges();
            return ds;
        }

        private void tongcong_145(LibBaocao.AccessData m,string exp, int ma, int k)
        {
            decimal l_tong = 0M;
            DataRow[] r1 = ds.Tables[0].Select(exp);
            short iRec = Convert.ToInt16(r1.Length);
            for (int j = k; j < ds.Tables[0].Columns.Count; j++)
            {
                l_tong = 0M;
                for (short i = 0; i < iRec; i = (short)(i + 1))
                {
                    l_tong += decimal.Parse(r1[i][j].ToString());
                }
                m.updrec_145(ds.Tables[0], ma, j, l_tong);
            }
        }
 
        private void upd_data(DataSet ds_in)
        {
            DataRow[] dr;
            foreach (DataRow r in ds_in.Tables[0].Rows)
            {
                dr = ds.Tables[0].Select("ma=" + int.Parse(r["stt"].ToString()));
                if (dr.Length > 0)
                {
                    dr[0]["c01"] = r["c01"].ToString();
                    dr[0]["c02"] = r["c02"].ToString();
                    dr[0]["c03"] = r["c03"].ToString();
                    dr[0]["c04"] = r["c04"].ToString();
                    dr[0]["c05"] = r["c05"].ToString();
                    dr[0]["c06"] = r["c06"].ToString();
                    dr[0]["c07"] = r["c07"].ToString();
                    dr[0]["c08"] = r["c08"].ToString();
                    dr[0]["c09"] = r["c09"].ToString();
                    dr[0]["c10"] = r["c10"].ToString();
                    dr[0]["c11"] = r["c11"].ToString();
                    dr[0]["c12"] = r["c12"].ToString();
                    dr[0]["c13"] = r["c13"].ToString();
                    dr[0]["c14"] = r["c14"].ToString();
                    dr[0]["c15"] = r["c15"].ToString();
                    dr[0]["c16"] = r["c16"].ToString();
                    dr[0]["c17"] = r["c17"].ToString();
                    dr[0]["c18"] = r["c18"].ToString();
                    dr[0]["c19"] = r["c19"].ToString();
                    dr[0]["c20"] = r["c20"].ToString();
                    dr[0]["c21"] = r["c21"].ToString();
                    dr[0]["c22"] = r["c22"].ToString();
                    dr[0]["c23"] = r["c23"].ToString();
                    dr[0]["c24"] = r["c24"].ToString();
                }
            }
        }

        public DataSet upd_ththbn(LibBaocao.AccessData m, string tu, string den, string makp, bool time)
        {
            //linh
            //string stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            string stime = (time) ? "'" + "'dd/mm/yyyy hh24:mi'" : "'dd/mm/yyyy'";
            if (time)
            {
                tu = tu + " " + m.sGiobaocao;
                den = den + " " + m.sGiobaocao;
            }
            string user = m.user;
            Int64 songay = m.songay(m.StringToDate(den.Substring(0, 10)), m.StringToDate(tu.Substring(0, 10)), 1);
            dt = new DataTable();
            dt = m.get_data("select * from " + user + ".btdkp_bv").Tables[0];
            ds = new DataSet();
            DataRow r1, r2;
            ds = m.get_data("select * from " + user + ".ththbn");
            dc = new DataColumn();
            dc.ColumnName = "C15";//ngaydt
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C16";//ngaydt ravien
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C17";//songay
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            DataSet dst = new DataSet();
                sql = "SELECT c.MAKP,sum(case when (to_date(to_char(a.ngay," + stime + ")," + stime + ")<to_date('" + tu + "'," + stime + ") and (b.ngay is null or to_date(to_char(b.ngay," + stime + ")," + stime + ")>=to_date('" + tu + "'," + stime + "))) then 1 else 0 end) as C03,";
                sql += "sum(case when a.khoachuyen='01' and to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C04,";
                sql += "sum(case when a.khoachuyen<>'01' and to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C05,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=1 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C06,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=2 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C07,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=3 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C08,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=4 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C09,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=5 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C10,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=6 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C11,";
                sql += "Sum(case when b.ngay is null then 0 else case when b.ttlucrk=7 And to_date(to_char(b.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C12,";
                sql += "sum(case when b.ngay is null or to_date(to_char(b.ngay," + stime + ")," + stime + ")>to_date('" + den + "'," + stime + ") then 1 else 0 end) as C13";
                sql += " FROM " + user + ".NHAPKHOA a left join " + user + ".XUATKHOA b on a.id=b.id inner join " + user + ".BTDKP_BV c on a.makp=c.makp inner join " + user + ".benhandt d on a.maql=d.maql";
                sql += " WHERE d.loaiba=1 and a.maba<20";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " GROUP BY c.makp ORDER BY c.makp";
                dst = m.get_data(sql);
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r2 = m.getrowbyid(dt, "makp='" + r["makp"].ToString() + "'");
                if (r2 != null)
                {
                    r1 = ds.Tables[0].NewRow();
                    r1["makp"] = r["makp"].ToString();
                    r1["tenkp"] = r2["tenkp"].ToString();
                    r1["c02"] = Decimal.Parse(r2["kehoach"].ToString());
                    r1["c14"] = Decimal.Parse(r2["thucke"].ToString());
                    r1["c03"] = Decimal.Parse(r["c03"].ToString());
                    r1["c13"] = Decimal.Parse(r["c13"].ToString());
                    r1["c17"] = songay.ToString();
                    r1["c04"] = Decimal.Parse(r["c04"].ToString());
                    r1["c05"] = Decimal.Parse(r["c05"].ToString());
                    r1["c06"] = Decimal.Parse(r["c06"].ToString());
                    r1["c07"] = Decimal.Parse(r["c07"].ToString());
                    r1["c08"] = Decimal.Parse(r["c08"].ToString());
                    r1["c09"] = Decimal.Parse(r["c09"].ToString());
                    r1["c10"] = Decimal.Parse(r["c10"].ToString());
                    r1["c11"] = Decimal.Parse(r["c11"].ToString());
                    r1["c12"] = Decimal.Parse(r["c12"].ToString());
                    r1["c13"] = Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) + Decimal.Parse(r["c05"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c10"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                    r1["c15"] = 0;
                    r1["c16"] = 0;
                    ds.Tables[0].Rows.Add(r1);
                }
            }
                sql = "select a.makp,sum(";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "'," + stime + ")< to_date(to_char(b.ngay," + stime + ")," + stime + ") or b.ngay is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date('" + tu + "'," + stime + "))+1 else";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(b.ngay," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(b.ngay," + stime + ")," + stime + ")-to_date('" + tu + "'," + stime + "))+ case when b.ttlucrk in (5,6,7) then 0 else 1 end else ";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "','dd/mm/yyyy')< to_date(to_char(b.ngay," + stime + ")," + stime + ") or b.ngay is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + "))+1 else ";
                sql += " case when to_date(to_char(b.ngay," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(b.ngay," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(b.ngay," + stime + ")," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + ")) + case when b.ttlucrk in(5,6,7) then 0 else 1 end end end end end) c15";
                sql += " from " + user + ".nhapkhoa a left join " + user + ".xuatkhoa b on a.id=b.id inner join " + user + ".benhandt c on a.maql=c.maql inner join " + user + ".btdbn d on a.mabn=d.mabn ";
                sql += " where a.maba<20 and c.loaiba=1 and a.makp is not null ";
                sql += " and to_date(to_char(a.ngay," + stime + ")," + stime + ") <=to_date('" + den + "'," + stime + ") and to_date(to_char(coalesce(b.ngay,now)," + stime + ")," + stime + ") >=to_date('" + tu + "'," + stime + ")";
                sql += " group by a.makp";
                dst = m.get_data(sql);
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r1 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                if (r1 != null)
                {
                    r1["c15"] = Decimal.Parse(r1["c15"].ToString()) + Decimal.Parse(r["c15"].ToString());
                    r1["c16"] = Decimal.Parse(r1["c16"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
            }
            return ds;
        }

        public DataSet upd_ththbn_ngtru(LibBaocao.AccessData m, string tu, string den, string makp, int loaiba, bool time)
        {
            string user=m.user,stime = (time) ? "'dd/mm/yyyy hh24:mi'" : "'dd/mm/yyyy'";
            if (time)
            {
                tu = tu + " " + m.sGiobaocao;
                den = den + " " + m.sGiobaocao;
            }
            Int64 songay = m.songay(m.StringToDate(den.Substring(0, 10)), m.StringToDate(tu.Substring(0, 10)), 1);
            dt = new DataTable();
            dt = m.get_data("select * from " + user + ".btdkp_bv").Tables[0];
            ds = new DataSet();
            DataRow r1, r2,r3;
            ds = m.get_data("select * from " + user + ".ththbn");
            dc = new DataColumn();
            dc.ColumnName = "C15";//ngaydt
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C16";//ngaydt ravien
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C17";//songay
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C18";//ngaydtbhyt
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "C19";//ngaydt ra vien bhyt
            dc.DataType = Type.GetType("System.Decimal");
            ds.Tables[0].Columns.Add(dc);
            DataSet dst = new DataSet();
            foreach (DataRow r in ds.Tables[0].Rows)
                r["c02"] = r["c14"] = r["c03"] = r["c13"] = r["c17"] = r["c04"] = r["c05"] = r["c06"] = r["c07"] = r["c08"] = r["c09"] = r["c10"] = r["c11"] = r["c12"] = r["c13"] = r["c14"] = r["c15"] = r["c16"] = r["c18"] = r["c19"] = 0;
            if (makp == "99")
            {
                sql = "SELECT c.MAKP,sum(case when (to_date(to_char(a.ngay," + stime + ")," + stime + ")<to_date('" + tu + "'," + stime + ") and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>=to_date('" + tu + "'," + stime + "))) then 1 else 0 end) as C03,";
                sql += "sum(case when (a.madoituong=1 and to_date(to_char(a.ngay," + stime + ")," + stime + ")<to_date('" + tu + "'," + stime + ") and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>=to_date('" + tu + "'," + stime + "))) then 1 else 0 end) as C02,";
                sql += "sum(case when to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C04,";
                sql += "sum(case when a.madoituong=1 and to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C05,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv not in (6,7) And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C06,";
                /*
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=2 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C07,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=3 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C08,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=4 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C09,";
                */
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=-2 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C07,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=-3 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C08,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=-4 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C09,";
                sql += "Sum(case when a.madoituong=1 and to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C10,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=6 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C11,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=7 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C12,";
                sql += "sum(case when a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>to_date('" + den + "'," + stime + ") then 1 else 0 end) as C13,";
                sql += "sum(case when (a.madoituong=1) and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>to_date('" + den + "'," + stime + ")) then 1 else 0 end) as C14";
                sql += " FROM xxx.benhancc a," + user + ".btdkp_bv c,"+user+".btdbn b";
                sql += " WHERE a.makp=c.makp and a.mabn=b.mabn ";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " GROUP BY c.makp ORDER BY c.makp";
                dst = m.get_data_mmyy(sql, m.DateToString("dd/MM/yyyy", m.StringToDate(tu.Substring(0, 10)).AddYears(-1)), den.Substring(0, 10), false);
            }
            else
            {
                sql = "SELECT c.MAKP,sum(case when (to_date(to_char(a.ngay," + stime + ")," + stime + ")<to_date('" + tu + "'," + stime + ") and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>=to_date('" + tu + "'," + stime + "))) then 1 else 0 end) as C03,";
                sql += "sum(case when (a.madoituong=1 and to_date(to_char(a.ngay," + stime + ")," + stime + ")<to_date('" + tu + "'," + stime + ") and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>=to_date('" + tu + "'," + stime + "))) then 1 else 0 end) as C02,";
                sql += "sum(case when to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C04,";
                sql += "sum(case when a.madoituong=1 and to_date(to_char(a.ngay," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C05,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv not in (2,3,4,5,6,7) And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C06,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=2 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C07,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=3 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C08,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=4 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C09,";
                sql += "Sum(case when a.madoituong=1 and to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end) as C10,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=6 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C11,";
                sql += "Sum(case when a.ngayrv is null then 0 else case when a.ttlucrv=7 And to_date(to_char(a.ngayrv," + stime + ")," + stime + ") Between to_date('" + tu + "'," + stime + ") And to_date('" + den + "'," + stime + ") then 1 else 0 end end) as C12,";
                sql += "sum(case when a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>to_date('" + den + "'," + stime + ") then 1 else 0 end) as C13,";
                sql += "sum(case when (a.madoituong=1) and (a.ngayrv is null or to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>to_date('" + den + "'," + stime + ")) then 1 else 0 end) as C14";
                sql += " FROM " + user + ".benhanngtr a," + user + ".btdkp_bv c,"+user+".btdbn b";
                sql += " WHERE a.makp=c.makp and a.mabn=b.mabn";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " GROUP BY c.makp ORDER BY c.makp";
                dst = m.get_data(sql);
            }
            DataRow [] dr;
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r2 = m.getrowbyid(dt, "makp='" + r["makp"].ToString() + "'");
                if (r2 != null)
                {
                    r3 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                    if (r3 == null)
                    {
                        r1 = ds.Tables[0].NewRow();
                        r1["makp"] = r["makp"].ToString();
                        r1["tenkp"] = r2["tenkp"].ToString();
                        r1["c02"] = Decimal.Parse(r["c02"].ToString());
                        r1["c14"] = Decimal.Parse(r["c14"].ToString());
                        r1["c03"] = Decimal.Parse(r["c03"].ToString());
                        r1["c13"] = Decimal.Parse(r["c13"].ToString());
                        r1["c17"] = songay.ToString();
                        r1["c04"] = Decimal.Parse(r["c04"].ToString());
                        r1["c05"] = Decimal.Parse(r["c05"].ToString());
                        r1["c06"] = Decimal.Parse(r["c06"].ToString());
                        r1["c07"] = Decimal.Parse(r["c07"].ToString());
                        r1["c08"] = Decimal.Parse(r["c08"].ToString());
                        r1["c09"] = Decimal.Parse(r["c09"].ToString());
                        r1["c10"] = Decimal.Parse(r["c10"].ToString());
                        r1["c11"] = Decimal.Parse(r["c11"].ToString());
                        r1["c12"] = Decimal.Parse(r["c12"].ToString());
                        r1["c13"] = Decimal.Parse(r["c03"].ToString()) + Decimal.Parse(r["c04"].ToString()) - (Decimal.Parse(r["c06"].ToString()) + Decimal.Parse(r["c07"].ToString()) + Decimal.Parse(r["c08"].ToString()) + Decimal.Parse(r["c09"].ToString()) + Decimal.Parse(r["c11"].ToString()) + Decimal.Parse(r["c12"].ToString()));
                        r1["c14"] = decimal.Parse(r["c02"].ToString()) + decimal.Parse(r["c05"].ToString()) - decimal.Parse(r["c10"].ToString());
                        r1["c15"] = 0;
                        r1["c16"] = 0;
                        r1["c18"] = 0;
                        r1["c19"] = 0;
                        ds.Tables[0].Rows.Add(r1);
                    }
                    else
                    {
                        dr = ds.Tables[0].Select("makp='" + r["makp"].ToString() + "'");
                        if (dr.Length > 0)
                        {
                            dr[0]["c02"] = Decimal.Parse(dr[0]["c02"].ToString())+Decimal.Parse(r["c02"].ToString());
                            dr[0]["c14"] = Decimal.Parse(dr[0]["c14"].ToString())+Decimal.Parse(r["c14"].ToString());
                            dr[0]["c03"] = Decimal.Parse(dr[0]["c03"].ToString())+Decimal.Parse(r["c03"].ToString());
                            dr[0]["c13"] = Decimal.Parse(dr[0]["c13"].ToString())+Decimal.Parse(r["c13"].ToString());
                            dr[0]["c17"] = Decimal.Parse(dr[0]["c17"].ToString())+songay.ToString();
                            dr[0]["c04"] = Decimal.Parse(dr[0]["c04"].ToString())+Decimal.Parse(r["c04"].ToString());
                            dr[0]["c05"] = Decimal.Parse(dr[0]["c05"].ToString())+Decimal.Parse(r["c05"].ToString());
                            dr[0]["c06"] = Decimal.Parse(dr[0]["c06"].ToString())+Decimal.Parse(r["c06"].ToString());
                            dr[0]["c07"] = Decimal.Parse(dr[0]["c07"].ToString())+Decimal.Parse(r["c07"].ToString());
                            dr[0]["c08"] = Decimal.Parse(dr[0]["c08"].ToString())+Decimal.Parse(r["c08"].ToString());
                            dr[0]["c09"] = Decimal.Parse(dr[0]["c09"].ToString())+Decimal.Parse(r["c09"].ToString());
                            dr[0]["c10"] = Decimal.Parse(dr[0]["c10"].ToString())+Decimal.Parse(r["c10"].ToString());
                            dr[0]["c11"] = Decimal.Parse(dr[0]["c11"].ToString())+Decimal.Parse(r["c11"].ToString());
                            dr[0]["c12"] = Decimal.Parse(dr[0]["c12"].ToString())+Decimal.Parse(r["c12"].ToString());
                            dr[0]["c13"] = Decimal.Parse(dr[0]["c03"].ToString()) + Decimal.Parse(dr[0]["c04"].ToString()) - (Decimal.Parse(dr[0]["c06"].ToString()) + Decimal.Parse(dr[0]["c07"].ToString()) + Decimal.Parse(dr[0]["c08"].ToString()) + Decimal.Parse(dr[0]["c09"].ToString()) + Decimal.Parse(dr[0]["c11"].ToString()) + Decimal.Parse(dr[0]["c12"].ToString()));
                            dr[0]["c14"] = decimal.Parse(dr[0]["c02"].ToString()) + decimal.Parse(dr[0]["c05"].ToString()) - decimal.Parse(dr[0]["c10"].ToString());
                        }
                    }
                }
            }
            #region linh 6/12/2007
            if (makp == "99")
            {
                sql = "select a.makp,sum(";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "'," + stime + ")< to_date(to_char(a.ngayrv," + stime + ")," + stime + ") or a.ngayrv is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date('" + tu + "'," + stime + "))+1 else";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(a.ngayrv," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(a.ngayrv," + stime + ")," + stime + ")-to_date('" + tu + "'," + stime + "))+ case when a.ttlucrv in (5,6,7) then 0 else 1 end else ";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "','dd/mm/yyyy')< to_date(to_char(a.ngayrv," + stime + ")," + stime + ") or a.ngayrv is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + "))+1 else ";
                sql += " case when to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(a.ngayrv," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(a.ngayrv," + stime + ")," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + ")) + case when a.ttlucrv in(5,6,7) then 0 else 1 end end end end end) c15";
                sql += " from xxx.benhancc a ,"+user+".btdbn b ";
                sql += " where a.mabn=b.mabn and a.makp is not null ";
                sql += " and to_date(to_char(a.ngay," + stime + ")," + stime + ") <=to_date('" + den + "'," + stime + ") and to_date(to_char(coalesce(a.ngayrv,now)," + stime + ")," + stime + ") >=to_date('" + tu + "'," + stime + ")";
                sql += " group by a.makp";
                dst = m.get_data_mmyy(sql, m.DateToString("dd/MM/yyyy", m.StringToDate(tu.Substring(0, 10)).AddYears(-1)), den.Substring(0, 10), false);
            }
            else
            {
                sql = "select a.makp,sum(";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "'," + stime + ")< to_date(to_char(a.ngayrv," + stime + ")," + stime + ") or a.ngayrv is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date('" + tu + "'," + stime + "))+1 else";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")< to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(a.ngayrv," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(a.ngayrv," + stime + ")," + stime + ")-to_date('" + tu + "'," + stime + "))+ case when a.ttlucrv in (5,6,7) then 0 else 1 end else ";
                sql += " case when to_date(to_char(a.ngay," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and (to_date('" + den + "','dd/mm/yyyy')< to_date(to_char(a.ngayrv," + stime + ")," + stime + ") or a.ngayrv is null) then date_part('day',to_date('" + den + "'," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + "))+1 else ";
                sql += " case when to_date(to_char(a.ngayrv," + stime + ")," + stime + ")>= to_date('" + tu + "'," + stime + ") ";
                sql += " and to_date(to_char(a.ngayrv," + stime + ")," + stime + ")<=to_date('" + den + "'," + stime + ") then date_part('day',to_date(to_char(a.ngayrv," + stime + ")," + stime + ")-to_date(to_char(a.ngay," + stime + ")," + stime + ")) + case when a.ttlucrv in(5,6,7) then 0 else 1 end end end end end) c15";
                sql += " from " + user + ".benhanngtr a ,"+user+".btdbn b";
                sql += " where a.mabn=b.mabn and a.makp is not null ";
                sql += " and to_date(to_char(a.ngay," + stime + ")," + stime + ") <=to_date('" + den + "'," + stime + ") and to_date(to_char(coalesce(a.ngayrv,now)," + stime + ")," + stime + ") >=to_date('" + tu + "'," + stime + ")";
                sql += " group by a.makp";
                dst = m.get_data(sql);
            }            
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r1 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                if (r1 != null)
                {
                    r1["c15"] = decimal.Parse(r1["c15"].ToString()) + Decimal.Parse(r["c15"].ToString());
                    r1["c16"] = Decimal.Parse(r1["c16"].ToString()) + Decimal.Parse(r["c15"].ToString());
                }
            }
#endregion
            //bhyt
            if (makp == "99")
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end+1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end+1) as c19 ";
                }
                sql += "FROM xxx.benhancc a where  a.madoituong=1 ";
                sql += "and (" + m.for_ngay("a.ngay", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
                sql += "or " + m.for_ngay("a.ngayrv", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + "))";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data_mmyy(sql, m.DateToString("dd/MM/yyyy", m.StringToDate(tu.Substring(0, 10)).AddYears(-1)), den.Substring(0, 10), false);
            }
            else
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end+1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end+1) as c19 ";
                }
                sql += "FROM " + user + ".benhanngtr a where  a.madoituong=1 ";
                sql += "and (" + m.for_ngay("a.ngay", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + ")";
                sql += "or " + m.for_ngay("a.ngayrv", stime) + " between to_date('" + tu + "'," + stime + ") and to_date('" + den + "'," + stime + "))";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data(sql);
            }
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r1 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                if (r1 != null)
                {
                    r1["c18"] = Decimal.Parse(r1["c18"].ToString()) + Decimal.Parse(r["c18"].ToString());
                    r1["c19"] = Decimal.Parse(r1["c19"].ToString()) + Decimal.Parse(r["c19"].ToString());
                }
            }
            if (makp == "99")
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                sql += "FROM xxx.benhancc a where a.madoituong=1 ";
                sql += " and " + m.for_ngay("a.ngay", stime) + "<to_date('" + tu + "'," + stime + ")";
                sql += " and " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ")";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data_mmyy(sql, m.DateToString("dd/MM/yyyy", m.StringToDate(tu.Substring(0, 10)).AddYears(-1)), den.Substring(0, 10), false);
            }
            else
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                sql += "FROM " + user + ".benhanngtr a where a.madoituong=1 ";
                sql += " and " + m.for_ngay("a.ngay", stime) + "<to_date('" + tu + "'," + stime + ")";
                sql += " and " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ")";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data(sql);
            }
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r1 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                if (r1 != null)
                {
                    r1["c18"] = Decimal.Parse(r1["c18"].ToString()) + Decimal.Parse(r["c18"].ToString());
                    r1["c19"] = Decimal.Parse(r1["c19"].ToString()) + Decimal.Parse(r["c19"].ToString());
                }
            }
            if (makp == "99")
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                sql += "FROM xxx.benhancc a where a.madoituong=1 ";
                sql += " and " + m.for_ngay("a.ngay", stime) + "<=to_date('" + den + "'," + stime + ")";
                sql += " and a.ngayrv is null ";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data_mmyy(sql, m.DateToString("dd/MM/yyyy", m.StringToDate(tu.Substring(0, 10)).AddYears(-1)), den.Substring(0, 10), false);
            }
            else
            {
                if (time)
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or a.ngayrv>to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when a.ngay>to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when a.ngayrv>to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                else
                {
                    sql = "SELECT a.makp,sum(case when a.ngayrv is null or " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") ";
                    sql += "then " + m.for_num_ngay(den.Substring(0, 10)) + " else " + m.for_num_ngay("a.ngayrv") + " end-";
                    sql += "case when " + m.for_ngay("a.ngay", stime) + ">to_date('" + tu + "'," + stime + ")";
                    sql += "then " + m.for_num_ngay("a.ngay") + " else " + m.for_num_ngay(tu.Substring(0, 10)) + " end+1) as c18,";
                    sql += "sum(case when a.ngayrv is null then 0 else case when " + m.for_ngay("a.ngayrv", stime) + ">to_date('" + den + "'," + stime + ") then 0 else " + m.for_num_ngay("a.ngayrv") + "-" + m.for_num_ngay("a.ngay") + " end end +1) as c19 ";
                }
                sql += "FROM " + user + ".benhanngtr a where a.madoituong=1 ";
                sql += " and " + m.for_ngay("a.ngay", stime) + "<=to_date('" + den + "'," + stime + ")";
                sql += " and a.ngayrv is null ";
                if (makp != "") sql += " and a.makp='" + makp + "'";
                sql += " group by a.makp";
                dst = m.get_data(sql);
            }
            foreach (DataRow r in dst.Tables[0].Rows)
            {
                r1 = m.getrowbyid(ds.Tables[0], "makp='" + r["makp"].ToString() + "'");
                if (r1 != null)
                {
                    r1["c18"] = Decimal.Parse(r1["c18"].ToString()) + Decimal.Parse(r["c18"].ToString());
                    r1["c19"] = Decimal.Parse(r1["c19"].ToString()) + Decimal.Parse(r["c19"].ToString());
                }
            }
            return ds;
        }

        public DataSet get_bctiepbenh(LibBaocao.AccessData m, string m_mmyyyy, string m_makp)
        {
            ds = new DataSet();
            string user = m.user;
            ds.ReadXml("..\\..\\..\\xml\\m_bctiepbenh.xml");
            sql = "select to_char(b.ngay,'dd') as dd,b.makp,a.namsinh,a.phai,c.mannbo as mann,b.bnmoi,b.madoituong,0 as tt,";
            sql += "case when substr(b.tuoivao,4,1)<>'0' then substr(b.tuoivao,2,3)||'0' else b.tuoivao end as tuoivao";
            sql += " from " + user + ".btdbn a,xxx.tiepdon b," + user + ".BTDNN_BV c," + user + ".btdkp_bv d";
            sql += " where a.mabn=b.mabn and a.mann=c.mann and b.makp=d.makp";
            sql += " and to_char(b.ngay,'mmyyyy')='" + m_mmyyyy + "'";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " order by to_char(b.ngay,'dd')";
            DataTable dt = m.get_data_mmyy(sql, "01/" + m_mmyyyy.Substring(0, 2) + "/" + m_mmyyyy.Substring(2), "01/" + m_mmyyyy.Substring(0, 2) + "/" + m_mmyyyy.Substring(2), false).Tables[0];
            for (int r = 0; r < ds.Tables[0].Rows.Count; r++)
            {
                for (int c = 4; c < ds.Tables[0].Columns.Count; c++)
                {
                    sql = "dd='" + ds.Tables[0].Columns[c].ToString().Substring(1) + "'";
                    if (ds.Tables[0].Rows[r]["dk"].ToString() != "")
                        sql += " and " + ds.Tables[0].Rows[r]["dk"].ToString().Trim();
                    ds.Tables[0].Rows[r][c] = long.Parse(ds.Tables[0].Rows[r][c].ToString()) + dt.Select(sql).Length;
                }
            }
            return ds;
        }

        public DataSet get_slkhambenh(LibBaocao.AccessData m, string m_tu, string m_den, string m_makp, bool time)
        {
            string user=m.user,stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                m_tu = m_tu + " " + m.sGiobaocao;
                m_den = m_den + " " + m.sGiobaocao;
            }
            DataTable dt = m.get_data("select * from " + user + ".icd10 order by cicd10").Tables[0];
            ds = new DataSet();
            DataSet dsxml = new DataSet();
            dsxml.ReadXml("..\\..\\..\\xml\\m_slkhambenh.xml");
            DataSet dsdk = new DataSet();
            dsdk.ReadXml("..\\..\\..\\xml\\m_dkslkhambenh.xml");
            sql = "select b.maicd,a.phai,c.mann,c.mannbo,count(*) as so";
            sql += " from xxx.benhanpk b inner join " + user + ".btdbn a on b.mabn=a.mabn";
            sql += " inner join " + user + ".BTDNN_BV c on a.mann=c.mann";
            sql += " where b.maql>0";
            if (m_tu != "") sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by b.maicd,a.phai,c.mann,c.mannbo";
            sql += " order by b.maicd,a.phai,c.mann,c.mannbo";
            DataRow r1, r2, r3;
            DataRow[] dr1;
            ds = m.get_data_mmyy(sql, m_tu, m_den, false);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                r1 = m.getrowbyid(dsxml.Tables[0], "maicd='" + r["maicd"].ToString() + "'");
                if (r1 == null)
                {
                    r3 = m.getrowbyid(dt, "cicd10='" + r["maicd"].ToString() + "'");
                    if (r3 != null)
                    {
                        r2 = dsxml.Tables[0].NewRow();
                        r2["maicd"] = r["maicd"].ToString();
                        r2["chandoan"] = r3["vviet"].ToString();
                        for (int i = 1; i <= 18; i++) r2["c" + i.ToString().PadLeft(2, '0')] = 0;
                        //
                        foreach (DataRow r4 in dsdk.Tables[0].Rows)
                        {
                            sql = "maicd='" + r["maicd"].ToString() + "'";
                            sql += " and " + r4["dk"].ToString();
                            dr1 = ds.Tables[0].Select(sql);
                            for (int i = 0; i < dr1.Length; i++)
                                r2[r4["cot"].ToString()] = decimal.Parse(r2[r4["cot"].ToString()].ToString()) + decimal.Parse(dr1[i]["so"].ToString());
                        }
                        //
                        dsxml.Tables[0].Rows.Add(r2);
                    }
                }
            }
            return dsxml;
        }

        public DataSet get_btpkham(LibBaocao.AccessData m, string m_tu, string m_den, string m_makp, bool time)
        {
            string user=m.user,stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                m_tu = m_tu + " " + m.sGiobaocao;
                m_den = m_den + " " + m.sGiobaocao;
            }
            DataTable dt = m.get_data("select * from " + user + ".doituong").Tables[0];
            ds = new DataSet();
            DataSet dsxml = new DataSet();
            dsxml.ReadXml("..\\..\\..\\xml\\m_btpkham.xml");
            sql = "select b.maicd,b.madoituong,a.phai,a.namsinh,a.matt,count(*) as so";
            sql += " from " + user + ".btdbn a inner join xxx.benhanpk b on a.mabn=b.mabn";
            sql += " where b.maql>0";
            if (m_tu != "")
                sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by b.maicd,b.madoituong,a.phai,a.namsinh,a.matt";
            sql += " union all ";
            sql += "select '' as maicd,b.madoituong,a.phai,a.namsinh,a.matt,count(*) as so";
            sql += " from " + user + ".btdbn a inner join xxx.tiepdon b on a.mabn=b.mabn";
            sql += " where b.noitiepdon=0 and b.done is null";
            if (m_tu != "")
                sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by maicd,b.madoituong,a.phai,a.namsinh,a.matt";
            ds = m.get_data_mmyy(sql, m_tu, m_den, false);
            int tuoi;
            string matt = m.Mabv.Substring(0, 3);
            DataRow r2;
            foreach (DataRow r in dsxml.Tables[0].Select("loai=0"))
            {
                foreach (DataRow r1 in ds.Tables[0].Select(r["DK"].ToString()))
                {
                    r2 = m.getrowbyid(dt, "madoituong=" + int.Parse(r1["madoituong"].ToString()));
                    if (r2 != null)
                    {
                        tuoi = int.Parse(m_tu.Substring(6, 4)) - int.Parse(r1["namsinh"].ToString());
                        if (r1["matt"].ToString() == matt) r["d01"] = decimal.Parse(r["d01"].ToString()) + decimal.Parse(r1["so"].ToString());
                        else r["d011"] = decimal.Parse(r["d011"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (r1["madoituong"].ToString() == "1") r["d02"] = decimal.Parse(r["d02"].ToString()) + decimal.Parse(r1["so"].ToString());
                        else if (r2["mien"].ToString() == "1")
                        {
                            if (r1["matt"].ToString() == matt) r["d04"] = decimal.Parse(r["d04"].ToString()) + decimal.Parse(r1["so"].ToString());
                            else r["d041"] = decimal.Parse(r["d041"].ToString()) + decimal.Parse(r1["so"].ToString());
                            if (tuoi < 6)
                            {
                                if (r1["matt"].ToString() == matt) r["d08"] = decimal.Parse(r["d08"].ToString()) + decimal.Parse(r1["so"].ToString());
                                else r["d09"] = decimal.Parse(r["d09"].ToString()) + decimal.Parse(r1["so"].ToString());
                            }
                        }
                        else r["d03"] = decimal.Parse(r["d03"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (tuoi < 15)
                        {
                            if (r1["matt"].ToString() == matt) r["d05"] = decimal.Parse(r["d05"].ToString()) + decimal.Parse(r1["so"].ToString());
                            else r["d051"] = decimal.Parse(r["d051"].ToString()) + decimal.Parse(r1["so"].ToString());
                        }
                        if (tuoi < 6)
                        {
                            if (r1["matt"].ToString() == matt) r["d06"] = decimal.Parse(r["d06"].ToString()) + decimal.Parse(r1["so"].ToString());
                            else r["d061"] = decimal.Parse(r["d061"].ToString()) + decimal.Parse(r1["so"].ToString());
                        }
                        if (r1["phai"].ToString() == "1") r["d07"] = decimal.Parse(r["d07"].ToString()) + decimal.Parse(r1["so"].ToString());
                    }
                }
            }
            return dsxml;
        }

        public DataSet get_btpkham_nguoi(LibBaocao.AccessData m, string m_tu, string m_den, string m_makp, bool time)
        {
            string user=m.user,stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                m_tu = m_tu + " " + m.sGiobaocao;
                m_den = m_den + " " + m.sGiobaocao;
            }
            int nam = int.Parse(m_tu.Substring(6, 4));
            string matt = m.Mabv.Substring(0, 3);
            sql = "select a.mabn,sum(1) as c01,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<15 and a.matt='" + matt + "' then 1 else 0 end) as c02,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<15 and a.matt<>'" + matt + "' then 1 else 0 end) as c03,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<6 and a.matt='" + matt + "' then 1 else 0 end) as c04,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<6 and a.matt<>'" + matt + "' then 1 else 0 end) as c05,";
            sql += "sum(case when a.phai=1 then 1 else 0 end) as c06 ";
            sql += "from " + user + ".btdbn a inner join xxx.benhanpk b on a.mabn=b.mabn";
            sql += " where ";
            if (m_tu != "")
                sql += " " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";

            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by a.mabn";
            sql += " union all ";
            sql += " select a.mabn,sum(1) as c01,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<15 and a.matt='" + matt + "' then 1 else 0 end) as c02,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<15 and a.matt<>'" + matt + "' then 1 else 0 end) as c03,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<6 and a.matt='" + matt + "' then 1 else 0 end) as c04,";
            sql += "sum(case when " + nam + "-to_number(a.namsinh,'0000')<6 and a.matt<>'" + matt + "' then 1 else 0 end) as c05,";
            sql += "sum(case when a.phai=1 then 1 else 0 end) as c06 ";
            sql += " from " + user + ".btdbn a inner join xxx.tiepdon b on a.mabn=b.mabn";
            sql += " where b.noitiepdon=0 and b.done is null";
            if (m_tu != "")
                sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by a.mabn";
            return m.get_data_mmyy(sql, m_tu, m_den, false);
        }

        public DataSet get_btngtru(LibBaocao.AccessData m, string m_tu, string m_den, string m_makp, bool time)
        {
            string user=m.user,stime = (time) ? "'" + m.f_ngaygio + "'" : "'" + m.f_ngay + "'";
            if (time)
            {
                m_tu = m_tu + " " + m.sGiobaocao;
                m_den = m_den + " " + m.sGiobaocao;
            }
            ds = new DataSet();
            DataSet dsxml = new DataSet();
            dsxml.ReadXml("..\\..\\..\\xml\\m_btpkham.xml");
            sql = "select b.maicd,a.namsinh,'3' as loaiba,case when b.ttlucrv=0 then 1 else b.ttlucrv end as ttlucrv,count(*) as so";
            sql += " from " + user + ".btdbn a inner join xxx.benhanpk b on a.mabn=b.mabn";
            sql += " where b.mangtr<>0 ";
            if (m_tu != "")
                sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by b.maicd,a.namsinh,loaiba,ttlucrv";
            sql += " union all ";
            sql += " select b.maicd,a.namsinh,'2' as loaiba,case when b.ttlucrv=0 then 1 else b.ttlucrv end as ttlucrv,count(*) as so";
            sql += " from " + user + ".btdbn a inner join " + user + ".benhanngtr b on a.mabn=b.mabn";
            sql += " where b.maql>0 ";
            if (m_tu != "")
                sql += " and " + m.for_ngay("b.ngay", stime) + " between to_date('" + m_tu + "'," + stime + ") and to_date('" + m_den + "'," + stime + ")";
            if (m_makp != "") sql += " and b.makp in (" + m_makp + ")";
            sql += " group by b.maicd,a.namsinh,loaiba,ttlucrv";
            ds = m.get_data_mmyy(sql, m_tu.Substring(0, 10), m_den.Substring(0, 10), false);
            int tuoi;
            foreach (DataRow r in dsxml.Tables[0].Rows)
            {
                foreach (DataRow r1 in ds.Tables[0].Select(r["DK"].ToString()))
                {
                    tuoi = int.Parse(m_tu.Substring(6, 4)) - int.Parse(r1["namsinh"].ToString());
                    if (r1["loaiba"].ToString() == "2")
                    {
                        r["d01"] = decimal.Parse(r["d01"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (tuoi < 15) r["d02"] = decimal.Parse(r["d02"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (tuoi < 6) r["d03"] = decimal.Parse(r["d03"].ToString()) + decimal.Parse(r1["so"].ToString());
                    }
                    if (r1["loaiba"].ToString() == "3")
                    {
                        r["d04"] = decimal.Parse(r["d04"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (tuoi < 15) r["d05"] = decimal.Parse(r["d05"].ToString()) + decimal.Parse(r1["so"].ToString());
                        if (tuoi < 6) r["d06"] = decimal.Parse(r["d06"].ToString()) + decimal.Parse(r1["so"].ToString());
                    }
                    //else if (r1["loaiba"].ToString()=="2")
                    //{
                    r["d07"] = decimal.Parse(r["d07"].ToString()) + decimal.Parse(r1["so"].ToString());
                    if (tuoi < 15) r["d08"] = decimal.Parse(r["d08"].ToString()) + decimal.Parse(r1["so"].ToString());
                    if (tuoi < 6) r["d09"] = decimal.Parse(r["d09"].ToString()) + decimal.Parse(r1["so"].ToString());
                    //}
                }
            }
            return dsxml;
        }
    }
}
