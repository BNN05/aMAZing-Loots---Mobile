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
        string scene = PlayingMiniGame.scene[rand.Next(PlayingMiniGame.scene.Length)];
        PlayingMiniGame.PlayMiniGame(scene);
    }

    public void EndMiniGame(bool win)
    {
        PlayingMiniGame.MiniGameEnd(win, PlayingMiniGame.EarnMiniGame);
        PlayingMiniGame = null;
    }
}
