using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _dal;

        public NhanVienBUS()
        {
            _dal = new NhanVienDAL();
        }

        public List<Models.NhanVien> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public Models.NhanVien LayNhanVienTheoMa(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("Mã nhân viên không được để trống");

            return _dal.GetById(maNV);
        }

        public List<Models.NhanVien> TimKiem(string ma = "", string ten = "")
        {
            return _dal.Search(ma, ten);
        }

        public void ThemNhanVien(Models.NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new ArgumentException("Tên nhân viên không được để trống");

            if (string.IsNullOrWhiteSpace(nv.MaNV))
                nv.MaNV = _dal.GetNewMaNV();

            // Kiểm tra trùng mã
            var existing = _dal.GetById(nv.MaNV);
            if (existing != null)
                throw new ArgumentException($"Mã nhân viên {nv.MaNV} đã tồn tại");

            _dal.Insert(nv);
        }

        public void CapNhatNhanVien(Models.NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.MaNV))
                throw new ArgumentException("Mã nhân viên không được để trống");

            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new ArgumentException("Tên nhân viên không được để trống");

            var existing = _dal.GetById(nv.MaNV);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy nhân viên với mã {nv.MaNV}");

            _dal.Update(nv);
        }

        public void XoaNhanVien(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("Mã nhân viên không hợp lệ");

            _dal.Delete(maNV);
        }
    }
}
