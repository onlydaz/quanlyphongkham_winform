using System;
using System.Collections.Generic;

namespace NhaKhoa.BUS
{
    public class LichBacSiBUS
    {
        private readonly DAL.LichBacSiDAL _dal;

        public LichBacSiBUS()
        {
            _dal = new DAL.LichBacSiDAL();
        }

        public List<Models.LichBacSi> LayTatCa()
        {
            return _dal.GetAll();
        }

        public List<Models.LichBacSi> LayTheoBacSi(string maBacSi)
        {
            return _dal.GetByBacSi(maBacSi);
        }

        public Models.LichBacSi LayTheoId(int id)
        {
            return _dal.GetById(id);
        }

        public void Them(Models.LichBacSi lich)
        {
            Validate(lich);

            // Kiểm tra trùng
            if (_dal.CheckOverlap(lich.MaBacSi, lich.NgayTrongTuan, lich.GioBatDau, lich.GioKetThuc))
                throw new ArgumentException("Lịch làm việc bị trùng.");

            lich.NgayTao = DateTime.Now;
            _dal.Insert(lich);
        }

        public void CapNhat(Models.LichBacSi lich)
        {
            Validate(lich);

            if (_dal.CheckOverlap(lich.MaBacSi, lich.NgayTrongTuan, lich.GioBatDau, lich.GioKetThuc, lich.Id))
                throw new ArgumentException("Lịch làm việc bị trùng.");

            _dal.Update(lich);
        }

        public void Xoa(int id)
        {
            _dal.Delete(id);
        }

        private void Validate(Models.LichBacSi lich)
        {
            if (string.IsNullOrWhiteSpace(lich.MaBacSi))
                throw new ArgumentException("Chưa chọn bác sĩ.");
            if (lich.GioKetThuc <= lich.GioBatDau)
                throw new ArgumentException("Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            if (lich.NgayTrongTuan > 6)
                throw new ArgumentException("Ngày trong tuần không hợp lệ.");
        }
    }
}
