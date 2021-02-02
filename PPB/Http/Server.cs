 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace PPB.Http
{
    public class Server
    {
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "PPB";

        private bool running = false;

        private string message = "";

        private TcpListener listener;

        public TcpClient client;

        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            running = true;
            listener.Start();

            while (running)
            {
                TcpClient client = listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }

            running = false;
            listener.Stop();
        }
       
        private void HandleClient(Object obj)
        {
            client = (TcpClient)obj;
            //message is "read from port"
            StreamReader reader = new StreamReader(client.GetStream());
            //message is stored 
            while (reader.Peek() != -1)
            {
                message += (char)reader.Read();
            }
            Console.WriteLine(message);
            Request request = new Request(message);
            MessageHandler messageHandler = new MessageHandler(client, request.Method, request.Command, request.Username, request.Message);
            client.Close();        
        }
   
    }
}

