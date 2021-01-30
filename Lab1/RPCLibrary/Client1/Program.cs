using System;
using Library;

namespace Client1
{
    class Program
    {
        private static RPC _rpc;
        static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            _rpc = new RPC(port);
            Console.ReadKey();
            var portToConnect = int.Parse(args[1]);
            _rpc.Connect(portToConnect);

            var data = _rpc.Receive();
            if()
        }

        static int Add(int a, int b)
        {
            
        }
    }
}