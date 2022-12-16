using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeRotate : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.1f;

    private Quaternion _objectiveRot;
    private bool _rotating = false;
    private float _count = 0.0f;

    private UnityEvent _onRotation = new UnityEvent();
    private bool _blocked = false;

    public bool _rotateBack = false;
    private bool _rotatingBackward = false;

    // Start is called before the first frame update
    void Start()
    {
        _objectiveRot = Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (_blocked)
            return;

        if (Input.touchCount > 0)
        {
            Touch touchInfo = Input.GetTouch(0);
            if (touchInfo.phase == TouchPhase.Began)
            {
                Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touchInfo.position);

                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

                RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

                if (hitInfo.collider != null && hitInfo.collider.gameObject == this.gameObject && !_rotating)
                    _rotating = true;
            }
        }

        if (_rotating)
        {
            transform.rotation = Quaternion.Lerp(this.transform.rotation, _objectiveRot, _speed * _count);
            _count += Time.deltaTime;
            float angle = Quaternion.Angle(transform.rotation, _objectiveRot); 
            if (angle <= 1)
            {
                if (!_rotatingBackward)
                    transform.Rotate(0, 0, angle);
                else
                    transform.Rotate(0, 0, -angle);

                if (_rotateBack && Mathf.Round(transform.rotation.eulerAngles.z) == 90)
                {
                    _objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
                    _rotatingBackward = true;
                }
                else
                {
                    _objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
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
