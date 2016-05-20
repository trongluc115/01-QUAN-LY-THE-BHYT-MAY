using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using LibBaocao;
using System.Data;
namespace DataOracle
{
    public class CThanhToanBHYTOracleNoiTru
    {
        #region khai bao bien
        CThanhToanBHYT thanhtoanbhyt;
        AccessData data;
        DataSet ds;
        string sql = "";
        #endregion
        CThanhToanBHYT ThanhToanBHYT
        {
            get { return thanhtoanbhyt; }
        }
        #region phuong thuc
        public CThanhToanBHYTOracleNoiTru()
        {
            thanhtoanbhyt = new CThanhToanBHYT();
            data = new AccessData();
            ds = new DataSet();
        }
        private long f_getTongTien(long IDTTRV)
        {
            long result = 0;
            sql = "select * from medibv115";
            try
            {
                ds = data.get_data(sql);
                result = long.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch { result = 0; }
            return result;
        }
        private string getdatabase(DateTime Ngay)
        {
            string database = "";
            try
            {
                database = "medibv115" + string.Format("{0:MMyy}", Ngay);
            }
            catch { }
            return database;
        }
        private string getdatabase(string ngay)
        {
            string database = "";
            try
            {
               
                database = "medibv115" + ngay.Substring(3, 2)+ngay.Substring(8, 2);
            }
            catch { }
            return database;
        }
        private string getFormatDDMMYYYY(DateTime Ngay)
        {
            string database = "";
            try
            {
                database =  string.Format("{0:ddMMyy}", Ngay);
            }
            catch { }
            return database;
        }
        public DataSet f_loadVienPhiChiTiet(string MaBN,DateTime Ngay,string MaQL,bool loadBHYT)
        {
            if(loadBHYT)
                       sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where a.maql='"+MaQL+"' and b.madoituong=1 and c.id_loai=l.id and vl.id=a.id and vl.loaibn<>3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            else
                       sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where a.maql='" + MaQL + "' and c.id_loai=l.id and vl.id=a.id and vl.loaibn<>3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadThuocChiTiet(string MaBN, DateTime Ngay,string MaQL,bool loadBHYT)
        {
            if(loadBHYT)
                      sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where a.maql='"+MaQL+"' and  b.madoituong=1 and vl.id=a.id and vl.loaibn<>3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            else
                      sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where a.maql='" + MaQL + "' and vl.id=a.id and vl.loaibn<>3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        private string addString(string chuoigoc, string chuoiadd)
        {
            string result = chuoigoc;
            try
            {
                if (chuoigoc.IndexOf(chuoiadd, 0) < 0)
                {
                    result = chuoigoc + ";" + chuoiadd;
                }
            }
            catch { }
            return result;
        }
        public string f_loadICD(string MaBN, DateTime Ngay)
        {
            string kq = "";
            string MaICD= "";
            
            sql = "select maicd from xxxxx.bhytkb where mabn="+MaBN+" and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    MaICD = addString(MaICD, row[0].ToString());
                }

            }
            catch { }
            sql = "select maicd from xxxxx.cdkemtheo where maql in (select maql from xxxxx.bhytkb where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "')";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            
            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    MaICD = addString(MaICD, row[0].ToString());
                }

            }
            catch { }
            return MaICD;
        }

        public string f_loadChanDoan(string MaBN, DateTime Ngay)
        {
            string kq = "";
            string ChanDoan = "";
            
            sql = "select chandoan from xxxxx.bhytkb where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    ChanDoan = addString(ChanDoan, row[0].ToString());
                }

            }
            catch { }

            sql = "select chandoan from xxxxx.cdkemtheo where maql in (select maql from xxxxx.bhytkb where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "')";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));

            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    ChanDoan = addString(ChanDoan, row[0].ToString());
                }

            }
            catch { }

            sql = "select ten from xxxxx.trieuchung where maql in (select maql from xxxxx.bhytkb where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "')";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));

            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    ChanDoan = addString(ChanDoan, row[0].ToString());
                }

            }
            catch { }
            return ChanDoan;
        }

        public string f_loadICDFull( string MaQL,DateTime Ngay)
        {
            string kq = "";
            string MaICD = "";
           
            sql = "select maicd from xuatvien where MaQL='" + MaQL + "' ";
            
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    MaICD = addString(MaICD,row[0].ToString());
                }

            }
            catch { }
           
           
            return MaICD;
        }

        public string f_loadChanDoanFull( string MaQL,DateTime Ngay)
        {
            string kq = "";
            string ChanDoan = "";

            sql = "select chandoan from xuatvien where MaQL='" + MaQL + "' ";
          
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    ChanDoan=addString(ChanDoan, row[0].ToString() );
                }

            }
            catch { }
           
            
            return ChanDoan;
        }
        public CThanhToanBHYT f_loadTT_BHYT(string MaBN, DateTime Ngay)
        {


            sql = "select kb.sothe,kb.mabv,nc.tenbv,bh.denngay,kb.traituyen,kb.sobienlai,kb.loaiba from xxxxx.bhytkb kb,xxxxx.bhyt bh,dmnoicapbhyt nc where nc.mabv=kb.mabv and kb.mabn=" + MaBN + " and bh.maql=kb.maql and to_char(kb.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                thanhtoanbhyt.SoTheBHYT = dset.Tables[0].Rows[0]["SOTHE"].ToString();
                thanhtoanbhyt.MaBV = dset.Tables[0].Rows[0]["MABV"].ToString();
                thanhtoanbhyt.NoiDangKyBHYT = dset.Tables[0].Rows[0]["TENBV"].ToString();
                thanhtoanbhyt.TraiTuyen = int.Parse(dset.Tables[0].Rows[0]["TRAITUYEN"].ToString());
                thanhtoanbhyt.HSD = DateTime.Parse(dset.Tables[0].Rows[0]["DENNGAY"].ToString());
                thanhtoanbhyt.LoaiBA = dset.Tables[0].Rows[0]["LOAIBA"].ToString();
                thanhtoanbhyt.SoBienLai = dset.Tables[0].Rows[0]["SOBIENLAI"].ToString();
            }
            catch { }

            return thanhtoanbhyt;
        }
        public CThanhToanBHYT f_loadTT_BHYT_CLS(string MaBN, DateTime Ngay)
        {


            sql = "select bh.sothe,bh.mabv,nc.tenbv,bh.ngay,bh.traituyen,ll.sobienlai,ll.loaibn from xxxxx.v_ttrvds ds,xxxxx.v_ttrvbhyt bh,dmnoicapbhyt nc,xxxxx.v_ttrvll ll where ll.id=ds.id and nc.mabv=bh.mabv and ds.mabn=" + MaBN + " and ds.id=bh.id and to_char(ds.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                thanhtoanbhyt.SoTheBHYT = dset.Tables[0].Rows[0]["SOTHE"].ToString();
                thanhtoanbhyt.MaBV = dset.Tables[0].Rows[0]["MABV"].ToString();
                thanhtoanbhyt.NoiDangKyBHYT = dset.Tables[0].Rows[0]["TENBV"].ToString();
                thanhtoanbhyt.TraiTuyen = int.Parse(dset.Tables[0].Rows[0]["TRAITUYEN"].ToString());
                thanhtoanbhyt.HSD = DateTime.Parse(dset.Tables[0].Rows[0]["NGAY"].ToString());
                thanhtoanbhyt.LoaiBA = dset.Tables[0].Rows[0]["LOAIBN"].ToString();
                thanhtoanbhyt.SoBienLai = dset.Tables[0].Rows[0]["SOBIENLAI"].ToString();
            }
            catch { }

            return thanhtoanbhyt;
        }
        public CThanhToanBHYT f_loadSoPhieu_IDTTRV(string maql,DateTime Ngay)
        {


            sql = "select ds.NgayVao,ds.ngayra,ds.mavaovien,ds.id from xxxxx.v_ttrvds ds where  maql='"+maql+"'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
               // thanhtoanbhyt.SoPhieu = dset.Tables[0].Rows[0]["SOTOA"].ToString();
                thanhtoanbhyt.MaVaoVien = long.Parse(dset.Tables[0].Rows[0]["MAVAOVIEN"].ToString());
                thanhtoanbhyt.IDTTRV = long.Parse(dset.Tables[0].Rows[0]["ID"].ToString());
                thanhtoanbhyt.NgayVao = DateTime.Parse(dset.Tables[0].Rows[0]["NgayVao"].ToString());
                thanhtoanbhyt.NgayRa = DateTime.Parse(dset.Tables[0].Rows[0]["NgayRa"].ToString());

            }
            catch { }

            return thanhtoanbhyt;
        }
        public DataSet f_loadNgayNhapVien_MaQL(string MaBN, DateTime Ngay)
        {

           
            sql = "select  to_char(ds.ngayvao,'dd/mm/yyyy hh:mi') NgayVao,ds.maql from xxxxx.v_ttrvds ds where  ds.mabn=" + MaBN + " and to_char(ds.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
               


            }
            catch { }

            return dset;
        }
        public CThanhToanBHYT f_loadSoPhieu_IDTTRV_CLS(string MaBN, DateTime Ngay)
        {


            sql = "select ll.sophieu,DS.mavaovien,DS.id from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll where  ll.id=ds.id and ds.mabn=" + MaBN + " and to_char(ds.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                thanhtoanbhyt.SoPhieu = dset.Tables[0].Rows[0]["SOPHIEU"].ToString();
                thanhtoanbhyt.MaVaoVien = long.Parse(dset.Tables[0].Rows[0]["MAVAOVIEN"].ToString());
                thanhtoanbhyt.IDTTRV = long.Parse(dset.Tables[0].Rows[0]["ID"].ToString());


            }
            catch { }

            return thanhtoanbhyt;
        }

        private string for_ngay_yyyymmdd(string ngay)
        {
            return ngay.Substring(6, 4) + ngay.Substring(3, 2) + ngay.Substring(0, 2);
        }
        public DataSet f_loadDanhSachChuaDuyet(string tungay,string denngay,string IDDaDuyet)
        {
            if (IDDaDuyet.Length == 0)
            {
                sql = "select  kp.tenkp,kb.mabn, bn.hoten, bn.namsinh, kb.sotoa,kb.sothe,kb.thuoc,kb.mavaovien, kb.idttrv ,kb.traituyen from xxxxx.bhytkb kb,btdbn bn,btdkp_bv kp where kb.makp=kp.makp and bn.mabn=kb.mabn and  kb.loaiba=3 and to_char(kb.Ngay,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(kb.Ngay,'YYYYMMDD')<='" + for_ngay_yyyymmdd(denngay) + "' ";
            }
            else
            {
                sql = "select  kp.tenkp,kb.mabn, bn.hoten, bn.namsinh, kb.sotoa,kb.sothe,kb.thuoc,kb.mavaovien, kb.idttrv ,kb.traituyen from xxxxx.bhytkb kb,btdbn bn,btdkp_bv kp where kb.makp=kp.makp and bn.mabn=kb.mabn and  kb.loaiba=3 and to_char(kb.Ngay,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(kb.Ngay,'YYYYMMDD')<='" + for_ngay_yyyymmdd(denngay) + "' and kb.idttrv not in (" + IDDaDuyet + ")";
            }
            sql = sql.Replace("xxxxx", getdatabase(tungay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                

            }
            catch { }

            return dset;
        }
        public DataSet f_loadDanhSachCLSChuaDuyet(string tungay, string denngay, string IDDaDuyet)
        {
            if (IDDaDuyet.Length == 0)
            {
                sql = "select  kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll,btdbn bn,btdkp_bv kp,xxxxx.v_ttrvct ct where ct.id=ds.id and ct.madoituong=1 and ll.makp=kp.makp and bn.mabn=ds.mabn and ds.id=ll.id and  ll.loaibn<>3 and to_char(ds.NgayUD,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(ds.NgayUD,'YYYYMMDD')<='" + for_ngay_yyyymmdd(tungay) + "'  group by kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien";
            }
            else
            {
                sql = "select  kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll,btdbn bn,btdkp_bv kp,xxxxx.v_ttrvct ct where ct.id=ds.id and ct.madoituong=1 and ll.makp=kp.makp and bn.mabn=ds.mabn and ds.id=ll.id and  ll.loaibn<>3 and to_char(ds.NgayUD,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(ds.NgayUD,'YYYYMMDD')<='" + for_ngay_yyyymmdd(tungay) + "'and  ds.id not in (" + IDDaDuyet + ") group by kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien";
            }
            sql = sql.Replace("xxxxx", getdatabase(tungay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);


            }
            catch { }

            return dset;
        }

        #endregion
    }
}
