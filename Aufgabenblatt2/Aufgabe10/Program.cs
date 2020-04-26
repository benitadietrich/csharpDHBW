using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe10
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionBuilder = new OleDbConnectionStringBuilder()
            {
                DataSource = "beispiel.mdb",
                Provider = "Microsoft.Jet.OLEDB.4.0"
            };

            var connection = new OleDbConnection(connectionBuilder.ConnectionString);
            var command = new OleDbCommand("SELECT NAME, VORNAME FROM VERTRETER");
            command.Connection = connection;
            connection.Open();

            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                Console.WriteLine(reader.GetString(0) + ", " + reader.GetString(1));
            }

            connection.Close();

            Console.ReadKey();

        }
    }
}
