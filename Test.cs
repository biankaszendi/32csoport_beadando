using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

public class ClientProgram
{
    private static readonly string SERVER = "127.0.0.1";
    private static readonly int PORT = 3333;

    public static void Main(string[] args)
    {
        ObjectInputStream input = null;
        ObjectOutputStream output = null;
        try
        {
            using (Socket client = new Socket(SERVER, PORT))
            {
                Console.WriteLine("Connected to server: " + client.RemoteEndPoint);
                input = new ObjectInputStream(client.InputStream);
                output = new ObjectOutputStream(client.OutputStream);

                new Thread(new MessageHandler(input)).Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            try
            {
                input ? .Close();
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine("Error during inputstream closing!");
            }
            try
            {
                output ? .Close();
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine("Error during outputstream closing!");
            }
        }
    }

    private void Send(Message msg, ObjectOutputStream output)
    {
        // Implementation of send method goes here
    }

    private class MessageHandler : Runnable
    {
        private readonly ObjectInputStream input;

        public MessageHandler(ObjectInputStream input)
        {
            this.input = input;
        }

        public void Run()
        {
            // Implementation of run method goes here
        }
    }
}
