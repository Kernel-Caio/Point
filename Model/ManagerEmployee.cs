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
        }

        // Registra um ponto de entrada ou saida do funcionario
        public void Point(string id)
        {
            DB db = new DB();
            db.RegisterPoint(id);
        }

        // Deleta o Cadastro de um funcionario
        public void DeleteRegistration(string nome)
        {
            DB db = new DB();
            db.DeleteEmployeeRegistration(nome);
        }
    }
}