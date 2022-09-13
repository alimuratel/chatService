using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatService.Client
{
    public class Client
    {
        public int PORT { get; private set; }
        public Socket Socket { get; private set; }

        public int ID { get; private set; }
        public DateTime LastMessageTime { get; set; }
        public int TryCount { get; set; }

        public Client(int port)
        {
            ID = new Random().Next(0, 100);
            PORT = port;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect()
        {
            while (Socket.Connected == false)
            {
                try
                {
                    Socket.Connect(IPAddress.Loopback, PORT);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("exception: {0}", ex.Message);
                    return;
                }
            }
        }

        public void Listen()
        {
            while (true)
            {
                string message = GetMessage();
                SendMessage(message);
            }
        }

        private void SendMessage(string message)
        {
            CheckMessageGap();
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            Socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            LastMessageTime = DateTime.Now;
        }

        private string GetMessage()
        {
            Console.WriteLine("Type your message: ....");
            return Console.ReadLine();
        }

        private void CheckMessageGap()
        {
            DateTime now = DateTime.Now;
            if (now.Second == LastMessageTime.Second)
            {
                Console.WriteLine("You can only send 1 message per second!");
                TryCount++;
                if (TryCount > 1)
                {
                    Exit();
                }
                return;
            }
        }

        private void Exit()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
            Environment.Exit(0);
        }
    }
}
