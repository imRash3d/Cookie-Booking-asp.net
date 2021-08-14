using CookieBooking.Constraint;
using CookieBooking.Dtos;
using CookieBooking.Entities;
using CookieBooking.Helpers;
using CookieBooking.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IPhotoService _photoService;
        public ProductController(IProductService productService, IPhotoService photoService)
        {
            _productService = productService;
            _photoService = photoService;
        }



        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<CommandResponse>> addProduct(CreateProductDto createProductDto)
        {
            CommandResponse response = new CommandResponse();



            Product product = new Product
            {
                ProductName = createProductDto.ProductName,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                TotalQty = createProductDto.TotalQty,
                Category = createProductDto.Category,
                ImageUrl = createProductDto.ImageUrl
               
            };

             _productService.addProduct(product);
            if (await _productService.SaveAllAsync())
            {
              
                Image image = new Image
                {
                      Url = createProductDto.ImageUrl,
                      PublicId = createProductDto.PublicId,
                      ConnectionId = product.Id.ToString(),
                      ConnectionType = StaticKeyValue.Product.Key,
                      Id = Guid.NewGuid().ToString()
                };
                _productService.SaveProductImage(image);
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Failed to add product";
                return BadRequest(response);
            }


            

            response.Result = product;
          
            return await Task.FromResult(response);
        }


        [Authorize]
        [HttpPost("update")]
        public async Task<ActionResult<CommandResponse>> UpdateProduct(CreateProductDto createProductDto)
        {
            CommandResponse response = new CommandResponse();



            Product product = new Product
            {
                ProductName = createProductDto.ProductName,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                TotalQty = createProductDto.TotalQty,
                Category = createProductDto.Category,
                ImageUrl = createProductDto.ImageUrl,
                Id = createProductDto.Id

            };

           _productService.Updateproduct(product);
            if (await _productService.SaveAllAsync())
            {
                Image _image = _productService.GetProductImage(createProductDto.Id);
                if (_image == null)
                {
                    Image image = new Image
                    {
                        Url = createProductDto.ImageUrl,
                        PublicId = createProductDto.PublicId,
                        ConnectionId = product.Id.ToString(),
                        ConnectionType = StaticKeyValue.Product.Key,
                        Id = Guid.NewGuid().ToString()
                    };
                    _productService.SaveProductImage(image);
                }
             
                response.Success = true;
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Failed to update product";
                return BadRequest(response);
            }

            response.Result = product;
            return await Task.FromResult(response);
        }


        [Authorize]
        [HttpPost("upload-image")]
        public async Task<ActionResult<CommandResponse>> UploadProductImage(IFormFile file)
        {
            CommandResponse response = new CommandResponse();
            var result = await _photoService.UploadPhoto(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var imgResponse  = new 
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
             
            };

            response.Result = imgResponse;
            response.Success = true;
            return await Task.FromResult(response);
        }


        [Authorize]
        [HttpPost("delete-image/{id}")]
        public async Task<ActionResult<CommandResponse>> DeleteProductImage(int id)
        {
            CommandResponse response = new CommandResponse();
            Image image  = _productService.GetProductImage(id);
            if (image == null)
            {
                response.Success = false;
                response.ErrorMessage = "Image not found";
                return NotFound(response);
            } else
            {
                _productService.DeleteProductImage(image);
                if (await _productService.SaveAllAsync())
                {
                    await _photoService.DeletePhoto(image.PublicId);
                    response.Success = true;
                }
            }

            return await Task.FromResult(response);
        }

        [Authorize]
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<CommandResponse>> DeleteProduct(string id)
        {
            CommandResponse response = new CommandResponse();
            var product = _productService.GetProduct(int.Parse(id));
            if(product == null)
            {
                response.Success = false;
                response.ErrorMessage = "Product not found";
                return NotFound(response);
            }

            _productService.DeleteProduct(product);

            if (await _productService.SaveAllAsync())
            {
                Image image = _productService.GetProductImage(product.Id);
                if (image == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Image not found";
                    return NotFound(response);
                }

                _productService.DeleteProductImage(image);
                if (await _productService.SaveAllAsync())
                {
                    await _photoService.DeletePhoto(image.PublicId);
                    response.Success = true;
                }

            }
            return await Task.FromResult(response);
        }
    }
}
