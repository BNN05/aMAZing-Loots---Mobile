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
        MainMenu.SetActive(false);
        ConnectMenu.SetActive(true);
        OpenGameObject = ConnectMenu;
        Debug.Log("connect");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        MainMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
        OpenGameObject = HowToPlayMenu;
    }

    public void BackToMainMenuButton()
    {
        OpenGameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ConnectWithIPButton()
    {
        string ip = IpText.text;
        SceneManager.LoadScene(SceneToLoadOnConnect, LoadSceneMode.Single);
    }
}
