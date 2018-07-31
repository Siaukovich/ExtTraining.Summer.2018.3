namespace No7.Solution.Console
{
    using System.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = ConfigurationManager.AppSettings["SourceFile"];

            IRecordsSource source = new FileRecordsSource(sourceFile, SimpleLogger.Instance);
            IRecordDestination destination = new DatabaseRecordDestination();

            IRecordsTransferService service = RecordTransferService.Instance;

            service.TransferRecords(source, destination, FileRecordValidator.Instance, RecordFactory.Instance, SimpleLogger.Instance);
        }
    }
}