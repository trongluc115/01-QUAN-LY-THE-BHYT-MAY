using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using LibBaocao;
using System.Data;
namespace DataOracle
{
    public class CThuocBHYTNgoaiTru
    {
        #region khai bao bien
      
        AccessData data;
        DataSet ds;
        string sql = "";
        #endregion
        public CThuocBHYTNgoaiTru()
        {
            data = new AccessData();
            ds = new DataSet();
        }
        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }
       public DataSet f_loadBaoCaoMau20(string tungay, string denngay, string MaKho)
        {
            if (MaKho.Length == 0)
            {
                sql = "select x.mabd,dm.ten,dm.hamluong,round(td.giamua,2) dongia,round(x.soluong,2) soluong,";
                sql+="round(td.giamua,2)*round(x.soluong,2) sotien,k.ten tenkho,dm.tenhc,dm.dang,dm.sodk QK05,";
                sql += "nc.ten tennuoc,h.ten tenhang from xxxxx.bhytthuoc x,xxxxx.d_theodoi td,d_dmbd dm,";
                sql+="d_dmkho k,d_dmnuoc nc,d_dmhang h,xxxxx.bhytkb kb where x.mabd=dm.id and k.id=x.makho ";
                sql+="and nc.id=dm.manuoc and dm.mahang=h.id and x.sttt=td.id and kb.id=x.id and kb.loaiba=3";
                sql+=" and to_char(x.ngayud,'yyyymmdd')>='"+for_ngay_yyyymmdd(tungay)+"' and to_char(x.ngayud,'yyyymmdd')<='"+for_ngay_yyyymmdd(denngay)+"'";
            }
            else
            {
                sql = "";
            }
          
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data_mmyy(sql,tungay,denngay,false);


            }
            catch { }

            return dset;
        }
        
    }
}
