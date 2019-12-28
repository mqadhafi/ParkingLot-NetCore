﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkingLot.Model;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.Business
{
    public class ParkingLotBusiness
    {
        private string[] _slots;
        private IDictionary<int, Vehicle> _vehicles = new Dictionary<int, Vehicle>();

        public void Create(int size)
        {
            _slots = new string[size];
            _vehicles = new Dictionary<int, Vehicle>();
        }

        public int Park(string plateNumber, string color)
        {
            if (Array.IndexOf(_slots, plateNumber) > -1)
                throw new ParkingLotCommandException("Same vehicle already exist");

            int slot = 0;

            for (int i = 0; i < _slots.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(_slots[i]))
                {
                    slot = i + 1;
                    _slots[i] = plateNumber;
                    _vehicles.Add(slot, new Vehicle
                    {
                        Colour = color,
                        PlateNumber = plateNumber
                    });
                    break;
                }
            }

            return slot;
        }

        public int Leave(int slot)
        {
            _slots[slot - 1] = null;
            _vehicles.Remove(slot);

            return slot;
        }

        public string GetStatus()
        {
            var message = new StringBuilder("Slot No.\tRegistration No\tColour\n");
            int index = 0;
            foreach (KeyValuePair<int, Vehicle> item in _vehicles)
            {
                message
                    .Append(item.Key).Append('\t')
                    .Append(item.Value.PlateNumber).Append('\t')
                    .Append(item.Value.Colour);

                // line break
                if (index++ < _vehicles.Count - 1)
                    message.AppendLine();
            }
            return message.ToString();
        }

        public string GetPlateNumberByColor(string color)
        {
            IEnumerable<string> platNumbers = _vehicles.Values
                .Where(x => x.Colour.Equals(color, StringComparison.CurrentCultureIgnoreCase))
                .Select(x => x.PlateNumber);

            return platNumbers?.Any() == true ? string.Join(", ", platNumbers) : null;
        }

        public string GetSlotNumberByColor(string color)
        {
            var expectedSlots = new List<int>();
            foreach (KeyValuePair<int, Vehicle> item in _vehicles)
            {
                if (item.Value.Colour.Equals(color, StringComparison.CurrentCultureIgnoreCase))
                    expectedSlots.Add(item.Key);
            }
            return expectedSlots?.Count > 0 ? string.Join(", ", expectedSlots) : null;
        }

        public int GetSlotNumberByPlateNumber(string plateNumber)
        {
            return Array.IndexOf(_slots, plateNumber) + 1;
        }
    }
}