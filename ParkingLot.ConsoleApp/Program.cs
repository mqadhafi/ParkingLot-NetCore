using ParkingLot.ConsoleApp.Executors;

namespace ParkingLot.ConsoleApp
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // initialize an empty executor
            BaseExecutor executor;

            // if arguments is exists, then call File executor
            // otherwise call Command Line Interface executor
            if (args?.Length > 0)
                executor = new FileExecutor(args[0]);
            else
                executor = new CommandLineExecutor();

            // execute!
            executor.Execute();
        }
    }
}