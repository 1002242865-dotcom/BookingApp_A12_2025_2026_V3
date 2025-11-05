using System.Data;
using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Data
{
    public class z_Connector2
    {
        private readonly string _connectionString;
        public string LastError { get; private set; }

        public z_Connector2(string dbFilePath)
        {
            _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbFilePath}";
        }

        public DataTable RunSelect(string sql)
        {
            var table = new DataTable();

            try
            {
                using (var connection = new OleDbConnection(_connectionString))
                {
                    using (var adapter = new OleDbDataAdapter(sql, connection))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString(); // Detailed error for debugging
                // Optional: log to a file or system logger here
            }

            return table;
        }

        public int RunNonQuery(string sql)
        {
            int affectedRows = -1;

            try
            {
                using (var connection = new OleDbConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OleDbCommand(sql, connection))
                    {
                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
                // Optional: log here
            }

            return affectedRows;
        }

        public object RunScalar(string sql)
        {
            object result = null;

            try
            {
                using (var connection = new OleDbConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new OleDbCommand(sql, connection))
                    {
                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
            }

            return result;
        }

        public DataSet GetDataSet(string sql, string tableName)
        {
            var dataSet = new DataSet();

            try
            {
                using (var connection = new OleDbConnection(_connectionString))
                {
                    using (var adapter = new OleDbDataAdapter(sql, connection))
                    {
                        adapter.Fill(dataSet, tableName);
                    }
                }
            }
            catch (Exception ex)
            {
                LastError = ex.ToString();
            }

            return dataSet;
        }
    }
}
