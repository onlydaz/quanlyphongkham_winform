using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class ChanDoanBUS
    {
        private readonly ChanDoanDAL _dal;

        public ChanDoanBUS()
        {
            _dal = new ChanDoanDAL();
        }

        public List<Models.ChanDoan> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.ChanDoan> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public Models.ChanDoan LayChanDoanTheoMa(string maCD)
        {
            if (string.IsNullOrWhiteSpace(maCD))
                throw new ArgumentException("Mã chẩn đoán không được để trống");

            return _dal.GetById(maCD);
        }

        public void ThemChanDoan(Models.ChanDoan cd)
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

        public void CapNhatChanDoan(Models.ChanDoan cd)
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
    }
}
