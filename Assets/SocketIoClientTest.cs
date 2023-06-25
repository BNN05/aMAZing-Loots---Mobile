using SocketIOClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using UnityEditor;
using UnityEngine.Events;

public class SocketIoClientTest : MonoBehaviour
{
    public static SocketIoClientTest Instance;
    [SerializeField]
    private string host;
    [SerializeField]
    private string RoomId;

    SocketIO client;

    [SerializeField]
    public string MapJson { get; private set; }

    public UnityEvent<string> OnRotation = new UnityEvent<string>();
    public UnityEvent<string> OnLever = new UnityEvent<string>();
    public UnityEvent<string> OnGameOver = new UnityEvent<string>();

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
            return;
        Instance = this;
    }
    public async void ConnectToServer()
    {
        client = new SocketIO(host);

        client.On("map-data", response =>
        {
            ManageMapData(response.GetValue<string>());
        });

        client.On("rotate-module", response =>
        {
            RotateModule(response.GetValue<string>());
        });

        client.On("lever-activate", response =>
        {
            LeverActivate(response.GetValue<string>());
        });

        client.On("game-over", response =>
        {
            GameOver(response.GetValue<string>());
        });

        await client.ConnectAsync();   
    }

    public void SendMalus()
    {
        client.EmitAsync("Malus");
    }

    public void SendRotate(string rotation)
    {
        client.EmitAsync("rotate-Module", rotation);
    }

    private void ManageMapData(string mapJson)
    {
        MapJson = mapJson;
    }

    private void RotateModule(string module)
    {
        OnRotation.Invoke(module);
    }
    private void LeverActivate(string lever)
    {
        OnLever.Invoke(lever);
    }
    private void GameOver(string gameOver)
    {
        OnGameOver.Invoke(gameOver);
    }

}

internal class TestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}