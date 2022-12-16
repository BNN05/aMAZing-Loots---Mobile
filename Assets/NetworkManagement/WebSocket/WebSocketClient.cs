using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;
    public string ip;
    public string unrealIp;

    private void Start()
    {
        ws = new WebSocket("ws://172.20.10.2:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }

    private void Update()
    {
        if (ws == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessage("Hello");
        }
    }

    public void TryReconnect()
    {
        ws = new WebSocket("ws://" + ip + ":8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }

    private void SendMessage(string message)
    {
        ws.Send(message + "//" + unrealIp);
    }
}