using System;

namespace ParkingLot.Model.Exceptions
{
    public class ParkingLotCommandException : ParkingLotException
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