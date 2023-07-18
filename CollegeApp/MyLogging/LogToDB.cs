namespace CollegeApp.MyLogging.NewFolder
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("log To DB");
        }
    }
}
