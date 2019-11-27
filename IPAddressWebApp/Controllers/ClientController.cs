using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPAddressRepository;
using IPAddressRepository.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using IPAddressWebApp.Filters;


namespace IPAddressWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IGeneralRepository _rep = null;
        
        public ClientController(IGeneralRepository rep_)
        {
            _rep = rep_;    
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _rep.ClientRep.GetClients();
        }

        [HttpGet("{id_}", Name = "GetClient")]
        public Client Get(int id_)
        {
            return _rep.ClientRep.Get(id_);
        }

        [HttpPost()]
        [AddClientExcFilter]
        public void Post([FromQuery] Client client_)
        {
            _rep.ClientRep.Create(client_);
        }

        // PUT: api/Client/5
        [HttpPut()]
        [EditClientExcFilter]
        public void Put([FromQuery] Client client_)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("введеные данные на изменение клиента не прошли валидацию");
            }
            _rep.ClientRep.Update(client_);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [DeleteClientExcFilter]
        public void Delete(int id)
        {
            _rep.ClientRep.Delete(id);
        }
    }
}
