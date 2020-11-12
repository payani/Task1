using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Task1
{
    public class TaskDefine : IDisposable
    {
        #region Public Fields

        public EventHandler<Tuple<int, int>> OnTaskComplete;
        private bool isThreadKeepAlive;
        private Thread PreviusThread;
        private Thread TaskOrganiserThread;

        #endregion Public Fields

        #region Private Fields

        private Random oRandom;

        #endregion Private Fields

        #region Public Constructors

        public TaskDefine()
        {
            oRandom = new Random();
            TaskQueue = new Queue<int>();
            isThreadKeepAlive = true;
            TaskOrganiserThread = new Thread(TaskOrganiser);
            TaskOrganiserThread.Start();
            OnTaskComplete += (s, a) => OnTaskUpdate();
        }

        #endregion Public Constructors

        #region Public Methods

        private Queue<int> TaskQueue;

        public void AddTask(int taskNo)
        {
            //ThreadPool.QueueUserWorkItem(Action2, taskNo);
            //new Thread(Action).Start(taskNo);
            TaskQueue.Enqueue(taskNo);
            OnTaskUpdate();
        }

        #endregion Public Methods

        #region Private Methods

        private bool disposedValue;

        private bool isOrganiserThreadInSleep;

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    isThreadKeepAlive = false;
                    OnTaskUpdate();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        private void Action(object taskNo)
        {
            lock (this)
            {
                int tTaskNo = (int)taskNo;
                //performing some time consuming task action
                int randTime = oRandom.Next(500, 1500);
                Thread.Sleep(randTime);
                //on task complete generate event.
                OnTaskComplete?.Invoke(this, new Tuple<int, int>(tTaskNo, randTime));
            }
        }

        private void OnTaskUpdate()
        {
            if (isOrganiserThreadInSleep)
            {
                TaskOrganiserThread.Interrupt();
            }
        }

        private void TaskOrganiser()
        {
            isOrganiserThreadInSleep = false;
            while (isThreadKeepAlive)
            {
                if (TaskQueue.Count > 0)
                {
                    if (PreviusThread == null || !PreviusThread.IsAlive)
                    {
                        int task = TaskQueue.Dequeue();
                        PreviusThread = new Thread(Action);
                        PreviusThread.Start(task);
                    }

                    try
                    {
                        if (TaskQueue.Count == 0)
                        {
                            isOrganiserThreadInSleep = true;
                            Thread.Sleep(Timeout.Infinite);
                        }
                    }
                    catch (ThreadInterruptedException)
                    {
                        //no need to handle and log.
                        isOrganiserThreadInSleep = false;
                    }
                }
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TaskDefine()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        #endregion Private Methods
    }
}