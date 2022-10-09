/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        private const int IntegersCount = 10;

        private static async Task Main(string[] args)
        {
            try
            {
                await Task.Run(() => CreateRandomIntegersArray())
                .ContinueWith(t => MultipleArrayItems(t.Result))
                .ContinueWith(t => SortArray(t.Result))
                .ContinueWith(t => GetArrayAverageValue(t.Result));

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private static int[] CreateRandomIntegersArray()
        {
            var random = new Random();
            var resultArray = new int[IntegersCount];

            for (int i = 0; i<resultArray.Length; i++)
            {
                resultArray[i] = random.Next(100, 1000);
            }

            PrintTaskResult($"Result of task 1: {string.Join(", ", resultArray)}");

            return resultArray;
        }

        private static int[] MultipleArrayItems(int[] sourceArray)
        {
            var randomInteger = new Random().Next(100, 1000);

            var resultArray = sourceArray.Select(i =>  i * randomInteger).ToArray();

            PrintTaskResult($"Result of task 2: {string.Join(", ", resultArray)}");

            return resultArray;
        }

        private static int[] SortArray(int[] sourceArray)
        {
            Array.Sort(sourceArray);

            PrintTaskResult($"Result of task 3: {string.Join(", ", sourceArray)}");

            return sourceArray;
        }

        private static void GetArrayAverageValue(int[] sourceArray)
        {
            double result = sourceArray.Average();

            PrintTaskResult($"Result of task 4: {result}");
        }

        private static void PrintTaskResult(string message)
        {
            Console.WriteLine(message);
        }
    }
}
