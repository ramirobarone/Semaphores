using Semaphore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semaphore.Rules.Intefaces
{
    public interface ITimes
    {
        TimeSemaphore Get(int idSemaphore);
        bool Save(TimeSemaphore timesSemaphore);
        TimeSemaphore Update(TimeSemaphore timesSemaphore);
        void Delete(int idSemaphore);
        
    }
}
