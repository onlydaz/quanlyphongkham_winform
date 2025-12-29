using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

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
                throw new ArgumentException("M� h�a don kh�ng du?c d? tr?ng");

            return _dal.GetById(maHD);
        }

        public List<HoaDon> TimKiem(string maHD = "", string maNV = "", string tenBN = "", string tenNV = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return _dal.Search(maHD, maNV, tenBN, tenNV, tuNgay, denNgay);
        }

        public void ThemHoaDon(HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaBN))
                throw new ArgumentException("M� b?nh nh�n kh�ng du?c d? tr?ng");

            if (string.IsNullOrWhiteSpace(hd.MaNV))
                throw new ArgumentException("M� nh�n vi�n kh�ng du?c d? tr?ng");

            if (hd.TongTien < 0)
                throw new ArgumentException("T?ng ti?n kh�ng h?p l?");

            if (string.IsNullOrWhiteSpace(hd.MaHD))
                hd.MaHD = _dal.GetNewMaHD();

            if (hd.NgayLapHD == default(DateTime))
                hd.NgayLapHD = DateTime.Now;

            _dal.Insert(hd);
        }

        public void CapNhatHoaDon(HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaHD))
                throw new ArgumentException("M� h�a don kh�ng du?c d? tr?ng");

            if (hd.TongTien < 0)
                throw new ArgumentException("T?ng ti?n kh�ng h?p l?");

            var existing = _dal.GetById(hd.MaHD);
            if (existing == null)
                throw new ArgumentException($"Kh�ng t�m th?y h�a don v?i m� {hd.MaHD}");

            _dal.Update(hd);
        }

        public void XoaHoaDon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                throw new ArgumentException("M� h�a don kh�ng h?p l?");

            _dal.Delete(maHD);
        }
    }
}
