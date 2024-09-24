using System.Data.SQLite;

namespace Connections
{
    class DB
    {
        public void Conecta()
        {
            string connectionStringFile = @"Data Source=/home/yoshim/Documentos/Linguagens/C#/FolhaPontos/DB/mydatabase.db";

            using var connection = new SQLiteConnection(connectionStringFile);
            connection.Open();
            
            //

            using var cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS MyTable " + 
                "(ID INTEGER PRIMARY KEY, Name TEXT);", connection);
            cmd.ExecuteNonQuery();

            using var paramCmd = new SQLiteCommand("INSERT INTO MyTable (Name) VALUES (@name);", connection);
            paramCmd.Parameters.AddWithValue("@name", "Caio");
            paramCmd.ExecuteNonQuery();

            //

            // using var transation = connection.BeginTransaction();

            //

            using var readCmd = new SQLiteCommand("SELECT * FROM MyTable", connection);
            using var reader = readCmd.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }
            Console.ReadLine();
        }
              
    }
}