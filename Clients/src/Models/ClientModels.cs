namespace Clients.Models
{
    // Model que representa um cliente
    public class Client
    {
        public int Id { get; set; }         // Identificador único do cliente
        public string Name { get; set; }    // Nome do cliente
        public string Email { get; set; }   // Email do cliente
        public string Phone { get; set; }   // Telefone do cliente
    }
}
