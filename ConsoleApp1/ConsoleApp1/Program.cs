using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Replace the connection string with your actual SQL Server connection string
        string connectionString = "server=DESKTOP-D9AQSGN;database=Day8Ass;trusted_connection=true;";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Insert 10 records into the Products table
            InsertSampleData(connection);

            // Display top 5 records in descending order of PName
            DisplayTop5Products(connection);
        }
        Console.ReadKey();
    }

    static void InsertSampleData(SqlConnection connection)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO Products (Pid, PName, PPrice, MofDate, ExpDate)
                VALUES
                ('P00001', 'Product1', 10.99, GETDATE(), DATEADD(DAY, 30, GETDATE())),
                ('P00002', 'Product2', 20.99, GETDATE(), DATEADD(DAY, 60, GETDATE())),
                -- Add more records as needed
                ('P00010', 'Product10', 50.99, GETDATE(), DATEADD(DAY, 90, GETDATE()))";

            command.ExecuteNonQuery();
        }
    }

    static void DisplayTop5Products(SqlConnection connection)
    {
        using (SqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @"
                SELECT TOP 5 Pid, PName, PPrice, MofDate, ExpDate
                FROM Products
                ORDER BY PName DESC";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Top 5 Products:");
                Console.WriteLine("===============================================");
                Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-20} {4,-20}", "Pid", "PName", "PPrice", "MofDate", "ExpDate");
                Console.WriteLine("===============================================");

                while (reader.Read())
                {
                    Console.WriteLine("{0,-10} {1,-20} {2,-10:C2} {3,-20:yyyy-MM-dd} {4,-20:yyyy-MM-dd}",
                        reader["Pid"], reader["PName"], reader["PPrice"], reader["MofDate"], reader["ExpDate"]);
                }
            }
        }
    }
}
