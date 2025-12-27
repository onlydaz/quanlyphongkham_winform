using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class HoaDonBUS
    {
        private readonly HoaDonDAL _dal;

        public HoaDonBUS()
        {
            _dal = new HoaDonDAL();
        }

        public List<Models.HoaDon> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public Models.HoaDon LayHoaDonTheoMa(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                throw new ArgumentException("Mã hóa đơn không được để trống");

            return _dal.GetById(maHD);
        }

        public List<Models.HoaDon> TimKiem(string maHD = "", string maNV = "", string tenBN = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return _dal.Search(maHD, maNV, tenBN, tuNgay, denNgay);
        }

        public void ThemHoaDon(Models.HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            if (string.IsNullOrWhiteSpace(hd.MaNV))
                throw new ArgumentException("Mã nhân viên không được để trống");

            if (hd.TongTien < 0)
                throw new ArgumentException("Tổng tiền không hợp lệ");

            if (string.IsNullOrWhiteSpace(hd.MaHD))
                hd.MaHD = _dal.GetNewMaHD();

            if (hd.NgayLapHD == default(DateTime))
                hd.NgayLapHD = DateTime.Now;

            _dal.Insert(hd);
        }

        public void CapNhatHoaDon(Models.HoaDon hd)
        {
            if (string.IsNullOrWhiteSpace(hd.MaHD))
                throw new ArgumentException("Mã hóa đơn không được để trống");

            if (hd.TongTien < 0)
                throw new ArgumentException("Tổng tiền không hợp lệ");

            var existing = _dal.GetById(hd.MaHD);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy hóa đơn với mã {hd.MaHD}");

            _dal.Update(hd);
        }

        public void XoaHoaDon(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
                throw new ArgumentException("Mã hóa đơn không hợp lệ");

            _dal.Delete(maHD);
        }
    }
}
