using Dapper;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Data.Settings;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class GenreService : IGenreService
    {
        private IGenreRepository repo;
        private readonly DatabaseSettings dbSettings;

        public GenreService(IGenreRepository repo, IOptions<DatabaseSettings> dbSettings)
        {
            this.repo = repo;
            this.dbSettings = dbSettings.Value;
        }

        public async Task<List<Genre>> FindByName(string filter)
        {
            using (IDbConnection connection = new SqlConnection(dbSettings.DefaultConnection))
            {
                var args = new { Search = $"%{filter}%" };
                const string sql = "WHERE [Name] like @Search";
                List<Genre> output = (await connection.GetListAsync<Genre>(sql, args)).ToList();

                return output;
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            return repo.GetAll();
        }

        public Genre GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Genre genre)
        {
            repo.Insert(genre);
        }

        public void Update(Genre genre)
        {
            repo.Update(genre);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
