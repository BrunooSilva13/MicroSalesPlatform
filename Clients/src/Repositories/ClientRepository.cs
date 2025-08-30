using Clients.Models;
using Npgsql;
using System.Collections.Generic;

namespace Clients.Repositories
{
    public class ClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Client> GetAll()
        {
            var clients = new List<Client>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand("SELECT id, name, email, phone FROM clients", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                clients.Add(new Client
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3)
                });
            }

            return clients;
        }

        public Client GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand("SELECT id, name, email, phone FROM clients WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Client
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3)
                };
            }

            return null;
        }

        public void Add(Client client)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(
                "INSERT INTO clients (name, email, phone) VALUES (@name, @email, @phone) RETURNING id",
                connection
            );

            command.Parameters.AddWithValue("name", client.Name);
            command.Parameters.AddWithValue("email", client.Email);
            command.Parameters.AddWithValue("phone", client.Phone);

            client.Id = (int)command.ExecuteScalar(); // Pega o id gerado pelo banco
        }
    }
}
