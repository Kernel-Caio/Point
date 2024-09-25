using ManagerEmployee;

namespace FolhaPontos
{
    public class Program
    {
        public static void Main()
        {
            DateTime dateTimeCurrent = DateTime.Now;
            string data = dateTimeCurrent.Date.ToString("dd/MM/yyyy");

            Registro registro= new Registro();
            registro.RegistroData(data);
        }
    }
}