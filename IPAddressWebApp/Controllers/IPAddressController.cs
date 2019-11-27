using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class IPAddressController : ControllerBase
    {
        private readonly IGeneralRepository _rep = null;
        public IPAddressController(IGeneralRepository rep_)
        {
            _rep = rep_;
        }


        [HttpGet("{clientId_}", Name = "GetIpNetsByClientId")]
        public IEnumerable<ClientIPNetFront> Get(int clientId_)
        {

            var ipNets = _rep.IPNetRep.GetIPNetsByClientId(clientId_);
            var res = ipNets.Select(s =>
            {
                var oktets = s.Address.GetAddressBytes();
                return new ClientIPNetFront()
                {
                    Id = s.Id,
                    ClientId = s.ClientId,
                    IpNetId = s.IpNetId,
                    Oktet1 = oktets[0],
                    Oktet2 = oktets[1],
                    Oktet3 = oktets[2],
                    Oktet4 = oktets[3],
                    Mask = s.Mask
                };
                ;
            }).ToList();
            return res;

        }

        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="ipNet_"></param>
        [HttpPost]
        [AddIpNetExcFilter]
        public void Post([FromQuery]ClientIPNetFront ipNet_)
        {
          
            var ipNet = new ClientIPNetExt()
            {
                Id = ipNet_.Id,
                IpNetId = ipNet_.IpNetId,
                ClientId = ipNet_.ClientId,
                Address = new System.Net.IPAddress(
                    new byte[] { (byte)ipNet_.Oktet1, (byte)ipNet_.Oktet2, (byte)ipNet_.Oktet3, (byte)ipNet_.Oktet4 }
                    ),
                Mask = ipNet_.Mask
            };
            _rep.IPClientIPNetRep.Create(ipNet);
           
        }
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="ipNet_"></param>
        [HttpPut]
        [EditIpNetExcFilter]
        public void Put([FromQuery]ClientIPNetFront ipNet_)
        {
            var ipNet = new IPNet()
            {
                Id = ipNet_.IpNetId,
                Address = new System.Net.IPAddress(
                   new byte[] { (byte)ipNet_.Oktet1, (byte)ipNet_.Oktet2, (byte)ipNet_.Oktet3, (byte)ipNet_.Oktet4 }
                   ),
                Mask = ipNet_.Mask
            };
            _rep.IPNetRep.Update(ipNet);

        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [DeleteIpNetExcFilter]
        public void Delete(int id)
        {
            _rep.IPClientIPNetRep.Delete(id);
        }
    }
}
