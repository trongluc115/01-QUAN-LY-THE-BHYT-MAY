using System;
using System.Collections.Generic;
using System.Text;
using Entity;

namespace Data
{
    public class CBenhNhanDAO
    {
        #region khai bao bien
        string sql = "";
        #endregion
        #region get
        #endregion
        #region insert
        public void Insert(CBenhNhan bn)
        { 
            sql="Insert into btdbn(mabn,hoten,namsinh,thon,phai)";
            sql += " values('" + bn.MaBN + "',N'" + bn.HoTen + "','" + bn.NamSinh + "',N'" + bn.DiaChi + "',"+bn.GioiTinh+")";
            CConnection data = new CConnection();
            data.setData(sql);
        }
        #endregion
        #region delete
        public void Delete(string MaBN)
        {
            sql = "Delete btdbn where mabn='" + MaBN + "'";
            CConnection data = new CConnection();
            data.setData(sql);
        }
         #endregion
    }
}
