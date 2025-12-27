using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class VatLieuBUS
    {
        private readonly VatLieuDAL _dal;

        public VatLieuBUS()
        {
            _dal = new VatLieuDAL();
        }

        public List<VatLieu> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<VatLieu> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public VatLieu LayVatLieuTheoMa(string maDC)
        {
            if (string.IsNullOrWhiteSpace(maDC))
                throw new ArgumentException("Mã d?ng c? không du?c d? tr?ng");

            return _dal.GetById(maDC);
        }

        public void ThemVatLieu(VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("Tên d?ng c? không du?c d? tr?ng");

            if (vl.SoLuong < 0)
                throw new ArgumentException("S? lu?ng không h?p l?");

            if (vl.DonGia < 0)
                throw new ArgumentException("Ðon giá không h?p l?");

            if (string.IsNullOrWhiteSpace(vl.MaDC))
                vl.MaDC = _dal.GetNewMaDC();

            _dal.Insert(vl);
        }

        public void CapNhatVatLieu(VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.MaDC))
                throw new ArgumentException("Mã d?ng c? không du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("Tên d?ng c? không du?c d? tr?ng");

            if (vl.SoLuong < 0)
                throw new ArgumentException("S? lu?ng không h?p l?");

            if (vl.DonGia < 0)
                throw new ArgumentException("Ðon giá không h?p l?");

            _dal.Update(vl);
        }

        public void XoaVatLieu(string maDC)
        {
            if (string.IsNullOrWhiteSpace(maDC))
                throw new ArgumentException("Mã d?ng c? không h?p l?");

            _dal.Delete(maDC);
        }
    }
}
