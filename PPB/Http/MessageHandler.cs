
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
        public string message;
        TcpClient client;
        public MessageHandler(TcpClient _client, string _method, string _command, string _username, string _message)
        {

            message = _message;
            username = _username;
            client = _client;

            switch (_method)
            {
                case "POST":
                    PostHandler( _command);
                    break;
                case "PUT":
                    PutHandler( _command);
                    break;
                case "GET":
                    GetHandler( _command);
                    break;
                case "DELETE":
                    DeleteHandler( _command);
                    break;
            }
        }
        public void PostHandler( string command)
        {
            switch (command)
            {
                case "/users" :
                    ResponseOK("User soll geadded werden.\n");
                    break;

                case "/sessions":
                    ResponseOK("User soll eingeloggt werden.\n");
                    break;

                case "/lib":
                    ResponseOK("Musikstück soll hinzugefügt werden.\n");
                    break;
                case "/battles":
                    ResponseOK("Neuer Battle wird gestartet.\n");
                    break;
                case "/playlist":
                    ResponseOK("Musikstück soll zu globaler Playlist hinzugefügt werden.\n");
                    break;
                default:
                    ResponseOK("Etwas wird noch nich behandelt.\n");
                    break;
            }

        }

        public void GetHandler(string command)
        {
            if(command.Contains("/users"))
            {
                string[] commandBlocks = command.Split("/");
                ResponseOK("Bio des Users '" + commandBlocks[2] + "' wird abgefragt.\n");
            }
            else
            {
                switch (command)
                {
                    case "/stats":
                        ResponseOK("Stats des Users wird abgefragt.\n");
                        break;
                    case "/score":
                        ResponseOK("Score des Users wird abgefraglt.\n");
                        break;
                    case "/lib":
                        ResponseOK("Library drs Users wird abgefragt\n");
                        break;
                    case "/playlist":
                        ResponseOK("Globale playlist wird abgefragt.\n");
                        break;
                    case "/actions":
                        ResponseOK("Gesetzte Actions wird abgefragt\n");
                        break;
                    default:
                        ResponseOK("Etwas wird noch nich behandelt.\n");
                        break;
                }

            }
           

        }

        public void PutHandler(string command)
        {
            switch (command)
            {
                case "/users":
                    ResponseOK("Bio des Users wird geändert.\n");
                    break;
                case "/actions":
                    ResponseOK("Actions des Users werden geändert.\n");
                    break;
                case "/playlist":
                    ResponseOK("Position eines Musikstückes soll geändert werden.\n");
                    break;
                default:
                    
                    break;
            }

        }

        public void DeleteHandler( string command)
        {
            if (command.Contains("/lib"))
            {
                string[] commandBlocks = command.Split("/");
                ResponseOK("Musikstück mit Namen'" + commandBlocks[2] + "' soll gelöscht werden.\n");
            }
            else
            {
                /*
                switch (command)
                {
                    case "/lib":
                        ResponseOK("Musikstück soll aus lib gelöscht werden.");
                        break;
                    default:
                        ResponseOK("Etwas wird noch nich behandelt.");
                        break;
                }
                */
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
        }

    }
}
