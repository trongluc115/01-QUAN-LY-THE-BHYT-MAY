using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class CBenhNhan
    {
        #region Khai Bao Bien
        private string mabn;
        private string hoten;
        private int namsinh;
        private int gioitinh;
        private string diachi;
        private string dienthoai;
        #endregion
        #region thuoc tinh
        public string MaBN
        {
            get { return mabn; }
            set { mabn = value; }
        }
        public string HoTen
        {
            get { return hoten; }
            set { hoten = value; }
        }
        public int NamSinh
        {
            get { return namsinh; }
            set { namsinh = value; }
        }
        public string DiaChi
        {
            get { return diachi; }
            set { diachi = value; }
        }
        public string DienThoai
        {
            get { return dienthoai; }
            set { dienthoai = value; }
        }
        public int GioiTinh
        {
            get { return gioitinh; }
            set { gioitinh = value; }
        }
        #endregion
    }
}
