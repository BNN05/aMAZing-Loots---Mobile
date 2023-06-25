using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "MiniGame", order = 0, fileName = "New MiniGame")]
public class MiniGame : ScriptableObject
{
    public string[] scene;
    public string sceneActive;
    private UnityEvent<bool, int> OnMiniGameEnd = new UnityEvent<bool, int>();
    private CancellationTokenSource cts;
    public int EarnMiniGame = 1;

    public bool Playing { get; private set; }

    public async Task LoadSceneMiniGameAsync(CancellationToken token, string sceneName)
    {
        token.ThrowIfCancellationRequested();
        if (token.IsCancellationRequested)
            return;
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (asyncOperation.progress < 0.9f)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
                return;
        }

        asyncOperation.allowSceneActivation = true;
        cts.Cancel();
    }

    public void MiniGameEnd(bool win, int energyWin)
    {
        SceneManager.UnloadSceneAsync(sceneActive);
        OnMiniGameEnd.Invoke(win, energyWin);
        Playing = false;
        OnMiniGameEnd.RemoveAllListeners();
    }

    public async void PlayMiniGame(string scene)
    {
        Playing = true;
        sceneActive = scene;

        if (cts == null)
        {
            cts = new CancellationTokenSource();
            try
            {
                await LoadSceneMiniGameAsync(cts.Token, sceneActive);
            }
            catch (OperationCanceledException ex)
            {
                if (ex.CancellationToken == cts.Token)
                {
                    Debug.Log("Task cancelled");
                }
            }
            finally
            {
                cts.Cancel();
                cts = null;
            }
        }
        else
        {
            cts.Cancel();
            cts = null;
        }

    }

    public void ListenToMiniGameEnd(UnityAction<bool, int> onEnd)
    {
        OnMiniGameEnd.AddListener(onEnd);
    }

    public void EndMiniGame()
    {
    }
}
