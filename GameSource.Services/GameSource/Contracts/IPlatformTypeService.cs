using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IPlatformTypeService
    {
        public IEnumerable<PlatformType> GetAll();
        public PlatformType GetByID(int id);
        public void Insert(PlatformType platformType);
        public void Update(PlatformType platformType);
        public void Delete(int id);
        public Task<IEnumerable<PlatformType>> GetAllAsync();
        public Task<PlatformType> GetByIDAsync(int id);
        public Task InsertAsync(PlatformType platformType);
        public Task UpdateAsync(PlatformType platformType);
        public Task DeleteAsync(int id);
    }
}
