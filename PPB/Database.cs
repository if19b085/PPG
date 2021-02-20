using System;
using System.Collections.Generic;
using Npgsql;

namespace PPB
{
    public class Database
    {
        public NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5432;user id=postgres; password=password; database=PPB");


        public bool AddUser(string username, string password)
        {
            try
            {
                string query = "INSERT INTO public.users(username, password, bio, image, publicname, admin, gamepoints, handtypes) values(@username, @password,'', ':))', @username,  'false', 100, 'VVVVV');";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                connect.Open();
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
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

        public bool CheckAdmin(string username)
        {
            connect.Open();
            var query = "SELECT COUNT(*) FROM public.users WHERE username= @username AND admin = 'true';";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
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

        public string GetActions(string username)
        {
            connect.Open();
            var query = "SELECT handtypes FROM public.users WHERE username=@username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            string handtypes = "";
            int line = 1;
            while (reader.Read())
            {
                handtypes = reader.GetString(0);
                line++;
            }
            connect.Close();
            return handtypes;
        }

        public bool ChangeAction(string username, string handtypes)
        {
            string query = "UPDATE public.users SET handtypes = @handtypes WHERE username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("handtypes", handtypes);
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

        public bool GainPoints(string username, int points)
        {
            string query = "UPDATE public.users SET gamepoints = gamepoints + @points  WHERE username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("points", points);
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

        public bool LostPoints(string username)
        {
            string query = "UPDATE public.users SET gamepoints = gamepoints - 1  WHERE username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
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
        public bool GiveAdministrator(string username)
        {
            string query = "UPDATE public.users SET admin = 'true' WHERE username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Parameters.AddWithValue("username", username);
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
        public void TakeAdministrator()
        {
            string query = "UPDATE public.users SET admin = 'false';";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            connect.Open();
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            if (n == 1)
            {
                connect.Close();
            }
            else
            {
                connect.Close();
            }

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
                scoreboard += "Points:\t" + reader.GetInt32(1).ToString() + ":\t\t " + reader.GetString(0) + " \n";
                line++;
            }
            connect.Close();
            return scoreboard;

        }

        public bool AddToLibrary(string username, string title, string url, string genre = "", string length = "", string rating = "", string album = "", string persTitle = "")
        {
            try
            {
                string query = "INSERT INTO public.library (username, title, url, genre, length, rating, album, perstitle) VALUES (@username, @title, @url, @genre, @length, @rating, @album, @perstitle); ";

                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                connect.Open();
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("title", title);
                cmd.Parameters.AddWithValue("genre", genre);
                cmd.Parameters.AddWithValue("length", length);
                cmd.Parameters.AddWithValue("url", url);
                cmd.Parameters.AddWithValue("rating", rating);
                cmd.Parameters.AddWithValue("album", album);
                cmd.Parameters.AddWithValue("perstitle", persTitle);
                cmd.Prepare();
                int n = cmd.ExecuteNonQuery();
                if (n == 0)
                {
                    connect.Close();
                    return false;
                }
                else
                {
                    connect.Close();
                    return true;
                }

            }
            catch (Exception)
            {
                connect.Close();
                return false;
            }

        }

        public bool DeleteMMCfromLibrary(string username, string title)
        {
            connect.Open();
            var query = "DELETE  FROM public.library WHERE title=@title AND username=@username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("title", title);
            cmd.Prepare();
            int n = cmd.ExecuteNonQuery();
            if (n == 0)
            {
                connect.Close();
                return false;
            }
            else
            {
                connect.Close();
                return true;
            }
        }

        public string ShowLibrary(string username)
        {
            connect.Open();
            var query = "SELECT title FROM public.library WHERE username=@username;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            string library = "";
            int line = 1;
            while (reader.Read())
            {
                string oneline = reader.GetString(0);
                library += oneline + "\n";
                line++;
            }

            connect.Close();

            return library;
        }

        public bool CheckLibrary(string title, string username)
        {
            connect.Open();
            var query = "SELECT COUNT(*) FROM public.library WHERE title = @title AND username = @username;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("title", title);
            cmd.Parameters.AddWithValue("username", username);
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
        public bool AddSongToGlobal(string title)
        {
            try
            {
                string query = "INSERT INTO public.global (title) VALUES( @title); ";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                connect.Open();
                cmd.Parameters.AddWithValue("title", title);
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

        public string ShowPlaylist()
        {
            connect.Open();
            var query = "SELECT title FROM public.global;";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            string library = "";
            int line = 1;
            while (reader.Read())
            {
                string oneline = reader.GetString(0);
                library += oneline + "\n";
                line++;
            }
            connect.Close();
            return library;
        }
        public void Reorder(int FromPosition, int ToPosition)
        {
            //Get Database in Form of List
            SortedDictionary<int, string> global = new SortedDictionary<int, string>();
            connect.Open();
            var query = "SELECT title FROM public.global;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Prepare();
            NpgsqlDataReader reader = cmd.ExecuteReader();
            int line = 1;
            while (reader.Read())
            {
                global.Add(line, reader.GetString(0));

                line++;
            }
            connect.Close();
            connect.Open();
            //Reorder Items in List
            string temp = global[FromPosition];
            global[FromPosition] = global[ToPosition];
            global[ToPosition] = temp;
            //Delete Entries in Database as they are
            query = "DELETE  FROM public.global;";
            cmd = new NpgsqlCommand(query, connect);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            connect.Close();
            //Save List in Database
            foreach (var song in global)
            {
                connect.Open();
                query = "INSERT INTO public.global(title) VALUES('" + song.Value + "');";
                cmd = new NpgsqlCommand(query, connect);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                connect.Close();
            }
        }

        public bool AddBlacklist(string url, string title)
        {
            try
            {
                string query = "INSERT INTO public.blacklist(url, title) values(@url, @title);";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                connect.Open();
                cmd.Parameters.AddWithValue("url", url);
                cmd.Parameters.AddWithValue("title", title);
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

        public bool CheckBlacklist(string title)
        {
            connect.Open();
            var query = "SELECT COUNT(*) FROM public.blacklist WHERE title = @title;";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
            cmd.Parameters.AddWithValue("title", title);
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
