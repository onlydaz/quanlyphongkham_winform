using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

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
                throw new ArgumentException("M� d?ng c? kh�ng du?c d? tr?ng");

            return _dal.GetById(maDC);
        }

        public void ThemVatLieu(VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("T�n d?ng c? kh�ng du?c d? tr?ng");

            if (vl.SoLuong < 0)
                throw new ArgumentException("S? lu?ng kh�ng h?p l?");

            if (vl.DonGia < 0)
                throw new ArgumentException("�on gi� kh�ng h?p l?");

            if (string.IsNullOrWhiteSpace(vl.MaDC))
                vl.MaDC = _dal.GetNewMaDC();

            _dal.Insert(vl);
        }

        public void CapNhatVatLieu(VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.MaDC))
                throw new ArgumentException("M� d?ng c? kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("T�n d?ng c? kh�ng du?c d? tr?ng");

            if (vl.SoLuong < 0)
                throw new ArgumentException("S? lu?ng kh�ng h?p l?");

            if (vl.DonGia < 0)
                throw new ArgumentException("�on gi� kh�ng h?p l?");

            _dal.Update(vl);
        }

        public void XoaVatLieu(string maDC)
        {
            if (string.IsNullOrWhiteSpace(maDC))
                throw new ArgumentException("M� d?ng c? kh�ng h?p l?");

            _dal.Delete(maDC);
        }
    }
}
