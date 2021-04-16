using Pippi_AsyncSocketLib;
using Pippi_AsyncSocketLib.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pippi_Chat_Client
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AsyncSocketClient Cliente = new AsyncSocketClient();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Cliente.SerServerIPAddress(txt_ip.Text);
            Cliente.SetServerPort(txt_porta.Text);

            Cliente.ConnettiAlServer();

            Cliente.invia(txt_nickname.Text);
            

            Chat app2 = new Chat(Cliente);
            app2.Show();
            
            this.Hide();

        }
    }
}
