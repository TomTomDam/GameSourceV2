using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using System.Threading.Tasks;
using System;

namespace GameSource.Infrastructure.Repositories.GameSourceUser.Contracts
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Task<Role> GetByIDAsync(Guid guid);
    }
}
