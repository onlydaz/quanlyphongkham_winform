using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaKhoa.Models
{
    [Table("LICHBACSI")]
    public class LichBacSi
    {
        [Key]
        public int Id { get; set; }

        // Mã bác sĩ (MaNV) - tham chiếu đến bảng NHANVIEN.MaNV
        [StringLength(10)]
        public string MaBacSi { get; set; }

        // 0 = Chủ nhật, 1 = Thứ 2, ... 6 = Thứ 7
        public byte NgayTrongTuan { get; set; }

        public TimeSpan GioBatDau { get; set; }

        public TimeSpan GioKetThuc { get; set; }

        public string GhiChu { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }

        [ForeignKey("MaBacSi")]
        public NhaKhoa.Models.NhanVien BacSi { get; set; }
    }
}
