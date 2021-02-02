
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace PPB.Http
{
    /*
    HTTP/1.1 200 OK
Via: HTTP/1.1 proxy_server_name
Server: Apache/1.3
Content-type: text/html, text, plain
Content-length: 78

<html>
<head>
<title>HTTP</TITLE>


</head>
<body>
<p> HTTP/1.1-Demo</p>
</body>
</html>
        */
    class Response
    {
        public NetworkStream stream;
        public Response(NetworkStream _stream)

        {
            stream = _stream;
        }

        public void Post(string message, string status, string contentType)
        {
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Server.VERSION + " " + status);
            sb.AppendLine("Content-Type: " + contentType);
            sb.AppendLine("Content-Length: " + Encoding.UTF8.GetBytes(message).Length);
            sb.AppendLine();
            sb.AppendLine(message);
            System.Diagnostics.Debug.WriteLine(sb.ToString());
            writer.Write(sb.ToString());
        }

        public void OK(string message, string status = "200", string contentType = "plain/text")
        {
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Server.VERSION + " " + status);
            sb.AppendLine("Content-Type: ");
            sb.AppendLine("Content-Length: " + Encoding.UTF8.GetBytes(message).Length);
            sb.AppendLine();
            sb.AppendLine(message);
            System.Diagnostics.Debug.WriteLine(sb.ToString());
            writer.Write(sb.ToString());
        }

        public void Error(string message, string status = "400", string contentType = "plain/text")
        {
            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
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
