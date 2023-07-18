namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>(){
                new Student
                {
                    Id = 1,
                    StudentName = "Student 1",
                    Email = "studentemail1@gmail.com",
                    Address = "Istanbul, TURKIYE"
                },
                 new Student
                 {
                     Id = 2,
                     StudentName = "Student 2",
                     Email = "studentemail2@gmail.com",
                     Address = "Amasya, TURKIYE"
                 },
                 new Student
                 {
                     Id = 3,
                     StudentName = "Baris",
                     Email = "studentemail3@gmail.com",
                     Address = "Ankara, TURKIYE"
                 }
                };
    }
}
