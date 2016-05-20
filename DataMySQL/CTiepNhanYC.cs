using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Entity;
namespace DataMySQL
{
    public class CTiepNhanYC
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

                sql = " select ID,MaBN,Hoten,LoaiYeuCau,NoiDung,NguoiYeuCau,KhoaPhong, NguoiNhanYC,ThoiGianYC, ThoiGianKT,NgayUD,HinhThuc,TinhTrang  ";
                sql += "from tiepnhanyc";
                sql += " where id='"+ID+"'";
                

                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSachXoaChiDinhFromUser(string User,DateTime ngay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,a.stt,a.MaBN,b.ten ,a.Mavp,a.Ten tenvp,a.soluong,a.ngaythuchien,'-',a.makp,a.id,a.stt  ";
                sql += "from v_chidinh a,btdbn b";
                sql += " where a.userid='" + User + "' and date(a.ngaythuchien)='"+f_formatDate(ngay)+"' ";
                sql += " and a.mabn=b.id ";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSachXoaChiDinhFromMaBN(string MaBN, DateTime ngay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,a.stt,a.MaBN,b.ten ,a.Mavp,a.Ten tenvp,a.soluong,a.ngaythuchien,'-',a.makp,a.id,a.stt  ";
                sql += "from v_chidinh a,btdbn b";
                sql += " where a.MaBN='" + MaBN + "' and month(a.ngaythuchien)='" + ngay.Month + "' and year(a.ngaythuchien)='" + ngay.Year + "'";
                sql += " and a.mabn=b.id ";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }

        public DataSet get_NoiDungXoaChiDinhFromIDYC(string IDYC)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,a.stt,a.MaBN,b.ten ,a.Mavp,a.Ten tenvp,a.soluong,DATE_FORMAT(a.ngaythuchien, '%d-%m-%Y ') ngaythuchien,'-',a.makp,a.id,a.stt,a.tennguoixoa ,a.ngaychidinh";
                sql += " from v_chidinh a,btdbn b";
                sql += " where a.STT='"+IDYC+"' ";
                sql += " and a.mabn=b.id ";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSachXoaChiDinhFromUser( DateTime ngay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,stt,MaBN,'-',Mavp,Ten tenvp,soluong,ngaythuchien,'-',makp,id  ";
                sql += "from v_chidinh ";
                sql += " where  date(ngaythuchien)='" + f_formatDate(ngay) + "'";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhSachXoaChiDinhFromUser(string MaBN)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,stt,MaBN,'-',Mavp,Ten tenvp,soluong,ngaythuchien,'-',makp,id  ";
                sql += "from v_chidinh ";
                sql += " where  maBN='" + MaBN + "'";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        
        public DataSet get_DanhSachDuyetXoaCLS(DateTime tungay,DateTime denngay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select 0,a.stt,a.MaBN,b.ten ,a.Mavp,a.Ten tenvp,a.soluong,DATE_FORMAT(a.ngaythuchien, '%d-%m-%Y ') ngaythuchien,'-',a.makp,a.id,a.stt,a.tennguoixoa ,a.ngaychidinh";
                sql += " from v_chidinh a,btdbn b";
                sql += " where  a.mabn=b.id ";
                sql += " and date(a.ngaythuchien)>='" + f_formatDate(tungay) + "' and date(a.ngaythuchien)<='" + f_formatDate(denngay) + "'";

                CConnection data = new CConnection(3);
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public string get_RightDangNhap(string ID,string pass)
        {
            DataSet ds = new DataSet();
            string result = "-1";
            try
            {

                sql = " select ID ";
                sql += " from dlogin";
                sql += " where ten='" + ID + "' and matkhau='"+pass+"'";


                CConnection data = new CConnection(3);
                ds = data.getData(sql);
                result = ds.Tables[0].Rows[0]["ID"].ToString();
            }
            catch { }
            return result;
        }
        public string get_RightDangNhap(string ID)
        {
            DataSet ds = new DataSet();
            string result = "-1";
            try
            {

                sql = " select _Right ";
                sql += " from dlogin";
                sql += " where ID='" + ID + "'" ;


                CConnection data = new CConnection(3);
                ds = data.getData(sql);
                result = ds.Tables[0].Rows[0]["_Right"].ToString();
            }
            catch { }
            return result;
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

                CConnection data = new CConnection(3);
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

                sql = " select a.id id,DATE_FORMAT(a.ThoiGianYC, '%d-%m-%Y %h:%i:%s %p') ThoiGianYC,a.MaBN,a.HoTen,b.Ten KhoaPhong,a.NoiDung NoiDung,a.NguoiYeuCau,c.Ten NguoiNhanYC,d.Ten LoaiYeuCau,e.Ten tinhtrang,a.ThoiGianKT  ";
                sql += "from tiepnhanyc a,dmkhoaphong b,dmuser c, dmloaiyeucau d,dmtinhtrang e";
                sql += " where a.KhoaPhong=b.ID and a.NguoiNhanYC=c.ID and a.LoaiYeuCau=d.ID and a.TinhTrang=e.ID";
                sql += " and (a.mabn like '%" + timkiem + "%' or a.hoten like '%" + timkiem + "%') and ";
                sql += " date(thoigianyc)>='" + f_formatDate(TuNgay) + "' and date(thoigianyc)<='" + f_formatDate(DenNgay) + "'";
                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
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
                CConnection data = new CConnection(3);
                ds = data.getData(sql);
                int value = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                strID = value.ToString();
            }
            catch { }
            return strID;
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
        public int InsertDanhMuc(string TableName, string ID, string Ten, string Enable)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into " + TableName + "(ID,Ten,Enable) values ('" + ID + "',N'" + Ten + "','" + Enable + "')";

                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertDanhMuc_btdbn(string MaBN, string Ten,string NamSinh,string DiaChi)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into btdbn(MaBN,Hoten,Namsinh,DiaChi) values ('" + MaBN + "',N'" + Ten + "','" + NamSinh + "',N'"+DiaChi+"')";

                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertDanhMuc_v_chidinh(string ID,string STT,string MaBN, string Loai,string mavp,string tenvp,string makp,DateTime ngaychidinh,string nguoixoa,string Userid,string soluong)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into v_chidinh(ID,STT,MaBN,loai,mavp,ten,makp,ngaythuchien,tennguoixoa,userid,soluong) values ('" + ID + "','" + STT + "','" + MaBN + "','" + Loai + "','"+mavp+"',N'" + tenvp + "','" + makp + "','" + string.Format("{0:yyyy/MM/dd}",ngaychidinh) + "',N'"+nguoixoa+"','"+Userid+"',"+soluong+")";

                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertDanhMuc_v_chidinh(string ID, string STT, string MaBN, string Loai, string mavp, string tenvp, string makp, DateTime ngayxoa, string nguoixoa, string Userid,string ngayCD,string soluong)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into v_chidinh(ID,STT,MaBN,loai,mavp,ten,makp,ngaythuchien,tennguoixoa,userid,ngaychidinh,soluong) values ('" + ID + "','" + STT + "','" + MaBN + "','" + Loai + "','" + mavp + "',N'" + tenvp + "','" + makp + "','" + string.Format("{0:yyyy/MM/dd}", ngayxoa) + "',N'" + nguoixoa + "','" + Userid + "','"+ngayCD+"',"+soluong+")";

                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        private string f_formatDate(DateTime d)
        {
            return string.Format("{0:yyyy-MM-dd}", d);
        }
       
       
        
       
        public int InsertTiepNhanYC(string ID, string MaBN, string Hoten, string LoaiYC, string NoiDung, string NguoiYC, string KhoaPhong, string NguoiNhan,string HinhThuc,string TinhTrang,DateTime ngay)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into tiepnhanyc(ID,MaBN,Hoten,LoaiYeuCau,NoiDung,NguoiYeuCau,KhoaPhong, NguoiNhanYC,ThoiGianYC, ThoiGianKT,NgayUD,HinhThuc,TinhTrang) ";
                sql += " values ('" + ID + "','" + MaBN + "',N'" + Hoten + "','" + LoaiYC + "','" + NoiDung + "',N'" + NguoiYC + "','" + KhoaPhong + "'," + NguoiNhan + ",'"+FormatYYYY_MM_DD_hh_ii(ngay)+"',NOW(),NOW(),'"+HinhThuc+"','"+TinhTrang+"')";

                CConnection data = new CConnection(3);
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

                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }

        

        public int UpdateTiepNhan(string ID, string MaBN, string Hoten, string LoaiYC, string NoiDung, string NguoiYC, string KhoaPhong, string NguoiNhan, string HinhThuc, string TinhTrang,DateTime ngay)
        {
            int roweffect = 0;
            try
            {
                sql = "Update TiepNhanYC set MaBN='" + MaBN + "',HoTen=N'" + Hoten + "',KhoaPhong='" + KhoaPhong + "',LoaiYeucau='" + LoaiYC + "'";
                sql += ",hinhthuc='" + HinhThuc + "',NguoiYeuCau=N'" + NguoiYC + "',NguoiNhanYC=" +NguoiNhan ;
                sql += ",tinhtrang='" + TinhTrang + "',noidung=N'" + NoiDung + "',thoigianyc='"+FormatYYYY_MM_DD_hh_ii(ngay)+"'";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateNoiDung(string ID,string NoiDung)
        {
            int roweffect = 0;
            try
            {
                sql = "Update TiepNhanYC set Noidung=N'"+NoiDung+"' ";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection(3);
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
                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int DeleteDanhMuc(string TableName, string ID,string   STT)
        {

            int roweffect = 0;
            try
            {

                sql = "Delete from " + TableName + " where ID='" + ID + "' and STT='"+STT+"'";
                CConnection data = new CConnection(3);
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion*/
    }
}
