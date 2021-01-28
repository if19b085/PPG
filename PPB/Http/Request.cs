using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace PPB.Http
{
    public class Request
    {

        public string Method, Command, Username, Message;
       
        public Request(string _request)
        {
            if(!string.IsNullOrEmpty(_request))
            {
                Method = GetMethodFromRequest(_request);
                Command = GetCommandFromRequest(_request);
                Username = GetUsernameFromAuthorization(_request);
                Message = GetRequestMessage(_request);
            }
            
        }
        //empty initialize to use functions
        public Request()
        {

        }


        public string GetMethodFromRequest(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return null;
            }

            string[] lines = request.Split("\r\n");
            string[] firstline = lines[0].Split(" ");
            return firstline[0];
        }

        public string GetCommandFromRequest(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return null;
            }

            string[] lines = request.Split("\r\n");
            string[] firstline = lines[0].Split(" ");
            return firstline[1];
        }

        public static string GetRequestMessage(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                return null;
            }
            string[] tokens = request.Split("\r\n\r\n");
            string message = tokens[1];
            return message;
        }

        private string GetUsernameFromAuthorization(string request)
        {
            string[] lines = request.Split("\r\n");
            foreach (var line in lines)
            {
                if (line.Contains("Authorization: "))
                {
                    string[] tokens = line.Split(" ");
                    string[] username = tokens[2].Split("-");
                    return username[0];
                }
            }
            return " ";
        }

        public string GetLogEntry()
        {

            string logEntry = Method + Command + Message + " " +Username;
            return logEntry;
        }

        
    }
}