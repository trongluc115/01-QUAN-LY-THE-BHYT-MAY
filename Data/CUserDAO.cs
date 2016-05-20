using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LibBaocao;
using Entity;

namespace Data
{
    public class CUserDAO
    {
        public bool KiemTraDangNhap(string user,string password)
        {
                 string sql;
                bool result = false;
                DataSet ds = new DataSet();
                try
                {
                    sql = "select * from m_user where [username]='"+user+"' and [password]='"+password+"'";

                    CConnection data = new CConnection();
                    ds = data.getData(sql);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }

                }
                catch { }
                return result;
          
        
        }
    }
}
