using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoa.Models
{
    [Table("UserRoles")]
    public class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        [ForeignKey("RoleId")]
        public Roles Role { get; set; }
    }
}
