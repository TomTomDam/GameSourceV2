using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.Contracts
{
    public interface IPublisherRepository
    {
        public IEnumerable<Publisher> GetAll();
        public Publisher GetByID(int id);
        public void Insert(Publisher publisher);
        public void Update(Publisher publisher);
        public void Delete(int id);
    }
}
