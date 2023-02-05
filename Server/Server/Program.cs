using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
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

                Console.WriteLine($"{request.HttpMethod} Request '{request.RawUrl}'");

                if(request.HttpMethod == "POST")
                {
                    using (StreamReader r = new StreamReader(request.InputStream))
                    {
                        string query = r.ReadToEnd();
                        Match upm = Regex.Match(query, "username=(.*)&password=(.*)&password2=.*");
                        if(upm.Success)
                        {
                            string username = upm.Groups[1].Value;
                            string password = upm.Groups[2].Value;
                            string html;

                            if(username == "kwales" && password == "passwor")
                            {
                                html = "Login succeeded";
                            }
                            else
                            {
                                html = "Login failed";
                            }

                            Console.WriteLine($"Attempting to login with u:{username} and p:{password}");

                            byte[] upbuffer = Encoding.UTF8.GetBytes(html);
                            Console.WriteLine($"Sending: {upbuffer.Length} bytes");
                            response.ContentLength64 = upbuffer.Length;
                            response.OutputStream.Write(upbuffer, 0, upbuffer.Length);
                            response.OutputStream.Close();
                        }
                    }
                }
                
                else
                {
                    string requestInfo = request.RawUrl;
                    byte[] buffer;
                    string path = "../../static" + request.RawUrl;

                    string pattern = "[.].*";
                    Match m = Regex.Match(requestInfo, pattern);
                    string switchingInfo = "";
                    if(m.Length != 0)
                    {
                        switchingInfo = m.Groups[1].Value;
                    }
                    else
                    {
                        switchingInfo = requestInfo;
                    }

                    switch(switchingInfo)
                    {
                    case "html":
                        buffer = File.ReadAllBytes($"{path}/index.html");
                        response.ContentType = "text/html";
                        break;
                    case "/":
                        buffer = File.ReadAllBytes($"{path}/index.html");
                        response.ContentType = "text/html";
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

                    Console.WriteLine($"Sending: {buffer.Length} bytes");
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.OutputStream.Close();
                }
            }
        }
    }
}
