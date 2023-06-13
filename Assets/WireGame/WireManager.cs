using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireManager : MonoBehaviour
{
    public static WireManager instance;
    public List<Wire> wire;

    [SerializeField]
    private Text gameOver;

    [SerializeField]
    private Text WinUI;

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
        WinUI.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
}