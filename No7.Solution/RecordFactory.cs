using System;
using System.Globalization;

namespace No7.Solution
{
    // Конкретная реализация класса-фабрики объектов типа Record.
    public class RecordFactory : IRecordFactory
    {
        private static readonly Lazy<RecordFactory> LazyRecordFactory = 
                            new Lazy<RecordFactory>(() => new RecordFactory());

        public static IRecordFactory Instance => LazyRecordFactory.Value;
        
        private RecordFactory()
        {
        }
        
        public Record CreateNewRecord(string destinationCurrency, 
                                      string sourceCurrency, 
                                      string price, 
                                      string lots, 
                                      IRecordValidator validator)
        {
            validator.Check(destinationCurrency, sourceCurrency, price, lots);

            var numberFormat = NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint;
            var invariantCulture = CultureInfo.InvariantCulture;

            return new Record(destinationCurrency,
                              sourceCurrency,
                              decimal.Parse(price, numberFormat, invariantCulture),
                              float.Parse(lots, numberFormat, invariantCulture));
        }
    }
}