using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class ThuocBUS
    {
        private readonly ThuocDAL _dal;

        public ThuocBUS()
        {
            _dal = new ThuocDAL();
        }

        public List<Models.Thuoc> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.Thuoc> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public void ThemThuoc(Models.Thuoc t)
        {
            if (string.IsNullOrWhiteSpace(t.TenThuoc))
                throw new ArgumentException("Tên thuốc không được để trống");
            if (t.DonGia < 0)
                throw new ArgumentException("Đơn giá không hợp lệ");
            if (t.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn không hợp lệ");

            if (string.IsNullOrEmpty(t.MaThuoc))
                t.MaThuoc = _dal.GetNewMaThuoc();

            _dal.Insert(t);
        }

        public void CapNhatThuoc(Models.Thuoc t)
        {
            if (string.IsNullOrWhiteSpace(t.MaThuoc))
                throw new ArgumentException("Mã thuốc không được để trống");
            if (string.IsNullOrWhiteSpace(t.TenThuoc))
                throw new ArgumentException("Tên thuốc không được để trống");
            if (t.DonGia < 0)
                throw new ArgumentException("Đơn giá không hợp lệ");
            if (t.SoLuongTon < 0)
                throw new ArgumentException("Số lượng tồn không hợp lệ");

            _dal.Update(t);
        }

        public void XoaThuoc(string maThuoc)
        {
            if (string.IsNullOrWhiteSpace(maThuoc))
                throw new ArgumentException("Mã thuốc không hợp lệ");

            _dal.Delete(maThuoc);
        }

        public Models.Thuoc LayThuocTheoMa(string maThuoc)
        {
            return _dal.GetById(maThuoc);
        }
    }
}
