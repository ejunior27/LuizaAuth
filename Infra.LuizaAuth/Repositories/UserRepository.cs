using Domain.LuizaAuth.Entities;
using Domain.LuizaAuth.Interfaces;
using Infra.LuizaAuth.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.LuizaAuth.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {

        public UserRepository(LuizaAuthContext context)
            : base(context) { }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

    }
}
