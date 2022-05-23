using System;

namespace Semaphore.Models.Entities
{
    public readonly record struct TimesOfSemaphore(int Id, TimeSpan Begin, TimeSpan End, string Color);
}
