﻿using System;
using System.Threading;
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
            _rpc = new RPC(port, procedures);
            Console.ReadKey();
            var portToConnect = int.Parse(args[1]);
            _rpc.Connect(portToConnect);

            Thread thread = new Thread(_rpc.GetRequest);
            thread.IsBackground = true;
            thread.Start();
        }
    }
}