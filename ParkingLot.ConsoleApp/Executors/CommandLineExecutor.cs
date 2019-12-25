using System;

namespace ParkingLot.ConsoleApp.Executors
{
    internal class CommandLineExecutor : BaseExecutor
    {
        #region Member of BaseExecutor
        internal override void Execute()
        {
            try
            {
                while (true)
                {
                    string input = Console.ReadLine();

                    // if user type "exit", then close this Command Line Interface
                    if (input.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                        return;

                    base.Execute(input);
                }
            }
            catch (Exception execption)
            {
                Console.WriteLine(execption.Message);
            }
        }
        #endregion
    }
}