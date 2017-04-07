using Microsoft.VisualStudio.TestTools.UnitTesting;
using MegaMillionAuto;
using System.Collections.Generic;

namespace MegaMillionAutoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AppRandomTest()
        {
            var appRandom = new AppRandom();
            var array = new int[10];
            for (var i = 0; i < 1000; i++)
            {
                var id = appRandom.Next(10);
                array[id]++;
            }

            for (var id = 0; id < 10; id++)
            {
                Assert.IsTrue(array[id] > 0, "array[id] == 0");
            }
        }

        [TestMethod]
        public void StatisticsRetrieveTest()
        {
            Statistics.Retrieve(out SortedList<int, Ball> balls, out SortedList<int, Ball> megas);
            Assert.IsTrue(balls.Count == 75, "balls.Count != 75");
            Assert.IsTrue(megas.Count == 15, "megas.Count != 15");
            for (var i = 0; i < balls.Count; i++)
            {
                Assert.IsTrue(balls.Keys[i] == i + 1, "balls.Keys[i] != i + 1");
                Assert.IsTrue(balls[i + 1].Freq > 0, "balls[i + 1].Freq <= 0");
            }

            for (var i = 0; i < megas.Count; i++)
            {
                Assert.IsTrue(megas.Keys[i] == i + 1, "megas.Keys[i] != i + 1");
                Assert.IsTrue(megas[i + 1].Freq > 0, "megas[i + 1].Freq <= 0");
            }
        }
    }
}
