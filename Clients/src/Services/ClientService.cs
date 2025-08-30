using Clients.Models;
using Clients.Repositories;
using System.Collections.Generic;

namespace Clients.Services
{
    // Camada de lógica de negócio
    public class ClientService
    {
        private readonly ClientRepository _repository;

        public ClientService(ClientRepository repository)
        {
            _repository = repository;
        }

        public List<Client> GetAllClients()
        {
            return _repository.GetAll();
        }

        public Client GetClientById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddClient(Client client)
        {
            // Aqui você pode colocar validações
            if (string.IsNullOrEmpty(client.Name))
                throw new System.Exception("Nome do cliente é obrigatório");

            _repository.Add(client);
        }
    }
}
