using System;

namespace ParkingLot.Model.Exceptions
{
    public abstract class ParkingLotException : Exception
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