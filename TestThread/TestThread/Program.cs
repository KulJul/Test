using System;
using System.Collections.Generic;
using System.Threading;
namespace ReaderWriterLockSlim_class
{
    class akshay
    {
        static ReaderWriterLockSlim ReaderWriter = new ReaderWriterLockSlim();
        static Queue<int> QueueItems = new Queue<int>();
        static Random Rand = new Random();

        static void Pop(object threadID)
        {
            while (true)
            {
                ReaderWriter.EnterReadLock();
                if (QueueItems.Count != 0)
                {
                    var dequeueNumber = QueueItems.Dequeue();
                    Console.WriteLine("Thread " + threadID + " Dequeue " + dequeueNumber.ToString());
                }
                ReaderWriter.ExitReadLock();
                Thread.Sleep(100);
            }
        }

        static void Push(object threadID)
        {
            while (true)
            {
                ReaderWriter.EnterWriteLock();
                var randomNumber = GetRandNum(1000);
                QueueItems.Enqueue(randomNumber);
                Console.WriteLine("Thread " + threadID + " Enqueue " + randomNumber.ToString());
                ReaderWriter.ExitWriteLock();
                Thread.Sleep(100);
            }
        }

        static int GetRandNum(int max)
        {
            lock (Rand)
                return Rand.Next(max);
        }



        static void Main()
        {
            new Thread(Push).Start("Push");
            new Thread(Pop).Start("Pop");
            Console.Read();
        }

    }
}