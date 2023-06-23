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
    public string scene;
    private UnityEvent<bool> OnMiniGameEnd = new UnityEvent<bool>();
    private CancellationTokenSource cts;

    public bool Playing { get; private set; }

    public async Task LoadSceneMiniGameAsync(CancellationToken token, string sceneName)
    {
        token.ThrowIfCancellationRequested();
        if (token.IsCancellationRequested)
            return;
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (asyncOperation.progress < 0.9f)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
                return;
        }

        asyncOperation.allowSceneActivation = true;
        cts.Cancel();
    }

    public void MiniGameEnd(bool win)
    {
        SceneManager.UnloadSceneAsync(scene);
        OnMiniGameEnd.Invoke(win);
        Playing = false;
        OnMiniGameEnd.RemoveAllListeners();
    }

    public async void PlayMiniGame()
    {
        Playing = true;

        if (cts == null)
        {
            cts = new CancellationTokenSource();
            try
            {
                await LoadSceneMiniGameAsync(cts.Token, scene);
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

    public void ListenToMiniGameEnd(UnityAction<bool> onEnd)
    {
        OnMiniGameEnd.AddListener(onEnd);
    }

    public void EndMiniGame()
    {
    }
}
