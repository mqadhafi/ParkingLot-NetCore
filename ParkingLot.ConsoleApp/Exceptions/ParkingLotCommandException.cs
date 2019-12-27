using System;

namespace ParkingLot.ConsoleApp.Exceptions
{
    internal class ParkingLotCommandException : ParkingLotException
    {
        public ParkingLotCommandException()
        {
        }

        public ParkingLotCommandException(string message) : base(message)
        {
        }

        public ParkingLotCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}