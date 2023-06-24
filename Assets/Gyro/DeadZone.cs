using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GyroManager.Instance.GameOver();
    }
}
