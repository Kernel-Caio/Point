using ManagerDB;

namespace ManagerEmployee
{
    public class Funcionario
    {
        public int? id { get; set;} = null;
        public string nome { get; set;} = string.Empty;
        public string cpf { get; set;} = string.Empty;
        public string telefone { get; set;} = string.Empty;
        public string pix { get; set;} = string.Empty;

        public Funcionario(string name, string cpf, string telefone, string pix){
            this.nome = name;
            this.cpf = cpf;
            this.telefone = telefone;
            this.pix = pix;
        }    
        public Funcionario(){}

        // Cadastra um funcionario no sistema
        public void Register() 
        {
            DB db = new DB();
            db.EmployeeRegistration(this.nome, this.cpf, this.pix, this.telefone);
            db.Desconectar();
        }

        // Deleta o Cadastro de um funcionario
        public void DeleteRegistration(string nome)
        {
            DB db = new DB();
            db.DeleteEmployeeRegistration(nome);
            db.Desconectar();
        }
    }

    public class Registro
    {
        public int? id_registro { get; set;} = null;
        public string data { get; set;} = string.Empty;
        public string hora { get; set;} = string.Empty;
        public string status { get; set;} = string.Empty;
        public int? id_funcionario { get; set;} = null;

        // Registra um ponto de entrada ou saida do funcionario
        public void Point(int id)
        {
            DB db = new DB();
            db.RegisterPoint(id);
            db.Desconectar();
        }

        // Lista todos registros de uma data
        public void RegistroData(string data)
        {
            List<Registro> lista_registros = new List<Registro>();
            DB db = new DB();

            lista_registros = db.ListaRegistrosPorData(data);
            
            foreach (Registro registro in lista_registros)
            {
                Console.WriteLine($"{registro.id_registro}, {registro.data}, {registro.hora}, {registro.status}, {registro.id_funcionario}");
            }

            db.Desconectar();
        }

        //Deleta o registro de uma data
        public void DeletePoint(string data)
        {
            DB db = new DB();
            db.DeleteLastPoint(data);
            db.Desconectar();
        }

        // Lista todos registros
        public void ListaRegistros()
        {
            List<Registro> lista_registros = new List<Registro>();
            DB db = new DB();

            lista_registros = db.ListaTodosRegistros();
            
            foreach (Registro registro in lista_registros)
            {
                Console.WriteLine($"{registro.id_registro}, {registro.data}, {registro.hora}, {registro.status}, {registro.id_funcionario}");
            }

            db.Desconectar();
        }
    }
}