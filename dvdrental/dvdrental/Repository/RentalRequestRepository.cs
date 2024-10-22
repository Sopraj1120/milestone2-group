using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dvdrental.Repository
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly string _connectionString;

        public RentalRequestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<RentalRequest>> GetAllRental()
        {
            var rentalRequests = new List<RentalRequest>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM RentalRequest WHERE IsDeleted = 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            rentalRequests.Add(new RentalRequest
                            {
                                Id = reader.GetInt32(0),
                                MovieId = reader.GetInt32(1),
                                CustomerId = reader.GetInt32(2),
                                RentDate = reader.GetDateTime(3),
                                ReturnDate = reader.GetDateTime(4),
                                Status = (RentalRequest.RentalStatus)reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            return rentalRequests;
        }

        public async Task<RentalRequest> GetByIdRentalRequest(int id)
        {
            RentalRequest rentalRequest = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM RentalRequest WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            rentalRequest = new RentalRequest
                            {
                                Id = reader.GetInt32(0),
                                MovieId = reader.GetInt32(1),
                                CustomerId = reader.GetInt32(2),
                                RentDate = reader.GetDateTime(3),
                                ReturnDate = reader.GetDateTime(4),
                                Status = (RentalRequest.RentalStatus)reader.GetInt32(5)
                            };
                        }
                    }
                }
            }

            return rentalRequest;
        }

        public async Task<int> AddRentalRequest(RentalRequest rentalRequest)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO RentalRequests (MovieId, CustomerId, RentDate, ReturnDate, Status)
                    VALUES (@MovieId, @CustomerId, @RentDate, @ReturnDate, @Status);
                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MovieId", rentalRequest.MovieId);
                    command.Parameters.AddWithValue("@CustomerId", rentalRequest.CustomerId);
                    command.Parameters.AddWithValue("@RentDate", rentalRequest.RentDate);
                    command.Parameters.AddWithValue("@ReturnDate", rentalRequest.ReturnDate);
                    command.Parameters.AddWithValue("@Status", (int)rentalRequest.Status);

                    connection.Open();
                    var result = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }
        //public async Task<RentalRequest> AddRentalRequest(RentalRequest rentalRequest)
        //{
        //    const string query = @"
        //INSERT INTO RentalRequests (MovieId, CustomerId, RentDate, ReturnDate, Status, MoviesAvailableCopies)
        //VALUES (@MovieId, @CustomerId, @RentDate, @ReturnDate, @Status, @MoviesAvailableCopies);
        //SELECT SCOPE_IDENTITY();";

        //    try
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@MovieId", rentalRequest.MovieId);
        //                command.Parameters.AddWithValue("@CustomerId", rentalRequest.CustomerId);

        //                // Ensure RentDate is not null. If it's null, use the current date as the default.
        //                command.Parameters.AddWithValue("@RentDate", rentalRequest.RentDate);

        //                // Handle null value for ReturnDate (this is allowed)
        //                command.Parameters.AddWithValue("@ReturnDate",rentalRequest.ReturnDate);

        //                command.Parameters.AddWithValue("@Status", rentalRequest.Status.ToString()); // Use ToString for enum
        //                command.Parameters.AddWithValue("@MoviesAvailableCopies", rentalRequest.MovieAvailableCopies);

        //                var id = await command.ExecuteScalarAsync();
        //                rentalRequest.Id = Convert.ToInt32(id);
        //                return rentalRequest;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error adding rental request", ex);
        //    }
        //}


        //public async Task<IEnumerable<RentalRequest>> GetAllRentalRequests()
        //{
        //    const string query = "SELECT * FROM RentalRequests";
        //    var rentalRequests = new List<RentalRequest>();

        //    try
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            await connection.OpenAsync();
        //            using (var command = new SqlCommand(query, connection))
        //            {
        //                using (var reader = await command.ExecuteReaderAsync())
        //                {
        //                    while (await reader.ReadAsync()) // Use ReadAsync for better performance
        //                    {
        //                        rentalRequests.Add(new RentalRequest
        //                        {
        //                            Id = reader.GetInt32(0),
        //                            MovieId = reader.GetInt32(1),
        //                            CustomerId = reader.GetInt32(2),
        //                            RentDate = reader.GetDateTime(3),
        //                            ReturnDate = reader.GetDateTime(4),
        //                            Status = Enum.Parse<RentalRequest.RentalStatus>(reader.GetString(5)), // Parse status to enum
        //                            MovieAvailableCopies = reader.GetInt32(6),
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error retrieving rental requests", ex);
        //    }

        //    return rentalRequests;
        // }
    }
}
