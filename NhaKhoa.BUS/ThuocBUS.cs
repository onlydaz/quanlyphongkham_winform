using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.BUS
{
    public class ThuocBUS
    {
        private readonly ThuocDAL _dal;

        public ThuocBUS()
        {
            _dal = new ThuocDAL();
        }

        public List<Thuoc> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Thuoc> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public void ThemThuoc(Thuoc t)
        {
            if (string.IsNullOrWhiteSpace(t.TenThuoc))
                throw new ArgumentException("T�n thu?c kh�ng du?c d? tr?ng");
            if (t.DonGia < 0)
                throw new ArgumentException("�on gi� kh�ng h?p l?");
            if (t.SoLuongTon < 0)
                throw new ArgumentException("S? lu?ng t?n kh�ng h?p l?");

            if (string.IsNullOrEmpty(t.MaThuoc))
                t.MaThuoc = _dal.GetNewMaThuoc();

            _dal.Insert(t);
        }

        public void CapNhatThuoc(Thuoc t)
        {
            if (string.IsNullOrWhiteSpace(t.MaThuoc))
                throw new ArgumentException("M� thu?c kh�ng du?c d? tr?ng");
            if (string.IsNullOrWhiteSpace(t.TenThuoc))
                throw new ArgumentException("T�n thu?c kh�ng du?c d? tr?ng");
            if (t.DonGia < 0)
                throw new ArgumentException("�on gi� kh�ng h?p l?");
            if (t.SoLuongTon < 0)
                throw new ArgumentException("S? lu?ng t?n kh�ng h?p l?");

            _dal.Update(t);
        }

        public void XoaThuoc(string maThuoc)
        {
            if (string.IsNullOrWhiteSpace(maThuoc))
                throw new ArgumentException("M� thu?c kh�ng h?p l?");

            _dal.Delete(maThuoc);
        }

        public Thuoc LayThuocTheoMa(string maThuoc)
        {
            return _dal.GetById(maThuoc);
        }
    }
}
