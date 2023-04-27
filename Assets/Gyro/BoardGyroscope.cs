using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGyroscope : MonoBehaviour
{
    [SerializeField]
    private Quaternion baseRotation = new Quaternion(0, 0, 1, 0);

    private void Start()
    {
        GyroManager.Instance.EnableGyro();
    }

    private void Update()
    {
        transform.localRotation = GyroManager.Instance.getGyroRotation() * baseRotation;
    }
}
