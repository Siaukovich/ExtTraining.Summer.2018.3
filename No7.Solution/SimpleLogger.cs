namespace No7.Solution
{
    using System;
    using System.IO;
    
    // Конкретная реализация класса-логгера.
    public class SimpleLogger : ISimpleLogger
    {
        private static readonly Lazy<SimpleLogger> LazyLogger = 
            new Lazy<SimpleLogger>(() => new SimpleLogger());

        public static SimpleLogger Instance => LazyLogger.Value;
        
        public string LogFileName { get; set; } = "logs.log";

        private SimpleLogger()
        {
        }
        
        public void Warning(string msg)
        {
            WriteToFile(msg, "WARNING");
        }

        public void Info(string msg)
        {
            WriteToFile(msg, "INFO");
        }

        public void Error(string msg)
        {
            WriteToFile(msg, "ERROR");
        }

        private void WriteToFile(string msg, string msgType)
        {
            using (var logFile = File.AppendText(this.LogFileName))
            {
                var logMessage = $"[{DateTime.Now}] {msgType}: {msg}.";
                
                logFile.WriteLine(logMessage);
            }
        }
    }
}