using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Entity;
using LibBaocao;
namespace DataOracle
{
    public class CBenhNhanOracle
    {
        #region khai bao bien
        string sql = "";
        AccessData data;

        #endregion
        public CBenhNhanOracle()
        {
            data = new AccessData();
           
        }
        #region get

        private string prepairstring(string s)
        {
            return "";

        }
        public CBenhNhan getBenhNhan(string MaBN)
        {
            CBenhNhan bn = new CBenhNhan();
            sql = "select bn.hoten,bn.namsinh,bn.phai,bn.sonha,bn.thon,tt.tentt, q.tenquan, px.tenpxa, bn.cholam ";
            sql+="from btdbn bn,btdtt tt,btdquan q,btdpxa px  ";
            sql += " where bn.mabn='"+MaBN+"' and bn.matt=tt.matt and bn.maqu=q.maqu and bn.maphuongxa=px.maphuongxa";
           
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                bn.MaBN = MaBN;
                bn.HoTen = dset.Tables[0].Rows[0]["HOTEN"].ToString();
                bn.NamSinh = int.Parse( dset.Tables[0].Rows[0]["NAMSINH"].ToString());
                bn.GioiTinh = int.Parse(dset.Tables[0].Rows[0]["PHAI"].ToString());
                bn.DiaChi = dset.Tables[0].Rows[0]["SONHA"].ToString() + " " + dset.Tables[0].Rows[0]["THON"].ToString() + ", " + dset.Tables[0].Rows[0]["TENPXA"].ToString() + ", " + dset.Tables[0].Rows[0]["TENQUAN"].ToString() + ", " + dset.Tables[0].Rows[0]["TENTT"].ToString();
         
            }
            catch { }
            return bn;

        }
       
        #endregion
        #region insert
        public void Insert(CBenhNhan bn)
        {
            
        }
        #endregion
        #region delete
        public void Delete(string MaBN)
        {
            
        }
        #endregion
    }
}
