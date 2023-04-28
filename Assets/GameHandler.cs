using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    public List<MiniGame> MiniGameList = new List<MiniGame>();

    public void PlayMiniGame()
    {
        System.Random rand = new System.Random();
        MiniGame randomElement = MiniGameList[rand.Next(MiniGameList.Count)];
        randomElement.PlayMiniGame();
    }
}
