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

            Console.WriteLine($"{client.ID} bağlanmaya çalışıyor");
            client.Connect();
            Console.WriteLine($"{client.ID} Bağlandı.");

            Console.WriteLine($"{client.ID} Mesaj bekleniyor.");
            client.Listen();
        }


    }
}
