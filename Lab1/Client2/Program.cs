using System;
using System.Threading.Tasks;
using Library;

namespace Client2
{
    class Program
    {
        private static RPC _rpc;
        static void Main(string[] args)
        {
            var procedures = new Procedures();
            var port = int.Parse(args[0]);
            _rpc = new RPC(port, procedures, HandleResponse);
            Console.ReadKey();
            var portToConnect = int.Parse(args[1]);
            _rpc.Connect(portToConnect);

            Task.Run(() => _rpc.Listen());
            
            _rpc.Send(new RPC.Request
            {
                Id = 100,
                Method = "Add",
                Parameters = new object[] {10, 20}
            });

            Console.ReadKey();
        }

        static void HandleResponse(RPC.Response response)
        {
            Console.WriteLine($"Response handled.");
        }
    }
}