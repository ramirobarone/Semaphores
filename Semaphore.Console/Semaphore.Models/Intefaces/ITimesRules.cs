using Semaphore.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semaphore.Models.Intefaces
{
    public interface ITimesRules
    {
        List<TimesOfSemaphore> Get(int idSemaphore);
    }
}
