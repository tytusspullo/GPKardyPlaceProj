namespace GPKadryPlace.Utils
{
    public class ConfigurationReader
    {
        public string ReturnConnection()
        {
            string password;
            password = Environment.GetEnvironmentVariable("GP_PASS");
            if (password == null)
            {
                password = string.Empty;
            }
            return "Server=GPPC\\SQL2022_2;Database=employee_schedule;Encrypt=false;TrustServerCertificate=False;User Id=sa;Password=" + password + ";";
        }
    }
}
