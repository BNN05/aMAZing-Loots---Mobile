using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketIoClientTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    private async void ConnectToServer()
    {
        var client = new SocketIO("http://localhost:4000/");

        client.On("message", response => {
            Debug.Log(response);
        });

        client.On("hi", response =>
        {
            Debug.Log(response + " Oh yooo");

            string text = response.GetValue<string>();

        });

        client.On("test", response =>
        {
            Debug.Log(response);

            string text = response.GetValue<string>();
            var dto = response.GetValue<TestDTO>(1);

        });

        client.OnConnected += async (sender, e) =>
        {
            await client.EmitAsync("message", "socket.io");

            var dto = new TestDTO { Id = 123, Name = "bob" };
            await client.EmitAsync("register", "source", dto);
        };
        await client.ConnectAsync();
    }
}

internal class TestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}