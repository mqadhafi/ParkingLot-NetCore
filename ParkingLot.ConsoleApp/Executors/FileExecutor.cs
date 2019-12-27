using System;
using System.IO;

namespace ParkingLot.ConsoleApp.Executors
{
    internal class FileExecutor : BaseExecutor
    {
        #region Private Field
        private readonly string _filePath;
        #endregion

        #region Constructor
        internal FileExecutor(string filePath)
        {
            _filePath = filePath;
        }
        #endregion

        #region Member of BaseExecutor
        internal override void Execute()
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
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        #endregion
    }
}