using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DataMySQL
{
    public class CMay
    {
        #region khai bao bien
        string sql = "";
        #endregion
        #region get
        public DataSet get_DSMay(string timkiem)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id,a.mamay,a.ten,b.ten,c.ten,a.Vitri,e.Ten,f.Ten,a.DonGia,g.ten,a.ngaybdsd,a.kiemke ";
                sql+="from may a,dmquocgia b,dmkhoaphong c,dmloaimay e,dmloaichitiet f,dmtinhtrang g ";
                sql+=" where a.NuocSX=b.ID and a.MaKP=c.ID and a.MaLoai=e.ID and a.tinhtrang=g.id and a.LoaiChiTiet=f.ID ";
                sql+="and (a.mamay like '%"+timkiem+"%' or a.ten like '%"+timkiem+"%')" ;
                
                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        

        public DataSet get_BCBaoTri(string s_tungay,string s_denngay)
        {
            DataSet ds = new DataSet();

            try
            {

                string sql = "select DATE_FORMAT(bt.Ngay, '%d-%m-%Y') ngaybaotri,kp.Ten TenKP,m.MaMay MAMAY,m.Ten TENMAY,M.Vitri Vitri ,bt.Noidung from baotri bt ";
                sql += "  join may m on bt.MaMay=m.ID ";
                sql += "  join dmkhoaphong kp on m.MaKP=kp.ID ";
                sql += " where bt.Ngay BETWEEN '{tungay}' and '{denngay}' ";
                sql = sql.Replace("{tungay}", s_tungay);
                sql = sql.Replace("{denngay}", s_denngay);

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DSMay(string timkiem,string khoaphong)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id,a.mamay,a.ten,b.ten,c.ten,a.Vitri,e.Ten,f.Ten,a.DonGia,g.ten,a.ngaybdsd,a.kiemke ";
                sql += "from may a,dmquocgia b,dmkhoaphong c,dmloaimay e,dmloaichitiet f,dmtinhtrang g ";
                sql += " where a.NuocSX=b.ID and a.MaKP=c.ID and a.MaLoai=e.ID and a.tinhtrang=g.id and a.LoaiChiTiet=f.ID ";
                sql += "and (a.mamay like '%" + timkiem + "%' or a.ten like '%" + timkiem + "%') and a.makp='"+khoaphong+"' order by mamay";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_KiemKe(string s_tungay,string s_denngay,string loaimay, string khoaphong)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id id,a.mamay mamay,a.ten tenmay,b.ten tennuoc,c.ten tenkhoaphong,a.Vitri vitri, ";
                sql +=" e.Ten loaimay ,f.Ten loaichitiet,a.DonGia dongia,g.ten tinhtrang,a.ngaybdsd ngaybdsd";
                sql +=" from may a";
                sql +=" join dmquocgia b on a.NuocSX=b.ID ";
                sql +=" join dmkhoaphong c on a.MaKP=c.ID ";
                sql +=" join dmloaimay e on a.MaLoai=e.ID ";
                sql +=" join dmloaichitiet f on a.LoaiChiTiet=f.ID ";
                sql +=" join dmtinhtrang g on a.tinhtrang=g.id ";
                sql += " where 1 ";
                if (loaimay.Length > 0)
                { 
                    sql+=" and a.maloai in ("+loaimay+") ";
                }
                if (khoaphong.Length > 0)
                {
                    sql += " and a.makp in (" + khoaphong + ") ";
                }
                    

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public string get_DSMayThongTin(string timkiem, string khoaphong)
        {
            DataSet ds = new DataSet();
            string strKQ = "";
            try
            {
                if (khoaphong != "-1")
                {
                    sql = " select c.ten , e.Ten , count(*) ";
                    sql += "from may a,dmkhoaphong c,dmloaimay e ";
                    sql += " where a.MaKP=c.ID and a.MaLoai=e.ID  ";
                    sql += "and (a.mamay like '%" + timkiem + "%' or a.ten like '%" + timkiem + "%') and a.makp='" + khoaphong + "'";
                    sql += " group by c.ten,e.ten ";
                }
                else
                {
                    sql = " select c.ten , e.Ten , count(*) ";
                    sql += "from may a,dmkhoaphong c,dmloaimay e ";
                    sql += " where a.MaKP=c.ID and a.MaLoai=e.ID  ";
                    sql += "and (a.mamay like '%" + timkiem + "%' or a.ten like '%" + timkiem + "%') ";
                    sql += " group by c.ten,e.ten ";
                }
                CConnection data = new CConnection();
                ds = data.getData(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    strKQ +=  dr[1].ToString() + ": " + dr[2].ToString() + ";";
                }

            }
            catch { }
            return strKQ;
        }
      
        public DataSet get_DSMay()
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id id,a.mamay barcode,a.ten tenmay,b.ten nuocsx,c.ten khoaphong,a.Vitri vitri,e.Ten loai,f.Ten loaichitiet,a.DonGia,g.ten tinhtrang,a.ngaybdsd tungay,a.kiemke ";
                sql += "from may a,dmquocgia b,dmkhoaphong c,dmloaimay e,dmloaichitiet f,dmtinhtrang g ";
                sql += " where a.NuocSX=b.ID and a.MaKP=c.ID and a.MaLoai=e.ID and a.tinhtrang=g.id and a.LoaiChiTiet=f.ID";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_May(string s_id)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select id,mamay,ten,nuocsx,makp,maloai,loaichitiet,dongia,vitri,tinhtrang,ngaybdsd ";
                sql += "from may  ";
                sql += " where ID='"+s_id+"'";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_MayFromID(string ID)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = " select a.id,a.mamay,a.ten tenmay,b.ten nuocsx,c.ten khoaphong,a.Vitri,e.Ten loaimay,f.Ten loaichitiet,a.DonGia,g.ten tinhtrang,a.ngaybdsd ngaybdsd ";
                sql += "from may a,dmquocgia b,dmkhoaphong c,dmloaimay e,dmloaichitiet f,dmtinhtrang g ";
                sql += " where a.id='"+ID+"' and a.NuocSX=b.ID and a.MaKP=c.ID and a.MaLoai=e.ID and a.tinhtrang=g.id and a.LoaiChiTiet=f.ID";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_cauhinh(string id)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select a.matb ma,b.ten ten,a.noidung noidung from cauhinh a,dmthietbi b where a.matb=b.id and a.id='" + id+"'";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_cauhinhmau(string id)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select a.matb,b.ten,a.noidung from cauhinhmauct a,dmthietbi b where a.matb=b.id and a.id='" + id + "'";

                CConnection data = new CConnection();
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

                sql = "select id,ten,Enable from "+table;

                CConnection data = new CConnection();
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

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhMucCT(string table,string loaimay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable,LoaiMay from " + table +" where loaimay='"+loaimay+"' and Enable='1' order by ten";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_DanhMuc(string table,string Enable)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select id,ten,Enable from " + table+" where Enable='"+Enable+"' order by ten";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_BaoTri(string table,string id)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select a.id,a.ngay,a.mamay,b.ten,a.noidung from " + table+" a,may b where a.mamay=b.id and a.mamay='"+id+"'";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public DataSet get_BCBaoTri(string mamay)
        {
            DataSet ds = new DataSet();

            try
            {

                sql = "select a.id,DATE_FORMAT(a.Ngay, '%d-%m-%Y') ngay,a.noidung from  baotri a where a.mamay='" + mamay + "'";

                CConnection data = new CConnection();
                ds = data.getData(sql);

            }
            catch { }
            return ds;
        }
        public string getID(string tablename)
        {
            string strID="1";
            DataSet ds = new DataSet();
            try
            {
                string sql = "select Max(ID+1) from " + tablename;
                CConnection data = new CConnection();
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
        public int InsertDanhMuc(string TableName,string ID, string Ten,string Enable)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into "+TableName+"(ID,Ten,Enable) values ('" + ID + "',N'" + Ten + "','"+Enable+"')";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertDanhMuc(string TableName, string ID, string Ten, string Enable,string loai)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into " + TableName + "(ID,Ten,Enable,loaimay) values ('" + ID + "',N'" + Ten + "','" + Enable + "','"+loai+"')";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        private string f_formatDate(DateTime d)
        {
            return string.Format("{0:yyyy-MM-dd}", d);
        }
        public int InsertBaoTri(string ID, DateTime Ngay,string MaMay,string NoiDung)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into baotri(ID,Ngay,MaMay,NoiDung,ngaybd) values ('" + ID + "','"+f_formatDate(Ngay)+"','" + MaMay + "',N'" + NoiDung + "',now())";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertCauHinhMaull(string ID,string MoTa)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into cauhinhmaull(ID,Ten) values ('"+ID+"',N'"+MoTa+"')";
               
                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertCauHinhMauct(string ID, string STT,string MaTB,string NoiDung)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into cauhinhmauct(ID,STT,MaTB,NoiDUng) values ('" + ID + "','" + STT + "','" + MaTB + "',N'" + NoiDung + "')";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertCauHinh(string ID, string STT, string MaTB, string NoiDung)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into cauhinh(ID,STT,MaTB,NoiDUng) values ('" + ID + "','" + STT + "','" + MaTB + "',N'" + NoiDung + "')";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int InsertMay(string ID,string MaMay, string Ten, string NuocSX, string MaKP, string MaLoai,string loaichitiet,string DonGia, string ViTri,string tinhtrang,DateTime NgayBDSD)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into May(ID,MaMay,Ten,NuocSX,MaKP,MaLoai,LoaiChiTiet,DonGia,Vitri,NgayUD,tinhtrang,ngaybdsd) ";
                sql += " values ('" + ID + "','" + MaMay + "',N'" + Ten + "','" + NuocSX + "','" + MaKP + "','" + MaLoai + "','" + loaichitiet + "'," + DonGia + ",'" + ViTri + "',now(),'"+tinhtrang+"','"+string.Format("{0:yyyy-MM-dd}",NgayBDSD)+"')";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion
        #region Update
        public int UpdateDanhMuc(string TableName, string ID, string Ten,string Enable)
        {
            int roweffect = 0;
            try
            {
                sql = "Update " + TableName + " set Ten=N'" + Ten + "',Enable='"+Enable+"' where ID='"+ID+"'";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateDanhMuc(string TableName, string ID, string Ten, string Enable,string loaimay)
        {
            int roweffect = 0;
            try
            {
                sql = "Update " + TableName + " set Ten=N'" + Ten + "',Enable='" + Enable + "', loaimay='"+loaimay+"' where ID='" + ID + "'";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateBaoTri( string ID, string NoiDung,DateTime Ngay)
        {
            int roweffect = 0;
            try
            {
                sql = "Update baotri set NoiDung=N'" + NoiDung + "',Ngay='"+f_formatDate(Ngay)+"' where ID='" + ID + "'";

                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateMay(string ID, string MaMay, string Ten, string NuocSX, string MaKP, string MaLoai, string loaichitiet, string DonGia, string ViTri, string tinhtrang, DateTime NgayBDSD)
        {
            int roweffect = 0;
            try
            {
                sql = "Update May set MaMay='" + MaMay + "',Ten=N'" + Ten + "',NuocSX='" + NuocSX + "',MaKP='" + MaKP + "'";
                sql+=",MaLoai='" + MaLoai + "',LoaiChiTiet='" + loaichitiet + "',DonGia=" + DonGia + ",Vitri='" + ViTri + "',NgayUD=now()";
                sql+=",tinhtrang='" + tinhtrang + "',ngaybdsd='" + string.Format("{0:yyyy-MM-dd}", NgayBDSD) + "' ";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        public int UpdateKiemKe(string ID,string s_kiemke)
        {
            int roweffect = 0;
            try
            {
                sql = "Update May set kiemke='" + s_kiemke +"'";
                sql += "WHERE ID='" + ID + "'";


                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion
        #region delete
        public int DeleteDanhMuc(string TableName,string ID)
        {
            
            int roweffect = 0;
            try
            {
               
                sql = "Delete from "+TableName+" where ID='" + ID + "'";
                CConnection data = new CConnection();
                roweffect = data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion*/
    }
}
