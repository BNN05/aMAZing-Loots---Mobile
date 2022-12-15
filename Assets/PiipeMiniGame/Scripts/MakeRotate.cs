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

    // Start is called before the first frame update
    void Start()
    {
        _objectiveRot = Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
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
                transform.Rotate(0, 0, angle);
                _objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
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
}
