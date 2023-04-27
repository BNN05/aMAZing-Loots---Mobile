using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GyroMap : MonoBehaviour
{
    private int[,] map;
    private int largeur;
    private int longueur;
    public bool set;
    private string path;

    public GyroMap(int largeur, int longueur)
    {
        this.map = new int [largeur, longueur];
        this.largeur = largeur;
        this.longueur = longueur;
    }

    public void CreateMap(int largeur, int longueur)
    {
        this.map = new int[largeur, longueur];
    }

    public int GetCase(int x, int y) {
        return map[x, y];
    }

    public void SetCase(int value, int x,int y)
    {
        map[x, y] = value;
    }
    private void Start()
    {
        path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\testGyroMap.txt";
        if (set)
        {
            CreateMap(10, 10);
            CreateEmptyMap();
            SaveMap();
            //CreateMap(3, 2);
            //SetCase(1, 0, 0);
            //SetCase(0, 0, 1);
            //SetCase(0, 1, 1);
            //SetCase(0, 1, 0);
            //SetCase(0, 2, 1);
            //SetCase(1, 2, 0);
            //SaveMap();
        }
        else
        {
            map = JsonConvert.DeserializeObject<int[,]>(File.ReadAllText(path));
            LoadMap();
        }

    }
    public void SaveMap() {
        string m = JsonConvert.SerializeObject(this.map);
        
        File.WriteAllText(path, m);
    }

    public void LoadMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if(map[i,j] == 1)
                {
                    var t = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    t.transform.position = new Vector3(i, 0, j);

                }

            }
        }
    }

    public void CreateEmptyMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                //int t = Random.Range(0, 2);
                map[i, j] = 0;
            }
        }
    }

}
