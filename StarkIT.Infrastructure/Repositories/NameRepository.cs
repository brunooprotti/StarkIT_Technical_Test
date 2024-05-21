using StarkIT.Application.Contracts.Persistence;
using StarkIT.Domain.Models;
using StarkIT.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarkIT.Infrastructure.Repositories
{
    public class NameRepository : BaseRepository<Names>, INameRepository
    {
        public NameRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
