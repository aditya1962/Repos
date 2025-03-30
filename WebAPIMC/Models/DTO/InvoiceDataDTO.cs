using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO
{
    public class InvoiceDataDTO
    {
        [BindProperty]
        [Required]
        public string? TransactionDate { get; set; }

        [BindProperty]
        [Required]
        public double Total { get; set; }

        [BindProperty]
        [Required]
        public List<InvoiceRequestDTO> ProductData { get; set; }

        [BindProperty]
        [Required]
        public double Balance { get; set; } = 0;
    }
}
