using Microsoft.EntityFrameworkCore;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StarkIT.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<ICollection<T>> Get(Expression<Func<T, bool>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"A database error occurred while creating the entity {typeof(T)}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while creating the entity {typeof(T)}.", ex);
            }
        }
    }
}
