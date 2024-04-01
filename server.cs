namespace WebApplication1
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class Server
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            TcpListener server = null;
            try
            {
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 8888;

                server = new TcpListener(ipAddress, port);

                server.Start();

                Console.WriteLine("Server started...");

                byte[] bytes = new byte[1024];
                string data;

                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine($"Received: {data}");

                        data = data.ToUpper();

                        byte[] msg = Encoding.UTF8.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine($"Sent: {data}");
                    }

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                server?.Stop();
            }
        }
    }

}
