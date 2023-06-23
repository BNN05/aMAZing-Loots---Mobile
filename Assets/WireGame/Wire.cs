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

    private bool _isClicked;
    private bool _wasClicked;
    public bool _isSelected;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _wasClicked = _isClicked;
        _isClicked = false;
        if(Input.touchCount == 1)
        {
            _isClicked = true;
            Vector3 rayOrigin = Camera.main.transform.position;
            Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Wire>() )
                {
                    
                    if (!_wasClicked)
                    {
                        OnMouseDown();
                        hit.transform.gameObject.GetComponent<Wire>()._isSelected = true;
                    }
                    else if(_isSelected)
                    {
                        OnMouseDrag();
                    }
                }
            }
            
        }
        else
        {
            if (_wasClicked && _isSelected)
                OnMouseUp();
        }
    }
    

    public void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
    }

    public void OnMouseDrag()
    {
        line.SetPosition(0, MouseWorldPosition() + offset);
        line.SetPosition(1, transform.position);
    }

    public void OnMouseUp()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hit;
        WireManager.instance.UnselectAll();
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            if (hit.transform.gameObject.GetComponent<Wire>())
            {
                line.SetPosition(0, hit.transform.position);
                transform.gameObject.GetComponent<Collider>().enabled = false;
                if (hit.transform.tag == destinationTag)
                {
                    hit.transform.gameObject.GetComponent<Wire>().completed = true;
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
        Vector3 mouseScreenPos;
        if (Input.touchCount ==1)
        {
             mouseScreenPos = Input.touches[0].position;
        }
        else
        {
            mouseScreenPos = Input.mousePosition;
        }
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}