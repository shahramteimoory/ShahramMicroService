using Catalog.Application.Services.Products;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Catalog.Common;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly IProductManagmentService _productManagmentService;
        public CatalogController(IProductManagmentService productManagmentService)
        {
            _productManagmentService = productManagmentService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResultDto<IEnumerable<Product>>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
           var rsult=await _productManagmentService.GetProducts();
            Response.StatusCode = (int)rsult.StatusCode;
            return Json (rsult);
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType(typeof(ResultDto<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProducts(string id)
        {
            var rsult = await _productManagmentService.GetProduct(id);
            Response.StatusCode = (int)rsult.StatusCode;
            return Json(rsult);
        }

        [HttpGet("Category")]
        [ProducesResponseType(typeof(ResultDto<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPRoductByCategoryName(string categpry)
        {
            var rsult = await _productManagmentService.GetProductByCategory(categpry);
            Response.StatusCode = (int)rsult.StatusCode;
            return Json(rsult);
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct(INserProduct_Dto product)
        {
            var result=await _productManagmentService.CreateProduct(product);
            Response.StatusCode = (int)result.StatusCode;
            return Json(result);
        }
    }
}
