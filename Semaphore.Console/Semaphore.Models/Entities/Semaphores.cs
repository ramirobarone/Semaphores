using System.Collections.Generic;

namespace Semaphore.Models.Entities
{
    public class Semaphores
    {
        public Semaphores()
        {
            ListSempahores = new List<TimeSemaphore>();
        }
        public IList<TimeSemaphore> ListSempahores { get; set; }
    }
}
