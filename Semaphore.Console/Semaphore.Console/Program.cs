using Semaphore.Data;
using Semaphore.Models.Entities;
using Semaphore.Rules;
using Semaphore.Rules.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Semaphore.Console
{
    internal class Program
    {
        static TimesSemaphore timesSemaphore;
        static ITimes _times;
        static List<TimesOfSemaphore> arraySemaphore;
        static int _currentPosition;
        static string _currentColor;
        public Program()
        {
        }
        static void Main(string[] args)
        {

            _times = _times = new DataSemaphore();
            timesSemaphore = new TimesSemaphore(_times);

            var saved = timesSemaphore.Save(new Models.Entities.TimeSemaphore()
            {
                Id = 1,
                Address = "La Pampa",
                Name = "Ruta e53 el pueblito",
                NumberAddres = "Km 45",
                GreenTime = new System.TimeSpan(0, 0, 40),
                RedTime = new System.TimeSpan(0, 0, 15),
                YellowTime = new System.TimeSpan(0, 0, 4),
                HourInit = new System.DateTime(2022, 5, 16, 00, 00, 00),
                HourEnd = new System.DateTime(2022, 5, 16, 01, 59, 59)
            });

            arraySemaphore = timesSemaphore.Get(1);
            TimeSpan currentHour = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            _currentPosition = arraySemaphore
                .Where(x => x.Begin > currentHour && currentHour < x.End)
                .FirstOrDefault().Id - 1;

            Timer _timer = new Timer(1);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

            System.Console.ReadKey();
        }
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {//falta comparar y mostrar color
            var signal = new TimeSpan(e.SignalTime.Hour, e.SignalTime.Minute, e.SignalTime.Second);
            if (signal >= arraySemaphore[_currentPosition].Begin && signal <= arraySemaphore[_currentPosition].End)
            {
                if (_currentColor != arraySemaphore[_currentPosition].Color)
                {
                    _currentColor = arraySemaphore[_currentPosition].Color;
                    System.Console.WriteLine(arraySemaphore[_currentPosition].Color);
                }

            }
            else
                _currentPosition++;
        }
    }
}
