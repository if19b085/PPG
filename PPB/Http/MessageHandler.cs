
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace PPB.Http
{
    class MessageHandler
    {

        public string username;
        public string authorizationName;
        public string password;
        public string message;
        public string title;
        private TcpClient client;
        public string command;
        public string method;
        
        //
        public dynamic jsonData;
        //
        private Database db = new Database();
       
        public void Go()
        {
            switch (method)
            {
                case "POST":
                    PostHandler(command);
                    break;
                case "PUT":
                    PutHandler(command);
                    break;
                case "GET":
                    GetHandler(command);
                    break;
                case "DELETE":
                    DeleteHandler(command);
                    break;
            }
        }
        public MessageHandler(TcpClient _client, string _method, string _command, string _authorizationName, string _message)
        {
            message = _message;
            authorizationName = _authorizationName;
            client = _client;
            method = _method;
            command = _command;
        }
        public void PostHandler(string command)
        {
            switch (command)
            {
                case "/users":
                    ParseJson(message);
                    username = jsonData.Username;
                    password = jsonData.Password;
                    if (db.AddUser(username, password))
                    {
                        ResponseOK("User wurde erfolgreich hinzugefügt.\n");
                    }
                    else
                    {
                        ResponseError("Es gab ein Problem beim Hinzufügen des Users.\n");
                    }
                    break;

                case "/sessions":
                    ParseJson(message);
                    username = jsonData.Username;
                    password = jsonData.Password;
                    if (db.Login(username, password))
                    {
                        ResponseOK("User wurde erfolgreich eingeloggt.\n");
                    }
                    else
                    {
                        ResponseError("User mit diesem Username und Password existiert nicht.\n");
                    }
                    break;

                case "/lib":
                    ParseJson(message);
                    username = jsonData.Username;
                    title = jsonData.Name;
                    if (db.AddMMC(authorizationName, title) && db.AddMMCToLibrary(authorizationName, title))
                    {
                        ResponseOK("Musikstück mit dem Titel '" + title + "' wurde hinzugefügt.\n");
                    }
                    else
                    {
                        ResponseError("Musikstück mit dem Titel '" + title + "' konnte nicht hinzugefügt werden.\n");
                    }

                    break;
                case "/playlist":
                    ParseJson(message);
                    title = jsonData.Name;
                    db.AddSongToGlobal(title);
                    ResponseOK("Musikstück mit dem Titel '" + title + "' konnte globaler Playlist hinzugefügt werden.\n");
                    break;
                default:
                    ResponseOK("Etwas wird noch nich behandelt.\n");
                    break;
            }

        }

        public void GetHandler(string command)
        {
            if (command.Contains("/users"))
            {
                string[] commandBlocks = command.Split("/");
                if (string.Compare(commandBlocks[2], authorizationName) == 0)
                {
                    ResponseOK(db.GetBio(commandBlocks[2]) + "\n");
                }
                else
                {
                    ResponseError("Username entspricht nicht der Authorisation" + "\n");
                }
            }
            else
            {
                switch (command)
                {
                    case "/stats":
                        ResponseOK("Der User mit dem Namen '" + authorizationName + "' hat " + db.GetStats(authorizationName).ToString() + " Game Points.");
                        break;
                    case "/score":
                        ResponseOK(db.Scoreboard() + "\n");
                        break;
                    case "/lib":
                        ResponseOK(db.ShowLibrary(authorizationName) + "\n");
                        break;
                    case "/playlist":
                        ResponseOK(db.ShowPlaylist() + "\n");
                        break;
                    case "/actions":
                        ResponseOK(db.GetActions(authorizationName) + "\n");
                        break;
                    default:
                        ResponseOK("Etwas wird noch nicht behandelt.\n");
                        break;
                }

            }


        }

        public void PutHandler(string command)
        {
            if (command.Contains("/users"))
            {
                string[] commandBlocks = command.Split("/");
                if (string.Compare(commandBlocks[2], authorizationName) == 0)
                {
                    ParseJson(message);
                    string publicname = jsonData.Name;
                    string bio = jsonData.Bio;
                    string image = jsonData.Image;
                    if (db.ChangeBio(commandBlocks[2], publicname, bio, image))
                    {
                        ResponseOK("Bio des Users '" + commandBlocks[2] + "' wurde geändert.\n");
                    }
                    else
                    {
                        ResponseError("User '" + commandBlocks[2] + "' konnte nicht gefunden werden.\n");
                    }
                }
                else
                {
                    ResponseError("Username entspricht nicht der Authorisation");
                }
            }
            else
            {
                switch (command)
                {
                    case "/actions":
                        ParseJson(message);
                        string handtypes = jsonData.actions;
                        if (db.ChangeAction(authorizationName, handtypes))
                        {
                            ResponseOK("Actions des Users " + authorizationName + " wuden geändert.\n");
                        }
                        else
                        {
                            ResponseError("Actions des Users " + authorizationName + " konnten nicht geändert werden.\n");
                        }
                        break;
                    case "/playlist":
                        ParseJson(message);
                        int from = jsonData.FromPosition;
                        int to = jsonData.ToPosition;
                        db.Reorder(from, to);
                        ResponseOK("Position der Musikstücke wurde geändert\n");
                        break;
                    default:

                        break;
                }
            }
        }

        public void DeleteHandler(string command)
        {
            if (command.Contains("/lib"))
            {
                string[] commandBlocks = command.Split("/");
                string songTitle = commandBlocks[2];
                if (db.DeleteMMCfromLibrary(authorizationName, songTitle))
                {
                    ResponseOK("Musikstück mit Namen'" + songTitle + "' wurde gelöscht.\n");
                }
                else
                {
                    ResponseError("Musikstück mit Namen'" + songTitle + "'konnte nicht gelöscht werden.\n");
                }
            }
        }
        public void ParseJson(string _string)
        {
            if (!string.IsNullOrEmpty(_string))
            {
                jsonData = JObject.Parse(_string);
            }
        }
        public void ResponseOK(string message, string status = "200", string contentType = "plain/text")
        {
            //Dispossed Object cannot be accssed Exception needs to be fixed /handled
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Server.VERSION + " " + status);
            sb.AppendLine("Content-Type: ");
            sb.AppendLine("Content-Length: " + Encoding.UTF8.GetBytes(message).Length);
            sb.AppendLine();
            sb.AppendLine(message);
            System.Diagnostics.Debug.WriteLine(sb.ToString());
            writer.Write(sb.ToString());
            writer.Close();
        }

        public void ResponseError(string message, string status = "400", string contentType = "plain/text")
        {
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Server.VERSION + " " + status);
            sb.AppendLine("Content-Type: ");
            sb.AppendLine("Content-Length: " + Encoding.UTF8.GetBytes(message).Length);
            sb.AppendLine();
            sb.AppendLine(message);
            System.Diagnostics.Debug.WriteLine(sb.ToString());
            writer.Write(sb.ToString());
            writer.Close();
        }

       
    }
}
