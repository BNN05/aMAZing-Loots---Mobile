using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GyroManager.Instance.Win();
    }
}
