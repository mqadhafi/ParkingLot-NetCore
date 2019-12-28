using System;
using System.IO;
using ParkingLot.ConsoleApp.Executors;
using ParkingLot.Model;
using ParkingLot.Model.Exceptions;
using Xunit;

namespace ParkingLot.Test
{
    internal class FakeExecutor : BaseExecutor
    {
        public override void Execute() => throw new NotImplementedException();

        new public void Execute(string command)
        {
            base.Execute(command);
        }
    }

    public class ParkingLotConsoleAppTest
    {
        private readonly FakeExecutor _executor;
        private readonly StringWriter _stringWriter;

        public ParkingLotConsoleAppTest()
        {
            _executor = new FakeExecutor();
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_InvalidCommand_Exception()
        {
            var exception = Assert.Throws<ParkingLotInvalidCommandException>(() => _executor.Execute("qwerty"));
            Assert.Equal("Please enter a valid command", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_CreateParkingLot_Success()
        {
            const int slot = 5;
            _executor.Execute($"{Command.CREATE} {slot}");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_InvalidSlotSize_Exception()
        {
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.CREATE} -1"));
            Assert.Equal("Invalid slot size", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_ParkOneVehicle_Success()
        {
            const int slot = 5;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_SlotFull_Exception()
        {
            _executor.Execute($"{Command.CREATE} 1");
            _executor.Execute($"{Command.PARK} car1 White");
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.PARK} KA-01-HH-9999 White"));
            Assert.Equal("Sorry, parking lot is full", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_SlotEmpty_Exception()
        {
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.PARK} KA-01-HH-9999 White"));
            Assert.Equal("You must create parking lot first", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_Leave_Success()
        {
            const int slot = 1;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.LEAVE} {slot}");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains($"Allocated slot number: {slot}", _stringWriter.ToString());
            Assert.Contains($"Slot number {slot} is free", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_InvalidLeaveCommand_Exception()
        {
            const int slot = 1;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains($"Allocated slot number: {slot}", _stringWriter.ToString());
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.LEAVE} -1"));
            Assert.Equal("The given slot doesn't exist", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_LeaveEmptySlot_Exception()
        {
            const int slot = 1;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.LEAVE} {slot}");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains($"Allocated slot number: {slot}", _stringWriter.ToString());
            Assert.Contains($"Slot number {slot} is free", _stringWriter.ToString());
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.LEAVE} {slot}"));
            Assert.Equal("No vehicle found on this slot", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetStatus_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            _executor.Execute(Command.STATUS);
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("Slot No.\tRegistration No\tColour\n1\tcar1\tWhite\r\n2\tcar2\tRed\r\n3\tcar3\tBlack\r\n4\tcar4\tWhite", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetPlateNumberByColor_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            _executor.Execute($"{Command.REGISTRATION_NUMBERS_WITH_COLOUR} White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("car1, car4", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetPlateNumberByColorCaseInsesitive_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 whItE");
            _executor.Execute($"{Command.REGISTRATION_NUMBERS_WITH_COLOUR} whItE");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("car1, car4", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetPlateNumberByWrongColor_Exception()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.REGISTRATION_NUMBERS_WITH_COLOUR} Blue"));
            Assert.Equal("Not found", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByColor_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            _executor.Execute($"{Command.SLOT_NUMBERS_WITH_COLOUR} White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("1, 4", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByColorCaseInsensitive_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            _executor.Execute($"{Command.SLOT_NUMBERS_WITH_COLOUR} red");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("2", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByWrongColor_Exception()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.SLOT_NUMBERS_WITH_COLOUR} Blue"));
            Assert.Equal("Not found", exception.Message);
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByPlateNumber_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            _executor.Execute($"{Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER} car3");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("3", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByPlateNumberCaseInsesitive_Success()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} CAR1 White");
            _executor.Execute($"{Command.PARK} CAR2 Red");
            _executor.Execute($"{Command.PARK} CAR3 Black");
            _executor.Execute($"{Command.PARK} CAR4 White");
            _executor.Execute($"{Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER} car3");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            Assert.Contains("3", _stringWriter.ToString());
        }

        [Fact]
        public void ParkingLotConsoleAppTest_GetSlotNumberByWrongPlateNumber_Exception()
        {
            const int slot = 4;
            _executor.Execute($"{Command.CREATE} {slot}");
            _executor.Execute($"{Command.PARK} car1 White");
            _executor.Execute($"{Command.PARK} car2 Red");
            _executor.Execute($"{Command.PARK} car3 Black");
            _executor.Execute($"{Command.PARK} car4 White");
            Assert.Contains($"Created a parking lot with {slot} slots", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 1", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 2", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 3", _stringWriter.ToString());
            Assert.Contains("Allocated slot number: 4", _stringWriter.ToString());
            var exception = Assert.Throws<ParkingLotException>(() => _executor.Execute($"{Command.SLOT_NUMBERS_FOR_REGISTRATION_NUMBER} car5"));
            Assert.Equal("Not found", exception.Message);
        }
    }
}