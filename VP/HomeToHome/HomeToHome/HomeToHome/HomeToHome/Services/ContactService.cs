using HomeToHome.Models;
using System.Data.SqlClient;



namespace HomeToHome.Services
{
    public class ContactService
    {
        private readonly string _connectionString;

        public ContactService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task SaveContactAsync(Contact contact)
        {
            try
            {
                var query = @"INSERT INTO Contacts (Name, Email, Message) 
                     VALUES (@Name, @Email, @Message)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", contact.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", contact.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Message", contact.Message ?? (object)DBNull.Value);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error saving contact: {ex.Message}");
                throw;
            }
        }
    }
}







