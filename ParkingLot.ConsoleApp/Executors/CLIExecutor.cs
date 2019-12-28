using System;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.ConsoleApp.Executors
{
    internal class CLIExecutor : BaseExecutor
    {
        #region Member of BaseExecutor
        internal override void Execute()
        {
            try
            {
                while (true)
                {
                    string command = Console.ReadLine();

                    if (command.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                        return;

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