using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1;

namespace Task1Test
{
    [TestClass]
    public class UnitTest1
    {
        private int[] ActualResult = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        [TestMethod]
        public void ThreadResultTest()
        {
            try
            {
                int[] result = new int[15];
                int indes = 0;
                TaskDefine oTaskDefine = new TaskDefine();
                oTaskDefine.OnTaskComplete += (s, a) =>
                {
                    result[indes++] = a.Item1;
                };

                for (int i = 1; i <= 15; i++)
                {
                    oTaskDefine.AddTask(i);
                }
                //wait to all task complete.
                while (indes <= 14)
                {
                    Thread.Sleep(500);
                }
                bool isResultCorrect = true;

                for (int i = 0; i < ActualResult.Length; i++)
                {
                    if (ActualResult[i] != result[i])
                    {
                        isResultCorrect = false;
                    }
                }
                Assert.IsTrue(isResultCorrect);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Assert.IsTrue(false);
            }
        }
    }
}