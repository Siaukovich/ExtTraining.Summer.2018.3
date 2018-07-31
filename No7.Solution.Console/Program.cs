namespace No7.Solution.Console
{
    using System.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = ConfigurationManager.AppSettings["SourceFile"];
            string connectionString = ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString;

            // LoggerService.Logger = FileSimpleLogger.Instance;
            LoggerService.Logger = ConsoleSimpleLogger.Instance;

            IRecordsSource source = new FileRecordsSource(sourceFile);
            IRecordDestination destination = new DatabaseRecordDestination(connectionString);

            IRecordsTransferService service = RecordTransferService.Instance;

            service.TransferRecords(source, destination, FileRecordValidator.Instance, RecordFactory.Instance);
        }
    }
}