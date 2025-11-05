using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class UserType
    {
        public int UserType_Id { get; set; }
        public string UserType_Name { get; set; }

        public List<UserType> GetAllFromDB()
        {
            string sql = "select * from UserTypes";
            List<UserType> userTypes = new List<UserType>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                UserType ut1 = new UserType
                {
                    UserType_Id = int.Parse(result["UserType_Id"].ToString()),
                    UserType_Name = result["UserType_Name"].ToString()
                };
                userTypes.Add(ut1);
            }
            cn.CloseConnection();
            return userTypes;
        }
    }
}
