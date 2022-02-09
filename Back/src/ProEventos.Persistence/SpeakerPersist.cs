using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class SpeakerPersist : ISpeakerPersist
    {
        private readonly ProEventosContext _context;

        public SpeakerPersist(ProEventosContext context)
        {
            _context = context;

        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialNetworks);
            
            if (includeEvents)
            {
                query = query
                    .Include(s => s.SpeakersEvents)
                    .ThenInclude(se => se.Event);
            }
            
            query = query.AsNoTracking().OrderBy(s => s.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialNetworks);
            
            if (includeEvents)
            {
                query = query
                    .Include(s => s.SpeakersEvents)
                    .ThenInclude(se => se.Event);
            }
            
            query = query.AsNoTracking().OrderBy(s => s.Id)
                .Where(s => s.Name.ToLower().Contains(name.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents)
        {
            IQueryable<Speaker> query = _context.Speakers
                .Include(s => s.SocialNetworks);
            
            if (includeEvents)
            {
                query = query
                    .Include(s => s.SpeakersEvents)
                    .ThenInclude(se => se.Event);
            }
            
            query = query.AsNoTracking().OrderBy(s => s.Id)
                .Where(s => s.Id == speakerId);
            return await query.FirstOrDefaultAsync();
        }
    }
}