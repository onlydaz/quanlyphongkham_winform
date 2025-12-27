using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
{
    [Table("THUOC")]
    public class Thuoc
    {
        [Key]
        [StringLength(50)]
        public string MaThuoc { get; set; }

        [Required]
        [StringLength(200)]
        public string TenThuoc { get; set; }

        [StringLength(50)]
        public string DVT { get; set; }   // Đơn vị tính

        public decimal DonGia { get; set; }

        public int SoLuongTon { get; set; }
    }
}
