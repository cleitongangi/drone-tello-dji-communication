using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TelloSdkCoreNet
{
    public class TelloUdpClient
    {

        private UdpClient _client;
        private IPAddress _ipaddress;
        private IPEndPoint _endpoint;
        private IPEndPoint _remoteIpEndPoint;
        private string _serverReponse;
        private bool _commandMode = false;

        public string Host => _ipaddress.ToString();
        public TelloUdpClient(IPAddress ipaddress, IPEndPoint endpoint)
        {
            _client = new UdpClient();
            _ipaddress = ipaddress;
            _endpoint = endpoint;
            _remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        public string ServerResponse => _serverReponse;
        public bool CommandModeEnabled => _commandMode;
        public SdkWrapper.SdkReponses SendMessage(actions.Action action)
        {
            if (action.Type == actions.Action.ActionTypes.CommandMode && _commandMode)
            {
                return SdkWrapper.SdkReponses.OK;
            }
            if (_client == null)
            {
                return SdkWrapper.SdkReponses.FAIL;
            }
            _client.Connect(_endpoint);
            var data = Encoding.ASCII.GetBytes(action.Command);
            _client.Send(data, data.Length);
            _client.Client.ReceiveTimeout = 2500;
            var receiveBytes = _client.Receive(ref _remoteIpEndPoint);
            _serverReponse = Encoding.ASCII.GetString(receiveBytes);

            if (action.Type == actions.Action.ActionTypes.Read)
            {
                return _serverReponse == "FAIL" ? SdkWrapper.SdkReponses.FAIL
                                                : SdkWrapper.SdkReponses.OK;
            }
            if (action.Type == actions.Action.ActionTypes.CommandMode && _serverReponse == "OK")
            {
                _commandMode = true;
            }
            return _serverReponse == "OK" ? SdkWrapper.SdkReponses.OK
                                          : SdkWrapper.SdkReponses.FAIL;
        }

        public void Close()
        {
            _client.Close();
            _client.Dispose();
        }
    }
}
