
namespace PPB.Http
{
    public class Request
    {
        public string method, command, username, message;
       
        public Request(string _request)
        {
            if(!string.IsNullOrEmpty(_request))
            {
                method = GetMethodFromRequest(_request);
                command = GetCommandFromRequest(_request);
                username = GetUsernameFromAuthorization(_request);
                message = GetRequestMessage(_request);
            } 
            
        }public string GetMethodFromRequest(string request)
        {
            string[] lines = request.Split("\r\n");
            string[] firstline = lines[0].Split(" ");
            return firstline[0];
        }

        public string GetCommandFromRequest(string request)
        {
            string[] lines = request.Split("\r\n");
            string[] firstline = lines[0].Split(" ");
            return firstline[1];
        }

        public static string GetRequestMessage(string request)
        {
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
    }
}