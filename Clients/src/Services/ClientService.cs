using Clients.Models;
using Clients.Repositories;
using System.Collections.Generic;

namespace Clients.Services
{
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

        public Client? GetClientById(string id)
        {
            return _repository.GetById(id);
        }

        public string AddClient(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
                throw new System.Exception("Nome do cliente é obrigatório");

            if (_repository.GetByEmail(client.Email) != null)
                throw new System.Exception("Email já cadastrado");

            return _repository.Add(client); // agora retorna o id
        }

        public bool UpdateClient(string id, Client client)
        {
            return _repository.Update(id, client);
        }

        public bool InactivateClient(string id)
        {
            return _repository.Inactivate(id);
        }
    }
}
