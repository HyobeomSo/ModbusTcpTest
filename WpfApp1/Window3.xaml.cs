﻿using System;
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
    /// Window3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window3 : Window
    {
        MainWindow mw;
        public Window3(MainWindow mw, string addr)
        {
            InitializeComponent();
            this.mw = mw;
            Addr.Text = addr;
            Id.Text = "1";
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            String packet = "";
            Convert.ToInt32(Addr.Text);
            packet += "0006" + Convert.ToInt32(Id.Text).ToString("X").PadLeft(2, '0') +
                (6).ToString("X").PadLeft(2, '0') +
                Convert.ToInt32(Addr.Text).ToString("X").PadLeft(4, '0') +
                Convert.ToInt32(Value.Text).ToString("X").PadLeft(4, '0');
            mw.modLen = 29;
            mw.isWrite = true;
            mw.writePacket = packet;
            this.Close();
        }
    }
}
