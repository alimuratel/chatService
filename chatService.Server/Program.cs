using System;

namespace ChatService.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Server";

            Console.WriteLine("Server Açılıyor.");
            var server = new Server(100);
            server.StartServer();
            Console.WriteLine("Server Açıldı.");

            Console.WriteLine("Server'ı kapatmak için herhangi bir düğmeye basabilirsin.");
            Console.ReadLine();
        }
    }
}
