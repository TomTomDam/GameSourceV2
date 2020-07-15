using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models.GameSource
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GenreID { get; set; }
        public int DeveloperID { get; set; }
        public int PublisherID { get; set; }
        public int PlatformID { get; set; }

        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
        public Publisher Publisher { get; set; }
        public Platform Platform { get; set; }
    }
}
