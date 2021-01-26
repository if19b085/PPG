 using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace HTTPServer
{
    public class HTTPServer
    {
        public const String MSG_DIR = "/root/msg/";
        public const String WEB_DIR = "/root/web";
        public const String VERSION = "HTTP/1.1";
        public const String NAME = "PPB";
        private bool running = false;


        private TcpListener listener;

        public HTTPServer(int port)
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
                Debug.WriteLine("Waiting for connection..");

                TcpClient client = listener.AcceptTcpClient();

                Debug.WriteLine("Client connected!");

                ThreadPool.QueueUserWorkItem(HandleClient, client);
            }

            running = false;
            listener.Stop();
        }
       
        private void HandleClient(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            string msg = "";
            //message is "read from port"
            StreamReader reader = new StreamReader(client.GetStream());
            //message is stored 
            while (reader.Peek() != -1)
            {
                msg += (char)reader.Read();
            }
            //parsing of request
            Request request = new Request(msg);
            //Do something with request
            client.Close();        
        }
        
        
        
    }
    }

