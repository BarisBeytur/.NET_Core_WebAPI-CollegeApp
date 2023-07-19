using System.Text;


namespace CollegeApp.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            var students = DataStorage.GetAllStudents();
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>This is the generated PDF report!!!</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Student Name</th>
                                        <th>Email</th>
                                        <th>Address</th>                                    
                                    </tr>");
            foreach (var student in students)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>                                   
                                  </tr>", student.StudentName, student.Email, student.Address);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }


    }
}
