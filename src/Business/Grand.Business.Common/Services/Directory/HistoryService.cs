using Grand.Business.Common.Interfaces.Directory;
using Grand.Domain;
using Grand.Domain.Data;
using Grand.Domain.History;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Business.Common.Services.Directory
{
    /// <summary>
    /// History service interface
    /// </summary>
    public partial class HistoryService : IHistoryService
    {
        private readonly IRepository<HistoryObject> _historyRepository;

        public HistoryService(IRepository<HistoryObject> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public virtual async Task SaveObject<T>(T entity) where T : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var history = new HistoryObject()
            {
                CreatedOnUtc = DateTime.UtcNow,
                Object = entity
            };
            await _historyRepository.InsertAsync(history);
        }

        public virtual async Task<IList<T>> GetHistoryForEntity<T>(BaseEntity entity) where T : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var history = await _historyRepository.Table.Where(x => x.Object.Id == entity.Id).Select(x => (T)x.Object).ToListAsync();
            return history;
        }

        public virtual async Task<IList<HistoryObject>> GetHistoryObjectForEntity(BaseEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var history = await _historyRepository.Table.Where(x => x.Object.Id == entity.Id).ToListAsync();
            return history;
        }
    }
}