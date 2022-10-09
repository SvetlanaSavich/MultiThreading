namespace MultiThreading.Task4.Threads.Join
{
    internal class State
    {
        public int Counter { get; set; }
        public int ThreadsCount { get; set; }

        public State(int counter, int threadsCount)
        {
            Counter = counter;
            ThreadsCount = threadsCount;
        }
    }
}
