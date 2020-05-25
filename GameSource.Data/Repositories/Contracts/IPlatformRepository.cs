using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.Contracts
{
    public interface IPlatformRepository
    {
        public IEnumerable<Platform> GetAll();
        public Platform GetByID(int id);
        public void Insert(Platform platform);
        public void Update(Platform platform);
        public void Delete(int id);
    }
}
