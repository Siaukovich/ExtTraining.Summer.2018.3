namespace No7.Solution
{
    // Интерфейс, который будут реализовавывать все классы, отвечающие за 
    // валидацию информации. Благодаря этому интерфейсу можно будет в будущем
    // изменить логику валидации не внося измеений в существующий код (кроме точки вызова), а только
    // лишь написав новый класс, реализующий данный интерфейс.
    public interface IRecordValidator
    {
        void Check(string destinationCurrency, 
                   string sourceCurrency, 
                   string price, 
                   string lots);
    }
}