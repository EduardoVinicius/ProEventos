using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contracts;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchesController : ControllerBase
    {
        
        private readonly IBatchService _batchService;

        public BatchesController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            try
            {
                var batches = await _batchService.GetBatchesByEventIdAsync(eventId);
                
                if (batches == null)
                    return NoContent();
                
                return Ok(batches);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to get batches. Error: {e.Message}");
            }
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> SaveBatches(int eventId, BatchDto[] models)
        {
            try
            {
                var batches = await _batchService.SaveBatches(eventId, models);
                
                if (batches == null)
                    return NoContent();
                
                return Ok(batches);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to save batches. Error: {e.Message}");
            }
        }

        [HttpDelete("{eventId}/{batchId}")]
        public async Task<IActionResult> Delete(int eventId, int batchId)
        {
            try
            {
                var batch = await _batchService.GetBatchByIdsAsync(eventId, batchId);
                if (batch == null)
                    return NoContent();

                if (await _batchService.DeleteBatch(batch.EventId, batch.Id))
                    return Ok(new { message = "Batch Deleted" });
                else
                    throw new Exception("A non specific error occurred during batch deletion!");

            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error trying to delete batch. Error: {e.Message}");
            }
        }
    }
}
