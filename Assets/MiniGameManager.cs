using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameManager : MonoBehaviour
{
    public UnityEvent<bool> OnEndMiniGame = new UnityEvent<bool>();

    public void MiniGameEnd(bool win)
    {
        OnEndMiniGame.Invoke(true);
    }
}
