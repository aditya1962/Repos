using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO
{
    public class InvoiceDataDTO
    {
        [BindProperty]
        [Required]
        public string? TransctionDate { get; set; }

        [BindProperty]
        [Required]
        public double Total { get; set; }

        [BindProperty]
        [Required]
        public List<InvoiceRequestDTO> ProductData { get; set; }
    }
}
