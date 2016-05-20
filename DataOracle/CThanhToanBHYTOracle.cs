using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using LibBaocao;
using System.Data;
namespace DataOracle
{
    public class CThanhToanBHYTOracle
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
        public CThanhToanBHYTOracle()
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
        public DataSet f_loadVienPhiChiTiet(string MaBN,DateTime Ngay,bool loadBHYT)
        {
            if(loadBHYT)
                       sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where b.madoituong=1 and c.id_loai=l.id and vl.id=a.id and vl.loaibn=3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            else
                       sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where  c.id_loai=l.id and vl.id=a.id and vl.loaibn=3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadChiDinh(string MaBN, DateTime Ngay )
        {

            sql = "select c.ten||' ( SL:'||a.soluong||') - K/P:'||kp.tenkp tenvp,a.mavp id  from xxxxx.v_chidinh a,v_giavp c,btdkp_bv kp where c.id=a.mavp and a.makp=kp.makp and a.mabn='" + MaBN + "' and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";


            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadChiDinh_v_chidinh(string MaBN, DateTime Ngay)
        {

            sql = "select 0,a.mavp mavp,c.ten tenvp,a.soluong soluong,kp.tenkp tenkp,a.mabn MaBN,to_char(a.ngay,'dd/mm/yyyy') ngaycd,'-' ngayxoa,' ' nguoixoa,a.makp makp  from xxxxx.v_chidinh a,v_giavp c,btdkp_bv kp where c.id=a.mavp and a.makp=kp.makp and a.mabn='" + MaBN + "' and to_char(a.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";

            sql = "SELECT 0,v.id mavp,v.ten tenvp, TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,10)+1, INSTR(a.noidung,'^',1,10+1) - INSTR(a.noidung,'^',1,10)-1 )) SOLUONG,c.tenkp tenkp,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 )) MABN,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,25)+1, INSTR(a.noidung,'^',1,25+1) - INSTR(a.noidung,'^',1,25)-1 )) NGAY_CHI_DINH,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) NGAY_XOA,b.hoten nguoixoa,c.makp makp ";
            sql += " FROM xxxxx.eve_upd_del a,dlogin b,btdkp_bv c,v_giavp v ";
            sql += " WHERE  a.TABLEID=42 and  a.command='del' and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 ))='" + MaBN + "' ";
            sql += " and a.userid=b.id and c.makp=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,7)+1, INSTR(a.noidung,'^',1,7+1) - INSTR(a.noidung,'^',1,7)-1 )) and v.id=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) AND TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,8)+1, INSTR(a.noidung,'^',1,8+1) - INSTR(a.noidung,'^',1,8)-1 ))  not in ('9') and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) in (select cc.id from v_nhomvp aa,v_loaivp bb,v_giavp cc where aa.ma in (5,6,15) and aa.ma=bb.id_nhom and cc.id_loai=bb.id) and ";
            sql += " TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) like  '%" + string.Format("{0:dd/MM/yyyy}", Ngay) + "%' ";


            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public int f_loadTongTien_v_chidinh(string MaBN, DateTime Ngay)
        {

            sql = "select sum(a.soluong *a.dongia) sotien  from xxxxx.v_chidinh a where  a.mabn='" + MaBN + "' and to_char(a.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and a.madoituong=1 and idkhoa=0";
            int kq = 0;
           

            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                kq=int.Parse(dset.Tables[0].Rows[0][0].ToString());
            }
            catch { }
            return kq;
        }
        public DataSet f_loadChiDinh_v_chidinh_user(string userid, DateTime Ngay)
        {

//          sql = "select 0,a.mavp mavp,c.ten tenvp,a.soluong soluong,kp.tenkp tenkp,a.mabn MaBN,to_char(a.ngay,'dd/mm/yyyy') ngaycd,'-' ngayxoa,' ' nguoixoa,a.makp makp  from xxxxx.v_chidinh a,v_giavp c,btdkp_bv kp where c.id=a.mavp and a.makp=kp.makp and  to_char(a.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";

            sql = "SELECT 0,v.id mavp,v.ten tenvp, TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,10)+1, INSTR(a.noidung,'^',1,10+1) - INSTR(a.noidung,'^',1,10)-1 )) SOLUONG,c.tenkp tenkp,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 )) MABN,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,25)+1, INSTR(a.noidung,'^',1,25+1) - INSTR(a.noidung,'^',1,25)-1 )) NGAY_CHI_DINH,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) NGAY_XOA,b.hoten nguoixoa,c.makp makp,TO_CHAR(SUBSTR(a.noidung, 0, INSTR(a.noidung,'^',1,0+1) -1 )) idchidinh,b.id Userid,bn.hoten ";
            sql +=" FROM xxxxx.eve_upd_del a,dlogin b,btdkp_bv c,v_giavp v,btdbn bn ";
            sql += " WHERE  a.userid='"+userid+"' and a.TABLEID=42 and  a.command='del' ";
            sql+=" and a.userid=b.id and c.makp=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,7)+1, INSTR(a.noidung,'^',1,7+1) - INSTR(a.noidung,'^',1,7)-1 )) and v.id=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) AND TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,8)+1, INSTR(a.noidung,'^',1,8+1) - INSTR(a.noidung,'^',1,8)-1 ))  not in ('9') and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) in (select cc.id from v_nhomvp aa,v_loaivp bb,v_giavp cc where aa.ma in (5,6,15) and aa.ma=bb.id_nhom and cc.id_loai=bb.id) and ";
            sql+=" TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) like  '%"+string.Format("{0:MM/dd/yyyy}",Ngay)+"%' ";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 ))=bn.mabn ";

            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadChiDinh_v_chidinh_user(string userid, DateTime Ngay, string iddatuyet)
        {

            //          sql = "select 0,a.mavp mavp,c.ten tenvp,a.soluong soluong,kp.tenkp tenkp,a.mabn MaBN,to_char(a.ngay,'dd/mm/yyyy') ngaycd,'-' ngayxoa,' ' nguoixoa,a.makp makp  from xxxxx.v_chidinh a,v_giavp c,btdkp_bv kp where c.id=a.mavp and a.makp=kp.makp and  to_char(a.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";

            sql = "SELECT 0,v.id mavp,v.ten tenvp, TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,10)+1, INSTR(a.noidung,'^',1,10+1) - INSTR(a.noidung,'^',1,10)-1 )) SOLUONG,c.tenkp tenkp,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 )) MABN,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,25)+1, INSTR(a.noidung,'^',1,25+1) - INSTR(a.noidung,'^',1,25)-1 )) NGAY_CHI_DINH,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) NGAY_XOA,b.hoten nguoixoa,c.makp makp,TO_CHAR(SUBSTR(a.noidung, 0, INSTR(a.noidung,'^',1,0+1) -1 )) idchidinh,b.id Userid ,bn.hoten";
            sql += " FROM xxxxx.eve_upd_del a,dlogin b,btdkp_bv c,v_giavp v,btdbn bn ";
            sql += " WHERE  a.userid='" + userid + "' and a.TABLEID=42 and  a.command='del' ";
            sql += " and a.userid=b.id and c.makp=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,7)+1, INSTR(a.noidung,'^',1,7+1) - INSTR(a.noidung,'^',1,7)-1 )) and v.id=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) AND TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,8)+1, INSTR(a.noidung,'^',1,8+1) - INSTR(a.noidung,'^',1,8)-1 ))  not in ('9') and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) in (select cc.id from v_nhomvp aa,v_loaivp bb,v_giavp cc where aa.ma in (5,6,15) and aa.ma=bb.id_nhom and cc.id_loai=bb.id) and ";
            sql += " TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) like  '%" + string.Format("{0:MM/dd/yyyy}", Ngay) + "%' ";
            sql += " and TO_CHAR(SUBSTR(a.noidung, 0, INSTR(a.noidung,'^',1,0+1) -1 ))not in (" + iddatuyet + ")";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 ))=bn.mabn ";


            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadChiDinh_v_chidinh_MaBN(string MaBN, DateTime Ngay, string iddatuyet)
        {

            //          sql = "select 0,a.mavp mavp,c.ten tenvp,a.soluong soluong,kp.tenkp tenkp,a.mabn MaBN,to_char(a.ngay,'dd/mm/yyyy') ngaycd,'-' ngayxoa,' ' nguoixoa,a.makp makp  from xxxxx.v_chidinh a,v_giavp c,btdkp_bv kp where c.id=a.mavp and a.makp=kp.makp and  to_char(a.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";

            sql = "SELECT 0,v.id mavp,v.ten tenvp, TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,10)+1, INSTR(a.noidung,'^',1,10+1) - INSTR(a.noidung,'^',1,10)-1 )) SOLUONG,c.tenkp tenkp,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 )) MABN,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,25)+1, INSTR(a.noidung,'^',1,25+1) - INSTR(a.noidung,'^',1,25)-1 )) NGAY_CHI_DINH,TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) NGAY_XOA,b.hoten nguoixoa,c.makp makp,TO_CHAR(SUBSTR(a.noidung, 0, INSTR(a.noidung,'^',1,0+1) -1 )) idchidinh,b.id Userid ,bn.hoten";
            sql += " FROM xxxxx.eve_upd_del a,dlogin b,btdkp_bv c,v_giavp v,btdbn bn ";
            sql += " WHERE   a.TABLEID=42 and  a.command='del' ";
            sql += " and a.userid=b.id and c.makp=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,7)+1, INSTR(a.noidung,'^',1,7+1) - INSTR(a.noidung,'^',1,7)-1 )) and v.id=TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) AND TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,8)+1, INSTR(a.noidung,'^',1,8+1) - INSTR(a.noidung,'^',1,8)-1 ))  not in ('9')  ";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) like  '%" + string.Format("{0:MM/dd/yyyy}", Ngay) + "%' ";
            sql += " and TO_CHAR(SUBSTR(a.noidung, 0, INSTR(a.noidung,'^',1,0+1) -1 ))not in (" + iddatuyet + ")";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 ))=bn.mabn  ";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,1)+1, INSTR(a.noidung,'^',1,1+1) - INSTR(a.noidung,'^',1,1)-1 ))='"+MaBN+"'";


            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadUser_v_chidinh(DateTime Ngay)
        {

            sql = "select distinct b.hoten ||' - '||b.userid ten,id id";

            sql += " FROM xxxxx.eve_upd_del a,dlogin b ";
            sql += " WHERE  a.TABLEID=42 and  a.command='del' ";
            sql += " and a.userid=b.id and  TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,35)+1, INSTR(a.noidung,'^',1,35+1) - INSTR(a.noidung,'^',1,35)-1 )) like  '%" + string.Format("{0:MM/dd/yyyy}", Ngay) + "%'";
            sql += " and TO_CHAR(SUBSTR(a.noidung, INSTR(a.noidung,'^',1,9)+1, INSTR(a.noidung,'^',1,9+1) - INSTR(a.noidung,'^',1,9)-1 )) in (select cc.id from v_nhomvp aa,v_loaivp bb,v_giavp cc where aa.ma in (5,6,15) and aa.ma=bb.id_nhom and cc.id_loai=bb.id)";
            sql += " order by ten";

            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadVienPhiChiTietKhacNgay(string MaBN, DateTime Ngay, bool loadBHYT)
        {
            if (loadBHYT)
                sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where b.madoituong=1 and c.id_loai=l.id and vl.id=a.id and vl.loaibn=3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(vl.ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")";
            else
                sql = "select c.ten ,c.DVT ,b.dongia ,b.SOLUONG ,B.SOTIEN  ,B.BHYTTRA,n.idnhombhyt,n.ten from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,v_giavp c,v_nhomvp n,v_loaivp l where  c.id_loai=l.id and vl.id=a.id and vl.loaibn=3 and l.id_nhom=n.ma and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(vl.ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and b.madoituong in ('1','2','7') and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadThuocChiTiet(string MaBN, DateTime Ngay,bool loadBHYT)
        {
            if(loadBHYT)
                      sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where  b.madoituong=1 and vl.id=a.id and vl.loaibn=3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            else
                      sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where  vl.id=a.id and vl.loaibn=3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")"; 
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);

            }
            catch { }
            return dset;
        }
        public DataSet f_loadThuocChiTietKhacNgay(string MaBN, DateTime Ngay, bool loadBHYT)
        {
            if (loadBHYT)
                sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where  b.madoituong=1 and vl.id=a.id and vl.loaibn=3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")";
            else
                sql = "select c.ten ,c.hamluong,c.Dang ,b.SOLUONG ,b.dongia ,B.SOTIEN  ,B.BHYTTRA from xxxxx.v_ttrvds a,xxxxx.v_ttrvll vl,xxxxx.v_ttrvct b,d_dmbd c where  vl.id=a.id and vl.loaibn=3 and a.id=b.id and c.id=b.mavp and a.mabn='" + MaBN + "' and to_char(a.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "' and vl.sobienlai not in (select sobienlai from xxxxx.v_hoantra where mabn=" + MaBN + ")";
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
        public string f_loadICD(string MaQL,DateTime Ngay)
        {
            string kq = "";
            string MaICD= "";
            
            sql = "select maicd from xxxxx.bhytkb where maql='"+MaQL+"'";
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
            sql = "select maicd from xxxxx.cdkemtheo where maql='" + MaQL + "'";
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

        public string f_loadChanDoan(string MaQL,DateTime Ngay)
        {
            string kq = "";
            string ChanDoan = "";

            sql = "select chandoan from xxxxx.bhytkb where maql='" + MaQL + "'";
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

            sql = "select chandoan from xxxxx.cdkemtheo where maql='" + MaQL + "'";
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

            sql = "select ten from xxxxx.trieuchung where maql='" + MaQL + "'";
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

        public string f_loadICDFull(string MaBN, DateTime Ngay)
        {
            string kq = "";
            string MaICD = "";
           
            sql = "select maicd from xxxxx.benhanpk where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
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
            sql = "select maicd from xxxxx.cdkemtheo where maql in (select maql from xxxxx.benhanpk where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "')";
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

        public string f_loadChanDoanFull(string MaBN, DateTime Ngay)
        {
            string kq = "";
            string ChanDoan = "";
            
            sql = "select chandoan from xxxxx.benhanpk where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
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
            sql = "select chandoan from xxxxx.cdkemtheo where maql in (select maql from xxxxx.benhanpk where mabn=" + MaBN + " and to_char(Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "')";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));

            try
            {
                dset = data.get_data(sql);
                foreach (DataRow row in dset.Tables[0].Rows)
                {
                    ChanDoan= ChanDoan = addString(ChanDoan, row[0].ToString());
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
        public CThanhToanBHYT f_loadTT_BHYT_TiepNhan(string MaBN, DateTime Ngay)
        {


            sql = "select bh.sothe SOTHE,bh.mabv MABV,nc.tenbv TENBV,bh.denngay DENNGAY,bh.traituyen TRAITUYEN from xxxxx.bhyt bh,dmnoicapbhyt nc where nc.mabv=bh.mabv and bh.mabn='" + MaBN + "' and  to_char(bh.NgayUD,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
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
                
            }
            catch { }

            return thanhtoanbhyt;
        }
        public CThanhToanBHYT f_loadTT_BHYT(string MaBN, DateTime Ngay)
        {


            sql = "select kb.sothe,kb.mabv,nc.tenbv,bh.denngay,kb.traituyen,kb.sobienlai,kb.loaiba from xxxxx.bhytkb kb,xxxxx.bhyt bh,dmnoicapbhyt nc where nc.mabv=kb.mabv and kb.mabn='" + MaBN + "' and bh.maql=kb.maql and to_char(kb.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
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


            sql = "select bh.sothe,bh.mabv,nc.tenbv,bh.ngay,bh.traituyen,ll.sobienlai,ll.loaibn from xxxxx.v_ttrvds ds,xxxxx.v_ttrvbhyt bh,dmnoicapbhyt nc,xxxxx.v_ttrvll ll where ll.id=ds.id and nc.mabv=bh.mabv and ds.mabn=" + MaBN + " and ds.id=bh.id and to_char(ds.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
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
        public CThanhToanBHYT f_loadSoPhieu_IDTTRV(string MaBN, DateTime Ngay)
        {


            sql = "select kb.sotoa,kb.mavaovien,kb.idttrv,kb.maql from xxxxx.bhytkb kb where  kb.mabn=" + MaBN + " and to_char(kb.Ngay,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                thanhtoanbhyt.SoPhieu = dset.Tables[0].Rows[0]["SOTOA"].ToString();
                thanhtoanbhyt.MaVaoVien = long.Parse(dset.Tables[0].Rows[0]["MAVAOVIEN"].ToString());
                thanhtoanbhyt.IDTTRV = long.Parse(dset.Tables[0].Rows[0]["IDTTRV"].ToString());
                thanhtoanbhyt.MaQuanLy = dset.Tables[0].Rows[0]["MAQL"].ToString();
                

            }
            catch { }

            return thanhtoanbhyt;
        }
        public CThanhToanBHYT f_loadSoPhieu_IDTTRV_CLS(string MaBN, DateTime Ngay)
        {


            sql = "select ll.sophieu,DS.mavaovien,DS.id,ds.maql from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll where  ll.id=ds.id and ds.mabn=" + MaBN + " and to_char(ds.NgayVao,'DDMMYY')='" + getFormatDDMMYYYY(Ngay) + "'";
            sql = sql.Replace("xxxxx", getdatabase(Ngay));
            DataSet dset = new DataSet();
            try
            {
                dset = data.get_data(sql);
                thanhtoanbhyt.SoPhieu = dset.Tables[0].Rows[0]["SOPHIEU"].ToString();
                thanhtoanbhyt.MaVaoVien = long.Parse(dset.Tables[0].Rows[0]["MAVAOVIEN"].ToString());
                thanhtoanbhyt.IDTTRV = long.Parse(dset.Tables[0].Rows[0]["ID"].ToString());
                thanhtoanbhyt.MaQuanLy = dset.Tables[0].Rows[0]["MAQL"].ToString();

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
                sql = "select  kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll,btdbn bn,btdkp_bv kp,xxxxx.v_ttrvct ct where ct.id=ds.id and ct.madoituong=1 and ll.makp=kp.makp and bn.mabn=ds.mabn and ds.id=ll.id and  ll.loaibn=3 and to_char(ds.NgayUD,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(ds.NgayVao,'YYYYMMDD')<='" + for_ngay_yyyymmdd(tungay) + "' and ds.mabn not in (select mabn from xxxxx.bhytkb where to_char(Ngay,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay)+" ') group by kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien";
            }
            else
            {
                sql = "select  kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien from xxxxx.v_ttrvds ds,xxxxx.v_ttrvll ll,btdbn bn,btdkp_bv kp,xxxxx.v_ttrvct ct where ct.id=ds.id and ct.madoituong=1 and ll.makp=kp.makp and bn.mabn=ds.mabn and ds.id=ll.id and  ll.loaibn=3 and to_char(ds.NgayUD,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "' and to_char(ds.NgayVao,'YYYYMMDD')<='" + for_ngay_yyyymmdd(tungay) + "' and ds.mabn not in (select mabn from xxxxx.bhytkb where to_char(Ngay,'YYYYMMDD')>='" + for_ngay_yyyymmdd(tungay) + "') and ds.mavaovien not in (" + IDDaDuyet + ") group by kp.tenkp,ds.mabn, bn.hoten, bn.namsinh, ll.sotien";
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
