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
            _rpc = new RPC(port, null);
            Console.ReadKey();
            var portToConnect = int.Parse(args[1]);
            _rpc.Connect(portToConnect);
            
            RemoteAdd(5, 10);
        }

        static void RemoteAdd(int a, int b)
        {
            var request = new RPC.Request()
            {
                Id = 1,
                Method = "Add",
                Parameters = new object?[] {1, 2}
            };
            
            _rpc.Send(request);

            var response = _rpc.GetResponse();
        }
    }
}