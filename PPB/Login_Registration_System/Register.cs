using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PPB
{
    class Register
    {
        
        NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");
        private void CreateUser(string username, string password)
        {
            string query = "INSERT into public.users(username,password) values(@username, @password)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            int n = cmd.ExecuteNonQuery();
            connect.Close();
        }
    }
}
