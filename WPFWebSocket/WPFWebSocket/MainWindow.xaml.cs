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
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WPFWebSocket
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        const int PORT = 9999;

        WebSocketServer webSocket;

        public MainWindow()
        {
            InitializeComponent();
            webSocket = new WebSocketServer(PORT);
            webSocket.AddWebSocketService<Echo>("/Echo");//이 주소로 접속하면 Echo실행
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!webSocket.IsListening)
            {
                webSocket.Start();
                btStart.Content = "Stop Server";
            }
            else
            {
                webSocket.Stop();
                btStart.Content = "Start Server";
            }
        }
    }
}
