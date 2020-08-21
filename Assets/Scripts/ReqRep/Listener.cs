using System;
using NetMQ;
using NetMQ.Sockets;

namespace ReqRep
{
    public class Listener
    {
        private readonly string _host;
        private readonly string _port;
        private readonly Action<string> _messageCallback;

        public Listener(string host, string port, Action<string> messageCallback)
        {
            _host = host;
            _port = port;
            _messageCallback = messageCallback;
        }

        public void RequestMessage()
        {
            string message;
            AsyncIO.ForceDotNet.Force();
            using (var socket = new RequestSocket())
            {
                socket.Connect($"tcp://{_host}:{_port}");
                socket.SendFrame("Hello");
                message = socket.ReceiveFrameString();
            }

            NetMQConfig.Cleanup();
            _messageCallback(message);
        }
    }
}