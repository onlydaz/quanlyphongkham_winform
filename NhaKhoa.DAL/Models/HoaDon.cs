using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.DAL.Models
{
    [Table("HOADON")]
    public class HoaDon
    {
        [Key]
        [StringLength(50)]
        public string MaHD { get; set; }

        public DateTime NgayLapHD { get; set; }

        public decimal TongTien { get; set; }

        [StringLength(50)]
        public string MaBN { get; set; }

        [StringLength(50)]
        public string MaNV { get; set; }

        // ===== Navigation properties =====

        [ForeignKey("MaBN")]
        public virtual BenhNhan BenhNhan { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        // Nếu có bảng CHITIETHOADON
        // public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}






