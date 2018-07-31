namespace No7.Solution
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    // Конкретная реализация "записывающего" класса, подразумевающая запись в базу данных.
    public class DatabaseRecordDestination : IRecordDestination
    {
        private readonly string connectionString;

        public DatabaseRecordDestination(string source)
        {
            this.connectionString = source ?? throw new ArgumentNullException(nameof(source));
        }

        public void WriteRecords(IEnumerable<Record> records)
        {
            int recordCount = 0;
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in records)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.Insert_Trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                        recordCount++;
                    }

                    transaction.Commit();
                }

                connection.Close();
            }

            LoggerService.Info($"{recordCount} trades processed");
        }
    }
}