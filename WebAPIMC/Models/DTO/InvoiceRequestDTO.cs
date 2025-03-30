using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO
{
    public class InvoiceRequestDTO
    {
        [BindProperty]
        [Required]
        public string? Product { get; set; }

        [BindProperty]
        [Required]
        public int Quantity { get; set; }

        [BindProperty]
        [Required]
        public double UnitPrice { get; set; }

        [BindProperty]
        [Required]
        public double Discount { get; set; }

        [BindProperty]
        [Required]
        public double Total { get; set; }
    }
}
