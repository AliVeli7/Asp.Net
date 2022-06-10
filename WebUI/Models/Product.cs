using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebUI.Models
{
    public class Product
    {   
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
        public bool isDeleted { get; set; }
        public ProductImage Images { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }

    }
}
