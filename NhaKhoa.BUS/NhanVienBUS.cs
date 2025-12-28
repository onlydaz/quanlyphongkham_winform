using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _dal;

        public NhanVienBUS()
        {
            _dal = new NhanVienDAL();
        }

        public List<NhanVien> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public NhanVien LayNhanVienTheoMa(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("M� nh�n vi�n kh�ng du?c d? tr?ng");

            return _dal.GetById(maNV);
        }

        public List<NhanVien> TimKiem(string ma = "", string ten = "")
        {
            return _dal.Search(ma, ten);
        }

        public void ThemNhanVien(NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new ArgumentException("T�n nh�n vi�n kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(nv.MaNV))
                nv.MaNV = _dal.GetNewMaNV();

            // Ki?m tra tr�ng m�
            var existing = _dal.GetById(nv.MaNV);
            if (existing != null)
                throw new ArgumentException($"M� nh�n vi�n {nv.MaNV} d� t?n t?i");

            _dal.Insert(nv);
        }

        public void CapNhatNhanVien(NhanVien nv)
        {
            if (string.IsNullOrWhiteSpace(nv.MaNV))
                throw new ArgumentException("M� nh�n vi�n kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(nv.TenNV))
                throw new ArgumentException("T�n nh�n vi�n kh�ng du?c d? tr?ng");

            var existing = _dal.GetById(nv.MaNV);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y nh�n vi�n v?i m� {nv.MaNV}");

            _dal.Update(nv);
        }

        public void XoaNhanVien(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("M� nh�n vi�n kh�ng h?p l?");

            _dal.Delete(maNV);
        }

        public List<NhanVienDAL.ChucVuInfo> LayDanhSachChucVu()
        {
            return _dal.GetChucVuList();
        }
    }
}
