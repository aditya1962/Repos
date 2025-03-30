using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO
{
    public class ProductDTO
    {
        [BindProperty]
        [Required]
        public string? Product { get; set; }

        [BindProperty]
        [Required]
        public decimal UnitPrice { get; set; }
    }
}
