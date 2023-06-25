using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    public List<BlockToGuess> allBlock = new List<BlockToGuess>();
    public Transform gridTransform;
    
    [SerializedDictionary("Lvl Number", "Nb block to guess")]
    public SerializedDictionary<int, int> NbToGuessByLvl;

    public List<BlockToGuess> blockInOrder = new List<BlockToGuess>();

    private int _lvlNumber = 1;

    private bool guessing = false;
    private bool learning = false;
    private int blockGuessed = 0;

    public float timeToLearn = 3.0f;
    public float timeLearning = 0.0f;

    public TMP_Text textTime;
    public TMP_Text textLoose;
    public TMP_Text textWin;

    private bool ending = false;
    public float timeToEnd = 3f;
    private float timeEnding = 0f;
    private bool win = false;

    private void Start()
    {
        allBlock = gridTransform.GetComponentsInChildren<BlockToGuess>().ToList();
        SetupBlockNumbers();
    }

    private void SetupBlockNumbers()
    {
        System.Random rand = new System.Random();
        int number = 0;

        for (int i = 0; i < NbToGuessByLvl[_lvlNumber]; i++)
        {
            BlockToGuess block  = allBlock[rand.Next(allBlock.Count)];
            if (block.setup)
            {
                i--;
            }
            else
            {
                number += rand.Next(1, 6);
                block.setupBlock(number);
                blockInOrder.Add(block);
            }
        }
        learning = true;
        textTime.text = timeToLearn.ToString();
    }

    public void Update()
    {
        if (learning)
        {
            timeLearning += Time.deltaTime;
            string timeToDisplay = Mathf.CeilToInt((Mathf.Clamp((timeToLearn - timeLearning), 0, timeToLearn))).ToString("0");
            textTime.text = timeToDisplay;
            if ((timeToLearn - timeLearning) <= 0)
            {
                learning = false;
                guessing = true;
                timeLearning = 0;
                foreach(BlockToGuess block in allBlock)
                {
                    block.SetHidden();
                }
            }
        }
        if (ending)
        {
            timeEnding += Time.deltaTime;
            if (timeEnding >= timeToEnd)
            {
                GameObject obj = GameObject.FindGameObjectWithTag("MiniGameManager");
                obj.GetComponent<GameHandler>().EndMiniGame(win);
                Debug.Log("finished !");
            }
        }
    }

    public void UncoverBlock(BlockToGuess block)
    {
        if (!guessing || block.guessed || ending)
            return;

        if (block.BlockNumber == blockInOrder[blockGuessed].BlockNumber)
        {
            block.setVisible(true);
            block.guessed = true;
            blockGuessed += 1;
        }
        else
        {
            block.setVisible(false);
            Loose();
        }

        if (blockGuessed >= blockInOrder.Count)
        {
            Debug.Log("win");
            _lvlNumber += 1;
            if (_lvlNumber <= NbToGuessByLvl.Last().Key)
            {
                ResetAllBlock();
                SetupBlockNumbers();
            }
            else
                Win();

            guessing = false;
        }
    }

    public void ResetAllBlock()
    {
        foreach (BlockToGuess block in allBlock)
            block.Reset();
    }

    public void Win()
    {
        ending = true;
        win = true;
        textWin.gameObject.SetActive(true);
        RevealAll();
    }

    public void Loose()
    {
        ending = true;
        textLoose.gameObject.SetActive(true);
        RevealAll();
    }

    public void RevealAll()
    {
        foreach (BlockToGuess block in allBlock)
            block.Reveal();
    }
}
