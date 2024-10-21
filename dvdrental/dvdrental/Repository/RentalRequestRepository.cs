using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;

public class RentalRequestRepository : IRentalRequestRepository
{
    private readonly string _connectionString;

    public RentalRequestRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<RentalRequest> AddRentalRequest(RentalRequest rentalRequest)
    {
        const string query = @"
            INSERT INTO RentalRequests (MovieId, CustomerId, RentDate, ReturnDate, Status, MoviesAvailableCopies)
            VALUES (@MovieId, @CustomerId, @RentDate, @ReturnDate, @Status, @MoviesAvailableCopies);
            SELECT SCOPE_IDENTITY();";

        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MovieId", rentalRequest.MovieId);
                    command.Parameters.AddWithValue("@CustomerId", rentalRequest.CustomerId);
                    command.Parameters.AddWithValue("@RentDate", rentalRequest.RentDate);
                    command.Parameters.AddWithValue("@ReturnDate", rentalRequest.ReturnDate);
                    command.Parameters.AddWithValue("@Status", rentalRequest.Status.ToString()); // Use ToString for enum
                    command.Parameters.AddWithValue("@MoviesAvailableCopies", rentalRequest.MovieAvailableCopies);

                    var id = await command.ExecuteScalarAsync();
                    rentalRequest.Id = Convert.ToInt32(id);
                    return rentalRequest;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding rental request", ex);
        }
    }

    public async Task<IEnumerable<RentalRequest>> GetAllRentalRequests()
    {
        const string query = "SELECT * FROM RentalRequests";
        var rentalRequests = new List<RentalRequest>();

        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) // Use ReadAsync for better performance
                        {
                            rentalRequests.Add(new RentalRequest
                            {
                                Id = reader.GetInt32(0),
                                MovieId = reader.GetInt32(1),
                                CustomerId = reader.GetInt32(2),
                                RentDate = reader.GetString(3),
                                ReturnDate = reader.GetString(4),
                                Status = Enum.Parse<RentalRequest.RentalStatus>(reader.GetString(5)), // Parse status to enum
                                MovieAvailableCopies = reader.GetInt32(6),
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving rental requests", ex);
        }

        return rentalRequests;
    }
}
