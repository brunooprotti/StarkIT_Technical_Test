using StarkIT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StarkIT.Application.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<ICollection<T>> Get(Expression<Func<T,bool>> expression);
    }
}
