using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pippi_AsyncSocketLib.Model
{
   public class ClientChat
   {
        public string Nickname { get; set; }

        public TcpClient Client { get; set; }
   }
}
