namespace ParkingLot.Business.Contract
{
    public interface IParkingDisplayService
    {
        string GetStatus();
        string GetPlateNumberByColor(string color);
        string GetSlotNumberByColor(string color);
        int GetSlotNumberByPlateNumber(string plateNumber);
    }
}