using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class VatLieuBUS
    {
        private readonly VatLieuDAL _dal;

        public VatLieuBUS()
        {
            _dal = new VatLieuDAL();
        }

        public List<Models.VatLieu> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.VatLieu> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public Models.VatLieu LayVatLieuTheoMa(string maDC)
        {
            if (string.IsNullOrWhiteSpace(maDC))
                throw new ArgumentException("Mã dụng cụ không được để trống");

            return _dal.GetById(maDC);
        }

        public void ThemVatLieu(Models.VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("Tên dụng cụ không được để trống");

            if (vl.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ");

            if (vl.DonGia < 0)
                throw new ArgumentException("Đơn giá không hợp lệ");

            if (string.IsNullOrWhiteSpace(vl.MaDC))
                vl.MaDC = _dal.GetNewMaDC();

            _dal.Insert(vl);
        }

        public void CapNhatVatLieu(Models.VatLieu vl)
        {
            if (string.IsNullOrWhiteSpace(vl.MaDC))
                throw new ArgumentException("Mã dụng cụ không được để trống");

            if (string.IsNullOrWhiteSpace(vl.TenDC))
                throw new ArgumentException("Tên dụng cụ không được để trống");

            if (vl.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ");

            if (vl.DonGia < 0)
                throw new ArgumentException("Đơn giá không hợp lệ");

            _dal.Update(vl);
        }

        public void XoaVatLieu(string maDC)
        {
            if (string.IsNullOrWhiteSpace(maDC))
                throw new ArgumentException("Mã dụng cụ không hợp lệ");

            _dal.Delete(maDC);
        }
    }
}
