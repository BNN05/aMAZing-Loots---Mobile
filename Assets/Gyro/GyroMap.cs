using Newtonsoft.Json;
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
            string t = "C:" + @"\" + "Users" + @"\" + "bn" + @"\" + "Documents" + @"\" + "test.txt";
            map = JsonConvert.DeserializeObject<int[,]>(File.ReadAllText(t));
            LoadMap();
        }

    }
    public void SaveMap() {
        string t = "C:" +@"\"+"Users" + @"\" + "bn" + @"\" +"Documents" + @"\" +"test.txt";
        string m = JsonConvert.SerializeObject(this.map);
        
        File.WriteAllText(t, m);
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
