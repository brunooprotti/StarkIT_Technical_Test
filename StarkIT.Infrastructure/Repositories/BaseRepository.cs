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
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

    }
}
