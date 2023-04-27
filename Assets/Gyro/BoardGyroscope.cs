using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGyroscope : MonoBehaviour
{
    private Vector3 firstClick;
    private bool isDragging;
    public float learp;
    private float timeCount = 0.0f;
    private Quaternion lastBoard;

    [SerializeField]
    private GameObject board;

    private void MouseDown()
    {
        firstClick = Input.mousePosition;
        Debug.Log("mouse down");
        isDragging = true;
    }

    private void MouseUp()
    {
        isDragging = false;
        timeCount = 0;
        lastBoard = board.transform.rotation;
        Debug.Log("mouse up");
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
        float progress = timeCount * learp;
        progress = Mathf.Clamp(progress, 0, 0.99f);
        Debug.Log(lastBoard);
        board.transform.rotation = Quaternion.Slerp(lastBoard, new Quaternion(0, 0, 0, 0), progress);
        timeCount = timeCount + Time.deltaTime;
    }

    private void UpdatePosition()
    {
        float rotZ = Mathf.Clamp((firstClick.x - Input.mousePosition.x) / 7, -30, 30);
        float rotX = Mathf.Clamp((firstClick.y - Input.mousePosition.y) / 7, -30, 30);
        board.transform.rotation = Quaternion.Euler(-rotX, 0, rotZ);
    }
}