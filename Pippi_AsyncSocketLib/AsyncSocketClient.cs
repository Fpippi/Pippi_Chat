using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pippi_AsyncSocketLib
{
    public class AsyncSocketClient
    {
        IPAddress mServerIpAddress;
        int mServerPort;
        TcpClient mClient;
        public IPAddress ServerIpAddress
        {
            get
            {
                return mServerIpAddress;
            }



        }
        public int ServerPort
        {
            get
            {
                return mServerPort;
            }
        }

        public AsyncSocketClient()
        {
            mServerIpAddress = null;
            mServerPort = -1;
            mClient = null;
        }

        public bool SerServerIPAddress(string Str_IPAddress)
        {
            IPAddress ipadr = null;
            if (!IPAddress.TryParse(Str_IPAddress, out ipadr))
            {
                Console.WriteLine("ip non valido");
                return false;
            }
            mServerIpAddress = ipadr;
            return true;
        }

        public bool SetServerPort(string str_port)
        {
            int port = -1;
            if (!int.TryParse(str_port,out port))
            {
                Console.WriteLine("Porta non valida");
                return false;            
            }
            if (port<0||port >65535)
            {
                Console.WriteLine("La porta deve essere compressa tra 0 e 65535");
                return false;
            }
            mServerPort = port;
            return true;
        }

        public async Task ConnettiAlServer()
        {
            if (mClient==null)
            {
                mClient = new TcpClient();
            }
            try
            {
                await mClient.ConnectAsync(ServerIpAddress, mServerPort);
                Console.WriteLine("Connesso al server ip/port: {0} {1}",
                    mServerIpAddress.ToString(),mServerPort);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            
        }

        private async Task RiceviMessaggi()
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            try
            {
                stream = mClient.GetStream();
                reader = new StreamReader(stream);
                char[] buff = new char[512];
                int nbytes = 0;

                while (true)
                {
                    nbytes = await reader.ReadAsync(buff, 0, buff.Length);
                    if (nbytes == 0)
                    {
                        Console.WriteLine("Disconesso.");
                        break;
                    }
                    string recvMessage = new string(buff, 0, nbytes);
                    Console.WriteLine(recvMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void invia(string messagio)
        {
            if (mClient==null)
            {
                return;
            }
            if (!mClient.Connected)
            {
                return;
            }
            try
            {
                byte[] buff = Encoding.ASCII.GetBytes(messagio);
                mClient.GetStream().WriteAsync(buff, 0, buff.Length);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
