using System.Data.SQLite;
using System.Runtime.CompilerServices;
using ManagerEmployee;

namespace ManagerDB
{
    public class DB
    {
        private const string PathDB = @"Data Source=DataBase/mydatabase.db";
        private SQLiteConnection conn;

        public DB()
        {
            conn = new SQLiteConnection(PathDB);
            conn.Open();
            this.CreateTables();
        }

        // Cria todas tabelas necessarias do banco de dados
        public void CreateTables()
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Funcionarios (" +
                    "id INTEGER PRIMARY KEY, " +
                    "nome TEXT, " +
                    "cpf TEXT, " +
                    "pix TEXT, " +
                    "telefone TEXT);";
                cmd.ExecuteNonQuery();
            
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Registros (" + 
                    "id INTEGER PRIMARY KEY, " + 
                    "data TEXT, " +
                    "hora TEXT, " +
                    "status TEXT," +
                    "id_funcionario INTEGER, " +
                    "FOREIGN KEY (id_funcionario) REFERENCES Funcionarios (id));";
                cmd.ExecuteNonQuery();
            }
        }

        // Cadastra um funcionario no banco de dados
        public void EmployeeRegistration(string nome, string cpf, string pix, string telefone)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Funcionarios " +
                    "(nome, cpf, pix, telefone) " +
                    "VALUES (@nome, @cpf, @pix, @telefone);";
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@pix", pix);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.ExecuteNonQuery();
            }
        }

        // Bate o ponto de um funcionario pelo id
        public void RegisterPoint(int id){
            bool validador = false; //Verifica se o id passado é existente
            string status = string.Empty; //Diz se o registro se trata de uma entrada ou saida
            int qtd_registros = 0; //Verifica quantos vezes a pessoa bateu ponto hoje

            using (var cmd = conn.CreateCommand())
            {
                DateTime dateTimeCurrent = DateTime.Now;
                string data = dateTimeCurrent.Date.ToString("dd/MM/yyyy");
                string hora = dateTimeCurrent.TimeOfDay.ToString(@"hh\:mm\:ss");

                cmd.CommandText = @"SELECT nome FROM Funcionarios WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    validador = reader.HasRows;
                }
                
                if (validador)
                {
                    cmd.CommandText = @"SELECT * FROM Registros WHERE " +
                        "data = @data AND id_funcionario = @id;";
                    cmd.Parameters.AddWithValue("@data", data);
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            qtd_registros++;
                        }
                    }

                    status = qtd_registros%2 == 0 ? "entrada" : "saida";

                    cmd.CommandText = @"INSERT INTO Registros " +
                        "(data, hora, status, id_funcionario)" +
                        "VALUES (@data, @hora, @status, @id_funcionario);";
                    cmd.Parameters.AddWithValue("@data", data);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id_funcionario", id);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine($"Não há nenhum funcionário com o número de registro: {id}");
                }
            }
        }

        // Deleta o registro de um funcionario
        public void DeleteEmployeeRegistration(string nome)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Funcionarios WHERE nome = @nome;";
                cmd.Parameters.AddWithValue("nome", nome);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Registro> ListaRegistrosPorData(string data)
        {
            List<Registro> lista_registros = new List<Registro>();
            int qtd_registros = 0;

            using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM Registros WHERE data = @data;";
                cmd.Parameters.AddWithValue("@data", data);

                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Registro registro = new Registro();
                        qtd_registros++;

                        registro.id_registro = reader.GetInt32(0);
                        registro.data = reader.GetString(1);
                        registro.hora = reader.GetString(2);
                        registro.status = reader.GetString(3);
                        registro.id_funcionario = reader.GetInt32(4);

                        lista_registros.Add(registro);
                    }

                    if (qtd_registros == 0) { Console.WriteLine("Nenhum registro"); }
                }
            }
            return lista_registros;
        }

        public void Desconectar()
        {
            conn.Close();
            conn.Dispose();
        }
    }
}