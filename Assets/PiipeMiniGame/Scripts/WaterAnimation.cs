using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Image water;

    private UnityEvent _endEvent = new UnityEvent();

    public bool waterComing;
    public bool comingFromUp;
    public bool isTube;
    private bool isFilled = false;

    private void Start()
    {
        water.fillAmount = 0;
        if (isTube)
            water.fillMethod = Image.FillMethod.Horizontal;
        else
        {
            water.fillMethod = Image.FillMethod.Radial90;
            water.fillOrigin = 1;
        }
    }

    // Update is called once per fram
    private void Update()
    {
        UpdateWater();
    }

    private void UpdateWater()
    {
        if (waterComing)
        {
            if (comingFromUp)
            {
                if (isTube)
                    water.fillOrigin = 1;
                else
                    water.fillClockwise = true;
            }
            else
            {
                if (isTube)
                    water.fillOrigin = 0;
                else
                    water.fillClockwise = false;
            }
            water.fillAmount += 1 * Time.deltaTime;

            if (water.fillAmount >= 1 && !isFilled)
            {
                _endEvent.Invoke();
                isFilled = true;
            }
        }
        else
        {
            water.fillAmount -= Mathf.Clamp(1 * Time.deltaTime, 0, 1);
        }
    }

    public void AddListenerOnEnd(UnityAction method)
    {
        _endEvent.AddListener(method);
    }

    public void RemoveListenerOnEnd(UnityAction method)
    {
        _endEvent.RemoveListener(method);
    }

    public void StartFillingWater(Direction direction)
    {

    }
}