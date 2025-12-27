using System;
using System.Collections.Generic;
using NhaKhoa.DAL;

namespace NhaKhoa.BUS
{
    public class ChuanDoanBUS
    {
        private readonly ChuanDoanDAL _dal;

        public ChuanDoanBUS()
        {
            _dal = new ChuanDoanDAL();
        }

        public List<Models.ChanDoan> LayDanhSach()
        {
            return _dal.GetAll();
        }

        public List<Models.ChanDoan> LayChuanDoanTheoMaBN(string maBN)
        {
            if (string.IsNullOrWhiteSpace(maBN))
                throw new ArgumentException("Mã bệnh nhân không được để trống");

            return _dal.GetByMaBN(maBN);
        }

        public Models.ChanDoan LayChuanDoanTheoMa(string maCD)
        {
            if (string.IsNullOrWhiteSpace(maCD))
                throw new ArgumentException("Mã chẩn đoán không được để trống");

            return _dal.GetById(maCD);
        }

        public void ThemChuanDoan(Models.ChanDoan cd)
        {
            if (string.IsNullOrWhiteSpace(cd.TenChuanDoan))
                throw new ArgumentException("Tên chẩn đoán không được để trống");

            if (string.IsNullOrWhiteSpace(cd.MaCD))
                cd.MaCD = _dal.GetNewMaCD();

            _dal.Insert(cd);
        }

        public void CapNhatChuanDoan(Models.ChanDoan cd)
        {
            if (string.IsNullOrWhiteSpace(cd.MaCD))
                throw new ArgumentException("Mã chẩn đoán không được để trống");

            if (string.IsNullOrWhiteSpace(cd.TenChuanDoan))
                throw new ArgumentException("Tên chẩn đoán không được để trống");

            var existing = _dal.GetById(cd.MaCD);
            if (existing == null)
                throw new ArgumentException($"Không tìm thấy chẩn đoán với mã {cd.MaCD}");

            _dal.Update(cd);
        }

        public void XoaChuanDoan(string maCD)
        {
            if (string.IsNullOrWhiteSpace(maCD))
                throw new ArgumentException("Mã chẩn đoán không hợp lệ");

            _dal.Delete(maCD);
        }
    }
}
