using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IPlatformService
    {
        public IEnumerable<Platform> GetAll();
        public Platform GetByID(int id);
        public void Insert(Platform platform);
        public void Update(Platform platform);
        public void Delete(int id);
    }
}
