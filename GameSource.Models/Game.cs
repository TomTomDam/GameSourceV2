using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models
{
    public class Game
    {
        [Key]
        public int Game_ID { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
        public Publisher Publisher { get; set; }
        public string Description { get; set; }
        public Platform Platforms { get; set; }
    }
}
