using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace WpfApp1
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow mw;
        public Window1(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    mw.tcp = new TcpClient(ipAddr.Text, Convert.ToInt32(port.Text));
                    if (!mw.tcp.Connected)
                    {
                        MessageBox.Show("Connection Failed");
                        Close();
                    }
                    else
                    {
                        Close();
                    }
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
