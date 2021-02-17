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


        private TcpListener listener;

        private Database db = new Database();
        //
        //
        List<string> gameLogList = new List<string>();
        string gameLog = "";
        //
        bool host = true;
        //
        private static object singleHost = new object();
        //
        private List<User> tournamentContestants = new List<User>();
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
            string message = "";

            TcpClient client = (TcpClient)obj;
            //message is "read from port"
            StreamReader reader = new StreamReader(client.GetStream());
            //message is stored 
            while (reader.Peek() != -1)
            {
                message += (char)reader.Read();
            }
            Console.WriteLine(message);
            Request request = new Request(message);
            MessageHandler messageHandler = new MessageHandler(client, request.method, request.command, request.username, request.message);
            if (request.command.Contains("/battles"))
            {
                string handtypes = "";
                lock (singleHost)
                {
                    handtypes= db.GetActions(request.username);
                }
                User user = new User(request.username, handtypes);
                string responseGameLog = BattleArrange(user);
                messageHandler.ResponseOK(responseGameLog);

            }
            else
            {
                messageHandler.Go();

            }
            client.Close();
        }
        public string BattleArrange(User user)
        {
            bool gameOver = false;

            lock (singleHost)
            {
                if (host)
                {
                    host = false;
                    tournamentContestants.Add(user);
                    user.gameStarter = true;
                }
                else
                {
                    tournamentContestants.Add(user);
                }
            }

            if (user.gameStarter)
            {
                Game.Game game = new Game.Game();
                Thread.Sleep(15000);
                game.tournamentContestants = tournamentContestants;
                gameLogList = game.Battle();
                gameOver = true;

                foreach (var line in gameLogList)
                {
                    gameLog += line;

                }
            }
            else
            {

                while (!gameOver)
                {
                    Thread.Sleep(3000);
                }
            }

            return gameLog;
        }
    }
}

