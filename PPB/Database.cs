﻿using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace PPB
{
    class Database
    {
        public NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");
       
        public void SaveUser(User user)
        {
            string query = "UPDATE public.users SET battlepoints = @battlepoints,  roundpoints = @roundpoints WHERE username = @username;)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", user.username);
            cmd.Parameters.AddWithValue("battlepoints", user.battlePoints);
            cmd.Parameters.AddWithValue("roundpoints", user.roundPoints);
            //Use n later for Error Handling
            int n = cmd.ExecuteNonQuery();
            connect.Close();

        }

        public void AddUser(string username, string password)
        {
            string query = "INSERT INTO public.users(username,password,battlepoints, roundpoints, admin) values(@username, @password, 0, 0, 'false');";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            //Exception should nto interrupt programm
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            connect.Close();
        }
    }
}
