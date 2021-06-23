using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DroneTelloCommunication.TelloCommunication
{
    public class TelloCommunication
    {
        private IPEndPoint _ipEndPoint;
        private UdpClient UdpClient { get; set; }
        private int ConnectionTimeoutSec { get; set; }
        private ConcurrentQueue<string> SendQueue { get; set; }

        private bool _isConnected;
        private Thread _receivingThread;
        private Thread _sendingThread;

        #region Constructor
        public TelloCommunication(string ip, int port, int connectionTimeoutSec)
        {
            SendQueue = new ConcurrentQueue<string>();
            var ipAddress = IPAddress.Parse(ip);
            _ipEndPoint = new IPEndPoint(ipAddress, port);
            ConnectionTimeoutSec = connectionTimeoutSec;
        }
        #endregion Constructor

        #region Methods
        public void StartCommunication()
        {
            UdpClient = new UdpClient();
            UdpClient.Connect(_ipEndPoint);
            /*
            var connectAsyncResult = UdpClient.BeginSend(.BeginConnect(IP, Port, null, null);
            var waitResult = connectAsyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(ConnectionTimeoutSec));

            if (!waitResult || !TcpClient.Connected)
            {                
                ErrorEvent(this, new CommunicationAppErrorEventArgs("Não foi possível conectar ao System 5. Verifique se o mesmo está em execução.", null));
                return false;
            }

            UdpClient.Client.EndConnect(connectAsyncResult);
            */

            _isConnected = true;
            //UdpClient.Client.ReceiveBufferSize = 1024;
            //UdpClient.Client.SendBufferSize = 1024;

            _receivingThread = new Thread(ReceivingThreadMethod) { IsBackground = true };
            _receivingThread.Start();

            _sendingThread = new Thread(SendingThreadMethod) { IsBackground = true };
            _sendingThread.Start();
            
        }

        public string DequeueCommandToSend()
        {
            if (SendQueue.IsEmpty)
                return null;

            SendQueue.TryDequeue(out string commandToSend);
            if (commandToSend == null)
                return null;

            return commandToSend;
        }

        public void EnqueueCommandToSend(string commandToSend)
        {
            if (commandToSend != null)
                SendQueue.Enqueue(commandToSend);
        }
        #endregion Methods

        #region Threads Methods 
        private void ReceivingThreadMethod()
        {
            while (_isConnected)
            {
                if (UdpClient.Available > 0)
                {
                    try
                    {
                        var receiveBytes = UdpClient.Receive(ref _ipEndPoint);
                        string receiveMsg = Encoding.ASCII.GetString(receiveBytes);
                        //_communicationService.OnReceiveCommand(receivedData);                        
                    }
                    catch (Exception ex)
                    {
                        //ErrorEvent(this, new CommunicationAppErrorEventArgs("Erro ao receber dados", ex));

                        Thread.Sleep(50);
                    }
                }

                Thread.Sleep(30);
            }
        }

        private void SendingThreadMethod()
        {
            while (_isConnected)
            {
                var commandToSend = this.DequeueCommandToSend();

                if (!string.IsNullOrEmpty(commandToSend))
                {
                    try
                    {
                        Byte[] sendBytes = Encoding.UTF8.GetBytes(commandToSend);
                        UdpClient.Send(sendBytes, sendBytes.Length);
                        break; //Sai do While                            
                    }
                    catch (Exception ex)
                    {
                        //CommunicationErrorEvent(this, new CommunicationErrorEventArgs("Máximo de tentativas de envio alcançado.", ex));                            
                        Thread.Sleep(50);
                    }
                }

                Thread.Sleep(30);
            }
        }
        #endregion Threads Methods 
    }
}
