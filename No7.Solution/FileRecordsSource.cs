using System;
using System.Collections.Generic;
using System.IO;

namespace No7.Solution
{
    // Конкретная реализация "считывающего" класса, подразумевающая чтение из текстового файла.
    public class FileRecordsSource : IRecordsSource
    {
        private readonly string sourcePath;
        private readonly ISimpleLogger logger;
        
        public FileRecordsSource(string sourcePath, ISimpleLogger logger)
        {
            ThrowForInvalidParammeters();
            
            this.sourcePath = sourcePath;
            this.logger = logger;

            void ThrowForInvalidParammeters()
            {
                if (sourcePath == null)
                {
                    throw new ArgumentNullException(nameof(sourcePath));
                }
                
                if (!File.Exists(sourcePath))
                {
                    throw new ArgumentException("No such source file.");
                }

                if (logger == null)
                {
                    throw new ArgumentNullException(nameof(logger));
                }
            }
        }
        
        public IEnumerable<Record> ReadValidRecords(IRecordValidator validator, IRecordFactory recordFactory)
        {
            ThrowForInvalidParameters();
            
            int lineNumber = 0;            
            using (var sr = new StreamReader(sourcePath))
            {
                var recordLine = sr.ReadLine();
                if (recordLine == null)
                {
                    yield break;
                }

                Record record = null;
                try
                {
                    record = ParseAndCreateRecord(recordLine.Split(), recordFactory, validator);
                }
                catch (ArgumentException e)
                {
                    string logMessage = $"{e.Message}. Invalid record was on line #{lineNumber}";

                    logger.Warning(logMessage);
                }

                if (record != null)
                {
                    yield return record;
                }
                
                lineNumber++;
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
                throw new ArgumentException("Not valid amount of parts.");
            }
                    
            var sourceCurrency = parts[0].Substring(0, 3);
            var destinationCurrency = parts[0].Substring(0, 3);

            return recordFactory.CreateNewRecord(destinationCurrency, sourceCurrency, parts[2], parts[1], validator);
        }
    }
}