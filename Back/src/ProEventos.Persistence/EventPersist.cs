using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class EventPersist : IEventPersist
    {
        private readonly ProEventosContext _context;

        public EventPersist(ProEventosContext context)
        {
            _context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Event[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Batch)
                .Include(e => e.SocialNetworks);
            
            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.SpeakersEvents)
                    .ThenInclude(se => se.Speaker);
            }
            
            query = query.AsNoTracking().OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetAllEventsBySubjectAsync(string subject, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Batch)
                .Include(e => e.SocialNetworks);
            
            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.SpeakersEvents)
                    .ThenInclude(se => se.Speaker);
            }
            
            query = query.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Subject.ToLower().Contains(subject.ToLower()));
            
            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEventByIdAsync(int eventId, bool includeSpeakers = false)
        {
            IQueryable<Event> query = _context.Events
                .Include(e => e.Batch)
                .Include(e => e.SocialNetworks);
            
            if (includeSpeakers)
            {
                query = query
                    .Include(e => e.SpeakersEvents)
                    .ThenInclude(se => se.Speaker);
            }
            
            query = query.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Id == eventId);
            
            return await query.FirstOrDefaultAsync();
        }
    }
}