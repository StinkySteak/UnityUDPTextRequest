using LiteNetLib;

namespace StinkySteak.Networking
{
    public class UDPTextClient
    {
        private int _serverPort;
        private string _url;
        private NetManager _client;
        private EventBasedNetListener _listener;

        private string _text;
        private bool _isDone;
        private bool _isError;
        public string Text => _text;
        public bool IsDone => _isDone;
        public bool IsError => _isError;

        private const int LiteNetLibPacketOffset = 9;

        public UDPTextClient(string url, int serverPort)
        {
            _serverPort = serverPort;
            _url = url;
        }

        public void SendRequest()
        {
            _listener = new EventBasedNetListener();
            _client = new NetManager(_listener);
            _client.Start();
            _client.Connect(_url, _serverPort, string.Empty);

            _listener.PeerDisconnectedEvent += PeerDisconnectedEvent;
        }

        private void PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            bool isSuccess = disconnectInfo.Reason == DisconnectReason.RemoteConnectionClose;

            if (!isSuccess)
            {
                _isError = true;
                return;
            }

            byte[] raw = disconnectInfo.AdditionalData.RawData;
            int userDataSize = disconnectInfo.AdditionalData.UserDataSize;

            _text = System.Text.Encoding.UTF8.GetString(raw, LiteNetLibPacketOffset, userDataSize);

            _isDone = true;
        }

        public void PollUpdate()
        {
            _client?.PollEvents();
        }

        private void Clear()
        {
            _serverPort = 0;
            _url = string.Empty;
            _listener = null;
            _client = null;
            _isError = false;
            _isDone = false;
        }
    }
}