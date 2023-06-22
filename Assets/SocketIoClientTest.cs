using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class SocketIoClientTest : MonoBehaviour
{
    public static SocketIoClientTest Instance;
    [SerializeField]
    private string host;
    [SerializeField]
    private string RoomId;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
    }
    void Start()
    {
        ConnectToServer();
    }
    private void ParseCode(string code)
    {
        code = code.Remove(0, "{\"roomId\":\"".Count());
        RoomId = code.Remove(7);
    }
    private async void ConnectToServer()
    {
        var client = new SocketIO(host);

        client.On("message", response => {
            if (response.ToString().Contains("roomId"))
            {
                ParseCode(response.GetValue<string>());
            }
        });
        await client.ConnectAsync();

        
    }

}

internal class TestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}