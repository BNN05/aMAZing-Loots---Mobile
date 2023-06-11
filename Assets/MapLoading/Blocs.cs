using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationBloc
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

public enum TypeBloc
{
    One = 0,
    Two = 1,
    Three = 2,
    Four = 3
}


public class Blocs : MonoBehaviour
{
    private RotationBloc _rotationBloc;
    public TypeBloc _typeBloc;
    public Position _positionBloc;

    public void Inititalization(TypeBloc type, RotationBloc rotation, Position position)
    {
        _rotationBloc = rotation;
        _typeBloc = type;
        _positionBloc = position;
    }
}
