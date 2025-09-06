using System;

namespace Products.Models
{
    public class Product
    {
        public string Id { get; set; }           // Identificador único
        public string Name { get; set; }         // Nome do produto
        public string Desc { get; set; }         // Descrição
        public float Price { get; set; }         // Preço em BRL
        public int Quantity { get; set; }        // Quantidade em estoque
        public DateTime CreatedAt { get; set; }  // Data de criação
        public DateTime UpdatedAt { get; set; }  // Data de atualização
        public bool IsActive { get; set; }       // Para inativar produto
    }
}
