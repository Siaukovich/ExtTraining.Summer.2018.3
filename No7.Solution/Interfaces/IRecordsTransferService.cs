namespace No7.Solution
{
    // Интерфейс, который будут реализовавывать все классы-сервисы, отвечающие за 
    // трансфер информации. Благодаря этому можно будет использовать различные сервисы
    // с различной внутренней логикой, изменяя код только в корне приложения.
    public interface IRecordsTransferService
    {
        float LotSize { get; set; }

        void TransferRecords(IRecordsSource source, 
                             IRecordDestination destination, 
                             IRecordValidator validator,
                             IRecordFactory recordFactory, 
                             ISimpleLogger logger);
    }
}