 using System.Data.SqlClient;
using HomeToHome.Models;

namespace HomeToHome.Services
{
    public class FeedbackService
    {
        private readonly string _connectionString;


        public FeedbackService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"INSERT INTO Feedbacks (Name, Email, Message, SubmittedAt) 
                             VALUES (@Name, @Email, @Message, @SubmittedAt)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", feedback.Name);
                    command.Parameters.AddWithValue("@Email", feedback.Email);
                    command.Parameters.AddWithValue("@Message", feedback.Message);
                    command.Parameters.AddWithValue("@SubmittedAt", feedback.SubmittedAt);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}



