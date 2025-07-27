using Domain.Contracts;
using Domain.Models;
using Domain.Models.CustomerModel;
using Domain.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
    {
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
        {
        private readonly OrderManagementSystemDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(OrderManagementSystemDbContext context)
            {
            _context = context;
            _dbSet = context.Set<TEntity>();
            }





        public async Task<IEnumerable<TEntity>> GetAllAsync()
            {

            return await _dbSet.ToListAsync(); ;
            }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
            {
            IQueryable<TEntity> query = _dbSet;

            if ( include is not null )
                query = include(query);

            return await query.ToListAsync();
            }

        #region  Get by ID
        public async Task<TEntity?> GetByIdAsync(TKey id)
            {
            return await _dbSet.FindAsync(id);
            }
        #endregion


        #region Add
        public async Task AddAsync(TEntity entity)
            {
            await _dbSet.AddAsync(entity);
            }
        #endregion

        #region Update
        public void Update(TEntity entity)
            {
            _dbSet.Update(entity);
            }
        #endregion

        #region Delete
        public void Delete(TEntity entity)
            {
            _dbSet.Remove(entity);
            }

        #endregion


        }
    }

