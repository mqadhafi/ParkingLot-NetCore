using System;
using System.IO;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.ConsoleApp.Executors
{
    public class FileExecutor : BaseExecutor
    {
        #region Private Field
        private readonly string _filePath;
        #endregion

        #region Constructor
        public FileExecutor(string filePath)
        {
            _filePath = filePath;
        }
        #endregion

        #region Member of BaseExecutor
        public override void Execute()
        {
            try
            {
                using StreamReader streamReader = new StreamReader(_filePath);
                string command;
                while ((command = streamReader.ReadLine()) != null)
                {
                    base.Execute(command);
                }
            }
            catch (ParkingLotException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        #endregion
    }
}