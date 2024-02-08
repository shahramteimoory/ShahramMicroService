using Catalog.Application.Interface;
using Catalog.Common;
using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Application.Services.Products
{
    public class ProductManagmentService : IProductManagmentService
    {
        private readonly IDataBaseContext _context;
        public ProductManagmentService(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ResultDto> CreateProduct(INserProduct_Dto req)
        {
            try
            {
                Product product = new Product();
                product.Summary = req.Summary;
                product.Name = req.Name;
                product.ImageName = req.ImageName;
                product.Name=req.Name;
                
                await _context.Products.InsertOneAsync(product);
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.Public.Success.GetDescription(),
                    StatusCode = System.Net.HttpStatusCode.Created
                };
            }
            catch (Exception)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.Public.ServerException.GetDescription(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<ResultDto> DeleteProduct(string Id)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);
                DeleteResult deleteResult=await _context.Products.DeleteOneAsync(filter);
                if (deleteResult.IsAcknowledged && deleteResult.DeletedCount>0)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Alert.Public.Success.GetDescription(),
                        StatusCode = System.Net.HttpStatusCode.NoContent
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.Public.ServerException.GetDescription(),
                    StatusCode = System.Net.HttpStatusCode.ServiceUnavailable
                };
            }
            catch (Exception)
            {

                return new ResultDto { IsSuccess = false,Message=Alert.Public.ServerException.GetDescription(),StatusCode = System.Net.HttpStatusCode.InternalServerError};
            }
        }

        public async Task<ResultDto<Product>> GetProduct(string Id)
        {
            try
            {
                var product=await _context.Products.Find(p=>p.Id==Id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return new ResultDto<Product>
                    {
                        IsSuccess = false,
                        Message = Alert.Public.NotFound.GetDescription(),
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };
                }
                return new ResultDto<Product>
                {
                    IsSuccess = true,
                    Message = Alert.Public.Success.GetDescription(),
                    Data = product,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception)
            {

                return new ResultDto<Product>
                {
                    IsSuccess = false,
                    Message = Alert.Public.ServerException.GetDescription(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<ResultDto<IEnumerable<Product>>> GetProductByCategory(string Category)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, Category);
                var products = await _context.Products.Find(filter).ToListAsync();
                return new ResultDto<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = products,
                    Message = Alert.Public.Success.GetDescription()
                };
            }
            catch (Exception)
            {

                return new ResultDto<IEnumerable<Product>> { IsSuccess = false,Message=Alert.Public.ServerException.GetDescription(),StatusCode = System.Net.HttpStatusCode.InternalServerError};
            }

        }

        public async Task<ResultDto<IEnumerable<Product>>> GetProductByName(string name)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
                var products = await _context.Products.Find(filter).ToListAsync();
                return new ResultDto<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = products,
                    Message = Alert.Public.Success.GetDescription(),
                };
            }
            catch (Exception)
            {

                return new ResultDto<IEnumerable<Product>> { IsSuccess = false,StatusCode = System.Net.HttpStatusCode.InternalServerError,Message=Alert.Public.ServerException.GetDescription() };
            }
        }

        public async Task<ResultDto<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var product=await _context.Products.Find(p=>true).ToListAsync();
                return new ResultDto<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Data = product,
                    Message=Alert.Public.Success.GetDescription()
                };
            }
            catch (Exception)
            {
                return new ResultDto<IEnumerable<Product>> 
                { IsSuccess = false,Message=Alert.Public.ServerException.GetDescription(),StatusCode=System.Net.HttpStatusCode.InternalServerError };
                
            }
        }

        public async Task<ResultDto> UpdateProduct(Product product)
        {
            try
            {
                var updateresult = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);
                if (updateresult.IsAcknowledged && updateresult.ModifiedCount>0)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Alert.Public.Success.GetDescription(),
                        StatusCode = System.Net.HttpStatusCode.NoContent,
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.ServiceUnavailable,
                    Message= Alert.Public.ServerException.GetDescription()
                };
            }
            catch (Exception)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.Public.ServerException.GetDescription(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
    public class INserProduct_Dto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Desceription { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
    }
}
