using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace UnitTestProject1
{
    [TestClass]
    public class TaskDefineTest
    {
        private const string Expected = "1 2 3 4 5 6 7 8 9 10 11 12 13 14 ";

        [TestMethod]
        public void TestMethod1()
        {
            string result = "";
            TaskDefine oTaskDefine = new TaskDefine();
            oTaskDefine.OnTaskComplete += (s, a) => result += a.Item1 + " ";
            for (int i = 0; i < 15; i++)
            {
                oTaskDefine.AddTask(i);
            }
            Console.WriteLine(Expected + "/n");
            Console.WriteLine(result + "/n");
            Assert.AreEqual(Expected, result);
        }
    }
}