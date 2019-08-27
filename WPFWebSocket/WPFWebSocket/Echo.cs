using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace WPFWebSocket
    //Echo는 유저 1개당 1개씩 생성됨
{
    public class Echo : WebSocketBehavior//외부에서 메세지를 보낼때 반응
    {
        static List<Echo> listUser = new List<Echo>();
        string nick = "";
        protected override void OnOpen() {//유저가 접속했을때
            listUser.Add(this);    
        }
        protected override void OnMessage(MessageEventArgs e) {
            lock (listUser)
            {
                if (nick.Length == 0)
                {
                    nick = e.Data;
                    this.Send("닉네임" + nick + "님이 접속했습니다.");

                    int count = listUser.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (listUser[i] != this)
                        {
                            listUser[i].Send(nick + "님이 접속했습니다.");
                        }
                    }
                }
                else
                {
                    int count = listUser.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (listUser[i].nick.Length != 0)
                        {
                            listUser[i].Send(nick + ":" + e.Data);
                        }
                    }
                }
            }
        }
        protected override void OnClose(CloseEventArgs e){//유저가 나갔을때
            lock (listUser)//블로킹
            {
                int count = listUser.Count;
                for (int i = 0; i < count; i++)
                {
                    if (listUser[i] != this)
                    {
                        listUser[i].Send(nick + "님이 나갔습니다.");
                    }
                }
                listUser.Remove(this);
            }
        }
        protected override void OnError(ErrorEventArgs e) {//에러. 보통 OnClose 호출해 접속을 끊어버림
        }

    }
}
