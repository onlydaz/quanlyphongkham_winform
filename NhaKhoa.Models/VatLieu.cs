using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
{
    [Table("DUNGCU")]
    public class VatLieu
    {
        [Key]
        [StringLength(50)]
        public string MaDC { get; set; }

        [Required]
        [StringLength(200)]
        public string TenDC { get; set; }

        public int SoLuong { get; set; }

        [StringLength(50)]
        public string DVT { get; set; }

        public decimal DonGia { get; set; }
    }
}
