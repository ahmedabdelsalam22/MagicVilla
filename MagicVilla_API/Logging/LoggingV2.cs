namespace MagicVilla_API.Logging
{
    public class LoggingV2 : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error -- " + message);
                Console.BackgroundColor = ConsoleColor.Black;

            }
            else
            {
                if (type == "warnning")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Error -- " + message);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else {
                    Console.WriteLine(message);
                }
            }
        }
    }
}
