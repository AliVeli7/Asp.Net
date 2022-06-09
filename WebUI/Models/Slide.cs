using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebUI.Models
{
    public class Slide
    {
        public string Url { get; set; }
        [NotMapped, Required]
        public int Id { get; set; }
        
        public IFormFile Photo { get; set; }
    }
}
