using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lesson5_Lab.Domain.Entities
{
    [Table ("tblRoles")]
    public class AppRole
    {
        [Key]
        public long Id { get; protected set; }
        
        [Required, StringLength(250)]
        public string Name { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }


    }
}
