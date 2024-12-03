using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Odyseusz.bll.Services.Interfaces;
using Odyseusz.dal.Repositories.Interfaces;

namespace Odyseusz.bll.Services.Implementations
{
    public class GenericService<T, TKey> : IGenericService<T, TKey> where T : class
    {
        private readonly IGenericRepository<T, TKey> _repository;

        public GenericService(IGenericRepository<T, TKey> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync() => _repository.GetAllAsync();

        public Task<T?> GetByIdAsync(TKey id) => _repository.GetByIdAsync(id);

        public Task AddAsync(T entity) => _repository.AddAsync(entity);

        public Task UpdateAsync(T entity) => _repository.UpdateAsync(entity);

        public Task DeleteAsync(TKey id) => _repository.DeleteAsync(id);
    }
}
