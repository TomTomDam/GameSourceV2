﻿using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSource
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        private GameSource_DBContext context;
        private DbSet<Publisher> entity;

        public PublisherRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<Publisher>();
        }

        public IEnumerable<Publisher> GetAll()
        {
            return entity.ToList();
        }

        public Publisher GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(Publisher publisher)
        {
            entity.Add(publisher);
            context.SaveChanges();
        }

        public void Update(Publisher publisher)
        {
            entity.Update(publisher);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var publisher = GetByID(id);
            entity.Remove(publisher);
            context.SaveChanges();
        }
    }
}