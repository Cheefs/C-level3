using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSenderWPF.Tests
{
    [TestClass()]
    public class SchedulerTests
    {
        static  IViev viev;
        private static Scheduler sc;
        private static TimeSpan ts;

        public SchedulerTests() {}
       
        public SchedulerTests(IViev Viev)
        {
            viev = Viev;
        }
        // Запускается перед стартом каждого тестирующего метода. 
        [ClassInitialize]
        public static void TestInitialize(TestContext context)
        {
           sc = new Scheduler(viev);
            Debug.WriteLine("Test Initialize");
            ts = new TimeSpan(); // возвращаем в случае ошибочно введенного времени
            sc.DatesEmailTexts = new Dictionary<DateTime, string>()
            {
        { new DateTime(2016, 12, 24, 22, 0, 0), "text1" },
        { new DateTime(2016, 12, 24, 22, 30, 0), "text2" },
        { new DateTime(2016, 12, 24, 23, 0, 0), "text3" }
             };
        }


        [TestMethod()]
            public void GetSendTime_empty_ts()
            {
                string strTimeTest = "";
                TimeSpan tsTest = sc.GetSendTime(strTimeTest);
                Assert.AreEqual(ts, tsTest);
            }

            [TestMethod()]
            public void GetSendTime_sdf_ts()
            {
                string strTimeTest = "sdf";
                TimeSpan tsTest = sc.GetSendTime(strTimeTest);
                Assert.AreEqual(ts, tsTest);
            }

            [TestMethod()]
            public void GetSendTime_correctTime_Equal()
            {
                string strTimeTest = "12:12";
                TimeSpan tsCorrect = new TimeSpan(12, 12, 0);
                TimeSpan tsTest = sc.GetSendTime(strTimeTest);
                Assert.AreEqual(tsCorrect, tsTest);
            }

            [TestMethod()]
            public void GetSendTime_inCorrectHour_ts()
            {
                string strTimeTest = "25:12";
                TimeSpan tsTest = sc.GetSendTime(strTimeTest);
                Assert.AreEqual(ts, tsTest);
            }

            [TestMethod()]
            public void GetSendTime_inCorrectMin_ts()
            {
                string strTimeTest = "12:65";
                TimeSpan tsTest = sc.GetSendTime(strTimeTest);
                Assert.AreEqual(ts, tsTest);
            }

        [TestMethod()]
        public void TimeTick_Dictionare_correct()
        {
            DateTime dt1 = new DateTime(2016, 12, 24, 22, 0, 0);
            DateTime dt2 = new DateTime(2016, 12, 24, 22, 30, 0);
            DateTime dt3 = new DateTime(2016, 12, 24, 23, 0, 0);

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt1.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt2.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            if (sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString() == dt3.ToShortTimeString())
            {
                Debug.WriteLine("Body " + sc.DatesEmailTexts[sc.DatesEmailTexts.Keys.First<DateTime>()]);
                Debug.WriteLine("Subject " + $"Рассылка от {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortDateString()}  {sc.DatesEmailTexts.Keys.First<DateTime>().ToShortTimeString()}");
                sc.DatesEmailTexts.Remove(sc.DatesEmailTexts.Keys.First<DateTime>());
            }

            Assert.AreEqual(0, sc.DatesEmailTexts.Count);
        }

    }

}