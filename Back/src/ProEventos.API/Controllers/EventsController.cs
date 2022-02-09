using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync(true);
                
                if (events == null)
                    return NotFound("No event found!");
                
                return Ok(events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to get events. Error: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var e = await _eventService.GetEventByIdAsync(id, true);
                
                if (e == null)
                    return NotFound("Event not found!");
                
                return Ok(e);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to get event. Error: {e.Message}");
            }
        }

        [HttpGet("{subject}/subject")]
        public async Task<IActionResult> GetBySubject(string subject)
        {
            try
            {
                var events = await _eventService.GetAllEventsBySubjectAsync(subject, true);
                
                if (events == null)
                    return NotFound("No events found!");
                
                return Ok(events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to get event. Error: {e.Message}");
            }
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
