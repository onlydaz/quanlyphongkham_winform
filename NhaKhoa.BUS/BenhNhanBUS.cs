using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class BenhNhanBUS
    {
        private readonly BenhNhanDAL _dal;

        public BenhNhanBUS()
        {
            _dal = new BenhNhanDAL();
        }

        public List<BenhNhan> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public BenhNhan LayBenhNhanTheoMa(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã b?nh nhân không du?c d? tr?ng");

            return _dal.GetById(maBN);
        }

        public List<BenhNhan> TimKiem(string ma = "", string ten = "", string sdt = "")
        {
            return _dal.Search(ma, ten, sdt);
        }

        public List<BenhNhan> LayDanhSachChoKham(string trangThai = "Ch? khám")
        {
            return _dal.GetByTrangThai(trangThai);
        }

        public void ThemBenhNhan(BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("Tên b?nh nhân không du?c d? tr?ng");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Nam sinh không h?p l?");

            if (string.IsNullOrWhiteSpace(bn.MaBN))
                bn.MaBN = _dal.GetNewMaBN();

            // Ki?m tra trùng mã
            var existing = _dal.GetById(bn.MaBN);
            if (existing != null)
                throw new ArgumentException($"Mã b?nh nhân {bn.MaBN} dã t?n t?i");

            _dal.Insert(bn);
        }

        public void CapNhatBenhNhan(BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.MaBN))
                throw new ArgumentException("Mã b?nh nhân không du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("Tên b?nh nhân không du?c d? tr?ng");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Nam sinh không h?p l?");

            var existing = _dal.GetById(bn.MaBN);
            if (existing == null)
                throw new ArgumentException($"Không tìm th?y b?nh nhân v?i mã {bn.MaBN}");

            _dal.Update(bn);
        }

        public void XoaBenhNhan(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã b?nh nhân không h?p l?");

            _dal.Delete(maBN);
        }

        public void CapNhatTrangThai(string maBN, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã b?nh nhân không du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Tr?ng thái không du?c d? tr?ng");

            var existing = _dal.GetById(maBN);
            if (existing == null)
                throw new ArgumentException($"Không tìm th?y b?nh nhân v?i mã {maBN}");

            existing.TrangThai = trangThai;
            _dal.Update(existing);
        }
    }
}
