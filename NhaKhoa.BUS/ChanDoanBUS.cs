using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.Models;

namespace NhaKhoa.BUS
{
    public class ChanDoanBUS
    {
        private readonly ChanDoanDAL _dal;

        public ChanDoanBUS()
        {
            _dal = new ChanDoanDAL();
        }

        public List<ChanDoan> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<ChanDoan> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public List<ChanDoan> LayChanDoanTheoMaBN(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            return _dal.GetByMaBN(maBN);
        }

        // Alias method để tương thích với code cũ
        public List<ChanDoan> LayChuanDoanTheoMaBN(string maBN)
        {
            return LayChanDoanTheoMaBN(maBN);
        }

        public ChanDoan LayChanDoanTheoMa(string maCD)
        {
            if (string.IsNullOrWhiteSpace(maCD))
                throw new ArgumentException("Mã chẩn đoán không được để trống");

            return _dal.GetById(maCD);
        }

        public void ThemChanDoan(ChanDoan cd)
        {
            if (string.IsNullOrWhiteSpace(cd.TenChuanDoan))
                throw new ArgumentException("Tên chẩn đoán không được để trống");

            // Theo ERD mới: CHANDOAN không có MaLS, liên kết được quản lý ở bảng LAMSAN
            // Không yêu cầu MaLS nữa
            // if (string.IsNullOrWhiteSpace(cd.MaLS))
            //     throw new ArgumentException("Mã lâm sàng (MaLS) không được để trống");

            if (string.IsNullOrWhiteSpace(cd.MaCD))
                cd.MaCD = _dal.GetNewMaCD();

            _dal.Insert(cd);
        }

        public void CapNhatChanDoan(ChanDoan cd)
        {
            if (string.IsNullOrWhiteSpace(cd.MaCD))
                throw new ArgumentException("Mã chẩn đoán không được để trống");

            if (string.IsNullOrWhiteSpace(cd.TenChuanDoan))
                throw new ArgumentException("Tên chẩn đoán không được để trống");

            // Theo ERD mới: CHANDOAN không có MaLS, liên kết được quản lý ở bảng LAMSAN
            // Không yêu cầu MaLS nữa
            // if (string.IsNullOrWhiteSpace(cd.MaLS))
            //     throw new ArgumentException("Mã lâm sàng (MaLS) không được để trống");

            var existing = _dal.GetById(cd.MaCD);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy chẩn đoán với mã {cd.MaCD}");

            _dal.Update(cd);
        }

        public void XoaChanDoan(string maCD)
        {
            if (string.IsNullOrWhiteSpace(maCD))
                throw new ArgumentException("Mã chẩn đoán không hợp lệ");

            _dal.Delete(maCD);
        }

        // Alias methods để tương thích với code cũ
        public ChanDoan LayChuanDoanTheoMa(string maCD)
        {
            return LayChanDoanTheoMa(maCD);
        }

        public void ThemChuanDoan(ChanDoan cd)
        {
            ThemChanDoan(cd);
        }

        public void CapNhatChuanDoan(ChanDoan cd)
        {
            CapNhatChanDoan(cd);
        }

        public void XoaChuanDoan(string maCD)
        {
            XoaChanDoan(maCD);
        }
    }
}
