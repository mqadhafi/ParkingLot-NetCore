using System;

namespace ParkingLot.Model.Exceptions
{
    public class ParkingLotVehicleExistException : ParkingLotException
    {
        public ParkingLotVehicleExistException() : base("Same vehicle already exist")
        {
        }

        public ParkingLotVehicleExistException(string message) : base(message)
        {
        }

        public ParkingLotVehicleExistException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}