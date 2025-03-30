using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProductApi.Models.DTO;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net;

namespace ProductApi.Controllers
{
    [Route("api/v1/product/")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly ILogger<ProductController>? _logger;

        public ProductController()
        {

        }

        [ActivatorUtilitiesConstructor]
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        /// <summary> 
        /// Get list of product data
        /// </summary>
        /// <returns> 
        /// A ObjectResult whether the data was returned (Status OK),
        /// data is invalid (Status BadRequest), or an internal error occurred 
        /// (Status InternalServerError)
        /// </returns>
        [HttpGet]
        [Route("get-productlist")]
        [SwaggerOperation("Get list of product data")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductList()
        {
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

            _logger?.LogInformation("Retrieve list of product data");

            var productList = new List<ProductDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionDB))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "sp_GetProducts";
                    cmd.CommandType = CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        productList.Add(new ProductDTO { Product = reader.GetString("Name"),
                                                         UnitPrice = reader.GetDecimal("UnitPrice") });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(productList);
        }
    }
}