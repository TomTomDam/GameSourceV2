using GameSource.API.Providers;
using System;

namespace GameSource.API
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
