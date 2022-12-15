using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    private List<List<Pipe>> _pipes;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
        InitializePipes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializePipes()
    {
        for (int i = 0; i < _pipes.Count; i++)
        {
            for (int y = 0; y < _pipes[i].Count; y++)
            {
                if (i > 0)
                    _pipes[i][y].AddNeighbourPipe(_pipes[i - 1][y], Direction.up);
                if (i < (_pipes.Count - 1))
                    _pipes[i][y].AddNeighbourPipe(_pipes[i + 1][y], Direction.down);
                if (y > 0)
                    _pipes[i][y].AddNeighbourPipe(_pipes[i][y - 1], Direction.left);
                if (y < (_pipes[i].Count - 1))
                    _pipes[i][y].AddNeighbourPipe(_pipes[i][y + 1], Direction.right);
            }
        }
    }

    private bool TrySolve(Pipe nextPipe)
    {
        
    }

    private void InitializeGrid()
    {
        _pipes = new List<List<Pipe>>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _pipes.Add(new List<Pipe>());
            Transform horizontalPipeLine = this.transform.GetChild(i);

            for (int y = 0; y < horizontalPipeLine.childCount; y++)
            {
                Transform tranformPipe = horizontalPipeLine.GetChild(y).GetChild(0);
                if (tranformPipe != null)
                    _pipes[i].Add(tranformPipe.GetComponent<Pipe>());
                else
                    _pipes[i].Add(null);
            }
        }
    }
}
