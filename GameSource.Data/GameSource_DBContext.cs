using GameSource.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data
{
    public class GameSource_DBContext : DbContext
    {
        public GameSource_DBContext(DbContextOptions<GameSource_DBContext> options) : base(options)
        {
        }

        DbSet<Game> Game { get; set; }
        DbSet<Genre> Genre { get; set; }
        DbSet<Developer> Developer { get; set; }
        DbSet<Publisher> Publisher { get; set; }
        DbSet<Platform> Platform { get; set; }
    }
}