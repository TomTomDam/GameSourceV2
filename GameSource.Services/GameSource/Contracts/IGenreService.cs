using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IGenreService
    {
        public IEnumerable<Genre> GetAll();
        public Genre GetByID(int id);
        public void Insert(Genre genre);
        public void Update(Genre genre);
        public void Delete(int id);
    }
}
