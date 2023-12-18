using System;
using System.Data;

class Program
{
    static void Main()
    {
        // Create a DataTable with the specified schema
        DataTable productsTable = CreateProductsTable();

        // Insert sample data into the DataTable
        InsertSampleData(productsTable);

        // Display all products
        DisplayProducts(productsTable);

        // Search for a product by name
        SearchProduct(productsTable, "Product3");

        // Delete a product by name
        DeleteProduct(productsTable, "Product2");

        // Display updated products after deletion
        DisplayProducts(productsTable);
        Console.ReadKey();
    }

    static DataTable CreateProductsTable()
    {
        DataTable table = new DataTable("Products");

        // Define columns
        DataColumn pidColumn = new DataColumn("Pid", typeof(string));
        DataColumn pnameColumn = new DataColumn("PName", typeof(string));
        DataColumn ppriceColumn = new DataColumn("PPrice", typeof(decimal));
        DataColumn mofDateColumn = new DataColumn("MofDate", typeof(DateTime));
        DataColumn expDateColumn = new DataColumn("ExpDate", typeof(DateTime));

        // Set additional properties
        pidColumn.Unique = true;
        pidColumn.MaxLength = 7; // P00001

        pnameColumn.MaxLength = 255; // Adjust as needed

        // Add columns to the DataTable
        table.Columns.Add(pidColumn);
        table.Columns.Add(pnameColumn);
        table.Columns.Add(ppriceColumn);
        table.Columns.Add(mofDateColumn);
        table.Columns.Add(expDateColumn);

        return table;
    }

    static void InsertSampleData(DataTable table)
    {
        table.Rows.Add("P00001", "Product1", 10.99, DateTime.Now, DateTime.Now.AddDays(30));
        table.Rows.Add("P00002", "Product2", 20.99, DateTime.Now, DateTime.Now.AddDays(60));
        table.Rows.Add("P00003", "Product3", 30.99, DateTime.Now, DateTime.Now.AddDays(90));
        // Add more rows as needed
    }

    static void DisplayProducts(DataTable table)
    {
        Console.WriteLine("Products:");
        Console.WriteLine("===============================================");
        Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-20} {4,-20}", "Pid", "PName", "PPrice", "MofDate", "ExpDate");
        Console.WriteLine("===============================================");

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-10:C2} {3,-20:yyyy-MM-dd} {4,-20:yyyy-MM-dd}",
                row["Pid"], row["PName"], row["PPrice"], row["MofDate"], row["ExpDate"]);
        }

        Console.WriteLine();
    }

    static void SearchProduct(DataTable table, string productName)
    {
        DataRow[] foundRows = table.Select($"PName = '{productName}'");

        if (foundRows.Length > 0)
        {
            Console.WriteLine($"Product '{productName}' found:");
            Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-20} {4,-20}", "Pid", "PName", "PPrice", "MofDate", "ExpDate");
            Console.WriteLine("===============================================");

            foreach (DataRow row in foundRows)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-10:C2} {3,-20:yyyy-MM-dd} {4,-20:yyyy-MM-dd}",
                    row["Pid"], row["PName"], row["PPrice"], row["MofDate"], row["ExpDate"]);
            }

            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Product '{productName}' not found.");
        }
    }

    static void DeleteProduct(DataTable table, string productName)
    {
        DataRow[] foundRows = table.Select($"PName = '{productName}'");

        foreach (DataRow row in foundRows)
        {
            table.Rows.Remove(row);
            Console.WriteLine($"Product '{productName}' deleted.");
        }
    }
}
