﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [DisplayName("DisplayOrder")]
        [Range(0, 100, ErrorMessage = "Display Order must be 1-100")]
        public int DisplayOrder { get; set; }

    }
}
