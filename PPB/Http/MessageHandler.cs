﻿
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
                    ResponseOK("User soll geadded werden.");
                    break;

                case "/sessions":
                    ResponseOK("User soll eingeloggt werden.");
                    break;

                case "/lib":
                    ResponseOK("Musikstück soll hinzugefügt werden.");
                    break;
                case "/battles":
                    ResponseOK("Neuer Battle wird gestartet.");
                    break;
                default:
                    ResponseOK("Etwas wird noch nich behandelt.");
                    break;
            }

        }

        public void GetHandler(string command)
        {
            switch (command)
            {
                case "/users":
                    ResponseOK("Bio des Users wird abgefragt.");
                    break;
                case "/stats":
                    ResponseOK("Stats des Users wird abgefragt.");
                    break;
                case "/score":
                    ResponseOK("Score des Users wird abgefraglt.");
                    break;
                case "/lib":
                    ResponseOK("Library drs Users wird abgefragt");
                    break;
                case "/playlist":
                    ResponseOK("Globale playlist wird abgefragt.");
                    break;
                case "/actions":
                    ResponseOK("Gesetzte Actions wird abgefragt");
                    break;
                default:
                    ResponseOK("Etwas wird noch nich behandelt.");
                    break;
            }

        }

        public void PutHandler(string command)
        {
            switch (command)
            {
                case "/users":
                    ResponseOK("Bio des Users wird geändert.");
                    break;
                case "/actions":
                    ResponseOK("Actions des Users werden geändert.");
                    break;
                case "/playlist":
                    ResponseOK("Position eines Musikstückes soll geändert werden");
                    break;
                default:
                    
                    break;
            }

        }

        public void DeleteHandler( string command)
        {
            switch (command)
            {
                case "/users":
                    ResponseOK("Bio des Users wird geändert.");
                    break;
                case "/stats":
                    ResponseOK("Stats des Users wird geändert.");
                    break;
                case "/score":
                    ResponseOK("Score des Users wird geändert.");
                    break;
                case "/lib":
                    ResponseOK("Librabr geändert.");
                    break;
                case "/playlist":
                    ResponseOK("Etwas wird noch nich behandelt.");
                    break;
                case "/actions":
                    ResponseOK("Etwas wird noch nich behandelt.");
                    break;
                default:
                    ResponseOK("Etwas wird noch nich behandelt.");
                    break;
            }
        }

        public void ResponseOK(string message, string status = "200", string contentType = "plain/text")
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
