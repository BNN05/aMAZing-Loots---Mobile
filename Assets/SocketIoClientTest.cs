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
    public async void ConnectToServer()
    {
        var client = new SocketIO(host);

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

    private void ManageMapData(string mapJson)
    {
        
    }

    private void RotateModule(string module)
    {

    }
    private void LeverActivate(string lever)
    {

    }
    private void GameOver(string gameOver)
    {
        if(gameOver == "true")
        {

        }
    }

}

internal class TestDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}