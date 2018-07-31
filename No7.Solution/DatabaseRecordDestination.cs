using System.Collections.Generic;

namespace No7.Solution
{
    // Конкретная реализация "записывающего" класса, подразумевающая запись в базу данных.
    public class DatabaseRecordDestination : IRecordDestination
    {
        public void WriteRecords(IEnumerable<Record> records)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(var transaction = connection.BeginTransaction())
                {
                    foreach(var trade in records)
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
                    }

                    transaction.Commit();
                }
                connection.Close();
            }   
        }
    }
}