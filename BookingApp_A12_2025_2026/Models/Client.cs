using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class Client
    {
        public int Client_Id { get; set; }
        public string Client_Username { get; set; }
        public string Client_Password { get; set; }

        public static Client GetClientByUsernameAndPassword(string Client_Username, string Client_Password)
        {
            string sql = $"select * from Clients where Client_Username='{Client_Username}' and Client_Password='{Client_Password}'";
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            if (result.Read())
            {
                Client c1 = new Client
                {
                    Client_Username = (result["Client_Username"].ToString()),
                    Client_Password = result["Client_Password"].ToString(),
                    Client_Id = int.Parse(result["Client_Id"].ToString())
                };
                return c1;
            }
            return null;
        }
    }
}
