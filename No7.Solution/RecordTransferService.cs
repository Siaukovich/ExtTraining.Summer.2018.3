namespace No7.Solution
{
    using System;
    using System.Linq;
    
    // Сервис, ответственный за передачу данных из одного хранилища в другое.
    public class RecordTransferService : IRecordsTransferService
    {
        private static readonly Lazy<RecordTransferService> LazyService =
                            new Lazy<RecordTransferService>(() => new RecordTransferService());

        public static RecordTransferService Instance => LazyService.Value;
        
        public float LotSize { get; set; } = 100000f;

        private RecordTransferService()
        {
        }
        
        public void TransferRecords(IRecordsSource source,
                                    IRecordDestination destination, 
                                    IRecordValidator validator,
                                    IRecordFactory recordFactory)
        {
            var validRecords = source.ReadValidRecords(validator, recordFactory);
            destination.WriteRecords(validRecords);
        }
    }
}