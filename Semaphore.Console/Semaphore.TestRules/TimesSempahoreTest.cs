using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semaphore.Data;
using Semaphore.Rules;
using Semaphore.Rules.Intefaces;

namespace Semaphore.TestRules
{
    [TestClass]
    public class TimesSempahoreTest
    {

        readonly TimesSemaphore timesSemaphore;
        ITimes _times;

        public TimesSempahoreTest()
        {
            _times = new DataSemaphore();
            timesSemaphore = new TimesSemaphore(_times);
        }
        [TestMethod]
        public void SaveSemaphore_test()
        {
            var saved = timesSemaphore.Save(new Models.Entities.TimeSemaphore()
            {
                Id = 1,
                Address = "La Pampa",
                Name = "Ruta e53 el pueblito",
                NumberAddres = "Km 45",
                GreenTime = new System.TimeSpan(0, 0, 40),
                RedTime = new System.TimeSpan(0, 0, 15),
                YellowTime = new System.TimeSpan(0, 0, 4),
                HourInit = new System.DateTime(2022, 5, 15, 23, 00, 00),
                HourEnd = new System.DateTime(2022, 5, 15, 23, 59, 59)
            });

            Assert.IsTrue(saved);
        }

        [TestMethod]
        public void Get_timesSemaphoreOk()
        {
            var result = timesSemaphore.Get(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_timesSemaphore_Fail()
        {
            var result = timesSemaphore.Get(2);

            Assert.IsNotNull(result);
        }
    }
}
