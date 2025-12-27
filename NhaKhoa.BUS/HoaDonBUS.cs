using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class HoaDonBUS
    {
        private readonly HoaDonDAL _dal;

        public HoaDonBUS()
        {
            _dal = new HoaDonDAL();
        }

        public List<HoaDon> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public HoaDon LayHoaDonTheoMa(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                throw new ArgumentException("Mã hóa don không du?c d? tr?ng");

            return _dal.GetById(maHD);
        }

        public List<HoaDon> TimKiem(string maHD = "", string maNV = "", string tenBN = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return _dal.Search(maHD, maNV, tenBN, tuNgay, denNgay);
        }

        public void ThemHoaDon(HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaBN))
                throw new ArgumentException("Mã b?nh nhân không du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(hd.MaNV))
                throw new ArgumentException("Mã nhân viên không du?c d? tr?ng");

            if (hd.TongTien < 0)
                throw new ArgumentException("T?ng ti?n không h?p l?");

            if (string.IsNullOrWhiteSpace(hd.MaHD))
                hd.MaHD = _dal.GetNewMaHD();

            if (hd.NgayLapHD == default(DateTime))
                hd.NgayLapHD = DateTime.Now;

            _dal.Insert(hd);
        }

        public void CapNhatHoaDon(HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaHD))
                throw new ArgumentException("Mã hóa don không du?c d? tr?ng");

            if (hd.TongTien < 0)
                throw new ArgumentException("T?ng ti?n không h?p l?");

            var existing = _dal.GetById(hd.MaHD);
            if (existing == null)
                throw new ArgumentException($"Không tìm th?y hóa don v?i mã {hd.MaHD}");

            _dal.Update(hd);
        }

        public void XoaHoaDon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                throw new ArgumentException("Mã hóa don không h?p l?");

            _dal.Delete(maHD);
        }
    }
}
