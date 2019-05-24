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

namespace WpfApp1
{
    /// <summary>
    /// Window2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window2 : Window
    {
        MainWindow mw;
        public Window2(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            String packet = "";
            Convert.ToInt32(Addr.Text);
            packet += "0006" + Convert.ToInt32(SlaveID.Text).ToString("X").PadLeft(2, '0') +
                (Function.SelectedIndex + 1).ToString("X").PadLeft(2, '0') + 
                Convert.ToInt32(Addr.Text).ToString("X").PadLeft(4, '0') +
                Convert.ToInt32(Quantity.Text).ToString("X").PadLeft(4, '0');
            mw.packet = packet;
            mw.modLen = 2 * Convert.ToInt32(Quantity.Text) + 9;
            mw.addr = Convert.ToInt32(Addr.Text);
            mw.SendThread();
            this.Close();
        }
    }
}
