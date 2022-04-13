using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
    public interface IBatchPersist
    {
        Task<Batch[]> GetBatchesByEventIdAsync(int eventId);
        Task<Batch> GetBatchByIdsAsync(int eventId, int batchId);
    }
}