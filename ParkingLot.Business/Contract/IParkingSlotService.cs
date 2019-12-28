namespace ParkingLot.Business.Contract
{
    public interface IParkingSlotService
    {
        void Create(int size);
        int Park(string plateNumber, string color);
        int Leave(int slot);
    }
}