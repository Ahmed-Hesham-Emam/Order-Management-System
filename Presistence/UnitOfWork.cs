using Domain.Contracts;
using Domain.Models;
using Presistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
    {
    public class UnitOfWork : IUnitOfWork
        {
        private readonly OrderManagementSystemDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(OrderManagementSystemDbContext context)
            {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
            }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            {
            var typeName = typeof(TEntity).Name;

            if ( !_repositories.ContainsKey(typeName) )
                {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.TryAdd(typeName, repository);
                }

            return (IGenericRepository<TEntity, TKey>) _repositories[typeName];
            }

        public async Task<int> SaveChangesAsync()
            {
            return await _context.SaveChangesAsync();
            }
        }
    }
