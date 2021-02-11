using System;
using Npgsql;

namespace PPB
{
    public class Database
    {
        public NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");

        //User Related ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool SaveUser(User user)
        {
            string query = "UPDATE public.users SET battlepoints = @battlepoints,  roundpoints = @roundpoints WHERE username = @username;)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", user.username);
            cmd.Parameters.AddWithValue("battlepoints", user.battlePoints);
            cmd.Parameters.AddWithValue("roundpoints", user.roundPoints);
            //Use n later for Error Handling
            if (cmd.ExecuteNonQuery() == 1)
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }


        public void LoadUser()
        {

        }

        public bool AddUser(string username, string password)
        {
            try
            {
                string query = "INSERT INTO public.users(username,password,battlepoints, roundpoints, bio, image, publicname, admin, gamepoints) values(@username, @password, 0, 0,'Hier könnte ihre Werbung stehen.', ':))', @username,  'false', 100);";
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
            catch (Exception)
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

        public bool ChangeBio(string username, string publicname, string bio, string image)
        {
            string query = "UPDATE public. users SET publicname = @publicname, bio = @bio, image = @image WHERE username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("publicname", publicname);
            cmd.Parameters.AddWithValue("bio", bio);
            cmd.Parameters.AddWithValue("image", image);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        public string GetBio(string username)
        {
            string bio = "";
            connect.Open();
            var query = "SELECT bio FROM public.users WHERE username=@username;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Prepare();
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            int line = 1;
            while (reader.Read())
            {
                bio = reader.GetString(0);
                line++;
            }
            connect.Close();
            return bio;
        }

        public int GetStats(string username)
        {
            connect.Open();
            var query = "SELECT gamepoints FROM public.users WHERE username=@username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            int stats = 0;
            int line = 1;
            while (reader.Read())
            {
                stats = reader.GetInt32(0);
                line++;
            }
            connect.Close();
            return stats;
        }

        public string Scoreboard()
        {
            string scoreboard = "";
            connect.Open();
            var query = "SELECT publicname, gamepoints FROM users ORDER BY gamepoints DESC;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            int line = 1;
            while (reader.Read())
            {
                scoreboard += "Points:\t"  + reader.GetInt32(1).ToString() +  ":\t\t " + reader.GetString(0) + " \n";
                line++;
            }
            connect.Close();
            return scoreboard;

        }
    }
}
