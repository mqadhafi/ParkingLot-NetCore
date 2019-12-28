using System;
using ParkingLot.Model.Exceptions;

namespace ParkingLot.ConsoleApp.Executors
{
    public class CLIExecutor : BaseExecutor
    {
        #region Member of BaseExecutor
        public override void Execute()
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