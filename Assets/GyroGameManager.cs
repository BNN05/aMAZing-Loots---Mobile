using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GyroGameManager : MonoBehaviour
{
    public static GyroGameManager instance;

    [SerializeField]
    private Text GameOverUI;

    [SerializeField]
    private Text WinUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        GameOverUI.gameObject.SetActive(false);
        WinUI.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverUI.gameObject.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        WinUI.gameObject.SetActive(true);
    }
}