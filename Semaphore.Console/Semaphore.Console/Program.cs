using Semaphore.Data;
using Semaphore.Models.Entities;
using Semaphore.Rules;
using Semaphore.Rules.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        static FlujoEnum _CurrentflujoEnum;
        static FlujoEnum _InitialflujoEnum;
        static CancellationTokenSource _ctsAutomatic;
        static CancellationTokenSource _ctsSemiAutomatic = new CancellationTokenSource();
        static CancellationTokenSource _ctsManual = new CancellationTokenSource();
        static System.Timers.Timer _timerAutomatic;
        public Program()
        {
        }
        static void Main(string[] args)
        {

            _times = _times = new DataSemaphore();
            timesSemaphore = new TimesSemaphore(_times);
            _CurrentflujoEnum = FlujoEnum.Automatico;


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

            System.Console.WriteLine("Ingresar opcion: B, C");

            while (true)
            {
                var Seleccion = System.Console.ReadLine();

                switch (Seleccion)
                {
                    case "A":
                        {
                            Task.Run(() => Automatic());
                            break;
                        }
                    case "SM":
                        {
                            SemitAutomatic(_ctsSemiAutomatic.Token);
                            break;
                        }
                    case "M":
                        {
                            System.Console.WriteLine("Ingresar Color");
                            string color = System.Console.ReadLine();
                            Task.Run(() => Manual(color));
                            break;
                        }
                    case "QA":
                        {
                            _ctsAutomatic.Cancel();
                            break;
                        }
                    case "QS":
                        {
                            _ctsSemiAutomatic.Cancel();
                            break;
                        }
                    case "QM":
                        {
                            _ctsManual.Cancel();
                            break;
                        }
                }
            }

            System.Console.ReadKey();
        }
        private static void Automatic()
        {
            _timerAutomatic = new(1);
            _timerAutomatic.Elapsed += _timer_Elapsed;
            _timerAutomatic.Start();
            _ctsAutomatic = new CancellationTokenSource();
        }
        private static Task SemitAutomatic(CancellationToken cts)
        {
            if (cts.IsCancellationRequested)
                return Task.CompletedTask;

            return Task.CompletedTask;
        }
        private static void Manual(string color)
        {
            string _currentColor = ConsoleColor.White.ToString();
            if (color != _currentColor)
            {
                _currentColor = color;
                System.Console.ForegroundColor = CambiarColor(color);
                System.Console.WriteLine(color);
            }
        }
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CambiarEstadoSemaforo(e);
        }
        private static void CambiarEstadoSemaforo(ElapsedEventArgs e)
        {
            var signal = new TimeSpan(e.SignalTime.Hour, e.SignalTime.Minute, e.SignalTime.Second);

            if (_ctsAutomatic.IsCancellationRequested)
            {
                _timerAutomatic.Elapsed -= _timer_Elapsed;
                System.Console.WriteLine("Automatico Cancelado");
                _ctsAutomatic.TryReset();
                return;
            }
            if (_currentPosition == arraySemaphore.Count)
            {
                CambiarColor("Yellow");
                System.Console.WriteLine("fin");
                _timerAutomatic.Elapsed -= _timer_Elapsed;
            }
            else
            {
                if (signal >= arraySemaphore[_currentPosition].Begin && signal <= arraySemaphore[_currentPosition].End)
                {
                    if (_currentColor != arraySemaphore[_currentPosition].Color)
                    {
                        _currentColor = arraySemaphore[_currentPosition].Color;
                        CambiarColor(_currentColor);
                        System.Console.WriteLine(arraySemaphore[_currentPosition].Color);
                    }
                }
                else
                    _currentPosition++;
            }
        }
        private static ConsoleColor CambiarColor(string color)
        {
            ConsoleColor consoleColor;
            if (color == "Green")
                consoleColor = ConsoleColor.Green;
            else if (color == "Yellow")
                consoleColor = ConsoleColor.Yellow;
            else
                consoleColor = ConsoleColor.Red;

            System.Console.ForegroundColor = consoleColor;

            return consoleColor;
        }
    }
}