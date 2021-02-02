
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
               

                    break;

                case "/sessions":
                  
                    break;

                case "/lib":
                  
                    break;
                case "/transactions/packages":
                  

                    break;
                case "/tradings":
                    
                    break;
                case "/battles":
                    
                    break;
                default:
                    
                    break;
            }

        }

        public void GetHandler(string command)
        {
            switch (command)
            {
                case "/users":
                   
                    break;
                case "/stats":
                  
                    break;
                case "/score":
                    
                    break;
                case "/lib":
                    
                    break;
                case "/playlist":
                   
                    break;
                case "/actions":

                    break;
                default:
                    
                    break;
            }

        }

        public void PutHandler(string command)
        {
            switch (command)
            {
                case "/deck":
                   
                    break;
                case "/deck/unset":
                    
                    break;
               
                default:
                    
                    break;
            }

        }

        public void DeleteHandler( string command)
        {
            
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
