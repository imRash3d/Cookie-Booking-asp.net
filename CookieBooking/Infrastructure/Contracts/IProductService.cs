using CookieBooking.Dtos;
using CookieBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBooking.Infrastructure.Contracts
{
    public interface IProductService
    {
        void  addProduct(Product productDto);
        Task<bool> SaveAllAsync();
        void SaveProductImage(Image image);
        void Updateproduct(Product model);
        Image GetProductImage(int productId);
        void DeleteProduct(Product product);
        Product GetProduct(int productId);
        void DeleteProductImage(Image image);
    }
}
