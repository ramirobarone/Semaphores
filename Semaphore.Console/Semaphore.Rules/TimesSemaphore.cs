using Semaphore.Models.Entities;
using Semaphore.Models.Intefaces;
using Semaphore.Rules.Intefaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Semaphore.Rules
{
    public class TimesSemaphore : ITimes, ITimesRules
    {
        readonly ITimes _times;
        public TimesSemaphore(ITimes times)
        {
            _times = times;
        }
        public void Delete(int idSemaphore)
        {
            throw new NotImplementedException();
        }

        public List<TimesOfSemaphore > Get(int idSemaphore)
        {
            //calcular tiempos de relleno
            var times = _times.Get(idSemaphore);

            List<TimesOfSemaphore> timesCreated = new List<TimesOfSemaphore>();
            TimeSpan _endDayInSeconds = new TimeSpan(23, 59, 59);
            TimeSpan _currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            double _totalSeconds = _endDayInSeconds.TotalSeconds - _currentTime.TotalSeconds;

            if (times != null)
            {
                for (double i = 0; i < _totalSeconds;)
                {
                    timesCreated.Add(new TimesOfSemaphore(idSemaphore,
                                                          _currentTime,
                                                          _currentTime + times.GreenTime,
                                                          "Green"));
                    _currentTime = _currentTime + times.GreenTime;
                    timesCreated.Add(new TimesOfSemaphore(idSemaphore,
                                                          _currentTime,
                                                          _currentTime + times.YellowTime,
                                                          "Yellow"));

                    _currentTime = _currentTime + times.YellowTime;

                    timesCreated.Add(new TimesOfSemaphore(idSemaphore,
                                                          _currentTime,
                                                          _currentTime + times.GreenTime,
                                                          "Red"));
                    _currentTime = _currentTime + times.RedTime;

                    i = i 
                        + times.GreenTime.TotalSeconds
                        + times.YellowTime.TotalSeconds
                        + times.RedTime.TotalSeconds;

                }

            }
            return timesCreated ;
        }

        public bool Save(TimeSemaphore timesSemaphore)
        {
            return _times.Save(timesSemaphore);
        }

        public TimeSemaphore Update(TimeSemaphore timesSemaphore)
        {
            throw new NotImplementedException();
        }

        TimeSemaphore ITimes.Get(int idSemaphore)
        {
            throw new NotImplementedException();
        }
    }
}
