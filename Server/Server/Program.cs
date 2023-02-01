using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Server
{
    class Program
    {
        const int port = 8080;
        
        static void Main(string[] args)
        {
            string[] prefixes = {$"http://localhost:{port}/"};
            Console.WriteLine("Welcome to the Blendr server");

            Console.WriteLine("Listening...");
            HttpListener server = new HttpListener();
            server.Start();
            foreach (string prefix in prefixes)
            {
                server.Prefixes.Add(prefix);
            }
            

            bool running = true;
            int hitCount = 0;
            while (running)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string requestInfo = request.RawUrl;
                byte[] buffer;
                string path = "../../static" + request.RawUrl;
                switch(requestInfo)
                {
                    case "/":
                        buffer = File.ReadAllBytes($"{path}/index.html");
                        break;
                    default:
                        if(File.Exists(path))
                        {
                            buffer = File.ReadAllBytes(path);
                        }
                        else
                        {
                            response.StatusCode = 404;
                            buffer = Encoding.UTF8.GetBytes("Error 404 - File not found");
                        }
                        break;
                }
                    
                requestInfo = request.RawUrl;

                Console.WriteLine($"Sending: {buffer.Length} bytes");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }
    }
}
