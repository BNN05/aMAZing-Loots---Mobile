using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<GyroBall>())
        {
            GyroGameManager.instance.Win();
        }
    }
}