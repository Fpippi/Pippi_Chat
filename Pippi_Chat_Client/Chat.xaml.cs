using Pippi_AsyncSocketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pippi_Chat_Client
{
    /// <summary>
    /// Logica di interazione per Chat.xaml
    /// </summary>
    public partial class Chat : Window
    {
        AsyncSocketClient Client;
        public Chat(AsyncSocketClient client)
        {
            InitializeComponent();
            Client = client;
            Client.OnNewMessage += Client_OnNewMessage;
        }

        private void Client_OnNewMessage(object sender, EventArgs e)
        {
            //aggiornamento della listbox
            lstMessaggi.ItemsSource = Client.Messaggi;
            lstMessaggi.Items.Refresh();
        }

        private void txt_invio_Click(object sender, RoutedEventArgs e)
        {
            PlaySound_invio();
            Client.invia(txt_invio1.Text);
        }

        private void btn_discontetti_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        void PlaySound_invio()
        {
            var uri = new Uri(@"D:\Scuola\Tpsit\Pippi_Chat\Pippi_Chat_Client\Sound\WhatsApp Sound Original Message.mp3", UriKind.RelativeOrAbsolute);
            var player = new MediaPlayer();

            player.Open(uri);
            player.Play();
        }

    }
}
