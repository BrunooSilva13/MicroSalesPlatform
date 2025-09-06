using Products.Models;
using Products.Repositories;
using System;
using System.Collections.Generic;

namespace Products.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public void AddProduct(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            product.IsActive = true;
            _repository.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _repository.Update(product);
        }

        public void InactivateProduct(string productId)
        {
            var product = _repository.GetById(productId);
            if (product != null)
            {
                product.IsActive = false;
                product.UpdatedAt = DateTime.UtcNow;
                _repository.Update(product);
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAllActive();
        }

        // Métodos adicionais de filtro podem ser implementados aqui
    }
}
