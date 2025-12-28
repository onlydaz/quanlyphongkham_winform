using System;
using System.Collections.Generic;
using NhaKhoa.DAL;
using NhaKhoa.DAL.Models;

namespace NhaKhoa.BUS
{
    public class DieuTriBUS
    {
        private readonly DieuTriDAL _dal;

        public DieuTriBUS()
        {
            _dal = new DieuTriDAL();
        }

        public List<DieuTri> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<DieuTri> TimTheoTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return LayDanhSach();

            return _dal.SearchByName(ten);
        }

        public DieuTri LayDieuTriTheoMa(string maDT)
        {
            if (string.IsNullOrWhiteSpace(maDT))
                throw new ArgumentException("Mã điều trị không được để trống");

            return _dal.GetById(maDT);
        }

        public void ThemDieuTri(DieuTri dt)
        {
            if (string.IsNullOrWhiteSpace(dt.TenDieuTri))
                throw new ArgumentException("Tên điều trị không được để trống");

            if (string.IsNullOrWhiteSpace(dt.MaDT))
                dt.MaDT = _dal.GetNewMaDT();

            _dal.Insert(dt);
        }

        public void CapNhatDieuTri(DieuTri dt)
        {
            if (string.IsNullOrWhiteSpace(dt.MaDT))
                throw new ArgumentException("Mã điều trị không được để trống");

            if (string.IsNullOrWhiteSpace(dt.TenDieuTri))
                throw new ArgumentException("Tên điều trị không được để trống");

            var existing = _dal.GetById(dt.MaDT);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy điều trị với mã {dt.MaDT}");

            _dal.Update(dt);
        }

        public void XoaDieuTri(string maDT)
        {
            if (string.IsNullOrWhiteSpace(maDT))
                throw new ArgumentException("Mã điều trị không hợp lệ");

            _dal.Delete(maDT);
        }
    }
}







