using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Data> recList;
        Window con;
        public TcpClient tcp;
        public String packet;
        public class Data
        {
            public int Address { get; set; }
            public int Value { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            con = null;
            con = new Window1(this);
            con.ShowDialog();
        }

        private void Setting(object sender, RoutedEventArgs e)
        {
            con = null;
            con = new Window2(this);
            con.ShowDialog();
        }
        Thread T1;
        public void SendThread()
        {
            T1 = new Thread(new ThreadStart(SendTCP));
            T1.Start();
        }
        int tID = 0;
        public int modLen;
        public int addr;
        private void SendTCP()
        {
            while (true)
            {
                recList.Clear();
                recList = new List<Data>();
                string temp = tID.ToString("X").PadLeft(4, '0') + "0000" + packet;
                byte[] sendBuff = new byte[temp.Length / 2];
                for (int i = 0; i < temp.Length; i += 2)
                {
                    sendBuff[i / 2] = byte.Parse("" + temp[i] + temp[i + 1], System.Globalization.NumberStyles.HexNumber);
                }
                NetworkStream st = tcp.GetStream();
                st.Write(sendBuff, 0, sendBuff.Length);
                st.Flush();
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                    PacketView.AppendText("[송신] " + temp + '\n');
                }));
                byte[] recBuff = new byte[modLen];
                st.Read(recBuff, 0, recBuff.Length);
                string msg = "";
                for (int i = 0; i < recBuff.Length; i++)
                {
                    msg += recBuff[i].ToString("X").PadLeft(2, '0') + " ";
                }
                string bToS;
                for (int i = 9; i + 2 < recBuff.Length; i+=2)
                {
                    bToS = "";
                    bToS += recBuff[i].ToString("X") +recBuff[i + 1].ToString("X");
                    recList.Add(new Data { Address = addr++, Value = int.Parse(bToS, System.Globalization.NumberStyles.HexNumber) });
                }
                DataView.a
                msg += '\n';
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate {
                    PacketView.AppendText("[수신] " + msg);
                }));
                tID++;
                Thread.Sleep(1000);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            T1.Abort();
        }
    }
}
