using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PPB.Login
{
    class Login
    {
        Database db = new Database();
        public bool LoginUser(User user)
        {
            db.connect.Open();
            string query = "SELECT COUNT(*) from public.users WHERE username = @username AND password = @password";
            NpgsqlCommand cmd = new NpgsqlCommand(query, db.connect);
            cmd.Parameters.AddWithValue("username", user.username);
            cmd.Parameters.AddWithValue("password", user.password);
            int n = cmd.ExecuteNonQuery();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            int login = 0;
            while (reader.Read())
            {
                login = reader.GetInt32(0);
            }
            db.connect.Close();
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
