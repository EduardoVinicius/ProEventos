using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Persistence
{
    public class BatchPersist : IBatchPersist
    {
        private readonly ProEventosContext _context;

        public BatchPersist(ProEventosContext context)
        {
            _context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Batch> GetBatchByIdsAsync(int eventId, int id)
        {
            IQueryable<Batch> query = _context.Batches;

            query = query.AsNoTracking()
                         .Where(batch => batch.EventId == eventId && batch.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Batch[]> GetBatchesByEventIdAsync(int eventId)
        {
            IQueryable<Batch> query = _context.Batches;

            query = query.AsNoTracking()
                         .Where(batch => batch.EventId == eventId);
                         
            return await query.ToArrayAsync();
        }
    }
}