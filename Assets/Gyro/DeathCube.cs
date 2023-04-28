using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<GyroBall>())
        {
            GyroGameManager.instance.GameOver();
        }
    }
}