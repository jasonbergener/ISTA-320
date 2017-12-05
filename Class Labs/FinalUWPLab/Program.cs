using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

/************************************************
 * This code was adapted from a tutorial by Dirk Bahle posted on Code Project on November 1, 2017
 * https://www.codeproject.com/Articles/1210189/Using-SQLite-in-Csharp-Net
 ************************************************/

namespace FinalUWPLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating a database connection.");
            // create a new database connection:
            SQLiteConnection sqlite_conn =
              new SQLiteConnection("Data Source=database.sqlite");

            Console.WriteLine("Opening the database connection.");
            // open the connection:
            sqlite_conn.Open();

            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            Console.WriteLine("Dropping the table if it exists.");
            // Let the SQLiteCommand object know our SQL-Query:
            sqlite_cmd.CommandText =
            "DROP TABLE IF EXISTS marriages;";

            // Now lets execute the SQL ;-)
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Creating the table.");
            sqlite_cmd.CommandText =
            "CREATE TABLE IF NOT EXISTS marriages (Number INTEGER, Name NVARCHAR(255));";
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Inserting first wife.");
            sqlite_cmd.CommandText =
            "INSERT INTO marriages (Number, Name) VALUES (1, 'Janele');";
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Inserting second wife.");
            sqlite_cmd.CommandText =
            "INSERT INTO marriages (Number, Name) VALUES (2, 'Michelle');";
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Deleting the first wife.");
            sqlite_cmd.CommandText =
            "DELETE FROM marriages WHERE Number = 1;";
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Updating the entry for third wife's data.");
            sqlite_cmd.CommandText =
            "UPDATE marriages SET Number = 3, Name = 'Lisa' WHERE Number = 2;";
            sqlite_cmd.ExecuteNonQuery();

            Console.WriteLine("Querying the database.");
            sqlite_cmd.CommandText =
            "SELECT * FROM marriages;";

            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            
            // The SQLiteDataReader allows us to run through each row per loop
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                // Print out the content of the text field:
                Console.WriteLine($"{sqlite_datareader.GetValue(0)}, {sqlite_datareader.GetString(1)}\n");
            }

            Console.WriteLine("Closing the database connection.");
            // close the connection:
            sqlite_conn.Close();
        }
    }
}
