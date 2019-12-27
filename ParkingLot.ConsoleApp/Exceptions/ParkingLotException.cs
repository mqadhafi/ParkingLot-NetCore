using System;

namespace ParkingLot.ConsoleApp.Exceptions
{
    internal abstract class ParkingLotException : Exception
    {
        protected ParkingLotException()
        {
        }

        protected ParkingLotException(string message) : base(message)
        {
        }

        protected ParkingLotException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}