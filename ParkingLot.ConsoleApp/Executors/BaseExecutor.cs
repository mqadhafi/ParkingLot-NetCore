using System;
using ParkingLot.ConsoleApp.Exceptions;
using ParkingLot.ConsoleApp.Validators;

namespace ParkingLot.ConsoleApp.Executors
{
    internal abstract class BaseExecutor
    {
        private readonly CommandValidator _validator = new CommandValidator();

        internal abstract void Execute();

        internal void Execute(string command)
        {
            try
            {
                if (!_validator.IsValid(command))
                    throw new ParkingLotCommandException("Please enter a valid command");

                string[] commands = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string carRegistrationNumber;
                string carColour;

                switch (commands[0])
                {
                }
            }
            catch (ParkingLotException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}