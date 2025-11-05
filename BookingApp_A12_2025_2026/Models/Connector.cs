using System.Data;
using System.Data.OleDb;



namespace BookingApp_A12_2025_2026
{
    public class Connector
    {
        private OleDbConnection aConnection;

        private OleDbCommand aCommand;
        private OleDbDataAdapter adapter;
        private DataSet ds;

        private string connect_string;
        private string the_exception;

        public Connector(string Db_Name)
        {
            //string dbPath = Path.Combine(env.ContentRootPath, "App_Data", "yourfile.accdb");
            this.connect_string = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Db_Name;
            aConnection = new OleDbConnection(connect_string);


        }

        public string GetError()
        {
            return the_exception;
        }

        public OleDbDataReader RunSelect(string sql)
        {
            OleDbDataReader aReader = null;
            aCommand = new OleDbCommand(sql, aConnection);
            try
            {
                aConnection.Open();
                aReader = aCommand.ExecuteReader();

            }
            catch (Exception err)
            {
                the_exception = err.Message;
            }

            return aReader;
        }

        public int RunUpdateInsertDelete(string sql)
        {
            int r = -1;
            aCommand = new OleDbCommand(sql, aConnection);
            try
            {
                aConnection.Open();
                r = aCommand.ExecuteNonQuery();
                aConnection.Close();
            }
            catch (Exception err)
            {
                the_exception = err.Message;
            }
            return r;

        }

        public int RunScalar(string sql)
        {
            int r = 0;
            aCommand = new OleDbCommand(sql, aConnection);
            try
            {
                aConnection.Open();
                r = Convert.ToInt16(aCommand.ExecuteScalar());
                //r = (int)(aCommand.ExecuteScalar());
                aConnection.Close();
            }
            catch (Exception err)
            {
                the_exception = err.Message;
            }
            return r;

        }

        public void CloseConnection()
        {
            aConnection.Close();
        }


        public DataSet GetDataSet(string sql, string table_name)
        {
            ds = new DataSet();
            try
            {
                aConnection.Open();
                adapter = new OleDbDataAdapter(sql, aConnection);
                adapter.Fill(ds, table_name);

            }
            catch (Exception err)
            {
                the_exception = err.Message;
            }
            return ds;
        }

    }
}