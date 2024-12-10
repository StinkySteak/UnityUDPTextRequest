using LiteNetLib;

namespace StinkySteak.Networking
{
    public class UDPTextServer
    {
        private int _listenPort = 7000;
        private EventBasedNetListener _listener;
        private NetManager _server;
        private string _content;

        public void SetContent(string content)
        {
            _content = content;
        }

        public UDPTextServer(int listenPort)
        {
            _listenPort = listenPort;
        }

        public void Start()
        {
            _listener = new EventBasedNetListener();
            _server = new NetManager(_listener);
            _server.Start(_listenPort);

            _listener.ConnectionRequestEvent += ConnectionRequestEvent;
            _listener.PeerConnectedEvent += PeerConnectedEvent;
        }


        private void PeerConnectedEvent(NetPeer peer)
        {
            peer.Disconnect(System.Text.Encoding.UTF8.GetBytes(_content));
        }

        public void PollUpdate()
        {
            _server.PollEvents();
        }

        public void Stop()
        {
            _server.Stop();
        }

        private void ConnectionRequestEvent(ConnectionRequest request)
        {
            request.Accept();
        }
    }
}