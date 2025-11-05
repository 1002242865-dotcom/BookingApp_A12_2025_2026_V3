using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class Hotel
    {
        public int Hotel_Id { get; set; }

        public int City_Id { get; set; }

        public string Hotel_Name { get; set; }

        public int Hotel_Stars { get; set; }

        public string Hotel_Photo { get; set; }

        public string Hotel_Video { get; set; }


        public string Hotel_Description { get; set; }


        public double Hotel_Lat { get; set; }

        public double Hotel_Lng { get; set; }

        public string Hotel_Phone { get; set; }

        public string Hotel_Username { get; set; }

        public string Hotel_Password { get; set; }

        public int Hotel_Status { get; set; }

        public static List<Hotel> GetAllHotelsFromDB()
        {
            string sql = "select * from Hotels";
            List<Hotel> hotels = new List<Hotel>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                Hotel h1 = new Hotel
                {
                    Hotel_Id = int.Parse(result["Hotel_Id"].ToString()),
                    City_Id = int.Parse(result["City_Id"].ToString()),
                    Hotel_Name = result["Hotel_Name"].ToString(),
                    Hotel_Stars = int.Parse(result["Hotel_Stars"].ToString()),
                    Hotel_Photo = result["Hotel_Photo"].ToString(),
                    Hotel_Video = result["Hotel_Video"].ToString(),
                    Hotel_Description = result["Hotel_Description"].ToString(),
                    Hotel_Lat = double.Parse(result["Hotel_Lat"].ToString()),
                    Hotel_Lng = double.Parse(result["Hotel_Lng"].ToString()),
                    Hotel_Phone = result["Hotel_Phone"].ToString(),
                    Hotel_Username = result["Hotel_Username"].ToString(),
                    Hotel_Password = result["Hotel_Password"].ToString(),
                    Hotel_Status = int.Parse(result["Hotel_Status"].ToString())
                };
                hotels.Add(h1);
            }
            cn.CloseConnection();
            return hotels;
        }

        public static List<Hotel> GetHotelsByCityId(int City_Id)
        {
            string sql = "select * from Hotels where City_Id=" + City_Id;
            List<Hotel> hotels = new List<Hotel>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                Hotel h1 = new Hotel
                {
                    Hotel_Id = int.Parse(result["Hotel_Id"].ToString()),
                    City_Id = int.Parse(result["City_Id"].ToString()),
                    Hotel_Name = result["Hotel_Name"].ToString(),
                    Hotel_Stars = int.Parse(result["Hotel_Stars"].ToString()),
                    Hotel_Photo = result["Hotel_Photo"].ToString(),
                    Hotel_Video = result["Hotel_Video"].ToString(),
                    Hotel_Description = result["Hotel_Description"].ToString(),
                    Hotel_Lat = double.Parse(result["Hotel_Lat"].ToString()),
                    Hotel_Lng = double.Parse(result["Hotel_Lng"].ToString()),
                    Hotel_Phone = result["Hotel_Phone"].ToString(),
                    Hotel_Username = result["Hotel_Username"].ToString(),
                    Hotel_Password = result["Hotel_Password"].ToString(),
                    Hotel_Status = int.Parse(result["Hotel_Status"].ToString())
                };
                hotels.Add(h1);
            }
            cn.CloseConnection();
            return hotels;
        }


        public static int AddNewHotel(Hotel h1)
        {
            string sql = $"insert into Hotels (City_Id, Hotel_Name, Hotel_Stars, Hotel_Photo, Hotel_Video, Hotel_Description, Hotel_Lat, Hotel_Lng, Hotel_Phone, Hotel_Username, Hotel_Password, Hotel_Status) " +
                $"values ({h1.City_Id}, '{h1.Hotel_Name}', {h1.Hotel_Stars}, '{h1.Hotel_Photo}', '{h1.Hotel_Video}', '{h1.Hotel_Description}', {h1.Hotel_Lat}, {h1.Hotel_Lng}, '{h1.Hotel_Phone}', '{h1.Hotel_Username}', '{h1.Hotel_Password}', {h1.Hotel_Status})";
            Connector cn = new Connector(Configs.DataBaseLocation);
            int rowsAffected = cn.RunUpdateInsertDelete(sql);
            cn.CloseConnection();
            return rowsAffected;
        }


        public static int DeleteHotelById(int Hotel_Id)
        {
            string sql = "delete from Hotels where Hotel_Id=" + Hotel_Id;
            Connector cn = new Connector(Configs.DataBaseLocation);
            int x = cn.RunUpdateInsertDelete(sql);
            cn.CloseConnection();
            return x;
        }

        public static int UpdateHotel(Hotel h1)
        {
            string sql = $"update Hotels set City_Id={h1.City_Id}, Hotel_Name='{h1.Hotel_Name}', Hotel_Stars={h1.Hotel_Stars}, Hotel_Photo='{h1.Hotel_Photo}', " +
                $"Hotel_Video='{h1.Hotel_Video}', Hotel_Description='{h1.Hotel_Description}', Hotel_Lat={h1.Hotel_Lat}, Hotel_Lng={h1.Hotel_Lng}, " +
                $"Hotel_Phone='{h1.Hotel_Phone}' " +
                $"where Hotel_Id={h1.Hotel_Id}";
            Connector cn = new Connector(Configs.DataBaseLocation);
            int rowsAffected = cn.RunUpdateInsertDelete(sql);
            cn.CloseConnection();
            return rowsAffected;
        }

        public static Hotel GetHotelById(int Hotel_Id)
        {
            string sql = "select * from Hotels where Hotel_Id=" + Hotel_Id;
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            Hotel h1 = null;
            if (result.Read())
            {
                h1 = new Hotel
                {
                    Hotel_Id = int.Parse(result["Hotel_Id"].ToString()),
                    City_Id = int.Parse(result["City_Id"].ToString()),
                    Hotel_Name = result["Hotel_Name"].ToString(),
                    Hotel_Stars = int.Parse(result["Hotel_Stars"].ToString()),
                    Hotel_Photo = result["Hotel_Photo"].ToString(),
                    Hotel_Video = result["Hotel_Video"].ToString(),
                    Hotel_Description = result["Hotel_Description"].ToString(),
                    Hotel_Lat = double.Parse(result["Hotel_Lat"].ToString()),
                    Hotel_Lng = double.Parse(result["Hotel_Lng"].ToString()),
                    Hotel_Phone = result["Hotel_Phone"].ToString(),
                    Hotel_Username = result["Hotel_Username"].ToString(),
                    Hotel_Password = result["Hotel_Password"].ToString(),
                    Hotel_Status = int.Parse(result["Hotel_Status"].ToString())
                };
            }
            cn.CloseConnection();
            return h1;
        }

        public static List<Hotel> SearchByHotelNameAndStars(string Hotel_Name, int Hotel_Stars)
        {
            string sql = $"select * from Hotels where Hotel_Name Like '%{Hotel_Name}%' and Hotel_Stars={Hotel_Stars}";
            List<Hotel> hotels = new List<Hotel>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                Hotel h1 = new Hotel
                {
                    Hotel_Id = int.Parse(result["Hotel_Id"].ToString()),
                    City_Id = int.Parse(result["City_Id"].ToString()),
                    Hotel_Name = result["Hotel_Name"].ToString(),
                    Hotel_Stars = int.Parse(result["Hotel_Stars"].ToString()),
                    Hotel_Photo = result["Hotel_Photo"].ToString(),
                    Hotel_Video = result["Hotel_Video"].ToString(),
                    Hotel_Description = result["Hotel_Description"].ToString(),
                    Hotel_Lat = double.Parse(result["Hotel_Lat"].ToString()),
                    Hotel_Lng = double.Parse(result["Hotel_Lng"].ToString()),
                    Hotel_Phone = result["Hotel_Phone"].ToString(),
                    Hotel_Username = result["Hotel_Username"].ToString(),
                    Hotel_Password = result["Hotel_Password"].ToString(),
                    Hotel_Status = int.Parse(result["Hotel_Status"].ToString())
                };
                hotels.Add(h1);
            }
            cn.CloseConnection();
            return hotels;
        }


        public static Hotel GetHotelByUsernameAndPassword(string username, string password)
        {
            string sql = $"select * from Hotels where Hotel_Username='{username}' and Hotel_Password='{password}'";
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            Hotel h1 = null;
            if (result.Read())
            {
                h1 = new Hotel
                {
                    Hotel_Id = int.Parse(result["Hotel_Id"].ToString()),
                    City_Id = int.Parse(result["City_Id"].ToString()),
                    Hotel_Name = result["Hotel_Name"].ToString(),
                    Hotel_Stars = int.Parse(result["Hotel_Stars"].ToString()),
                    Hotel_Photo = result["Hotel_Photo"].ToString(),
                    Hotel_Video = result["Hotel_Video"].ToString(),
                    Hotel_Description = result["Hotel_Description"].ToString(),
                    Hotel_Lat = double.Parse(result["Hotel_Lat"].ToString()),
                    Hotel_Lng = double.Parse(result["Hotel_Lng"].ToString()),
                    Hotel_Phone = result["Hotel_Phone"].ToString(),
                    Hotel_Username = result["Hotel_Username"].ToString(),
                    Hotel_Password = result["Hotel_Password"].ToString(),
                    Hotel_Status = int.Parse(result["Hotel_Status"].ToString())
                };
            }
            cn.CloseConnection();
            return h1;
        }
    }
}
