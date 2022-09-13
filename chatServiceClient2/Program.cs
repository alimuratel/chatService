using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client(100);
            Console.Title = $"The Client #{client.ID}";

            Console.WriteLine($"{client.ID} trying to connect...");
            client.Connect();
            Console.WriteLine($"{client.ID} CONNECTED.");

            Console.WriteLine($"{client.ID} started to listening...");
            client.Listen();
        }


    }
}
