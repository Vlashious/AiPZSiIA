using System;
using System.Threading.Tasks;
using Library;

namespace Client1
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

            while (true)
            {
                var action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 0:
                        _rpc.Send(new RPC.Request
                        {
                            Id = 1,
                            Method = "Kek",
                            Parameters = new object[] {"kek"}
                        });
                        break;
                    case 1:
                        _rpc.Send(new RPC.Request
                        {
                            Id = 2,
                            Method = "StringConcat",
                            Parameters = new object?[] {"Hello, ", "world!"}
                        });
                        break;
                }
            }

            Console.ReadKey();
        }

        static void HandleResponse(RPC.Response response)
        {
            Console.WriteLine($"Response handled.");
        }
    }
}