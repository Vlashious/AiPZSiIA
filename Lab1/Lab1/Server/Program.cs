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
            var server = new UdpClient(4000);
            var helloString = Encoding.UTF8.GetBytes("Hello Client!");
            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, 4000);
                var data = server.Receive(ref remoteEP);
                Console.WriteLine($"<-- {remoteEP}: {data}");
                server.Send(helloString, helloString.Length, remoteEP);
            }
        }
    }
}