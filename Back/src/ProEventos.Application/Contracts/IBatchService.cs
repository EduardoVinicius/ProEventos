using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contracts
{
    public interface IBatchService
    {
        Task<BatchDto[]> SaveBatches(int eventId, BatchDto[] models);
        Task<bool> DeleteBatch(int eventId, int batchId);

        Task<BatchDto[]> GetBatchesByEventIdAsync(int eventId);
        Task<BatchDto> GetBatchByIdsAsync(int eventId, int batchId);
    }
}