using System;

namespace ParkingLot.Model.Exceptions
{
    public class ParkingLotCommandException : ParkingLotException
    {
        public ParkingLotCommandException() : base("Please enter a valid command")
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