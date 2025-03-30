using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProductApi.Models.DTO;
using ProductApi.UtilityClasses;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace ProductApi.Controllers
{
    [Route("api/v1/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController>? _logger;

        public InvoiceController()
        {

        }

        [ActivatorUtilitiesConstructor]
        public InvoiceController(ILogger<InvoiceController> logger)
        {
            _logger = logger;
        }

        /// <summary> 
        /// Saves invoice data
        /// </summary>
        /// <returns> 
        /// A ObjectResult whether the data was inserted (Status Created),
        /// data is invalid (Status BadRequest), or an internal error occurred 
        /// (Status InternalServerError)
        /// </returns>
        [HttpPost]
        [Route("generate-invoice")]
        [SwaggerOperation("Saves invoice data")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GenerateInvoice([FromBody, Required] InvoiceDataDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            _logger?.LogInformation("Validating data");

            var valid = true;
            var validate = new InvoiceValidate();

            foreach (var invoice in invoiceDTO.ProductData)
            {
                if(!validate.ValidateProduct(invoice.Product) || !validate.ValidateQuantity(invoice.Quantity)
                    || !validate.ValidateUnitPrice(invoice.UnitPrice) 
                    || !validate.ValidateDiscount(invoice.UnitPrice,invoice.Discount, invoice.Quantity)) 
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
                return BadRequest("Invalid request");

            _logger?.LogInformation("Data is valid. Inserting invoice data.");

            var connectionDB = string.Empty;

            try
            {
                connectionDB = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
                                    .GetSection("ConnectionStrings")["ConnectionString"];
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var invoiceID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionDB))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "sp_InsertInvoiceData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TransctionDate", invoiceDTO.TransactionDate);
                    cmd.Parameters.AddWithValue("@Total", invoiceDTO.Total);
                    cmd.Parameters.AddWithValue("@Balance", invoiceDTO.Balance);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        invoiceID = reader.GetInt32("InvoiceID");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            foreach (var invoice in invoiceDTO.ProductData)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionDB))
                    {
                        connection.Open();
                        var cmd = connection.CreateCommand();
                        cmd.CommandText = "sp_InsertInvoiceProduct";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                        cmd.Parameters.AddWithValue("@Product", invoice.Product);
                        cmd.Parameters.AddWithValue("@Quantity", invoice.Quantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", invoice.UnitPrice);
                        cmd.Parameters.AddWithValue("@Discount", invoice.Discount);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}