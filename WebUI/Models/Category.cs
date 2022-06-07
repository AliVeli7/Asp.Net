using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
