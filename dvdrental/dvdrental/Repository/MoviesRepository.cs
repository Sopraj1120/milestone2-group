﻿using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace dvdrental.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Movies> AddMovie(Movies movie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "INSERT INTO Movies (Title, Description, Copies, CategoryId, CategoryName, Image) OUTPUT INSERTED.Id VALUES (@Title, @Description, @Copies, @CategoryId, @CategoryName, @Image)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Description", movie.Description);
                    command.Parameters.AddWithValue("@Copies", movie.Copies);
                    command.Parameters.AddWithValue("@CategoryId", movie.CategoryId);
                    command.Parameters.AddWithValue("@CategoryName", movie.CategoryName);
                    command.Parameters.AddWithValue("@Image", movie.Image);

                    // Execute the command and get the inserted movie ID
                    var result = await command.ExecuteScalarAsync();
                    var insertedId = Convert.ToInt32(result);

                    // Set the ID of the movie object to the inserted ID
                    movie.Id = insertedId;

                    // Return the updated movie object
                    return movie;
                }
            }
        }


        public async Task<Movies> GetMovieById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Movies WHERE Id = @Id AND IsDeleted = 0";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Movies
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Copies = (int)reader["Copies"],
                                CategoryId = (int)reader["CategoryId"],
                                CategoryName = reader["CategoryName"].ToString(),
                                Image = reader["Image"].ToString(),
                                IsDeleted = (bool)reader["IsDeleted"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<Movies>> GetAllMovies()
        {
            var movies = new List<Movies>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Movies WHERE IsDeleted = 0";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        movies.Add(new Movies
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Copies = (int)reader["Copies"],
                            CategoryId = (int)reader["CategoryId"],
                            CategoryName = reader["CategoryName"].ToString(),
                            Image = reader["Image"].ToString(),
                            IsDeleted = (bool)reader["IsDeleted"]
                        });
                    }
                }
            }
            return movies;
        }

        public async Task<bool> UpdateMovie(Movies movie)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Movies SET Title = @Title, Description = @Description, Copies = @Copies, CategoryId = @CategoryId, CategoryName = @CategoryName, Image = @Image WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", movie.Title);
                    command.Parameters.AddWithValue("@Description", movie.Description);
                    command.Parameters.AddWithValue("@Copies", movie.Copies);
                    command.Parameters.AddWithValue("@CategoryId", movie.CategoryId);
                    command.Parameters.AddWithValue("@CategoryName", movie.CategoryName);
                    command.Parameters.AddWithValue("@Image", movie.Image);
                    command.Parameters.AddWithValue("@Id", movie.Id);

                    var affectedRows = await command.ExecuteNonQueryAsync();
                    return affectedRows > 0;
                }
            }
        }

        public async Task<bool> DeleteMovie(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE Movies SET IsDeleted = 1 WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var affectedRows = await command.ExecuteNonQueryAsync();
                    return affectedRows > 0;
                }
            }
        }

        public async Task<List<Movies>> GetMoviesByCategory(int categoryId)
        {
            var movies = new List<Movies>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Movies WHERE CategoryId = @CategoryId AND IsDeleted = 0";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            movies.Add(new Movies
                            {
                                Id = (int)reader["Id"],
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                Copies = (int)reader["Copies"],
                                CategoryId = (int)reader["CategoryId"],
                                CategoryName = reader["CategoryName"].ToString(),
                                Image = reader["Image"].ToString(),
                                IsDeleted = (bool)reader["IsDeleted"]
                            });
                        }
                    }
                }
            }
            return movies;
        }
    }

}
