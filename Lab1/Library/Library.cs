using System;
using System.IO;
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
            [JsonRequired] public string Method { get; set; }
            public object?[] Parameters { get; set; }
            [JsonRequired] public int Id { get; set; }
        }

        [Serializable]
        public record Response
        {
            public object? Result { get; set; }
            [JsonRequired] public string Error { get; set; }
            [JsonRequired] public int Id { get; set; }
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
            var client = _listener.AcceptTcpClient();
            while (true)
            {
                while (client.Available == 0)
                {
                }

                var span = new Span<byte>(new byte[client.Available]);
                client.GetStream().Read(span);
                var json = Encoding.UTF8.GetString(span);
                try
                {
                    var request = JsonConvert.DeserializeObject<Request>(json);
                    Console.WriteLine($"<-- {request}");
                    HandleRequest(request);
                    continue;
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
                    continue;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Is not a response.");
                }
            }
        }

        private void HandleRequest(Request request)
        {
            try
            {
                var methodToCall = _procedureObject.GetType().GetMethod(request.Method);
                if (methodToCall == null) throw new Exception($"Method {request.Method} is not found.");
                var result = methodToCall.Invoke(_procedureObject, request.Parameters);

                var response = new Response
                {
                    Id = request.Id,
                    Error = "",
                    Result = result
                };

                Send(response);
            }
            catch (Exception e)
            {
                var response = new Response
                {
                    Id = request.Id,
                    Error = e.Message,
                    Result = null
                };
                
                Send(response);
            }
        }
    }
}