using System;
using System.Collections.Generic;
using ParkingLot.Model;

namespace ParkingLot.ConsoleApp.Validators
{
    internal class CommandValidator
    {
        /// <summary>
        /// Key: argument name
        /// Value: argument length
        /// </summary>
        private readonly IDictionary<string, byte> _validCommands;

        internal CommandValidator()
        {
            _validCommands = new Dictionary<string, byte>
            {
                { Command.CREATE, 1 },
                { Command.PARK, 2 },
                { Command.LEAVE, 1 },
                { Command.STATUS, 0 },
                { Command.REGISTRATION_NUMBERS_WITH_COLOUR, 1 },
                { Command.SLOT_NUMBERS_WITH_COLOUR, 1 },
                { Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER, 1 },
            };
        }

        internal bool IsValid(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return false;

            string[] commandList = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return _validCommands.TryGetValue(commandList[0], out byte value) && value == commandList.Length - 1;
        }
    }
}