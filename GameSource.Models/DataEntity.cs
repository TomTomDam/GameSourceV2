using System.ComponentModel.DataAnnotations;

namespace GameSource.Models
{
    public abstract class DataEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
