using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class City
    {
        //رقم تسلسلي تلقائي
        public int City_Id { get; set; }
        //اسم المدينة
        public string City_Name { get; set; }
        //مثلا القدس: lat: 31.7683, lng: 35.2137

        public double City_Lat { get; set; }

        public double City_Lng { get; set; }

        //عنوان صورة للمدينة
        public string City_Photo { get; set; }
        //عنوان فيديو يوتيوب..يكفي قيمة v
        public string City_Video { get; set; }
        //هل المدينة امنة
        public bool City_IsSafe { get; set; }
        //شرح حول المدينة
        public string City_Description { get; set; }

        public static int GetTotalCities()
        {
            string sql = "select اسم_الدالة_التجميعية(* او اسم العمود) from اسم_الجدول";
            //ارتبط بقاعدة البيانات واحضر العدد الكلي للمدن
            Connector cn = new Connector(Configs.DataBaseLocation);
            int total = cn.RunScalar("SELECT COUNT(*) from Cities");
            return total;


        }


        public static List<City> GetAllCitiesFromDB()
        {

            string sql = "select * from Cities";
            List<City> cities = new List<City>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                City c1 = new City
                {
                    City_Id = int.Parse(result["City_Id"].ToString())
                    ,
                    City_Name = result["City_Name"].ToString()
                    ,
                    City_IsSafe = bool.Parse(result["City_IsSafe"].ToString())
                    ,
                    City_Description = result["City_Description"].ToString()
                    ,
                    City_Lat = double.Parse(result["City_Lat"].ToString())
                    ,
                    City_Lng = double.Parse(result["City_Lng"].ToString())
                    ,
                    City_Photo = result["City_Photo"].ToString()
                    ,
                    City_Video = result["City_Video"].ToString()
                };
                cities.Add(c1);
            }
            cn.CloseConnection();
            return cities;
        }



        public static int DeleteCityById(int City_Id)
        {
            string sql = "delete from Cities where City_Id=" + City_Id;
            Connector cn = new Connector(Configs.DataBaseLocation);
            int x = cn.RunUpdateInsertDelete(sql);
            return x;

        }



        public static City GetCityById(int City_Id)
        {
            City ct = null;
            string sql = "select * from Cities where City_Id=" + City_Id;
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
            {
                return null;
            }
            while (result.Read())
            {
                ct = new City
                {
                    City_Id = int.Parse(result["city_Id"].ToString())
                    ,
                    City_Name = result["City_Name"].ToString()
                    ,
                    City_Photo = result["City_Photo"].ToString()
                    ,
                    City_Video = result["City_Video"].ToString()
                    ,
                    City_Description = result["City_Description"].ToString()
                    ,
                    City_Lat = double.Parse(result["City_Lat"].ToString())
                    ,
                    City_Lng = double.Parse(result["City_Lng"].ToString())
                    ,
                    City_IsSafe = bool.Parse(result["City_IsSafe"].ToString())
                };
            }
            return ct;
        }

        public static int AddCityToDB(City ct)
        {
            string sql = "INSERT INTO Cities (City_Name,City_Lat,City_Lng,City_Photo,City_Video,";
            sql += "City_IsSafe,City_Description) ";
            sql += " VALUES (";
            sql += "'" + ct.City_Name + "'";
            sql += ",'" + ct.City_Lat + "'";
            sql += ",'" + ct.City_Lng + "'";
            sql += ",'" + ct.City_Photo + "'";
            sql += ",'" + ct.City_Video + "'";
            sql += "," + ct.City_IsSafe + "";
            sql += ",'" + ct.City_Description + "'";
            sql += ")";

            Connector cn = new Connector(Configs.DataBaseLocation);
            int x = cn.RunUpdateInsertDelete(sql);
            return x;
        }


        public static List<City> SearchCitiesByName(string name)
        {

            string sql = "select * from Cities WHERE City_Name LIKE %%";
            List<City> cities = new List<City>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                City c1 = new City
                {
                    City_Id = int.Parse(result["City_Id"].ToString())
                    ,
                    City_Name = result["City_Name"].ToString()
                    ,
                    City_IsSafe = bool.Parse(result["City_IsSafe"].ToString())
                    ,
                    City_Description = result["City_Description"].ToString()
                    ,
                    City_Lat = double.Parse(result["City_Lat"].ToString())
                    ,
                    City_Lng = double.Parse(result["City_Lng"].ToString())
                    ,
                    City_Photo = result["City_Photo"].ToString()
                    ,
                    City_Video = result["City_Video"].ToString()
                };
                cities.Add(c1);
            }
            cn.CloseConnection();
            return cities;
        }

        public static int UpdateCityInDB(City ct)
        {
            string sql = "UPDATE Cities SET";
            sql += "City_Name='" + ct.City_Name + "'";
            sql += ",City_Lat=" + ct.City_Lat + "";
            sql += ",City_Lng=" + ct.City_Lng + "";
            sql += ",City_Photo='" + ct.City_Photo + "'";
            sql += ",City_Video='" + ct.City_Video + "'";
            sql += ",City_IsSafe=" + ct.City_IsSafe + "";
            sql += ",City_Description='" + ct.City_Description + "'";
            sql += " WHERE City_Id=" + ct.City_Id;
            Connector cn=new Connector(Configs.DataBaseLocation);
            int x= cn.RunUpdateInsertDelete(sql);



            return x;
        }

    }

}
