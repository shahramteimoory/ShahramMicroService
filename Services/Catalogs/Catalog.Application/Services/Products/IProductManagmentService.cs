using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catalog.Common;
using Catalog.Domain.Entities;

namespace Catalog.Application.Services.Products
{
    public interface IProductManagmentService
    {
        Task<ResultDto<IEnumerable<Product>>> GetProducts();
        Task<ResultDto<Product>> GetProduct(string Id);
        Task<ResultDto<IEnumerable<Product>>> GetProductByName(string name);
        Task<ResultDto<IEnumerable<Product>>> GetProductByCategory(string Category);
        Task<ResultDto> CreateProduct(INserProduct_Dto req);
        Task<ResultDto> UpdateProduct(Product product);
        Task<ResultDto> DeleteProduct(string Id);
    }
}
