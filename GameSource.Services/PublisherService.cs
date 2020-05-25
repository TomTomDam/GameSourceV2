using GameSource.Data.Repositories.Contracts;
using GameSource.Models;
using GameSource.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository repo;

        public PublisherService(IPublisherRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return repo.GetAll();
        }

        public Publisher GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(Publisher publisher)
        {
            repo.Insert(publisher);
        }

        public void Update(Publisher publisher)
        {
            repo.Update(publisher);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
