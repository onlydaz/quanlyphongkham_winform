using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

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
                throw new ArgumentException("M� b?nh nh�n kh�ng du?c d? tr?ng");

            return _dal.GetById(maBN);
        }

        public List<BenhNhan> TimKiem(string ma = "", string ten = "", string sdt = "")
        {
            return _dal.Search(ma, ten, sdt);
        }

        public List<BenhNhan> LayDanhSachChoKham(string trangThai = "Ch? kh�m")
        {
            return _dal.GetByTrangThai(trangThai);
        }

        public void ThemBenhNhan(BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("T�n b?nh nh�n kh�ng du?c d? tr?ng");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Nam sinh kh�ng h?p l?");

            if (string.IsNullOrWhiteSpace(bn.MaBN))
                bn.MaBN = _dal.GetNewMaBN();

            // Ki?m tra tr�ng m�
            var existing = _dal.GetById(bn.MaBN);
            if (existing != null)
                throw new ArgumentException($"M� b?nh nh�n {bn.MaBN} d� t?n t?i");

            _dal.Insert(bn);
        }

        public void CapNhatBenhNhan(BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.MaBN))
                throw new ArgumentException("M� b?nh nh�n kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("T�n b?nh nh�n kh�ng du?c d? tr?ng");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Nam sinh kh�ng h?p l?");

            var existing = _dal.GetById(bn.MaBN);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y b?nh nh�n v?i m� {bn.MaBN}");

            _dal.Update(bn);
        }

        public void XoaBenhNhan(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("M� b?nh nh�n kh�ng h?p l?");

            _dal.Delete(maBN);
        }

        public void CapNhatTrangThai(string maBN, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("M� b?nh nh�n kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Tr?ng th�i kh�ng du?c d? tr?ng");

            var existing = _dal.GetById(maBN);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y b?nh nh�n v?i m� {maBN}");

            existing.TrangThai = trangThai;
            _dal.Update(existing);
        }
    }
}
