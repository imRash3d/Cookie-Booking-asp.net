using AutoMapper;
using CookieBooking.Constraint;
using CookieBooking.Dtos;
using CookieBooking.Entities;
using CookieBooking.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextService _context;
        private readonly IMapper _mapper;
      

        public ProductService(DbContextService context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
         
        }
        public void addProduct(Product productDto)
        {
             _context.Products.Add(productDto);
     


        }

        public async Task<bool> SaveAllAsync()
        {
          return await _context.SaveChangesAsync() > 0;
        }

        public void SaveProductImage(Image image)
        {
            _context.Add(image);
            _context.SaveChanges();
        }

        public void Updateproduct(Product model)
        {
           
            _context.Products.Update(model);
            
        }

        public Image GetProductImage(int productId)
        {
           return _context.Images.SingleOrDefault(x => x.ConnectionId == productId.ToString() && x.ConnectionType == StaticKeyValue.Product.Key);
        }

        public void DeleteProductImage(Image image)
        {
                _context.Remove(image); 
        }
        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.SingleOrDefault(x => x.Id == productId);
        }
    }
}
