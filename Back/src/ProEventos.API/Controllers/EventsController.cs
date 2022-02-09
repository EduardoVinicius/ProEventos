﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using ProEventos.Domain;

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
                    $"Error trying to get events. Error: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Event model)
        {
            try
            {
                var events = await _eventService.AddEvents(model);
                
                if (events == null)
                    return BadRequest("Error trying to add event.");
                
                return Ok(events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to add event. Error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Event model)
        {
            try
            {
                var events = await _eventService.UpdateEvent(id, model);
                
                if (events == null)
                    return BadRequest("Error trying to add event.");
                
                return Ok(events);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to update event. Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _eventService.DeleteEvent(id))
                    return Ok("Event deleted");
                else
                    return BadRequest();

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to delete event. Error: {e.Message}");
            }
        }
    }
}
