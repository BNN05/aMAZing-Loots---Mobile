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

    public void StartFillingWater(Direction direction, Direction origin, float rotation)
    {
        switch (origin)
        {
            case Direction.up:
                FillFromUp(direction, rotation);
                break;

            case Direction.left:
                FillFromLeft(direction, rotation);
                //if (water.fillMethod == Image.FillMethod.Radial90)
                //    water.fillOrigin = (int)Image.Origin90.TopLeft;
                break;

            case Direction.down:
                FillFromDown(direction, rotation);
                break;

            case Direction.right:
                FillFromRight(direction, rotation);
                break;

            default:
                break;
        }
        waterComing = true;
    }

    private void FillFromUp(Direction direction, float rotation)
    {
        switch (direction)
        {
            case Direction.left:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = true;
                }
                break;

            case Direction.down:
                if (water.fillMethod == Image.FillMethod.Horizontal)
                {
                    if ((rotation % 360) == 180)
                        water.fillOrigin = 1;
                    else 
                        water.fillOrigin = 0;
                }
                break;

            case Direction.right:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = false;
                }
                break;

            default:
                break;
        }
    }
    private void FillFromDown(Direction direction, float rotation)
    {
        switch (direction)
        {
            case Direction.left:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = false;
                }
                break;

            case Direction.up:
                if (water.fillMethod == Image.FillMethod.Horizontal)
                {
                    if ((rotation % 360) > 180)
                        water.fillOrigin = 0;
                    else
                        water.fillOrigin = 1;
                }
                break;

            case Direction.right:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = true;
                }
                break;

            default:
                break;
        }
    }
    private void FillFromLeft(Direction direction, float rotation)
    {
        switch (direction)
        {
            case Direction.down:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = true;
                }
                break;

            case Direction.up:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = false;
                }
                break;

            case Direction.right:
                if (water.fillMethod == Image.FillMethod.Horizontal)
                {
                    if ((rotation % 360) > 180)
                        water.fillOrigin = 1;
                    else
                        water.fillOrigin = 0;
                }
                break;

            default:
                break;
        }
    }
    private void FillFromRight(Direction direction, float rotation)
    {
        switch (direction)
        {
            case Direction.left:
                if (water.fillMethod == Image.FillMethod.Horizontal)
                {
                    if ((rotation % 360) > 180)
                        water.fillOrigin = 0;
                    else
                        water.fillOrigin = 1;
                }
                break;

            case Direction.up:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = false;
                }
                break;

            case Direction.down:
                if (water.fillMethod == Image.FillMethod.Radial90)
                {
                    water.fillClockwise = false;
                }
                break;

            default:
                break;
        }
    }
}