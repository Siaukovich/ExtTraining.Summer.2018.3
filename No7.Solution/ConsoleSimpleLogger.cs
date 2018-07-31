namespace No7.Solution
{
    using System;

    public class ConsoleSimpleLogger : ISimpleLogger
    {
        private static readonly Lazy<ConsoleSimpleLogger> LazyLogger =
            new Lazy<ConsoleSimpleLogger>(() => new ConsoleSimpleLogger());

        public static ConsoleSimpleLogger Instance => LazyLogger.Value;

        public void Warning(string msg) => this.WriteToConsole(msg, "Warning");

        public void Info(string msg) => this.WriteToConsole(msg, "Info");

        public void Error(string msg) => this.WriteToConsole(msg, "Error");

        private void WriteToConsole(string msg, string type)
        {
            var logMessage = $"[{DateTime.Now}] {type.ToUpper()}: {msg}.";

            Console.WriteLine(logMessage);
        }
    }
}
