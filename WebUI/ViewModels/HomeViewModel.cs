﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewModels
{
    public class HomeViewModel
    {
        public List<Slide> Slides { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public Summary Summary { get; set; }
    }

}
