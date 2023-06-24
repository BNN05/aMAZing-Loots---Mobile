using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject HowToPlayMenu;
    public GameObject ConnectMenu;

    public TMP_Text IpText;

    public string SceneToLoadOnConnect;

    private GameObject OpenGameObject;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConnectButton()
    {
        SoundManager.Instance.PlayClickSound();
        MainMenu.SetActive(false);
        ConnectMenu.SetActive(true);
        OpenGameObject = ConnectMenu;
        Debug.Log("connect");
    }

    public void QuitButton()
    {
        SoundManager.Instance.PlayClickSound();
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        SoundManager.Instance.PlayClickSound();
        MainMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
        OpenGameObject = HowToPlayMenu;
    }

    public void BackToMainMenuButton()
    {
        SoundManager.Instance.PlayClickSound();
        OpenGameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ConnectWithIPButton()
    {
        SoundManager.Instance.PlayClickSound();
        string ip = IpText.text;
        SceneManager.LoadScene(SceneToLoadOnConnect, LoadSceneMode.Single);
    }
}
