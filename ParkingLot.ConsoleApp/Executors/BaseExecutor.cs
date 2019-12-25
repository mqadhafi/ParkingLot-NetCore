using System;

namespace ParkingLot.ConsoleApp.Executors
{
    internal abstract class BaseExecutor
    {
        internal abstract void Execute();

        internal void Execute(string input)
        {
            try
            {

            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
        }
    }
}