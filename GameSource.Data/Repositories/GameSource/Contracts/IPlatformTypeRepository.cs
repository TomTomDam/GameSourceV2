using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IPlatformTypeRepository
    {
        public IEnumerable<PlatformType> GetAll();
        public PlatformType GetByID(int id);
        public void Insert(PlatformType platformType);
        public void Update(PlatformType platformType);
        public void Delete(int id);
    }
}
