using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
{
    [Table("BENHNHAN")]
    public class BenhNhan
    {
        [Key]
        [StringLength(50)]
        public string MaBN { get; set; }
        public string TenBN { get; set; }
        public string GioiTinh { get; set; }
        public int NamSinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayKham { get; set; }
        public string LyDoKham { get; set; }
        
        [Column("TrangThai")]
        [StringLength(50)]
        public string TrangThai { get; set; }
    }
}
