namespace No7.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = new FileRecordsSource("sourcefile.txt", SimpleLogger.Instance);
            var destination = new DatabaseRecordDestination();
            
            var service = RecordTransferService.Instance;

            service.TransferRecords(source, destination, FileRecordValidator.Instance, RecordFactory.Instance, SimpleLogger.Instance);
        }
    }
}