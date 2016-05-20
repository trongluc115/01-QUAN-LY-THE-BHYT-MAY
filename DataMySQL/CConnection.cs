using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using Entity;

namespace DataMySQL
{
    public class CConnection
    {
        #region khai bao bien
        MySqlConnection conn;
        MySqlCommand cmd;
        string String_Connection = "server=192.168.5.19; uid=root;password=enter; database=DBQLMay";

        string sql = "";
        #endregion
        #region phuong thuc
        public CConnection()
        {
            try
            {
                CXml xml = new CXml();
            String_Connection = xml.ReadXML(2, "configMySQL.xml");
            }catch{}
        
        }
        public CConnection(int item)
        {
            try
            {
                CXml xml = new CXml();
                String_Connection = xml.ReadXML(item, "configMySQL.xml");
            }
            catch { }

        }
        public int setData(string sqlExcute)
        {
            int kq= 0;
            try
            {
                conn = new MySqlConnection(String_Connection);
                conn.Open();
                cmd = new MySqlCommand(sqlExcute, conn);

                kq = cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
             
            }
            catch { }
            return kq;
        }
        public DataSet getData(string sql)
        {
            DataSet ds=new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            try
            {
                conn = new MySqlConnection(String_Connection);
                conn.Open();
                adapter=new MySqlDataAdapter(sql,conn);
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
