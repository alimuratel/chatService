using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatService.Server
{
    public class Server
    {
        public int PORT { get; private set; }
        public Socket Socket { get; private set; }
        public int BufferSize { get; private set; }
        public byte[] Buffer { get; set; }
        public Server(int port)
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            PORT = port;
            BufferSize = 2048;
            Buffer = new byte[BufferSize];
        }

        public void StartServer()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);
            Socket.Bind(endPoint);

            Socket.Listen(0);
            Socket.BeginAccept(Connect, null);
        }

        private void Connect(IAsyncResult result)
        {
            Socket socket;
            try
            {
                socket = Socket.EndAccept(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return;
            }

            socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, Listen, socket);
            Socket.BeginAccept(Connect, null);
        }

        private void Listen(IAsyncResult result)
        {
            Socket current = (Socket)result.AsyncState;
            int received;
            try
            {
                // get the text.
                received = current.EndReceive(result);
            }
            catch (Exception ex)
            {
                current.Close();
                Console.WriteLine("Error: {0}", ex.Message);
                return;
            }

            ShowMessage(received);

            SendInformation(current);

            current.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, Listen, current);
        }

        private void SendInformation(Socket currentSocket)
        {
            byte[] response = Encoding.ASCII.GetBytes("Message successfuly delivered.");
            currentSocket.Send(response);
        }

        public void ShowMessage(int received)
        {
            byte[] recBuf = new byte[received];
            Array.Copy(Buffer, recBuf, received);
            string message = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Message: " + message);
        }
    }
}
    