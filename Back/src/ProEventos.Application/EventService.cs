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
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteEvent(int eventId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Event[]> GetAllEventsBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            throw new System.NotImplementedException();
        }

        
    }
}