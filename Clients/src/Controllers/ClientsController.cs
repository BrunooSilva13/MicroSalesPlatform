using Clients.Models;
using Clients.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Clients.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientsController(ClientService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            return _service.GetAllClients();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetById(int id)
        {
            var client = _service.GetClientById(id);
            if (client == null)
                return NotFound();
            return client;
        }

        [HttpPost]
        public ActionResult Add(Client client)
        {
            _service.AddClient(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }
    }
}
