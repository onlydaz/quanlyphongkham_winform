using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.DAL.Models
{
    [Table("LAMSAN")]
    public class LamSan
    {
        [Key]
        [StringLength(10)]
        public string MaLS { get; set; }

        // Liên kết với BENHNHAN qua MaBN (nvarchar(10) in DB)
        [StringLength(10)]
        public string MaBN { get; set; }

        public DateTime? NgayKham { get; set; } // Default getdate()

        // Thêm giờ khám để có thể kiểm tra trùng giờ chi tiết
        [Column(TypeName = "time")]
        public TimeSpan? GioBatDau { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? GioKetThuc { get; set; }

        public string TrieuChung { get; set; } // nvarchar(MAX)

        [StringLength(10)]
        public string MaNV { get; set; } // nvarchar(10)

        // Khóa ngoại liên kết tới DIEUTRI
        [StringLength(10)]
        public string MaDT { get; set; }

        // Khóa ngoại liên kết tới CHANDOAN
        [StringLength(10)]
        public string MaCD { get; set; }

        // --- MAPPING / NAVIGATION PROPERTIES ---
        // Mapping: REFERENCES BENHNHAN(MaBN)
        public virtual BenhNhan BenhNhan { get; set; }

        // Mapping: REFERENCES DIEUTRI(MaDT) - một LAMSAN có một DIEUTRI
        public virtual DieuTri DieuTri { get; set; }

        // Mapping: REFERENCES CHANDOAN(MaCD) - một LAMSAN có một CHANDOAN
        public virtual ChanDoan ChanDoan { get; set; }
    }
}
