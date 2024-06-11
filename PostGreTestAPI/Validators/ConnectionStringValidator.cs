using Npgsql;

namespace PostGreTestAPI.Validators
{
    public static class ConnectionStringValidator
    {
        public static bool ValidateConnectionString(string connectionString)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }
    }
}
