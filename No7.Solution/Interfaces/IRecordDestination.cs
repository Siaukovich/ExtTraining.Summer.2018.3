using System.Collections.Generic;

namespace No7.Solution
{
    // Интерфейс, который будут реализовавывать все классы, отвечающие за 
    // запись информации. Благодаря этому интерфейсу можно будет в будущем записывать не только 
    // в бд, а, например, в другой файл, в json и т.д. не внося измеений в существующий код (кроме точки вызова),
    // а только лишь написав новый класс, реализующий данный интерфейс.
    public interface IRecordDestination
    {
        void WriteRecords(IEnumerable<Record> records, ISimpleLogger logger);
    }
}