using System;
using System.Diagnostics;

namespace BittrexOrderList
{
    internal static class DiagSpeed
    {
        static private Stopwatch stopWatch = new Stopwatch();

        static public void DiagTime(bool DiagState) //вызов в начале и конце функции true false вывод времени выполнения
        {
            if (DiagState)
            {
                stopWatch.Start();
            }
            else
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds);
                Console.WriteLine("RunTime " + elapsedTime);
                stopWatch.Reset();
            }
        }
    }
}