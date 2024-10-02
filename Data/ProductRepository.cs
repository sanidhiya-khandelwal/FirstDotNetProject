using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyApiProject.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Stock", connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}