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
                string[] arguments = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string carRegistrationNumber;
                string carColour;

                switch (arguments[0])
                {
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{exception.Message}");
            }
        }
    }
}