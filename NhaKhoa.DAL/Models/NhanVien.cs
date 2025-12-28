using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.DAL.Models
{
    [Table("NHANVIEN")]
    public class NhanVien
    {
        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(100)]
        public string TenNV { get; set; }

        [StringLength(10)]
        public string MaCV { get; set; }

        [StringLength(20)]
        public string SDT { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayVaoLam { get; set; }

        public int? UserId { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }
    }
}
