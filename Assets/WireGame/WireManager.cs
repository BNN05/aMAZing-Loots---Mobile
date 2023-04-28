using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    public List<Wire> wire;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CheckIfAllCompleted()
    {
        int compltednb = 0;
        foreach (Wire wire in wire)
        {
            if (wire.IsCompleted)
                compltednb++;
        }
        if (compltednb >= wire.Count)
            Win();
    }

    private void Win()
    {
        Debug.Log("Win");
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}