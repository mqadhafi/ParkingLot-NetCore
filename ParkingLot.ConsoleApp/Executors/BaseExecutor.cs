using System;
using ParkingLot.Business;
using ParkingLot.Business.Contract;
using ParkingLot.ConsoleApp.Validators;
using ParkingLot.Model;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.ConsoleApp.Executors
{
    public abstract class BaseExecutor
    {
        private readonly CommandValidator _validator = new CommandValidator();
        private readonly IParkingLotBusiness _parkingLotBusiness = new ParkingLotBusiness();

        public abstract void Execute();

        public void Execute(string command)
        {
            if (!_validator.IsValid(command))
                throw new ParkingLotInvalidCommandException();

            int slot;
            string[] commands = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (commands[0])
            {
                case Command.CREATE: // create_parking_lot 6
                    int.TryParse(commands[1], out int size);
                    _parkingLotBusiness.Create(size);
                    print($"Created a parking lot with {size} slots");
                    break;
                case Command.PARK: // park KA-01-HH-1234 White
                    slot = _parkingLotBusiness.Park(commands[1], commands[2]);
                    print($"Allocated slot number: {slot}");
                    break;
                case Command.LEAVE: // leave 4
                    int.TryParse(commands[1], out slot);
                    _parkingLotBusiness.Leave(slot);
                    print($"Slot number {slot} is free");
                    break;
                case Command.STATUS: // status
                    print(_parkingLotBusiness.GetStatus());
                    break;
                case Command.REGISTRATION_NUMBERS_WITH_COLOUR: // registration_numbers_for_cars_with_colour White
                    print(_parkingLotBusiness.GetPlateNumberByColor(commands[1]));
                    break;
                case Command.SLOT_NUMBERS_WITH_COLOUR: // slot_numbers_for_cars_with_colour White
                    print(_parkingLotBusiness.GetSlotNumberByColor(commands[1]));
                    break;
                case Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER: // slot_number_for_registration_number KA-01-HH-3141
                    print(_parkingLotBusiness.GetSlotNumberByPlateNumber(commands[1]));
                    break;
            }
        }

        private void print(object item)
        {
            Console.WriteLine(item);
        }
    }
}