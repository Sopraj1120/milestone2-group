using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using static dvdrental.Repository.RentalRequestRepository;

namespace dvdrental.Repository
{
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
             INSERT INTO RentalRequests (MovieId, CustomerId, RentDate, ReturnDate,Imagepath)
             VALUE(@MovieId, @CustomerId, @RentDate, @ReturnDate,@Imagepath);
             ";


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MovieId", rentalRequest.MovieId);
                    command.Parameters.AddWithValue("@CustomerId", rentalRequest.CustomerId);
                    command.Parameters.AddWithValue("@RentDate", rentalRequest.RentDate);
                    command.Parameters.AddWithValue("@ReturnDate", rentalRequest.ReturnDate);
                    command.Parameters.AddWithValue("@Imagepath", rentalRequest.imagefile);
                    //command.Parameters.AddWithValue("@MovieImageType", rentalRequest.MovieImageType);

                    var id = await command.ExecuteScalarAsync();
                    rentalRequest.Id = Convert.ToInt32(id);
                    return rentalRequest;
                }
            }


        }




        public async Task<bool> AcceptRentalRequest(int id, bool isAccepted)
        {
            return await AcceptRentalRequest(id, isAccepted);
        }




        public async Task<bool> ReturnRentalRequest(int id)
        {
            return !await AcceptRentalRequest(id, false);

        }





        public async Task<List<RentalResponceDto>> GetAllRentalRequests()
        {
            var responcelist = new List<RentalResponceDto>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"select * from RentalRequests";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())

                    {
                        var reaponsemodel = new RentalResponceDto()
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            MovieId = reader.GetInt32(2),
                            RentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            Image = reader.GetString(5),
                            Status = reader.GetString(6),
                            MovieAvailableCopies = reader.GetInt32(7),
                        };

                        responcelist.Add(reaponsemodel);
                    }
                }

            }
            return responcelist;

        }

        public async Task<List<RentalResponceDto>> GetRentalRequestById(int id)
        {
            var responcelist = new List<RentalResponceDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(); // Await here to ensure connection is opened before executing the command
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM RentalRequests WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync()) // Await for reading the data asynchronously
                    {
                        var reaponsemodel = new RentalResponceDto()
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            MovieId = reader.GetInt32(2),
                            RentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            Image = reader.GetString(5),
                            Status = reader.GetString(6),
                            MovieAvailableCopies = reader.GetInt32(7),
                        };

                        responcelist.Add(reaponsemodel);
                    }
                }
            }
            return responcelist;

        }


       

        public async Task<List<RentalResponceDto>> GetRentalsByCustomerId(int customerId)
        {
            var responcelist = new List<RentalResponceDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"select *from RentalRequests where CustomerId=@MovieId";
                command.Parameters.AddWithValue("CustomerId", customerId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())

                    {
                        var reaponsemodel = new RentalResponceDto()
                        {
                            Id = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            MovieId = reader.GetInt32(2),
                            RentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            Image = reader.GetString(5),
                            Status = reader.GetString(6),
                            MovieAvailableCopies = reader.GetInt32(7),
                        };

                        responcelist.Add(reaponsemodel);
                    }
                }

            }
            return responcelist;
        }

    } 

}
