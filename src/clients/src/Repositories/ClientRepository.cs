using Clients.Models;
using Npgsql;
using System;
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

        // Recupera todos os clientes ativos
        public List<Client> GetAll()
        {
            var clients = new List<Client>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT id, name, surname, email, birthdate, is_active, created_at, updated_at
                FROM clients
                WHERE is_active = TRUE
                ORDER BY created_at DESC", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client
                {
                    Id = reader.GetGuid(0).ToString(),  // UUID tratado corretamente
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Email = reader.GetString(3),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                    IsActive = reader.GetBoolean(5),
                    CreatedAt = reader.GetDateTime(6),
                    UpdatedAt = reader.GetDateTime(7)
                });
            }

            return clients;
        }

        // Recupera um cliente pelo ID
        public Client? GetById(string id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT id, name, surname, email, birthdate, is_active, created_at, updated_at
                FROM clients
                WHERE id = @id", connection);

            command.Parameters.AddWithValue("id", Guid.Parse(id)); // Converte string para UUID

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                {
                    Id = reader.GetGuid(0).ToString(),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Email = reader.GetString(3),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                    IsActive = reader.GetBoolean(5),
                    CreatedAt = reader.GetDateTime(6),
                    UpdatedAt = reader.GetDateTime(7)
                };
            }

            return null;
        }

        // Recupera um cliente pelo email
        public Client? GetByEmail(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(@"
                SELECT id, name, surname, email, birthdate, is_active, created_at, updated_at
                FROM clients
                WHERE email = @email", connection);

            command.Parameters.AddWithValue("email", email);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                {
                    Id = reader.GetGuid(0).ToString(),
                    Name = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Email = reader.GetString(3),
                    Birthdate = DateOnly.FromDateTime(reader.GetDateTime(4)),
                    IsActive = reader.GetBoolean(5),
                    CreatedAt = reader.GetDateTime(6),
                    UpdatedAt = reader.GetDateTime(7)
                };
            }

            return null;
        }

        // Adiciona um cliente
        public string Add(Client client)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var id = Guid.NewGuid(); // UUID como Guid

            using var command = new NpgsqlCommand(@"
                INSERT INTO clients (id, name, surname, email, birthdate, is_active, created_at, updated_at)
                VALUES (@id, @name, @surname, @email, @birthdate, TRUE, NOW(), NOW())", connection);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", client.Name);
            command.Parameters.AddWithValue("surname", client.Surname);
            command.Parameters.AddWithValue("email", client.Email);
            command.Parameters.AddWithValue("birthdate", client.Birthdate.ToDateTime(TimeOnly.MinValue));

            command.ExecuteNonQuery();

            client.Id = id.ToString();
            return client.Id;
        }

        // Atualiza um cliente existente
        public bool Update(string id, Client client)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(@"
                UPDATE clients
                SET name = @name,
                    surname = @surname,
                    email = @email,
                    birthdate = @birthdate,
                    updated_at = NOW()
                WHERE id = @id", connection);

            command.Parameters.AddWithValue("id", Guid.Parse(id)); // Converte string para UUID
            command.Parameters.AddWithValue("name", client.Name);
            command.Parameters.AddWithValue("surname", client.Surname);
            command.Parameters.AddWithValue("email", client.Email);
            command.Parameters.AddWithValue("birthdate", client.Birthdate.ToDateTime(TimeOnly.MinValue));

            return command.ExecuteNonQuery() > 0;
        }

        // Inativa um cliente (delete lógico)
        public bool Inactivate(string id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(@"
                UPDATE clients
                SET is_active = FALSE,
                    updated_at = NOW()
                WHERE id = @id", connection);

            command.Parameters.AddWithValue("id", Guid.Parse(id)); // Converte string para UUID
            return command.ExecuteNonQuery() > 0;
        }
    }
}
