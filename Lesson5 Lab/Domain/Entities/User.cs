using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lesson5_Lab.Domain.Entities
{
    [Table("tblUsers")]
    public class AppUser
    {
        [Key]
        public long Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<AppRole> Roles { get; set; }

    }
}
