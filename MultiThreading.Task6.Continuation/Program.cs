/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            // Task a
            Task.Run(() => ParentTaskJob(false)).ContinueWith(t => ContinuationTask(), TaskContinuationOptions.None).Wait();
            Task.Run(() => ParentTaskJob(true)).ContinueWith(t => ContinuationTask(t.Exception), TaskContinuationOptions.None).Wait();

            //Task b
            Task.Run(() => ParentTaskJob(true)).ContinueWith(t => ContinuationTask(t.Exception), TaskContinuationOptions.OnlyOnFaulted).Wait();

            //Task c
            Task.Run(() => ParentTaskJob(true)).ContinueWith(t => ContinuationTask(t.Exception), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously).Wait();

            //Task d 
            var task = Task.Run(() => ParentTaskJob(false)); // without exception
            try
            {
                task.Wait();
                ContinuationTask();
            }
            catch
            {
                ContinuationTask(task.Exception);
            }

            task = Task.Run(() => ParentTaskJob(true)); // with exception 
            try
            {
                task.Wait();
                ContinuationTask();
            }
            catch
            {
                ContinuationTask(task.Exception);
            }


            Console.ReadLine();
        }

        private static void ParentTaskJob(bool isFailed)
        {
            Console.WriteLine("Parent Task has been started");
            Console.WriteLine($"Parent Task Thread: {Thread.CurrentThread.ManagedThreadId}");

            if (!isFailed)
            {
                Console.WriteLine("Parent Task has been completed without exception.");
            }
            else
            {
                Console.WriteLine("Parent Task has been completed with an exception.");
                throw new ArgumentException("Exception has been thrown in parent task");
            }
        }

        private static void ContinuationTask(Exception parentTaskexception = null)
        {
            Console.WriteLine("Continuation Task has been started");
            Console.WriteLine($"Continuation Task Thread: {Thread.CurrentThread.ManagedThreadId}");

            if (parentTaskexception != null)
            {
                Console.WriteLine(parentTaskexception.Message);
            }

            Console.WriteLine("Continuation Task has been completed");
            Console.WriteLine("\n-------------------------------\n");
        }
    }
}
