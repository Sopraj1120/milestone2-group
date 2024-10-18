using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dvdrental.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"INSERT INTO Customers (FirstName, LastName, Address, Email, Password, MobileNo, NicNo, UserName, IsActive) 
                                  OUTPUT INSERTED.Id 
                                  VALUES (@FirstName, @LastName, @Address, @Email, @Password, @MobileNo, @NicNo, @UserName, 0)";

                    using (var command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@Password", customer.Password);
                        command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        command.Parameters.AddWithValue("@NicNo", customer.NicNo);
                        command.Parameters.AddWithValue("@UserName", customer.UserName);


                        customer.Id = (int)await command.ExecuteScalarAsync();
                    }
                }
                return customer;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM Customers";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new Customer
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNo = reader.GetString(reader.GetOrdinal("MobileNo")),
                                    NicNo = reader.GetString(reader.GetOrdinal("NicNo")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM Customers WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                customer = new Customer
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNo = reader.GetString(reader.GetOrdinal("MobileNo")),
                                    NicNo = reader.GetString(reader.GetOrdinal("NicNo")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                };
                            }
                        }
                    }
                }
                return customer;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"UPDATE Customers SET 
                                  FirstName = @FirstName, 
                                  LastName = @LastName, 
                                  Address = @Address, 
                                  Email = @Email, 
                                  Password = @Password, 
                                  MobileNo = @MobileNo, 
                                  NicNo = @NicNo, 
                                  UserName = @UserName, 
                                  IsActive = @IsActive 
                                  WHERE Id = @Id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@Password", customer.Password);
                        command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        command.Parameters.AddWithValue("@NicNo", customer.NicNo);
                        command.Parameters.AddWithValue("@UserName", customer.UserName);
                        command.Parameters.AddWithValue("@IsActive", customer.IsActive);
                        command.Parameters.AddWithValue("@Id", customer.Id);

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "DELETE FROM Customers WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
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
