using System;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventPersist _eventPersist;

        public EventService(IGeneralPersist generalPersist, IEventPersist eventPersist)
        {
            _eventPersist = eventPersist;
            _generalPersist = generalPersist;
        }

        public async Task<Event> AddEvents(Event model)
        {
            try
            {
                _generalPersist.Add<Event>(model);
                if (await _generalPersist.SaveChangesAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> UpdateEvent(int eventId, Event model)
        {
            try
            {
                var e = await _eventPersist.GetEventByIdAsync(eventId);
                
                if (e == null)
                    return null;
                
                model.Id = eventId;

                _generalPersist.Update(model);

                if (await _generalPersist.SaveChangesAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id);
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

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsAsync();
                if (events == null)
                    return null;
                
                return events;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event[]> GetAllEventsBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsBySubjectAsync(subject);
                if (events == null)
                    return null;
                
                return events;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            try
            {
                var e = await _eventPersist.GetEventByIdAsync(eventId);
                if (e == null)
                    return null;
                
                return e;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
    }
}