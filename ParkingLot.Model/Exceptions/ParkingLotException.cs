using System;

namespace ParkingLot.Model.Exceptions
{
    public class ParkingLotException : Exception
    {
        public ParkingLotException()
        {
        }

        public ParkingLotException(string message) : base(message)
        {
        }

        public ParkingLotException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}