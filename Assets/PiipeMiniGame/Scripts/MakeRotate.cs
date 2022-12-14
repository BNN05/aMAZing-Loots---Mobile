using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRotate : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.1f;

    Quaternion objectiveRot;
    bool rotating = false;
    float count = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        objectiveRot = Quaternion.Euler(0, 0, 90);
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

                if (hitInfo.collider != null && hitInfo.collider.gameObject == this.gameObject && !rotating)
                {
                    rotating = true;
                }
            }
        }
        if (rotating)
        {
            transform.rotation = Quaternion.Lerp(this.transform.rotation, objectiveRot, _speed * count);
            count += Time.deltaTime;
            float angle = Quaternion.Angle(transform.rotation, objectiveRot); 
            if (angle <= 1)
            {
                transform.Rotate(0, 0, angle);
                objectiveRot = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
                Debug.Log("stop");
                count = 0.0f;
                rotating = false;
            }
        }
    }
}
