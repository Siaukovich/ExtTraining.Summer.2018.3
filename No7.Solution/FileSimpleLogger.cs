namespace No7.Solution
{
    using System;
    using System.IO;
    
    // Конкретная реализация класса-логгера.
    public class FileSimpleLogger : ISimpleLogger
    {
        private static readonly Lazy<FileSimpleLogger> LazyLogger = 
            new Lazy<FileSimpleLogger>(() => new FileSimpleLogger());

        public static FileSimpleLogger Instance => LazyLogger.Value;
        
        public string LogFileName { get; set; } = "logs.log";

        private FileSimpleLogger()
        {
        }

        public void Warning(string msg) => WriteToFile(msg, "WARNING");

        public void Info(string msg) => WriteToFile(msg, "INFO");

        public void Error(string msg) => WriteToFile(msg, "ERROR");

        private void WriteToFile(string msg, string msgType)
        {
            using (var logFile = File.AppendText(this.LogFileName))
            {
                var logMessage = $"[{DateTime.Now}] {msgType.ToUpper()}: {msg}.";
                
                logFile.WriteLine(logMessage);
            }
        }
    }
}