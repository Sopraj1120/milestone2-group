using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

internal class DatabaseInitial
{
    private readonly string _connectionString;

    public DatabaseInitial(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal void Initialize()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();


            string adminQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Admins' AND xtype='U')
                    BEGIN
                        CREATE TABLE Admins (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            AdminId NVARCHAR(20) NOT NULL,
                            Email NVARCHAR(100) NOT NULL UNIQUE,
                            Password NVARCHAR(256) NOT NULL
                        );
                    END";
            
            using (var adminCommand = new SqlCommand(adminQuery, connection))
            {
                adminCommand.ExecuteNonQuery();
                Console.WriteLine("Database initialized. Admins table created if it didn't exist.");
            }


            string customerQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
                    BEGIN
                        CREATE TABLE Customers (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            FirstName NVARCHAR(100) NOT NULL,
                            LastName NVARCHAR(100) NOT NULL,
                            Address NVARCHAR(200) NOT NULL,
                            Email NVARCHAR(100) NOT NULL UNIQUE,
                            Password NVARCHAR(256) NOT NULL,
                            MobileNo NVARCHAR(20) NOT NULL,
                            NicNo NVARCHAR(20) NOT NULL UNIQUE,
                            UserName NVARCHAR(100) NOT NULL UNIQUE,
                            IsActive BIT DEFAULT 0
                        );
                    END";

            using (var customerCommand = new SqlCommand(customerQuery, connection))
            {
                customerCommand.ExecuteNonQuery();
                Console.WriteLine("Database initialized. Customers table created if it didn't exist.");
            }


            string categoryQuery = @"
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
        BEGIN
            CREATE TABLE Categories (
                Id INT PRIMARY KEY IDENTITY(1,1),
                Name NVARCHAR(255) NOT NULL
            )
        END";

            using (var categoryCommand = new SqlCommand(categoryQuery, connection))
            {
                categoryCommand.ExecuteNonQuery();
                Console.WriteLine("Categories table created if it didn't exist.");
            }


            string movieQuery = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Movies' AND xtype='U')
            BEGIN
                CREATE TABLE Movies (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Title NVARCHAR(255) NOT NULL,
                    Description NVARCHAR(MAX),
                    Copies INT NOT NULL,
                    CategoryId INT NOT NULL,
                    CategoryName NVARCHAR(50) NOT NULL,
                    Image VARCHAR(MAX),
                    IsDeleted BIT DEFAULT 0,
                    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
                );
            END";

            using (var movieCommand = new SqlCommand(movieQuery, connection))
            {
                movieCommand.ExecuteNonQuery();
                Console.WriteLine("Movies table created if it didn't exist.");
            }

            string rentalRequestQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name='RentalRequests')
                BEGIN
                    CREATE TABLE RentalRequests (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        MovieId  INT NOT NULL,
                        CustomerId INT NOT NULL, 
                        RentDate DATETIME NOT NULL DEFAULT GETDATE(),
                        ReturnDate DATETIME NOT NULL,
                        Imagepath NVARCHAR(50),
                        Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',
                        MoviesAvailableCopies INT NOT NULL
                    )
                END";

            using (var rentalRequestCommand = new SqlCommand(rentalRequestQuery, connection))
            {
                rentalRequestCommand.ExecuteNonQuery();
                Console.WriteLine("RentalRequests table created if it didn't exist.");
            }
        }


        catch (SqlException sqlEx)
        {
            Console.WriteLine($"SQL Error initializing the database: {sqlEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing the database: {ex.Message}");
            throw;
        }
    }
}
