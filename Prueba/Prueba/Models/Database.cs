using System.Data.SqlClient;

namespace Prueba.Models
{
    public class Database
    {
        SqlConnection databaseConnection;
        public SqlConnection Conectar()
        {
            string cadena = "Server=localhost;Database=Ticket;Trusted_Connection=True";
            databaseConnection = new SqlConnection(cadena);
            databaseConnection.Open();
            return databaseConnection;
        }

        public void Desconectar()
        {
            databaseConnection.Close();
        }

    }
}