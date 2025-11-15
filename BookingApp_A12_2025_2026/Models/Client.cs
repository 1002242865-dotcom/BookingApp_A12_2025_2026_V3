using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class Client
    {
        public int Client_Id { get; set; }
        public string Client_Username { get; set; }
        public string Client_Password { get; set; }

        public DateTime Client_Birthdate { get; set; }

        public string Client_Name { get; set; }

        public string Client_Photo { get; set; }



        //1 get client by username and password
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
                    Client_Username = result["Client_Username"].ToString(),
                    Client_Password = result["Client_Password"].ToString(),
                    Client_Id = int.Parse(result["Client_Id"].ToString()),
                    Client_Name = result["Client_Name"].ToString(),
                    Client_Photo = result["Client_Photo"].ToString(),
                    Client_Birthdate = DateTime.Parse(result["Client_Birthdate"].ToString())
                };
                return c1;
            }
            return null;
        }

        //2 get all clients from db
        public static List<Client> GetAllClients()
        {
            List<Client> clients = new List<Client>();
            string sql = "select * from Clients";
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return clients;
            while (result.Read())
            {
                Client c1 = new Client
                {
                    Client_Username = result["Client_Username"].ToString(),
                    Client_Password = result["Client_Password"].ToString(),
                    Client_Id = int.Parse(result["Client_Id"].ToString()),
                    Client_Name = result["Client_Name"].ToString(),
                    Client_Photo = result["Client_Photo"].ToString(),
                    Client_Birthdate = DateTime.Parse(result["Client_Birthdate"].ToString())
                };
                clients.Add(c1);
            }
            return clients;
        }

        //3 add new client to db
        public static int AddClient(Client newClient)
        {
            string sql = $"insert into Clients (Client_Username, Client_Password, Client_Birthdate, Client_Name, Client_Photo) values ('{newClient.Client_Username}', '{newClient.Client_Password}', '{newClient.Client_Birthdate}', '{newClient.Client_Name}', '{newClient.Client_Photo}')";
            Connector cn = new Connector(Configs.DataBaseLocation);
            int result = cn.RunUpdateInsertDelete(sql);
            return result;
        }

        //4 update client in db
        public static int UpdateClient(Client updatedClient)
        {
            string sql = $"update Clients set Client_Username='{updatedClient.Client_Username}', Client_Password='{updatedClient.Client_Password}', Client_Birthdate='{updatedClient.Client_Birthdate}', Client_Name='{updatedClient.Client_Name}', Client_Photo='{updatedClient.Client_Photo}' where Client_Id={updatedClient.Client_Id}";
            Connector cn = new Connector(Configs.DataBaseLocation);
            int result = cn.RunUpdateInsertDelete(sql);
            return result;


        }

        //5 delete client from db
        public static int DeleteClient(int Client_Id)
        {
            string sql = $"delete from Clients where Client_Id={Client_Id}";
            Connector cn = new Connector(Configs.DataBaseLocation);
            int result = cn.RunUpdateInsertDelete(sql);
            return result;
        }

        //6 get client by id
        public static Client GetClientById(int Client_Id)
        {
            string sql = $"select * from Clients where Client_Id={Client_Id}";
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            if (result.Read())
            {
                Client c1 = new Client
                {
                    Client_Username = result["Client_Username"].ToString(),
                    Client_Password = result["Client_Password"].ToString(),
                    Client_Id = int.Parse(result["Client_Id"].ToString()),
                    Client_Name = result["Client_Name"].ToString(),
                    Client_Photo = result["Client_Photo"].ToString(),
                    Client_Birthdate = DateTime.Parse(result["Client_Birthdate"].ToString())
                };
                return c1;
            }
            return null;
        }

    }
}
