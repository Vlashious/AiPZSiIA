using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Library
{
    public class RPC
    {
        // JSON-RPC protocol
        [Serializable]
        public record Request
        {
            public string Method { get; set; }
            public object?[] Parameters { get; set; }
            public int Id { get; set; }
        }

        [Serializable]
        public record Response
        {
            public object? Result { get; set; }
            public string Error { get; set; }
            public int Id { get; set; }
        }

        private TcpClient _client;
        private TcpListener _listener;
        private IPEndPoint _remoteIP;

        private object _procedureObject;

        public RPC(int port, object objectWithProcedures)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            _listener.Start();
            Console.WriteLine($"Listening on port {port}...");

            _procedureObject = objectWithProcedures;
        }

        public void Connect(int portToConnect)
        {
            _client = new TcpClient();
            _client.Connect(IPAddress.Parse("127.0.0.1"), portToConnect);
            Console.WriteLine("Successfully connected!");
        }

        public void Send(object objToSend)
        {
            var stream = _client.GetStream();

            var json = JsonConvert.SerializeObject(objToSend);
            var data = Encoding.UTF8.GetBytes(json);
            ReadOnlySpan<byte> span = data;
            stream.Write(span);
            Console.WriteLine($"--> {objToSend}");
        }

        public Response GetResponse()
        {
            var client = _listener.AcceptSocket();
            while (client.Available == 0)
            {
                
            }

            var span = new Span<byte>(new byte[client.Available]);
            client.Receive(span);
            var json = Encoding.UTF8.GetString(span);
            var response = JsonConvert.DeserializeObject<Response>(json);
            Console.WriteLine($"<-- {response}");

            return response;
        }

        public void GetRequest()
        {
            var client = _listener.AcceptSocket();
            while (client.Available == 0)
            {
            }

            ;
            var span = new Span<byte>(new byte[client.Available]);

            client.Receive(span);
            if (span.Length > 0)
            {
                var json = Encoding.UTF8.GetString(span);
                var request = JsonConvert.DeserializeObject<Request>(json);
                Console.WriteLine($"<-- {request}");
                var methodToCall = _procedureObject.GetType().GetMethod(request.Method);
                var result = methodToCall.Invoke(_procedureObject, request.Parameters);

                Send(new Response()
                {
                    Error = "",
                    Id = request.Id,
                    Result = result
                });
            }
        }
    }
}