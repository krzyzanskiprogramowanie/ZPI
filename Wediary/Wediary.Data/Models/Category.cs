﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wediary.Data.Models
{
   public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
