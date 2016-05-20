using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataMySQL
{
    public class CQLTheBHYT
    {
        #region khai bao bien
        string sql = "";
        #endregion
        #region get
        public DataSet get_TiepNhanYCFromID(string ID)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select ID,MaBN,Hoten,LoaiYeuCau,NoiDung,NguoiYeuCau,KhoaPhong, NguoiNhanYC,ThoiGianYC, ThoiGianKT,NgayUD,HinhThuc,TinhTrang,HSD,TraiTuyen  ";
                sql += "from tiepnhanyc";
                sql += " where id='"+ID+"'";
                

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSach(string timkiem)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id,a.ThoiGianYC,a.MaBN,a.HoTen,b.Ten,a.NoiDung,a.NguoiYeuCau,c.Ten NguoiNhanYC,d.Ten LoaiYeuCau,e.Ten tinhtrang,a.ThoiGianKT  ";
                sql += "from tiepnhanyc a,dmkhoaphong b,dmuser c, dmloaiyeucau d,dmtinhtrang e";
                sql += " where a.KhoaPhong=b.ID and a.NguoiNhanYC=c.ID and a.LoaiYeuCau=d.ID and a.TinhTrang=e.ID";
                sql += " and (a.mabn like '%" + timkiem + "%' or a.hoten like '%" + timkiem + "%')";

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSach(string timkiem,DateTime TuNgay,DateTime DenNgay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id id,a.Done,DATE_FORMAT(a.ThoiGianYC, '%d-%m-%Y %H:%i') thoigianyc,a.MaBN,a.HoTen,b.Ten KhoaPhong,a.NoiDung NoiDung,a.NguoiYeuCau,c.Ten NguoiNhanYC,d.Ten LoaiYeuCau,e.Ten tinhtrang,a.ThoiGianKT  ";
                sql += "from tiepnhanyc a,dmkhoaphong b,dmuser c, dmloaiyeucau d,dmtinhtrang e";
                sql += " where a.KhoaPhong=b.ID and a.NguoiNhanYC=c.ID and a.LoaiYeuCau=d.ID and a.TinhTrang=e.ID";
                sql += " and (a.mabn like '%" + timkiem + "%' or a.hoten like '%" + timkiem + "%') and ";
                sql += " date(thoigianyc)>='" + f_formatDate(TuNgay) + "' and date(thoigianyc)<='" + f_formatDate(DenNgay) + "'";
                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSach(string timkiem, DateTime TuNgay, DateTime DenNgay,string done)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id id,a.Done,DATE_FORMAT(a.ThoiGianYC, '%d-%m-%Y %H:%i') thoigianyc,a.MaBN,a.HoTen,b.Ten KhoaPhong,a.NoiDung NoiDung,a.NguoiYeuCau SoThe,c.Ten NguoiNhanYC,d.Ten LoaiYeuCau,e.Ten tinhtrang,a.ThoiGianKT ,0 sotien,HSD Denngay,traituyen ";
                sql += "from tiepnhanyc a,dmkhoaphong b,dmuser c, dmloaiyeucau d,dmtinhtrang e";
                sql += " where a.KhoaPhong=b.ID and a.NguoiNhanYC=c.ID and a.LoaiYeuCau=d.ID and a.TinhTrang=e.ID";
                sql += " and (a.mabn like '%" + timkiem + "%' or a.hoten like '%" + timkiem + "%') and ";
                sql += " date(thoigianyc)>='" + f_formatDate(TuNgay) + "' and date(thoigianyc)<='" + f_formatDate(DenNgay) + "' and (a.done is null or a.done=0)";
                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSachFrom_Ngay_Dot(string timkiem, DateTime Ngay, string Dot)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id id,a.done,DATE_FORMAT(a.ThoiGianYC, '%d-%m-%Y %H:%i') thoigianyc,a.MaBN,a.HoTen,b.Ten KhoaPhong,a.NoiDung NoiDung,a.NguoiYeuCau,c.Ten NguoiNhanYC,d.Ten LoaiYeuCau,e.Ten tinhtrang,a.ThoiGianKT  ";
                sql += "from tiepnhanyc a,dmkhoaphong b,dmuser c, dmloaiyeucau d,dmtinhtrang e";
                sql += " where a.KhoaPhong=b.ID and a.NguoiNhanYC=c.ID and a.LoaiYeuCau=d.ID and a.TinhTrang=e.ID";
                sql += " and (a.mabn like '%" + timkiem + "%' or a.hoten like '%" + timkiem + "%') and ";
                sql += " date(thoigianyc)='" + f_formatDate(Ngay) + "' and a.loaiyeucau='" + Dot + "'";
                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
       
      
        
        public DataSet get_DanhMuc(string table)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable from " + table;

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhMucCT(string table)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable,LoaiMay from " + table;

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhMucCT(string table, string loaimay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable,LoaiMay from " + table + " where loaimay='" + loaimay + "' and Enable='1' order by ten";

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhMuc(string table, string Enable)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable from " + table + " where Enable='" + Enable + "' order by ten";

                CConnection data = new CConnection(4);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
      
       
        public string getID(string tablename)
        {
            string strID = "1";
            DataSet ds = new DataSet();
            try
            {
                string sql = "select Max(ID+1) from " + tablename;
                CConnection data = new CConnection(4);
                ds = data.getData(sql);
                int value = int.Parse(ds.Tables[0].Rows[0][0].ToString()) ;
                strID = value.ToString();
            }
            catch { }
            return strID;
        }

        private string FormatYYYY_MM_DD_hh_ii(DateTime Ngay)
        {
            string s = "";
            try
            {
                s = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Ngay);
            }
            catch { }
            return s;
        }

        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }

        #endregion
        #region insert
        private string FormatYYYY_MM_DD(DateTime Ngay)
        {
            string s = "";
            try
            {
                s = string.Format("{0:yyyy-MM-dd}", Ngay);
            }
            catch { }
            return s;
        }
        public int InsertDanhMuc(string TableName, string ID, string Ten, string Enable)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into " + TableName + "(ID,Ten,Enable) values ('" + ID + "',N'" + Ten + "','" + Enable + "')";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertDanhMuc(string TableName, string ID, string Ten, string Enable, string loai)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into " + TableName + "(ID,Ten,Enable,loaimay) values ('" + ID + "',N'" + Ten + "','" + Enable + "','" + loai + "')";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        private string f_formatDate(DateTime d)
        {
            return string.Format("{0:yyyy-MM-dd}", d);
        }
       
       
        
       
        public int InsertTiepNhanYC(string ID, string MaBN, string Hoten, string LoaiYC, string NoiDung, string NguoiYC, string KhoaPhong, string NguoiNhan,string HinhThuc,string TinhTrang,string HSD,string traituyen)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into tiepnhanyc(ID,MaBN,Hoten,LoaiYeuCau,NoiDung,NguoiYeuCau,KhoaPhong, NguoiNhanYC,ThoiGianYC, ThoiGianKT,NgayUD,HinhThuc,TinhTrang,HSD,TraiTuyen) ";
                sql += " values ('" + ID + "','" + MaBN + "',N'" + Hoten + "','" + LoaiYC + "','" + NoiDung + "',N'" + NguoiYC + "','" + KhoaPhong + "'," + NguoiNhan + ",NOW(),NOW(),NOW(),'"+HinhThuc+"','"+TinhTrang+"','"+HSD+"','"+traituyen+"')";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion
        #region Update
        public int UpdateDanhMuc(string TableName, string ID, string Ten, string Enable)
        {
            int roweffect = 0;
            try
            {
                sql = "Update " + TableName + " set Ten=N'" + Ten + "',Enable='" + Enable + "' where ID='" + ID + "'";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateDanhMuc(string TableName, string ID, string Ten, string Enable, string loaimay)
        {
            int roweffect = 0;
            try
            {
                sql = "Update " + TableName + " set Ten=N'" + Ten + "',Enable='" + Enable + "', loaimay='" + loaimay + "' where ID='" + ID + "'";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateDanhMuc(string TableName, string ID, int Enable,bool tatca )
        {
            int roweffect = 0;
            try
            {
                sql = "Update " + TableName + " set Enable='" + Enable + "' where 1=1";
                if (tatca == false)
                    sql += " and ID='" + ID + "'";

                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }


        public int UpdateTiepNhan(string ID, string MaBN, string Hoten, string LoaiYC, string NoiDung, string NguoiYC, string KhoaPhong, string NguoiNhan, string HinhThuc, string TinhTrang)
        {
            int roweffect = 0;
            try
            {
                sql = "Update TiepNhanYC set MaBN='" + MaBN + "',HoTen=N'" + Hoten + "',KhoaPhong='" + KhoaPhong + "',LoaiYeucau='" + LoaiYC + "'";
                sql += ",hinhthuc='" + HinhThuc + "',NguoiYeuCau=N'" + NguoiYC + "',NguoiNhanYC=" +NguoiNhan ;
                sql += ",tinhtrang='" + TinhTrang + "',noidung=N'" + NoiDung + "' ";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }

        public int UpdateTiepNhan_Done(string ID, string done,DateTime thoigiantra)
        {
            int roweffect = 0;
            try
            {
                sql = "Update TiepNhanYC set done=" + done + ",thoigiankt='"+FormatYYYY_MM_DD_hh_ii(thoigiantra)+"' ";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
       
        #endregion
        #region delete
        public int DeleteDanhMuc(string TableName, string ID)
        {

            int roweffect = 0;
            try
            {

                sql = "Delete from " + TableName + " where ID='" + ID + "'";
                CConnection data = new CConnection(4);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion*/
    }
}
