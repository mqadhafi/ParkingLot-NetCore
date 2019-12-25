using System;

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
            throw new NotImplementedException();
        }
        #endregion
    }
}