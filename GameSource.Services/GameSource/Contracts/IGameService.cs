using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IGameService
    {
        public IEnumerable<Game> GetAll();
        public Game GetByID(int id);
        public void Insert(Game game);
        public void Update(Game game);
        public void Delete(int id);
    }
}
