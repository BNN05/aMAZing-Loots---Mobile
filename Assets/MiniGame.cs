using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "MiniGame", order = 0, fileName = "New MiniGame")]
public class MiniGame : ScriptableObject
{
    public string scene;
    private UnityEvent<bool> OnMiniGameEnd = new UnityEvent<bool>();
    public bool Playing { get; private set; }

    public void LoadSceneMiniGameAsync()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().OnEndMiniGame.AddListener(MiniGameEnd);
    }

    public void MiniGameEnd(bool win)
    {
        OnMiniGameEnd.Invoke(win);
        EndMiniGame();
        OnMiniGameEnd.RemoveAllListeners();
    }

    public void PlayMiniGame()
    {
        Playing = true;
        LoadSceneMiniGameAsync();
    }

    public void ListToMiniGameEnd(UnityAction<bool> onEnd)
    {
        OnMiniGameEnd.AddListener(onEnd);
    }

    public void EndMiniGame()
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
