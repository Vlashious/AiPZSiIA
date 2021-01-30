using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 4000);
            client.Connect(ep);

            client.Send(new byte[] {1, 2, 3}, 3);

            var answer = client.Receive(ref ep);
            
            Console.WriteLine($"<-- {Encoding.UTF8.GetString(answer)}");
        }
    }
}