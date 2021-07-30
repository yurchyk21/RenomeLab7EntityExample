using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lesson5_Lab.Domain.Entities
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public long Id { get; protected set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }
}
