using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {

        public IEnumerable<Event> _event = new Event[]
        {
            new Event()
            {
                EventId = 1,
                Subject = "Angular 12 & .NET 5",
                Location = "Recife",
                Batch = "First batch",
                PeopleQuantity = 250,
                EventDate = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImageURL = "image.png"
            },
            new Event()
            {
                EventId = 2,
                Subject = "Angular 12 e suas novidades",
                Location = "Bahia",
                Batch = "Second batch",
                PeopleQuantity = 200,
                EventDate = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"),
                ImageURL = "image.png"
            }
        };

        public EventController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return _event;
        }

        [HttpGet("{id}")]
        public IEnumerable<Event> GetById(int id)
        {
            return _event.Where(e => e.EventId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "Post example";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Put example with id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Delete example with id = {id}";
        }
    }
}
