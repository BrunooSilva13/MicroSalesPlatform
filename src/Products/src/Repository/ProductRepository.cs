using Products.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Products.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Inserir produto
        public void Insert(Product product)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@"
                INSERT INTO products 
                (id, name, desc, price, quantity, created_at, updated_at, is_active) 
                VALUES (@id, @name, @desc, @price, @quantity, @created_at, @updated_at, @is_active)
            ", conn);

            cmd.Parameters.AddWithValue("id", product.Id);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("desc", product.Desc);
            cmd.Parameters.AddWithValue("price", product.Price);
            cmd.Parameters.AddWithValue("quantity", product.Quantity);
            cmd.Parameters.AddWithValue("created_at", product.CreatedAt);
            cmd.Parameters.AddWithValue("updated_at", product.UpdatedAt);
            cmd.Parameters.AddWithValue("is_active", product.IsActive);

            cmd.ExecuteNonQuery();
        }

        // Atualizar produto
        public void Update(Product product)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand(@"
                UPDATE products
                SET name = @name,
                    desc = @desc,
                    price = @price,
                    quantity = @quantity,
                    updated_at = @updated_at,
                    is_active = @is_active
                WHERE id = @id
            ", conn);

            cmd.Parameters.AddWithValue("id", product.Id);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("desc", product.Desc);
            cmd.Parameters.AddWithValue("price", product.Price);
            cmd.Parameters.AddWithValue("quantity", product.Quantity);
            cmd.Parameters.AddWithValue("updated_at", product.UpdatedAt);
            cmd.Parameters.AddWithValue("is_active", product.IsActive);

            cmd.ExecuteNonQuery();
        }

        // Buscar produto por Id
        public Product GetById(string id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM products WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    Id = reader.GetString(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Desc = reader.GetString(reader.GetOrdinal("desc")),
                    Price = reader.GetFloat(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"))
                };
            }

            return null;
        }

        // Listar todos os produtos ativos
        public IEnumerable<Product> GetAllActive()
        {
            var list = new List<Product>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * FROM products WHERE is_active = true", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Product
                {
                    Id = reader.GetString(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Desc = reader.GetString(reader.GetOrdinal("desc")),
                    Price = reader.GetFloat(reader.GetOrdinal("price")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                    UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"))
                });
            }

            return list;
        }
    }
}    