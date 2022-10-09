/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task1._100Tasks
{
    public static class Program
    {
        private const int TaskAmount = 100;
        private const int MaxIterationsCount = 1000;

        private static void Main(string[] args)
        {
            HundredTasks();

            Console.ReadLine();
        }

        private static void HundredTasks()
        {
            var tasks = new Task[TaskAmount];

            for(int i = 0; i < TaskAmount; i++)
            {
                int taskNumber = i + 1;
                tasks[i] = Task.Factory.StartNew(() => PrintThousandLines(taskNumber));
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch(AggregateException ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private static void PrintThousandLines(int taskNumber)
        {
           for(int i= 1; i <= MaxIterationsCount; i++)
            {
                Output(taskNumber, i);
            }
        }

        private static void Output(int taskNumber, int iterationNumber)
        {
            Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
        }
    }
}
