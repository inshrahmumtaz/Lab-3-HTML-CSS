using System.Data.SqlClient;

using HomeToHome.Models;

namespace HomeToHome.Services
{
    public class WorkerService
    {
        private readonly string _connectionString;

        public WorkerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Worker?> AuthenticateWorker(string email, string password)
        {
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT * FROM Workers WHERE Email = @Email AND Password = @Password";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password); // Hash match recommended in production

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Worker
                {
                    FirstName = reader["FirstName"].ToString(),
                    Email = reader["Email"].ToString()
                    // Add other non-sensitive fields if needed
                };
            }

            return null;
        }

        public async Task<bool> RegisterWorker(Worker worker)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                string query = @"
        INSERT INTO Workers 
        (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, City, FullAddress, Designation, 
         Experience, PreferredWorkingHours, CNIC, Password, Skills)
        VALUES 
        (@FirstName, @LastName, @Email, @PhoneNumber, @DateOfBirth, @Gender, @City, @FullAddress, @Designation,
         @Experience, @PreferredWorkingHours, @CNIC, @Password, @Skills);";

                using SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FirstName", worker.FirstName);
                cmd.Parameters.AddWithValue("@LastName", worker.LastName);
                cmd.Parameters.AddWithValue("@Email", worker.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", worker.PhoneNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", worker.DateOfBirth.Value);
                cmd.Parameters.AddWithValue("@Gender", worker.Gender);
                cmd.Parameters.AddWithValue("@City", worker.City);
                cmd.Parameters.AddWithValue("@FullAddress", worker.FullAddress);
                cmd.Parameters.AddWithValue("@Designation", worker.Designation);
                cmd.Parameters.AddWithValue("@Experience", (object?)worker.Experience ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PreferredWorkingHours", worker.PreferredWorkingHours);
                cmd.Parameters.AddWithValue("@CNIC", worker.CNIC);
                cmd.Parameters.AddWithValue("@Password", worker.Password); // Preferably hash before storing
                cmd.Parameters.AddWithValue("@Skills", string.Join(", ", worker.Skills));

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving worker: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Worker>> SearchWorkersBySkill(string skill)
        {
            List<Worker> workers = new();

            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = @"
    SELECT FirstName, LastName, City, Designation, Experience, Skills, Email
    FROM Workers
    WHERE Skills LIKE @Skill";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Skill", "%" + skill + "%");

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Worker worker = new()
                {
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    City = reader["City"].ToString(),
                    Designation = reader["Designation"].ToString(),
                    Experience = reader["Experience"] as int?,
                    Skills = reader["Skills"].ToString()?.Split(",").Select(s => s.Trim()).ToList(),
                    Email = reader["Email"].ToString()
                };
                workers.Add(worker);
            }

            return workers;
        }
    }


}

