using System;

namespace ParkingLot.Model.Exceptions
{
    public class ParkingLotInvalidCommandException : ParkingLotException
    {
        public ParkingLotInvalidCommandException() : base("Please enter a valid command")
        {
        }

        public ParkingLotInvalidCommandException(string message) : base(message)
        {
        }

        public ParkingLotInvalidCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}