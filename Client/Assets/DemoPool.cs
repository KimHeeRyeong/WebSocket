using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoPool : MonoBehaviour
{
    public InputField objMsg;
    public Scrollbar sb;
    Text tx;
    RectTransform rc;
    bool message;
    public NetworkPool net;
    public List<string> serverMsg = new List<string>();//socket의 스레드를 직접 건드리면 안되기때문에 변수에 값을 넣어놓고 unity update에서 실행
    void Awake()
    {
        tx = this.GetComponent<Text>();
        rc = this.GetComponent<RectTransform>();
        message = false;
    }
    public void SendMessageButton() {
        rc.sizeDelta += new Vector2(0, 21);
        sb.value = 0;

        net.SendMsg(objMsg.text);
        objMsg.text = "";
        objMsg.ActivateInputField();
        
    }
    public void AddMsg(string msg) {
        lock (serverMsg)
        {
            serverMsg.Add(msg);
            message = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (message)
        {
            message = false;
            List<string> msg = null;
            lock (serverMsg)
            {
                msg = new List<string>(serverMsg);
                serverMsg.Clear();
            }
            if (msg != null)
            {
                int cnt = msg.Count;
                for (int i = 0; i < cnt; i++)
                {
                    rc.sizeDelta += new Vector2(0, 21);
                    sb.value = 0;
                    tx.text += msg[i] + "\n";
                }
            }
        }
    }
}
