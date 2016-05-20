using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LibBaocao;
using Entity;

namespace Data
{
    public class CThanhToanBHYTDAO
    {
        #region khai bao bien
        string sql = "";
        #endregion
        #region get
        public DataSet get_BaoCaoBHYT(string FromDate,string ToDate)
        {
            DataSet ds = new DataSet();
           
            try
            {

                sql = "select convert(varchar,A.MABN) [MABN],B.HOTEN [HOTEN],B.NAMSINH [NAMSINH],A.ICD [ICD10],A.SOTHEBHYT [SOTHEBHYT],A.SOPHIEU [SOTOA],A.TONGTIEN [TONGTIEN],A.BHYTTRA [BHYTTRA],A.BNTRA [BNTRA],";
                sql+="A.NOIDANGKY [TENBV],'701' [MATINH],A.MANHOMTHE [MANHOMTHE],";
            sql+="A.NHOMTHE [NHOMTHE],A.LOAITHE [LOAITHE],convert(varchar,A.IDTTRV) [IDTTRV],A.MAVAOVIEN [MAVAOVIEN],";
            sql += "A.MABV [MABV],A.NGAYHD [NGAYHD],convert(varchar,A.SOBIENLAI) [SOBIENLAI],B.PHAI [GIOITINH] ,";
            sql+="A.NGAYVAO [NGAYVAO],A.NGAYRA [NGAYRA],A.LOAIBN [LOAIBN],";
            sql+="A.CHANDOAN [CHANDOAN],A.THUOC [THUOC],A.MAU [MAU],A.XETNGHIEM [XETNGHIEM],";
            sql+="A.CDHA [CDHA],A.DVKTTHONGTHUONG [DVKTTHONGTHUONG],A.DVKTCAO [DVKTCAO],";
            sql+="A.VTYT [VATTUYTE],A.TIENKHAM [TIENKHAM],A.GIUONG [GIUONG],A.CHIPHIVC [CHIPHIVC],A.KHAC [KHAC],";
            sql+="A.THAMDOCHUCNANG [THAMDOCHUCNANG],'1011' [MMYY],convert(varchar,A.NGAYLAMVIEC,103) [NGAYLAMVIEC],";
            sql += "A.TRAITUYEN [TRAITUYEN],[ID] [ID]from thanhtoanbhyt a,btdbn b where a.mabn=b.mabn and convert(varchar,a.ngaylamviec,112) >='" + for_ngay_yyyymmdd(FromDate) + "' and convert(varchar,a.ngaylamviec,112) <='" + for_ngay_yyyymmdd(ToDate) + "'   ";
            CConnection data = new CConnection();
            ds = data.getData(sql);
              
            }
            catch { }
            return ds;
        }
        public string get_IDTTRVdaduyet(string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string s = "";
            try
            {

                sql = "select convert(varchar,A.IDTTRV) [IDTTRV] from thanhtoanbhyt a where convert(varchar,a.ngaylamviec,112) >='" + for_ngay_yyyymmdd(FromDate) + "' and convert(varchar,a.ngaylamviec,112) <='" + for_ngay_yyyymmdd(ToDate) + "'   ";
                CConnection data = new CConnection();
                ds = data.getData(sql);
                DataTable t = ds.Tables[0];
                foreach (DataRow row in t.Rows)
                {
                    s+=row["IDTTRV"].ToString()+",";
                }
            }
            catch { }
            s = s.TrimEnd(',');
            return s;
        }
        public DataSet get_trungmau01bv(string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string s = "";
            try
            {

                sql = "select convert(varchar,a.ngaylamviec,103) Ngay,a.MaBN MaBN from thanhtoanbhyt a where convert(varchar,a.ngaylamviec,112) >='" + for_ngay_yyyymmdd(FromDate) + "' and convert(varchar,a.ngaylamviec,112) <='" + for_ngay_yyyymmdd(ToDate) + "'    group by a.ngaylamviec,a.mabn having count(*)>1";
                CConnection data = new CConnection();
                ds = data.getData(sql);
                
            }
            catch { }
           
            return ds;
        }
        public string get_MaBNdaduyet(string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string s = "";
            try
            {

                sql = "select a.mabn [MABN] from thanhtoanbhyt a where convert(varchar,a.ngaylamviec,112) >='" + for_ngay_yyyymmdd(FromDate) + "' and convert(varchar,a.ngaylamviec,112) <='" + for_ngay_yyyymmdd(ToDate) + "'   ";
                CConnection data = new CConnection();
                ds = data.getData(sql);
                DataTable t = ds.Tables[0];
                foreach (DataRow row in t.Rows)
                {
                    s +="'"+ row["MaBN"].ToString() + "',";
                }
            }
            catch { }
            s = s.TrimEnd(',');
            return s;
        }
        public string get_MAVAOVIENdaduyet(string FromDate, string ToDate)
        {
            DataSet ds = new DataSet();
            string s = "";
            try
            {

                sql = "select convert(varchar,A.mavaovien) [MAVAOVIEN] from thanhtoanbhyt a where convert(varchar,a.ngaylamviec,112) >='" + for_ngay_yyyymmdd(FromDate) + "' and convert(varchar,a.ngaylamviec,112) <='" + for_ngay_yyyymmdd(ToDate) + "'   ";
                CConnection data = new CConnection();
                ds = data.getData(sql);
                DataTable t = ds.Tables[0];
                foreach (DataRow row in t.Rows)
                {
                    s += row["MAVAOVIEN"].ToString() + ",";
                }
            }
            catch { }
            s = s.TrimEnd(',');
            return s;
        }

        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }
        public string KiemTraDaDuyet(string MaBN, string IDTTRV, DateTime Ngay)
        {
            string result = "";
            DataSet ds=new DataSet();
            try
            {
                sql = "select [ID] from Thanhtoanbhyt where mabn='"+MaBN+"' and IDTTRV='"+IDTTRV+"' and convert(varchar,NgayLamViec,112)='"+string.Format("{0:yyyyMMdd}", Ngay)+"'";
               
                CConnection data = new CConnection();
                ds = data.getData(sql);
                result = ds.Tables[0].Rows[0]["ID"].ToString();
              
            }
            catch { }
            return result;
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
        public int Insert(CThanhToanBHYT t)
        {
            int roweffect = 0;
            try
            {
                sql = "Insert into thanhtoanbhyt(mabn,nhomthe,loaithe,mavaovien,sothebhyt,noidangky,icd,chandoan,tongtien,bhyttra,bntra,thuoc,mau,xetnghiem,cdha,dvktthongthuong,dvktcao,vtyt,tienkham,chiphivc,thamdochucnang,giuong,traituyen,idttrv,ngaylamviec,sophieu,khac,manhomthe,ngayhd,loaiba,sobienlai)";
                sql += " values('" + t.MaBN + "',N'" + t.NhomThe + "','" + t.LoaiThe + "'," + t.MaVaoVien + ",";
                sql += "'" + t.SoTheBHYT + "',N'" + t.NoiDangKyBHYT + "','" + t.ICD + "',N'" + t.ChanDoan + "',";
                sql += t.TongTien + "," + t.BHYTTra + "," + t.BNTra + "," + t.Thuoc + "," + t.Mau + "," + t.XetNghiem + ",";
                sql += t.CDHA + "," + t.DVKTthongthuong + "," + t.DVKTcao + "," + t.VTYT + "," + t.TienKham + "," + t.ChiPhiVC + ",";
                sql += t.ThamDoChucNang + "," + t.Giuong + "," + t.TraiTuyen + "," + t.IDTTRV + ",'" + FormatYYYY_MM_DD(t.NgayLamViec) + "','" + t.SoPhieu + "'," + t.Khac + ",'" + t.MaNhomThe + "','" + FormatYYYY_MM_DD(t.HSD) + "','" + t.LoaiBA + "'," + t.SoBienLai + ")";
                CConnection data = new CConnection();
                roweffect= data.setData(sql);
            }
            catch { }
            return roweffect;
        }
        #endregion
        #region delete
        public void Delete(string ID)
        {
            sql = "Delete thanhtoanbhyt where [ID]='" + ID + "'";
            CConnection data = new CConnection();
            data.setData(sql);
        }
        #endregion
    }
}
