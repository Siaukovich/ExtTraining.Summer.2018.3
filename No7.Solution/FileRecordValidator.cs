using System;
using System.Globalization;
using System.Linq;

namespace No7.Solution
{
    // Конкретная реализация класса-валидатора.
    public class FileRecordValidator : IRecordValidator
    {
        private static readonly Lazy<FileRecordValidator> LazyValidator = 
            new Lazy<FileRecordValidator>(() => new FileRecordValidator());

        public static FileRecordValidator Instance => LazyValidator.Value;

        private FileRecordValidator()
        {
        }
        
        public void Check(string destinationCurrency, string sourceCurrency, string price, string lots)
        {
            ThrowForNullArguments();

            if (destinationCurrency.Length != 3 || !destinationCurrency.All(char.IsUpper))
            {
                throw new ArgumentException("Invalid dectination currency", nameof(destinationCurrency));
            }
            
            if (sourceCurrency.Length != 3 || !sourceCurrency.All(char.IsUpper))
            {
                throw new ArgumentException("Invalid source currency", nameof(sourceCurrency));
            }

            var numberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            var invariantCulture = CultureInfo.InvariantCulture;
            
            if (!float.TryParse(lots, numberStyle, invariantCulture, out _))
            {
                throw new ArgumentException("Invalid lost value", nameof(lots));
            }
            
            if (!decimal.TryParse(price, numberStyle, invariantCulture, out _))
            {
                throw new ArgumentException("Invalid price", nameof(price));
            }

            void ThrowForNullArguments()
            {
                if (destinationCurrency == null)
                {
                    throw new ArgumentNullException();
                }
                
                if (sourceCurrency == null)
                {
                    throw new ArgumentNullException();
                }
                
                if (price == null)
                {
                    throw new ArgumentNullException();
                }
                
                if (lots == null)
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}