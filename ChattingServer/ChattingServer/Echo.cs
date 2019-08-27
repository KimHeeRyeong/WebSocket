using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
namespace ChattingServer
{
    class Echo : WebSocketBehavior
    {
        static List<Echo> clientEcho = new List<Echo>();

        protected override void OnClose(CloseEventArgs e) {
        }
        protected override void OnError(ErrorEventArgs e) {
        }
        protected override void OnMessage(MessageEventArgs e) {
            int cnt = clientEcho.Count;
            for(int i = 0; i < cnt; i++)
            {
                if (clientEcho[i] != this)
                {
                    clientEcho[i].Send(e.Data);
                }
            }
        }
        protected override void OnOpen() {//유저 접속
            clientEcho.Add(this);
        }
    }
}
