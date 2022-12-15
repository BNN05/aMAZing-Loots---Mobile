using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum direction
{
    up,
    left,
    down,
    right
}

public class Pipe : MonoBehaviour
{

    private Dictionary<direction, Pipe> _neighbourPipes = new Dictionary<direction, Pipe>();

    private Dictionary<direction, bool> _connectors = new Dictionary<direction, bool>();

    [SerializeField]
    private List<direction> _baseActiveConnectors = new List<direction>();

    private bool _initialized = false;

    private RectTransform _parentRectTranform;

    // Start is called before the first frame update
    void Start()
    {
        _parentRectTranform = this.GetComponentInParent<RectTransform>();
        _connectors.Add(direction.up,  false);
        _connectors.Add(direction.left,    false);
        _connectors.Add(direction.down,  false);
        _connectors.Add(direction.right, false);

        this.GetComponent<MakeRotate>().AddListenerOnRotation(OnRotation);

        foreach (direction connectedDirection in _baseActiveConnectors)
        {
            _connectors[connectedDirection] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initialized && _parentRectTranform != null && _parentRectTranform.rect.width != 0)
        {
            SetCorrectDirection(_parentRectTranform.rotation.eulerAngles.z % 360);
            _initialized = true;
        }

        if (!_initialized)
            return;

    }

    public void AddNeighbourPipe(Pipe neighbour, direction direction)
    {
        _neighbourPipes.Add(direction, neighbour);
    }

    private void SetCorrectDirection(float rotationInDegree)
    {
        if (rotationInDegree <= 0)
            return;

        List<direction> directions = new List<direction>(_connectors.Keys);
        List<bool>      activeDirections = new List<bool>(_connectors.Values);

        for(int i = 0; i < directions.Count; i++)
            _connectors[directions[i]] = i == 0 ? activeDirections[directions.Count - 1] : activeDirections[i - 1];

        SetCorrectDirection(rotationInDegree - 90);
    }

    private void OnRotation()
    {
        SetCorrectDirection(90);
    }
}


/*
 * up : false    -> false -> true  -> false
 * left : true   -> false -> false -> true
 * down : true   -> true  -> false -> false
 * right : false -> true  -> true -> false
 * 
 * up : true
 * left : false
 * down : true
 * right : false
 * 
 * 
 */
