using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models
{
    public class Platform
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PlatformTypeID { get; set; }

        [DisplayName("Platform Type")]
        public PlatformType PlatformType { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
