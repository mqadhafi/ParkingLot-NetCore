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
        public FileExecutor(string filePath)
        {
            _filePath = filePath;
        }
        #endregion

        #region Member of BaseExecutor
        internal override void Execute()
        {
            try
            {
                using StreamReader file = new StreamReader(_filePath);
                string input;
                while ((input = file.ReadLine()) != null)
                {
                    base.Execute(input);
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