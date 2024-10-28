using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace dvdrental.Repository
{
    public class AdminRepository :IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "INSERT INTO Admins (AdminId, Email, Password) OUTPUT INSERTED.Id VALUES (@AdminId, @Email, @Password)";
                    using (var command = new SqlCommand(query, connection))
                    {
                      
                        admin.AdminId = "ADM-" + Guid.NewGuid().ToString().Substring(0, 8);

                    
                        command.Parameters.AddWithValue("@AdminId", admin.AdminId);
                        command.Parameters.AddWithValue("@Email", admin.Email);
                        command.Parameters.AddWithValue("@Password", admin.Password);

                       
                        admin.Id = (int)(await command.ExecuteScalarAsync());
                    }
                }
                return admin;
            }
            catch (SqlException sqlEx)
            {
                
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
           
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdmin()
        {
            try
            {
                var admins =new List<Admin>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = connection.CreateCommand();
                   
                    query.CommandText = @"Select * from Admins";

                    var reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                       var obj = new Admin
                       {
                           AdminId= reader.GetString(1),
                           Email= reader.GetString(2),
                           Password= reader.GetString(3)
                       };

                        admins.Add(obj);
                    }

                    return admins;
                    
                }
            }
            catch (SqlException sqlEx)
            {

                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

    }
}
