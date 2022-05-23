using Semaphore.Models.Entities;
using Semaphore.Rules.Intefaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Semaphore.Data
{
    public class DataSemaphore : ITimes
    {
        private readonly Semaphores _sempahores;

        public DataSemaphore()
        {
            _sempahores = new Semaphores();
        }
        public void Delete(int idSemaphore)
        {
            throw new NotImplementedException();
        }

        public TimeSemaphore Get(int idSemaphore)
        {
            return _sempahores.ListSempahores.Where(x => x.Id == idSemaphore).FirstOrDefault();
        }

        public bool Save(TimeSemaphore timesSemaphore)
        {
            if (timesSemaphore == null)
                return false;

            _sempahores.ListSempahores.Add(timesSemaphore);
            return true;
        }

        public TimeSemaphore Update(TimeSemaphore timesSemaphore)
        {
            throw new NotImplementedException();
        }
    }
}
