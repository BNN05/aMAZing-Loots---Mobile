using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    public void PlayClickSound()
    {
        AkSoundEngine.PostEvent("Click", gameObject);
    }

    public void PlayRotateSound()
    {
        AkSoundEngine.PostEvent("RotateTube", gameObject);
    }
    public void PlayBadCardSound()
    {
        AkSoundEngine.PostEvent("MauvaiseCarte", gameObject);
    }
    public void PlayGoodCardSound()
    {
        AkSoundEngine.PostEvent("BonneCarte", gameObject);

    }
    public void PlayWinMiniGameSound()
    {
        AkSoundEngine.PostEvent("Win", gameObject);

    }
    public void PlayLooseMiniGameSound()
    {
        AkSoundEngine.PostEvent("Loose", gameObject);

    }

}
