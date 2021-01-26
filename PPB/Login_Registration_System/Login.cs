using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PPB
{
    class Login
    {
        NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");
        private bool LoginUser(string username, string password)
        {
            connect.Open();
            string query = "SELECT COUNT(*) from public.users WHERE username = @username AND password = @password";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            int n = cmd.ExecuteNonQuery();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            int login = 0;
            while (reader.Read())
            {
                login = reader.GetInt32(0);
            }
            connect.Close();
            if (login == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
