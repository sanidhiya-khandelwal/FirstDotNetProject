using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using MyApiProject.Models;
using System.Runtime.CompilerServices;

namespace MyApiProject.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetProductDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            SupplierID = Convert.ToInt32(reader["SupplierID"])
                        });
                    }
                }
            }
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetProductById", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", id);

                connection.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            SupplierID = Convert.ToInt32(reader["SupplierID"])
                        };
                    }
                }
            }
            return product;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                connection.Open();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                connection.Open();
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", id);

                connection.Open();
                return await cmd.ExecuteNonQueryAsync();
            }
        }






        // private readonly string _connectionString;

        // public ProductRepository(string connectionString)
        // {
        //     _connectionString = connectionString;
        // }

        // public List<Product> GetProducts()
        // {
        //     List<Product> products = new List<Product>();

        //     using (SqlConnection connection = new SqlConnection(_connectionString))
        //     {
        //         SqlCommand command = new SqlCommand("SELECT * FROM dbo.Stock", connection);
        //         connection.Open();

        //         using (SqlDataReader reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 Product product = new Product
        //                 {
        //                     Id = reader.GetInt32(0),
        //                     Title = reader.GetString(1),
        //                 };
        //                 products.Add(product);
        //             }
        //         }
        //     }

        //     return products;
        // }
    }

    // public class Product
    // {
    //     public int Id { get; set; }
    //     public string Title { get; set; }
    // }
}