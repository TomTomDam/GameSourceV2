using GameSource.Models.GameSourceUser;
using System;

namespace GameSource.Models
{
    public class EventsLog
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public User User { get; set; }
    }
}
