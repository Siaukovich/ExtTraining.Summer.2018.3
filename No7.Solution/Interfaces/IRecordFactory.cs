namespace No7.Solution
{
    // Интерфейс, который будут реализовавывать все классы, отвечающие за 
    // создание объектов класса Record. Благодаря этому интерфейсу можно будет в будущем 
    // изменить логику создания объектов.
    public interface IRecordFactory
    {
        Record CreateNewRecord(string destinationCurrency, 
                               string sourceCurrency, 
                               string price,
                               string lots,
                               IRecordValidator validator);
    }
}