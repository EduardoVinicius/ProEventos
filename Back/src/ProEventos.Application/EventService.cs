using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contracts;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;
        private readonly IMapper _mapper;

        public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist, IMapper mapper)
        {
            _eventPersist = eventPersist;
            _generalPersist = generalPersist;
            _mapper = mapper;
        }

        public async Task<EventDto> AddEvents(EventDto model)
        {
            try
            {
                var e = _mapper.Map<Event>(model);

                _generalPersist.Add<Event>(e);
                if (await _generalPersist.SaveChangesAsync())
                {
                    var returnEvent = await _eventPersist.GetEventByIdAsync(e.Id);

                    return _mapper.Map<EventDto>(returnEvent);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> UpdateEvent(int eventId, EventDto model)
        {
            try
            {
                var e = await _eventPersist.GetEventByIdAsync(eventId);
                
                if (e == null)
                    return null;
                
                model.Id = eventId;

                _mapper.Map(model, e);

                _generalPersist.Update<Event>(e);

                if (await _generalPersist.SaveChangesAsync())
                {
                    var returnEvent = await _eventPersist.GetEventByIdAsync(e.Id);

                    return _mapper.Map<EventDto>(returnEvent);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                var e = await _eventPersist.GetEventByIdAsync(eventId);
                
                if (e == null)
                    throw new Exception("Event not found! Deletion could not be completed.");

                _generalPersist.Update(e);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsAsync();
                if (events == null)
                    return null;

                var result = _mapper.Map<EventDto[]>(events);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto[]> GetAllEventsBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsBySubjectAsync(subject);
                if (events == null)
                    return null;
                
                var result = _mapper.Map<EventDto[]>(events);

                return result;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            try
            {
                var e = await _eventPersist.GetEventByIdAsync(eventId);
                if (e == null)
                    return null;
                
                var result = _mapper.Map<EventDto>(e);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
    }
}