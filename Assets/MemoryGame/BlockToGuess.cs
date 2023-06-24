using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockToGuess : MonoBehaviour
{
    public int BlockNumber = -1;
    public bool setup = false;
    public bool guessed = false;

    public TMP_Text text;

    public Sprite HiddenSprite;
    public Sprite NormalSprite;
    public Color WrongColor;
    public Color GoodColor;

    public Image ImageToSet;


    public void setupBlock(int number)
    {
        BlockNumber = number;
        text.text = number.ToString();
        setup = true;
    }

    public void SetHidden()
    {
        text.gameObject.SetActive(false);
        ImageToSet.sprite = HiddenSprite;
    }

    public void setVisible(bool goodBlock)
    {
        if (goodBlock)
        {
            ImageToSet.sprite = NormalSprite;
            text.color = GoodColor;
            text.gameObject.SetActive(true);
        }
        else if (!goodBlock)
        {
            ImageToSet.sprite = NormalSprite;
            text.color = WrongColor;
            if (!setup)
                text.text = "X";
            text.gameObject.SetActive(true);
        }
    }

    public void Reveal()
    {
        ImageToSet.sprite = NormalSprite;
        setup = false;
        text.gameObject.SetActive(true);
        guessed = false;
    }

    public void Reset()
    {
        ImageToSet.sprite = NormalSprite;
        setup = false;
        text.color = Color.white;
        text.gameObject.SetActive(true);
        text.text = "";
        BlockNumber = -1;
        guessed = false;
    }
}
