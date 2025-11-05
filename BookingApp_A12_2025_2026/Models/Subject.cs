using System.Data.OleDb;

namespace BookingApp_A12_2025_2026.Models
{
    public class Subject
    {
        public int Subject_Id { get; set; }
        public string Subject_Name { get; set; }

        public static List<Subject> GetAllFromDB()
        {
            string sql = "select * from Subjects";
            List<Subject> subjects = new List<Subject>();
            Connector cn = new Connector(Configs.DataBaseLocation);
            OleDbDataReader result = cn.RunSelect(sql);
            if (result == null)
                return null;
            while (result.Read())
            {
                Subject s1 = new Subject
                {
                    Subject_Id = int.Parse(result["Subject_Id"].ToString()),
                    Subject_Name = result["Subject_Name"].ToString()
                };
                subjects.Add(s1);
            }
            cn.CloseConnection();
            return subjects;
        }
    }
}
