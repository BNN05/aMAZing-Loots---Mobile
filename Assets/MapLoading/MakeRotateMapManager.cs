using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeRotateMapManager : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.1f;
    [SerializeField]
    private bool _touchInput = true;


    private Quaternion _objectiveRot;
    private Quaternion _originalRot;
    private bool _rotating = false;
    private float _count = 0.0f;

    private UnityEvent _onRotation = new UnityEvent();
    private bool _blocked = false;

    public bool _rotateBack = false;
    public bool _rotatingBackward = false;

    // Start is called before the first frame update
    void Start()
    {
        _objectiveRot = Quaternion.Euler(0, 0, 90);
    }

    public void ForceRotation(int rotation)
    {
        if (rotation == 1)
        {
            _rotateBack = true;
            _objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
        }
        else
        {
            _rotateBack = false;
            _objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
        }
        _originalRot = this.transform.rotation;
        _rotating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_blocked)
            return;

        if (_rotating)
        {
            transform.rotation = Quaternion.Lerp(this.transform.rotation, _objectiveRot, _speed * _count);
            _count += Time.deltaTime;
            float angle = Quaternion.Angle(transform.rotation, _objectiveRot);
            if (angle <= 1)
            {
                transform.Rotate(0, 0, angle);
                transform.rotation = _objectiveRot;
                if (_rotateBack && Mathf.Round(transform.rotation.eulerAngles.z) == _objectiveRot.eulerAngles.z)
                {
                    _rotatingBackward = true;
                }
                else if (Mathf.Round(transform.rotation.eulerAngles.z) == _objectiveRot.eulerAngles.z)
                {
                    _rotatingBackward = false;
                }
                _count = 0.0f;
                _rotating = false;
                _onRotation.Invoke();
            }
        }
    }

    public void AddListenerOnRotation(UnityAction method)
    {
        _onRotation.AddListener(method);
    }

    public void RemoveListenerOnRotation(UnityAction method)
    {
        _onRotation.RemoveListener(method);
    }

    public void BlockRotation()
    {
        _blocked = true;
    }
    public void ResumeRotation()
    {
        _blocked = false;
    }
}
