﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Category
{
    public class CategoryCreateVM
    {   
        [Required]
        public string Name { get; set; }
    }
}
