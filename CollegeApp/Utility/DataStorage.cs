using CollegeApp.Models;

namespace CollegeApp.Utility
{
    public class DataStorage
    {


        public static List<Student> GetAllStudents() =>
            new List<Student>
            {
                new Student { StudentName = "Baris Beytur", Email = "beyturbaris1@gmail.com", Address = "Sancaktepe"},
                new Student { StudentName = "Ogrenci 2", Email = "ogrenci2@gmail.com", Address = "Kadıkoy"},
                new Student { StudentName = "Ogrenci 3", Email = "ogrenci3@gmail.com", Address = "Uskudar"},
                new Student { StudentName = "Ogrenci 4", Email = "ogrenci4@gmail.com", Address = "Maltepe"},
                new Student { StudentName = "Ogrenci 5", Email = "ogrenci5@gmail.com", Address = "Küçükçekmece"},
                new Student { StudentName = "Ogrenci 6", Email = "ogrenci6@gmail.com", Address = "Bağcılar"},
                new Student { StudentName = "Ogrenci 7", Email = "ogrenci7@gmail.com", Address = "Sultanbeyli"},

            };



    }
}
