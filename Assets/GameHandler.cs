using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    public List<MiniGame> MiniGameList = new List<MiniGame>();


    public MiniGame PlayingMiniGame = null;


    public void PlayMiniGame()
    {
        if (PlayingMiniGame != null)
            return;

        System.Random rand = new System.Random();
        PlayingMiniGame = MiniGameList[rand.Next(MiniGameList.Count)];
        PlayingMiniGame.PlayMiniGame();
    }

    public void EndMiniGame()
    {
        PlayingMiniGame.MiniGameEnd(true);
        PlayingMiniGame = null;
    }
}
