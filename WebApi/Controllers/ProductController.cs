using System;
using System.Linq;
using BusinessEntities;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Products;
using System.Net;



namespace WebApi.Controllers
{

    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create([FromBody] ProductModel  productModel)
        {

            if (productModel == null)
            {
                return  DoesNotExist();
            }

            try
            {

                var product = new Product(productModel.Name, productModel.Price ?? 0, productModel.StockQuantity);

                var createdProduct = _productService.CreateProduct(product);
                productModel.Id = product.Id;

                return Request.CreateResponse(HttpStatusCode.Created, productModel);

            }
            catch 
            {
                return DoesNotExist();
            }
        }


        [Route("{id:guid}/update")]
        [HttpPost]
        public HttpResponseMessage Update(Guid id, [FromBody] ProductModel productModel)
        {
            if (productModel == null)
            {
                return DoesNotExist();
            }
            if (productModel == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid product data.");

            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found.");

            existingProduct.SetName(productModel.Name);
            existingProduct.SetPrice(productModel.Price ?? 0);
            existingProduct.SetStockQuantity(productModel.StockQuantity);

            bool updated = _productService.UpdateProduct(id, existingProduct);
            return updated
                ? Request.CreateResponse(HttpStatusCode.OK, existingProduct)
                : Request.CreateResponse(HttpStatusCode.NotFound, "Update failed.");

        }

        [Route("{id:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            var result = _productService.DeleteProduct(id);
            if (!result)
            {
                return DoesNotExist();
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Product Deleted");
        }

        [Route("{id:guid}/getById")]
        [HttpGet]
        public HttpResponseMessage GetById(Guid id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return DoesNotExist();
            }

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        [Route("getList")]
        [HttpGet]
        public HttpResponseMessage GetList(string name = null,decimal? minPrice = null, decimal? maxPrice = null)
        {
            var products = _productService.GetList(name, minPrice, maxPrice)
                .Select(p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    StockQuantity = p.StockQuantity,
                    Price = p.Price
                })
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

    }

}