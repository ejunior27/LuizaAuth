using Domain.LuizaAuth.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.LuizaAuth.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        Task<IEnumerable<User>> GetAll();
    }
}
