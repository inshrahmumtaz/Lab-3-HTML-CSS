using System.Data.SqlClient;
using HomeToHome.Models;

namespace HomeToHome.Services
{
    public class UserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User?> AuthenticateUser(string email, string password)
        {
            Console.WriteLine($"Authenticating user with email: {email}");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM Users WHERE LOWER(Email) = LOWER(@Email) AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@Password", password); // ⚠️ Use hashed password check in production

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                City = reader["City"].ToString(),
                                // You can map more fields as needed
                            };
                            Console.WriteLine($"User found: {user.Email}");
                            return user;
                        }
                    }
                }
            }
            Console.WriteLine($"No user found for email: {email}");
            return null;
        }

        public async Task RegisterUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            Console.WriteLine($"Registering user with email: {user.Email}");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                INSERT INTO Users 
                (FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, City, FullAddress, CNIC, Password)
                VALUES 
                (@FirstName, @LastName, @Email, @PhoneNumber, @DateOfBirth, @Gender, @City, @FullAddress, @CNIC, @Password)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@LastName", user.LastName?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@Email", user.Email?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Gender", user.Gender?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@City", user.City?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@FullAddress", user.FullAddress?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@CNIC", user.CNIC?.Trim() ?? string.Empty);
                    command.Parameters.AddWithValue("@Password", user.Password?.Trim() ?? string.Empty); // Should be hashed in production

                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine($"User registered successfully: {user.Email}");
                }
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            Console.WriteLine($"Fetching user by email: {email}");
            using SqlConnection conn = new(_connectionString);
            await conn.OpenAsync();

            string query = @"
            SELECT FirstName, LastName, Email, PhoneNumber, DateOfBirth, Gender, City, FullAddress, CNIC
            FROM Users
            WHERE LOWER(Email) = LOWER(@Email)";

            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Email", email?.Trim() ?? string.Empty);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var user = new User
                {
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth")) ? null : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    Gender = reader["Gender"].ToString(),
                    City = reader["City"].ToString(),
                    FullAddress = reader["FullAddress"].ToString(),
                    CNIC = reader["CNIC"].ToString()
                };
                Console.WriteLine($"User retrieved: {user.Email}");
                return user;
            }

            Console.WriteLine($"No user found for email: {email}");
            return null;
        }
    }
}
