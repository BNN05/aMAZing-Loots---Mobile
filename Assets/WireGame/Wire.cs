using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private LineRenderer line;

    [SerializeField]
    private string destinationTag;

    private Vector3 offset;
    private bool completed;
    public bool IsCompleted => completed;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        line.SetPosition(0, MouseWorldPosition() + offset);
        line.SetPosition(1, transform.position);
    }

    private void OnMouseUp()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            if (hit.transform.gameObject.GetComponent<WireEnd>())
            {
                line.SetPosition(0, hit.transform.position);
                transform.gameObject.GetComponent<Collider>().enabled = false;
                if (hit.transform.tag == destinationTag)
                {
                    completed = true;
                    WireManager.instance.CheckIfAllCompleted();
                }
                else
                {
                    WireManager.instance.GameOver();
                }
            }
            else
            {
                line.SetPosition(0, transform.position);
            }
        }
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}