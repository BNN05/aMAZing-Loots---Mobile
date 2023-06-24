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
        Debug.Log("Finito");
    }

    public void Win()
    {
        Debug.Log("Win");
    }

}
