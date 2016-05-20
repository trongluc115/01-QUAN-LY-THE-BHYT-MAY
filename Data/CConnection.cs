using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Entity;

namespace Data
{
    public class CConnection
    {
        #region khai bao bien
        SqlConnection conn;
        SqlCommand cmd;
        string String_Connection = "data source=192.168.5.19; user id=sa;password=enter; database=medi115";

        string sql = "";
        #endregion
        #region phuong thuc
        public CConnection()
        {
            try
            {
                CXml xml = new CXml();
            String_Connection = xml.ReadXML(2, "config.xml");
            }catch{}
        
        }
        public CConnection(int item)
        {
            try
            {
                CXml xml = new CXml();
                String_Connection = xml.ReadXML(item, "config.xml");
            }
            catch { }

        }
        public int setData(string sqlExcute)
        {
            int kq= 0;
            try
            {
                conn = new SqlConnection(String_Connection);
                conn.Open();
                cmd = new SqlCommand(sqlExcute, conn);
                
                kq=cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
             
            }
            catch { }
            return kq;
        }
        public DataSet getData(string sql)
        {
            DataSet ds=new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                conn = new SqlConnection(String_Connection);
                conn.Open();
                adapter=new SqlDataAdapter(sql,conn);
                adapter.Fill(ds);
                adapter.Dispose();
                conn.Dispose();
                conn.Close();
            }
            catch { }
            return ds;
        }
        #endregion

    }
}
