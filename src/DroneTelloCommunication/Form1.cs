using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroneTelloCommunication
{
    public partial class Form1 : Form
    {
        IPEndPoint _endpoint;
        UdpClient _udpClient;
        IPEndPoint _remoteIpEndPoint;
        bool isConected;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ipAddress = IPAddress.Parse("192.168.10.1");

            _endpoint = new IPEndPoint(ipAddress, 8889);
            _udpClient = new UdpClient();
            _remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            _udpClient.Connect(_endpoint);
            _udpClient.Client.ReceiveTimeout = 2500;
            isConected = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isConected)
            {
                txtResultado.Text = "Conect at drone first!";
                return;
            }

            SendData(txtComando.Text);
        }

        private void btnImportarArquivo_Click(object sender, EventArgs e)
        {
            txtResultado.Clear();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!isConected)
                    btnConectar_Click(null, null);

                string command = null;

                try
                {
                    using (var fileStream = openFileDialog1.OpenFile())
                    {
                        SendData("command");

                        if (fileStream != null)
                        {
                            var reader = new StreamReader(fileStream);

                            while ((command = reader.ReadLine()) != null)
                            {
                                if (command.IndexOf("__wait ") != -1)
                                {
                                    var waitTimeMs = int.Parse(command.Substring(command.IndexOf(" ") + 1, command.Length - command.IndexOf(" ") - 1));
                                    txtResultado.Text += string.Format("Wait {0} ms;{1}", waitTimeMs, Environment.NewLine);
                                    System.Threading.Thread.Sleep(waitTimeMs);
                                }
                                else
                                    SendData(command);
                            }
                        }
                    }

                    if (command != null && command != "land")
                        SendData("land");
                }
                catch (Exception ex)
                {
                    txtResultado.Text += string.Format("{0} => Error: {1};", command, ex.Message);
                    MessageBox.Show("Error when processing file information.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _udpClient.Close();
                    _udpClient.Dispose();
                }
            }
        }

        private void SendData(string command)
        {
            var data = Encoding.ASCII.GetBytes(command);
            _udpClient.Send(data, data.Length);
            var receiveBytes = _udpClient.Receive(ref _remoteIpEndPoint);
            txtResultado.Text += string.Format("{0} => Resp: {1};{2}", command, Encoding.ASCII.GetString(receiveBytes), Environment.NewLine);
            
            //txtResultado.Text += string.Format("{0} => Resp: Teste;{1}", command, Environment.NewLine);
        }
    }
}
