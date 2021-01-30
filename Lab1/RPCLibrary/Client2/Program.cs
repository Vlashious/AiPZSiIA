using System;
using Library;

namespace Client2
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
        }

        static void RemoteAdd(int a, int b)
        {
            var request = new RPC.Request()
            {
                id = 1,
                method = "Add",
                parameters = new int[] {a, b}
            };
            
            
        }
    }
}