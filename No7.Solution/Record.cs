using System.Globalization;

namespace No7.Solution
{
    public class Record
    {
        public Record(string destinationCurrency, string sourceCurrency, decimal price, float lots)
        {
            this.DestinationCurrency = destinationCurrency;
            this.SourceCurrency = sourceCurrency;
            this.Price = price;
            this.Lots = lots;
        }
                
        public string DestinationCurrency { get; }
        public string SourceCurrency { get; }
        public decimal Price { get; }
        public float Lots { get; }
    }
}