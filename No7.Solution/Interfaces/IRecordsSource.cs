using System.Collections.Generic;

namespace No7.Solution
{
    // Интерфейс, который будут реализовавывать все классы, отвечающие за 
    // чтение информации. Благодаря этому интерфейсу можно будет в будущем считывать не только 
    // из текстового файла, а, например, из бд, из json и т.д. не внося измеений в существующий код
    // (кроме точки вызова), а только лишь написав новый класс, реализующий данный интерфейс.
    public interface IRecordsSource
    {
        IEnumerable<Record> ReadValidRecords(IRecordValidator validator, IRecordFactory recordFactory);
    }
}