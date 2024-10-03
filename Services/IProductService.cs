using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiProject.Models;

namespace MyApiProject.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> AddProductAsync(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
    }
}