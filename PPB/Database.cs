using System;
using Npgsql;

namespace PPB
{
    class Database
    {
        public NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");
       //User Related
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

        public void LoadUser()
        {

        }

        public bool AddUser(string username, string password)
        {
            try
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
                return true;
            }
            catch(Exception)
            {
                connect.Close();
                return false;
            }
          
        }

        public bool Login(string username, string password)
        {
            connect.Open();
            var query = "SELECT COUNT(*) FROM public.users WHERE username= @username AND password = @password;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            int countAll = 0;
            while (reader.Read())
            {
                countAll = reader.GetInt32(0);
            }
            connect.Close();
            if (countAll == 0)
            { 
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
