namespace Clients.Models
{
    // Model que representa um cliente
    public class Client
    {
        public string Id { get; set; } = default!;    // Identificador único (UUID)
        public string Name { get; set; } = default!;  // Nome do cliente
        public string Surname { get; set; } = default!; // Sobrenome do cliente
        public string Email { get; set; } = default!; // Email do cliente
        public DateOnly Birthdate { get; set; }       // Data de nascimento
        public bool IsActive { get; set; } = true;    // Para delete lógico
        public DateTime CreatedAt { get; set; }       // Data de criação
        public DateTime UpdatedAt { get; set; }       // Data de atualização
    }
}
