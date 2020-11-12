using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Task1
{
    public class TaskDefine
    {
        public EventHandler<Tuple<int, int>> OnTaskComplete;
        private Random oRandom;

        public TaskDefine()
        {
            oRandom = new Random();
        }

        public void AddTask(int taskNo)
        {
            //ThreadPool.QueueUserWorkItem(Action2, taskNo);
            new Thread(Action2).Start(taskNo);
        }

        private void Action2(object taskNo)
        {
            lock (this)
            {
                int tTaskNo = (int)taskNo;
                //performing some time consuming task action
                int randTime = oRandom.Next(500, 1500);
                Thread.Sleep(randTime);
                if (OnTaskComplete != null)
                {
                    OnTaskComplete(this, new Tuple<int, int>(tTaskNo, randTime));
                }
            }
        }
    }
}