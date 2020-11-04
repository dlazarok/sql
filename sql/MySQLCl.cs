using System;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class MySQLCon
    {
        MySqlConnection conn; //хендлер подключения
        public void DBConnect(string host, string database, string username, string password) //подключение в бд
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database +
               ";User Id=" + username + ";password=" + password;

            conn = new MySqlConnection(connString);
        }

        public string getValue(string query) //получение 1 значения
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            string name = command.ExecuteScalar().ToString();
            conn.Close();
            return name;
        }
        public string[][] getValues(string query) //получение нескольких значений
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader = command.ExecuteReader();
            string[][] mas = new string[10][];
            int n = 0;
            while (reader.Read())
            {
                mas[n] = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                    mas[n][i] = reader[i].ToString();
                n++;
            }
            reader.Close();
            conn.Close();
            return mas;
        }

        public void execute(string querry)//выполнение без возвращение значений
        {
            conn.Open();
            MySqlCommand command = new MySqlCommand(querry, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public MySqlConnection getDb() => conn; //возврат хендлера конекта(в основном для датагридов)
    }
}