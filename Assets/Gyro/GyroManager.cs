using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    public static GyroManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MiniGameManager");
        obj.GetComponent<GameHandler>().EndMiniGame(false);
        //"FINITO"
        Debug.Log("Finito");
    }

    public void Win()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MiniGameManager");
        obj.GetComponent<GameHandler>().EndMiniGame(true);
        Debug.Log("Win");
    }

}
