namespace BookingApp_A12_2025_2026.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Sora { get; set; }

        public bool IsSport { get; set; }

        public override string ToString()
        {
            return "ID:" + Id + ", Name:" + Name;
        }

        public static List<Student> GetDemoStudents()
        {
            List<Student> students = new List<Student>();
            Student st1 = new Student { Id = 99, IsSport = true, Name = "Ahmad", Sora = "Photos/a1.png" };
            students.Add(st1);

            students.Add(new Student{ Id = 1, Name = "Kareem", Sora = "Photos/a1.png", IsSport=true });
            students.Add(new Student { Id = 2, Name = "Hoda", Sora = "Photos/a2.png", IsSport = true });
            students.Add(new Student { Id = 3, Name = "Hosni", Sora = "Photos/a3.png", IsSport = false });
            students.Add(new Student { Id = 4, Name = "Aya", Sora = "Photos/a4.png", IsSport = true });
            students.Add(new Student { Id = 5, Name = "Montaha", Sora = "Photos/a5.png", IsSport = false });
            students.Add(new Student { Id = 6, Name = "Mohamed", Sora = "Photos/a1.png", IsSport = true });
            students.Add(new Student { Id = 7, Name = "Wa3d", Sora = "Photos/a2.png", IsSport = false });
            students.Add(new Student { Id = 8, Name = "Zohdi", Sora = "Photos/a3.png", IsSport = true });
            students.Add(new Student { Id = 9, Name = "Noor", Sora = "Photos/a4.png", IsSport = true });
            students.Add(new Student { Id = 10, Name = "Fairoz", Sora = "Photos/a5.png", IsSport = false });
            students.Add(new Student { Id = 77, Name = "Tamer", Sora = "Photos/a1.png", IsSport = false });
            students.Add(new Student { Id = 11, Name = "شام", Sora = "Photos/a4.png", IsSport = true });
            return students;
        }


        public static List<Student> SearchByName(string name)
        {
            List<Student> lst1=new List<Student>();
            //دبر من وين تجيب الطلاب مثلا من قاعدة البيانات يعني من ملف نوعه اكسس
            return lst1;
        }

        public static List<Student> GetAllStudents()
        {
            List<Student> lst1 = new List<Student>();
            //دبر من وين تجيب الطلاب مثلا من قاعدة البيانات يعني من ملف نوعه اكسس
            return lst1;
        }


        public static bool AddNewStudent(Student st)
        {
            //how to add the st to DB
            //id is ok return true, else  return false;
            return false;
        }

        public static List<Student> GetTop10()
        {
            return null; //just to calm down relax for now
        }

        //CRUD 
        //Create: Add New Student to DB
        //Retrive: Read or Get Students from DB
        //Update: update the student detaiuls in DB
        //Delete: Remove student from DB


    }
}
