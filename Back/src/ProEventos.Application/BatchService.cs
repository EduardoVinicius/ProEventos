using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contracts;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class BatchService : IBatchService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IBatchPersist _batchPersist;
        private readonly IMapper _mapper;

        public BatchService(IGeneralPersist generalPersist, IBatchPersist batchPersist, IMapper mapper)
        {
            _batchPersist = batchPersist;
            _generalPersist = generalPersist;
            _mapper = mapper;
        }

        public async Task AddBatch(int eventId, BatchDto model)
        {
            try
            {
                var batch = _mapper.Map<Batch>(model);
                batch.EventId = eventId;

                _generalPersist.Add<Batch>(batch);

                await _generalPersist.SaveChangesAsync();
            
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BatchDto[]> SaveBatches(int eventId, BatchDto[] models)
        {
            try
            {
                var batches = await _batchPersist.GetBatchesByEventIdAsync(eventId);
                
                if (batches == null)
                    return null;
                
                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddBatch(eventId, model);
                    }
                    else
                    {
                        var batch = batches.FirstOrDefault(batch => batch.Id == model.Id);
                        model.EventId = eventId;

                        _mapper.Map(model, batch);

                        _generalPersist.Update<Batch>(batch);

                        await _generalPersist.SaveChangesAsync();
                    }
                    
                }
                    var returnBatches = await _batchPersist.GetBatchesByEventIdAsync(eventId);
                    return _mapper.Map<BatchDto[]>(returnBatches);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteBatch(int eventId, int batchId)
        {
            try
            {
                var batch = await _batchPersist.GetBatchByIdsAsync(eventId, batchId);
                
                if (batch == null)
                    throw new Exception("Batch not found! Deletion could not be completed.");

                _generalPersist.Delete<Batch>(batch);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BatchDto[]> GetBatchesByEventIdAsync(int eventId)
        {
            try
            {
                var batches = await _batchPersist.GetBatchesByEventIdAsync(eventId);
                if (batches == null)
                    return null;
                
                var result = _mapper.Map<BatchDto[]>(batches);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<BatchDto> GetBatchByIdsAsync(int eventId, int batchId)
        {
            try
            {
                var batch = await _batchPersist.GetBatchByIdsAsync(eventId, batchId);
                if (batch == null)
                    return null;
                
                var result = _mapper.Map<BatchDto>(batch);

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}