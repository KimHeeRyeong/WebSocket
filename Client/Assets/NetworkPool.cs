using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class NetworkPool : MonoBehaviour
{
    WebSocket websocket;
    public DemoPool demo;
    // Start is called before the first frame update
    void Awake()
    {
        websocket = new WebSocket("ws://localhost:9999/Echo");
        websocket.OnOpen += OnOpen;
        websocket.OnMessage += OnMessage;//서버가 나한테 메세지 보낸 경우
        websocket.OnClose += OnClose;
        websocket.OnError += OnError;
        websocket.Connect();
    }
    public void OnOpen(object sender, EventArgs e) {
        demo.AddMsg("접속 완료");
    }
    public void OnMessage(object sender, MessageEventArgs e) {
        demo.AddMsg(e.Data);
    }
    public void OnClose(object sender,CloseEventArgs e) {
        demo.AddMsg("접속 해제");
    }
    public void OnError(object sender, ErrorEventArgs e) {

    }

    public void SendMsg(string msg)
    {

        websocket.Send(msg);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
