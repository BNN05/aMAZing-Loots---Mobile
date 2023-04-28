using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGyroscope : MonoBehaviour
{
    private Vector3 firstClick;
    private bool isDragging;
    public float learp;
    private Quaternion lastBoard;

    [SerializeField]
    private GameObject board;

    private void MouseDown()
    {
        if (Time.timeScale == 0)
            return;
        if (firstClick == new Vector3())
            firstClick = Input.mousePosition;
        isDragging = true;
    }

    private void MouseUp()
    {
        isDragging = false;
        lastBoard = board.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();
        if (Input.GetMouseButtonUp(0))
            MouseUp();

        if (isDragging)
        {
            UpdatePosition();
            return;
        }
    }

    private void UpdatePosition()
    {
        float rotZ = Mathf.Clamp((firstClick.x - Input.mousePosition.x) / 7, -30, 30);
        float rotX = Mathf.Clamp((firstClick.y - Input.mousePosition.y) / 7, -30, 30);
        board.transform.rotation = Quaternion.Euler(-rotX, 0, rotZ);
    }
}