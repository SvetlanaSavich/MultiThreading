/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    public static class Program
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        static void Main(string[] args)
        {
            var createThreadState = new State(0, 10);
            CreateThread(createThreadState);

            var runThreadInThreadPoolState = new State(10, 10);
            RunThreadInThreadPool(runThreadInThreadPoolState);


            Console.ReadLine();
        }

        private static void CreateThread(State state)
        {
            if(state.ThreadsCount < 1)
            {
                return;
            }

            --state.ThreadsCount;
            --state.Counter;

            Console.WriteLine(state.Counter);

            var thread = new Thread(() => CreateThread(state));
            thread.Start();
            thread.Join();
        }

        private static void RunThreadInThreadPool(object state)
        {
            semaphoreSlim.Wait();
            if (((State)state).ThreadsCount < 1)
            {
                return;
            }

            --((State)state).ThreadsCount;
            --((State)state).Counter;

            Console.WriteLine(((State)state).Counter);

            ThreadPool.QueueUserWorkItem(new WaitCallback(RunThreadInThreadPool), state);

            semaphoreSlim.Release();
        }
    }
}
