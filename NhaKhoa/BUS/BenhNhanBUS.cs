using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class BenhNhanBUS
    {
        private readonly BenhNhanDAL _dal;

        public BenhNhanBUS()
        {
            _dal = new BenhNhanDAL();
        }

        public List<Models.BenhNhan> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public Models.BenhNhan LayBenhNhanTheoMa(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            return _dal.GetById(maBN);
        }

        public List<Models.BenhNhan> TimKiem(string ma = "", string ten = "", string sdt = "")
        {
            return _dal.Search(ma, ten, sdt);
        }

        public List<Models.BenhNhan> LayDanhSachChoKham(string trangThai = "Chờ khám")
        {
            return _dal.GetByTrangThai(trangThai);
        }

        public void ThemBenhNhan(Models.BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("Tên bệnh nhân không được để trống");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Năm sinh không hợp lệ");

            if (string.IsNullOrWhiteSpace(bn.MaBN))
                bn.MaBN = _dal.GetNewMaBN();

            // Kiểm tra trùng mã
            var existing = _dal.GetById(bn.MaBN);
            if (existing != null)
                throw new ArgumentException($"Mã bệnh nhân {bn.MaBN} đã tồn tại");

            _dal.Insert(bn);
        }

        public void CapNhatBenhNhan(Models.BenhNhan bn)
        {
            if (string.IsNullOrWhiteSpace(bn.MaBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            if (string.IsNullOrWhiteSpace(bn.TenBN))
                throw new ArgumentException("Tên bệnh nhân không được để trống");

            if (bn.NamSinh < 1900 || bn.NamSinh > DateTime.Now.Year)
                throw new ArgumentException("Năm sinh không hợp lệ");

            var existing = _dal.GetById(bn.MaBN);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy bệnh nhân với mã {bn.MaBN}");

            _dal.Update(bn);
        }

        public void XoaBenhNhan(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã bệnh nhân không hợp lệ");

            _dal.Delete(maBN);
        }

        public void CapNhatTrangThai(string maBN, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            if (string.IsNullOrWhiteSpace(trangThai))
                throw new ArgumentException("Trạng thái không được để trống");

            var existing = _dal.GetById(maBN);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy bệnh nhân với mã {maBN}");

            existing.TrangThai = trangThai;
            _dal.Update(existing);
        }
    }
}
