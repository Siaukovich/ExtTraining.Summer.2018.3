using System;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    // Конкретная реализация "считывающего" класса, подразумевающая чтение из текстового файла.
    public class FileRecordsSource : IRecordsSource
    {
        private readonly string sourcePath;
        
        public FileRecordsSource(string sourcePath)
        {
            ThrowForInvalidParammeters();
            
            this.sourcePath = sourcePath;

            void ThrowForInvalidParammeters()
            {
                if (sourcePath == null)
                {
                    throw new ArgumentNullException(nameof(sourcePath));
                }
                
                if (!File.Exists(sourcePath))
                {
                    throw new ArgumentException("No such source file");
                }
            }
        }
        
        public IEnumerable<Record> ReadValidRecords(IRecordValidator validator, IRecordFactory recordFactory)
        {
            ThrowForInvalidParameters();
            
            int lineNumber = 0;            
            using (var sr = new StreamReader(sourcePath))
            {
                while (true)
                {
                    var recordLine = sr.ReadLine();
                    if (recordLine == null)
                    {
                        yield break;
                    }

                    Record record = null;
                    try
                    {
                        record = ParseAndCreateRecord(recordLine.Split(",".ToCharArray()), recordFactory, validator);
                    }
                    catch (ArgumentException e)
                    {
                        string logMessage = $"{e.Message}. Invalid record was on line #{lineNumber}";

                        LoggerService.Instance.Warning(logMessage);
                    }

                    if (record != null)
                    {
                        yield return record;
                    }

                    lineNumber++;
                }
            }

            void ThrowForInvalidParameters()
            {
                if (validator == null)
                {
                    throw new ArgumentNullException(nameof(validator));
                }

                if (recordFactory == null)
                {
                    throw new ArgumentNullException(nameof(recordFactory));
                }
            }
        }

        private Record ParseAndCreateRecord(string[] parts, IRecordFactory recordFactory, IRecordValidator validator)
        {
            if (parts.Length != 3)
            {
                throw new ArgumentException("Not valid amount of parts");
            }

            if (parts[0].Length != 6)
            {
                throw new ArgumentException("Not valid currency codes");
            }
                    
            var sourceCurrency = parts[0].Substring(0, 3);
            var destinationCurrency = parts[0].Substring(3, 3);

            return recordFactory.CreateNewRecord(destinationCurrency, sourceCurrency, parts[2], parts[1], validator);
        }
    }
}
