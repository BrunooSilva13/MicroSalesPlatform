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

        // GET api/clients
        [HttpGet]
        public ActionResult<List<Client>> GetAll()
        {
            return _service.GetAllClients();
        }

        // GET api/clients/{id}
        [HttpGet("{id}")]
        public ActionResult<Client> GetById(string id)
        {
            var client = _service.GetClientById(id);
            if (client == null)
                return NotFound();
            return client;
        }

        // POST api/clients
        [HttpPost]
        public ActionResult Add(Client client)
        {
            var newId = _service.AddClient(client);
            return CreatedAtAction(nameof(GetById), new { id = newId }, client);
        }

        // PUT api/clients/{id}
        [HttpPut("{id}")]
        public ActionResult Update(string id, Client client)
        {
            var updated = _service.UpdateClient(id, client);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // DELETE api/clients/{id} (delete lógico → inativar)
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var inactivated = _service.InactivateClient(id);
            if (!inactivated)
                return NotFound();
            return NoContent();
        }
    }
}
