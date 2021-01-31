using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PPB.Login
{
    class Register
    {
        Database db = new Database();
        public void CreateUser(User user)
        {
            string query = "INSERT into public.users(username,password) values(@username, @password)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, db.connect);
            db.connect.Open();
            cmd.Parameters.AddWithValue("username", user.username);
            cmd.Parameters.AddWithValue("password", user.password);
            //User n later for Error Handling
            int n = cmd.ExecuteNonQuery();
            db.connect.Close();
        }
    }
}
