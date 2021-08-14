using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int TotalQty { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string PublicId { get; set; }
        public int Id { get; set; }
    }
}
