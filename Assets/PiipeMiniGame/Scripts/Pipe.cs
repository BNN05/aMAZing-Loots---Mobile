using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    up,
    left,
    down,
    right,
    none
}

public enum Type
{
    start,
    end,
    none
}

public class Pipe : MonoBehaviour
{

    private Dictionary<Direction, Pipe> _neighbourPipes = new Dictionary<Direction, Pipe>();

    private Dictionary<Direction, bool> _connectors = new Dictionary<Direction, bool>();

    [SerializeField]
    private List<Direction> _baseActiveConnectors = new List<Direction>();

    private bool _initialized = false;

    private RectTransform _parentRectTranform;

    [SerializeField]
    private Type typePipe = Type.none;

    // Start is called before the first frame update
    void Start()
    {
        _parentRectTranform = this.GetComponentInParent<RectTransform>();
        _connectors.Add(Direction.up,  false);
        _connectors.Add(Direction.left,    false);
        _connectors.Add(Direction.down,  false);
        _connectors.Add(Direction.right, false);

        this.GetComponent<MakeRotate>().AddListenerOnRotation(OnRotation);

        foreach (Direction connectedDirection in _baseActiveConnectors)
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

    public void AddNeighbourPipe(Pipe neighbour, Direction direction)
    {
        _neighbourPipes.Add(direction, neighbour);
    }

    private void SetCorrectDirection(float rotationInDegree)
    {
        if (rotationInDegree <= 0)
            return;

        List<Direction> directions = new List<Direction>(_connectors.Keys);
        List<bool>      activeDirections = new List<bool>(_connectors.Values);

        for(int i = 0; i < directions.Count; i++)
            _connectors[directions[i]] = i == 0 ? activeDirections[directions.Count - 1] : activeDirections[i - 1];

        SetCorrectDirection(rotationInDegree - 90);
    }

    private void OnRotation()
    {
        SetCorrectDirection(90);
    }

    public List<Pipe> ConnectedPipes()
    {
        List<Pipe> connectedPipes = new List<Pipe>();

        foreach(KeyValuePair<Direction, Pipe> pipe in _neighbourPipes)
        {
        }

        return connectedPipes;
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
