using System;
using ParkingLot.Business;
using ParkingLot.ConsoleApp.Validators;
using ParkingLot.Model;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.ConsoleApp.Executors
{
    internal abstract class BaseExecutor
    {
        private readonly CommandValidator _validator = new CommandValidator();
        private readonly ParkingLotBusiness _parkingLotBusiness = new ParkingLotBusiness();

        internal abstract void Execute();

        internal void Execute(string command)
        {
            try
            {
                if (!_validator.IsValid(command))
                    throw new ParkingLotCommandException();

                string[] commands = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int slot = 0;

                switch (commands[0])
                {
                    case Command.CREATE: // create_parking_lot 6
                        int.TryParse(commands[1], out int size);
                        _parkingLotBusiness.Create(size);
                        print($"Created a parking lot with {size} slots");
                        break;
                    case Command.PARK: // park KA-01-HH-1234 White
                        slot = _parkingLotBusiness.Park(commands[1], commands[2]);
                        print(slot > 0 ? $"Allocated slot number: {slot}" : "Sorry, parking lot is full");
                        break;
                    case Command.LEAVE: // leave 4
                        int.TryParse(commands[1], out slot);
                        print(_parkingLotBusiness.Leave(slot));
                        break;
                    case Command.STATUS: // status
                        print(_parkingLotBusiness.GetStatus());
                        break;
                    case Command.REGISTRATION_NUMBERS_WITH_COLOUR: // registration_numbers_for_cars_with_colour White
                        print(_parkingLotBusiness.GetPlateNumberByColor(commands[1]) ?? "Not found");
                        break;
                    case Command.SLOT_NUMBERS_WITH_COLOUR: // slot_numbers_for_cars_with_colour White
                        print(_parkingLotBusiness.GetSlotNumberByColor(commands[1]) ?? "Not found");
                        break;
                    case Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER: // slot_number_for_registration_number KA-01-HH-3141
                        slot = _parkingLotBusiness.GetSlotNumberByPlateNumber(commands[1]);
                        print(slot > 0 ? slot.ToString() : "Not found");
                        break;
                }
            }
            catch (ParkingLotException exception)
            {
                print(exception.Message);
            }
        }

        private void print(object item)
        {
            Console.WriteLine(item);
        }
    }
}