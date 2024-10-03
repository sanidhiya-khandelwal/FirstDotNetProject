using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiProject.Models;
using MyApiProject.Repository;

namespace MyApiProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Product>> GetProductAsync()
        {
            return await _productRepository.GetProductsAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<int> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }
        public async Task<int> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }
        public async Task<int> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
    }
}