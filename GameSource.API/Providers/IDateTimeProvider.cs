using System;

namespace GameSource.API.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
