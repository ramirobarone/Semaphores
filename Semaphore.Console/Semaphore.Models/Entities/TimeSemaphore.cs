using System;
using System.Collections.Generic;
using System.Text;

namespace Semaphore.Models.Entities
{
    public class TimeSemaphore : Semaphore
    {
        public DateTime HourInit { get; set; }
        public DateTime HourEnd { get; set; }
        public TimeSpan GreenTime { get; set; }
        public TimeSpan YellowTime { get; set; }
        public TimeSpan RedTime { get; set; }
    }
}
