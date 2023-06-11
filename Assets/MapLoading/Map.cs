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


[Serializable]
public class Map
{
    public MapSize MapSize;
    public Bloc[] Blocs;
}
