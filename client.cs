namespace WebApplication1
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    class Client
    {
        static void Main(string[] args)
        {
            StartClient();
        }

        static void StartClient()
        {
            string ipAddress = "127.0.0.1";
            int port = 8888;

            try
            {
                TcpClient client = new TcpClient(ipAddress, port);

                NetworkStream stream = client.GetStream();

                byte[] data = new byte[1024];

                Console.Write("Enter message: ");
                string message = Console.ReadLine();

                data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);

                data = new byte[1024];

                int bytes = stream.Read(data, 0, data.Length);
                string responseData = Encoding.UTF8.GetString(data, 0, bytes);

                Console.WriteLine($"Received: {responseData}");

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }

}
