using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ShadowFN
{
    internal class Listener
    {
        private static int _port = 5595;
        private static bool _isInUse = false;

        public static void Listen()
        {
            foreach (string commandLineArg in Environment.GetCommandLineArgs())
            {
                if (commandLineArg.Contains("port"))
                    Listener._port = int.Parse(commandLineArg.Split('=')[1]);
            }
            try
            {
                using (new TcpClient("127.0.0.1", Listener._port))
                    Listener._isInUse = true;
            }
            catch
            {
            }
            if (Listener._isInUse)
            {
                Console.WriteLine("Port is in use!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            HttpListener httpListener = new HttpListener();
            try
            {
                httpListener.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", (object)Listener._port));
                httpListener.Start();
                Console.WriteLine(string.Format("Listening On Port {0}", (object)Listener._port));
            }
            catch
            {
                Console.WriteLine("Run program as admin!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system")
                {
                    string s = JsonConvert.SerializeObject((object)new
                    {
                        uniqueFilename = "3460cbe1c57d4a838ace32951a4d7171",
                        filename = "DefaultGame.ini",
                        hash = "603E6907398C7E74E25C0AE8EC3A03FFAC7C9BB4",
                        hash256 = "973124FFC4A03E66D6A4458E587D5D6146F71FC57F359C8D516E0B12A50AB0D9",
                        length = System.IO.File.ReadAllText("DefaultGame.ini").Length,
                        contentType = "application/octet-stream",
                        uploaded = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'"),
                        storageType = "S3",
                        doNotCache = false
                    });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    context.Response.ContentLength64 = (long)Encoding.UTF8.GetBytes(s).Length;
                    context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
                }
                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system/config")
                {
                    string s = JsonConvert.SerializeObject((object)new
                    {
                    });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    context.Response.ContentLength64 = (long)Encoding.UTF8.GetBytes(s).Length;
                    context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
                }
                if (context.Request.Url.PathAndQuery.Contains("/fortnite/api/cloudstorage/user"))
                {
                    string s = JsonConvert.SerializeObject((object)new
                    {
                    });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    context.Response.ContentLength64 = (long)Encoding.UTF8.GetBytes(s).Length;
                    context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
                }
                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system/3460cbe1c57d4a838ace32951a4d7171")
                {
                    byte[] buffer = System.IO.File.ReadAllBytes("DefaultGame.ini");
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.StatusCode = 200;
                    context.Response.ContentLength64 = (long)buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}