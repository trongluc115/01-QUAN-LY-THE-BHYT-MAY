using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class CThanhToanBHYT
    {
        #region Khai Bao Bien
        private long id;
        private string mabn;
        private string nhomthe;
        private string manhomthe;
        private string loaithe;
        private string sothebhyt;
        private string noidangkybhyt;
        private string mabv;
        private long mavaovien;
        private string icd;
        private string chandoan;
        private long tongtien=0;
        private long bhyttra=0;
        private long bntra=0;
        private long thuoc=0;
        private long mau=0;
        private long xetnghiem=0;
        private long cdha=0;
        private long dvktthongthuong=0;
        private long dvktcao=0;
        private long vtyt=0;
        private long tienkham=0;
        private long chiphivc=0;
        private long thamdochucnang=0;
        private long giuong=0;
        private long khac = 0;
        private int traituyen;
        private string sophieu;
        private long idttrv;
        private string loaiba;
        private string sobienlai;
        private DateTime hsd;
        private DateTime ngaylamviec;
        private DateTime ngayvao;
        private DateTime ngayra;
        private string maql;
        #endregion
        #region thuoc tinh
        public long ID
        {
            get { return id; }
            set { id = value; }
        }
        public string MaBN
        {
            get { return mabn; }
            set { mabn = value; }
        }
        public string NhomThe
        {
            get { return nhomthe; }
            set { nhomthe = value; }
        }
        public string MaNhomThe
        {
            get { return manhomthe; }
            set { manhomthe = value; }
        }
        public string LoaiThe
        {
            get { return loaithe; }
            set { loaithe = value; }
        }
        public string SoTheBHYT
        {
            get { return sothebhyt; }
            set { sothebhyt = value; }
        }
        public long MaVaoVien
        {
            get { return mavaovien; }
            set { mavaovien = value; }
        }
        public string ICD
        {
            get { return icd; }
            set { icd = value; }
        }
        public string ChanDoan
        {
            get { return chandoan; }
            set { chandoan = value; }
        }
        public long TongTien
        {
            get { return tongtien; }
            set { tongtien = value; }
        }
        public long BHYTTra
        {
            get { return bhyttra; }
            set { bhyttra = value; }
        }
        public long BNTra
        {
            get { return bntra; }
            set { bntra = value; }
        }
        public long Thuoc
        {
            get { return thuoc; }
            set { thuoc = value; }
        }
        public long Mau
        {
            get { return mau; }
            set { mau = value; }
        }
        public long XetNghiem
        {
            get { return xetnghiem; }
            set { xetnghiem = value; }
        }
        public long CDHA
        {
            get { return cdha; }
            set { cdha = value; }
        }
        public long DVKTthongthuong
        {
            get { return dvktthongthuong; }
            set { dvktthongthuong = value; }
        
        }
        public long DVKTcao
        {
            get { return dvktcao; }
            set { dvktcao = value; }
        }
        public long VTYT
        {
            get { return vtyt; }
            set { vtyt = value; }
        }
        public long TienKham
        {
            get { return tienkham; }
            set { tienkham= value; }
        }

        public long ChiPhiVC
        {
            get { return chiphivc; }
            set { chiphivc = value; }
        }
        public long ThamDoChucNang
        {
            get { return thamdochucnang; }
            set { thamdochucnang = value; }
        }
        public long Giuong
        {
            get { return giuong; }
            set { giuong = value; }
        }
        public long Khac
        {
            get { return khac; }
            set { khac = value; }
        }
        public int TraiTuyen
        {
            get { return traituyen; }
            set { traituyen = value; }
        }
        public string SoPhieu
        {
            get { return sophieu; }
            set { sophieu = value; }
        }
        public long IDTTRV
        {
            get { return idttrv; }
            set { idttrv = value; }
        }
        public DateTime NgayLamViec
        {
            get { return ngaylamviec; }
            set { ngaylamviec = value; }
        }
        public string NoiDangKyBHYT
        {
            get { return noidangkybhyt; }
            set { noidangkybhyt = value; }
        }
        public string MaBV
        {
            get { return mabv; }
            set { mabv = value; }
        }
        public DateTime HSD
        {
            get { return hsd; }
            set { hsd = value; }
        }
        public string LoaiBA
        {
            get { return loaiba; }
            set { loaiba = value; }
        }
        public string SoBienLai
        {
            get { return sobienlai; }
            set { sobienlai = value; }
        }

        public DateTime NgayVao
        {
            get { return ngayvao; }
            set { ngayvao = value; }
        }
        public DateTime NgayRa
        {
            get { return ngayra; }
            set { ngayra = value; }
        }
        public string MaQuanLy
        {
            get { return maql; }
            set { maql = value; }
        }


        #endregion
    }
}
