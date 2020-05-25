using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.Contracts
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
