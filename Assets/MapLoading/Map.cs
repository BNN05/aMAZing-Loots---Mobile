using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Position
{
    public int x;
    public int y;


    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[Serializable]
public class MapSize
{
    public int x;
    public int y;


}

[Serializable]
public class Bloc
{
    public Position PositionBloc;
    public TypeBloc TypeBloc;
    public RotationBloc RotationBloc;
}

public class Piece
{
    public int x;
    public int y;
    public int rotation;
    public int piece;
}


[Serializable]
public class Map
{
    public List<Piece> pieces;
}
