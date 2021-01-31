using System;
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

        private Action<Response> _responseHandler;

        private TcpClient _client;
        private TcpListener _listener;

        private object _procedureObject;

        public RPC(int port, object objectWithProcedures, Action<Response> responseHandler)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            _listener.Start();
            Console.WriteLine($"Listening on port {port}...");

            _procedureObject = objectWithProcedures;
            _responseHandler = responseHandler;
        }

        ~RPC()
        {
            _client.Dispose();
            _listener.Stop();
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

        public void Listen()
        {
            while (true)
            {
                var client = _listener.AcceptSocket();
                while (client.Available == 0)
                {
                    
                }

                var span = new Span<byte>(new byte[client.Available]);
                client.Receive(span);
                var json = Encoding.UTF8.GetString(span);
                try
                {
                    var request = JsonConvert.DeserializeObject<Request>(json);
                    Console.WriteLine($"<-- {request}");
                    HandleRequest(request);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Is not a request.");
                }

                try
                {
                    var response = JsonConvert.DeserializeObject<Response>(json);
                    Console.WriteLine($"<-- {response}");
                    _responseHandler(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Is not a response.");
                }
            }
        }

        private void HandleRequest(Request request)
        {
            var methodToCall = _procedureObject.GetType().GetMethod(request.Method);
            var result = methodToCall.Invoke(_procedureObject, request.Parameters);

            var response = new Response
            {
                Id = request.Id,
                Error = "",
                Result = result
            };
            
            Send(response);
        }
    }
}