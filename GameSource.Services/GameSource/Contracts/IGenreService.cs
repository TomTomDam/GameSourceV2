using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IGenreService
    {
        public Task<List<Genre>> FindByName(string filter);
        public IEnumerable<Genre> GetAll();
        public Genre GetByID(int id);
        public void Insert(Genre genre);
        public void Update(Genre genre);
        public void Delete(int id);
    }
}
