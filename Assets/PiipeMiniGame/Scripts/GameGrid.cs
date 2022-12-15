using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    private List<List<Pipe>> _pipes;
    private bool _gameFinished = false;

    private List<Pipe> _winningWay = new List<Pipe>();
    private int _filledPipes = 0;


    // Start is called before the first frame update
    private void Start()
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
        List<Pipe> nextPipes = nextPipe.ConnectedPipes();

        if (nextPipes.Count == 0)
            return false;

        foreach(Pipe pipe in nextPipes)
        {
            _winningWay.Add(pipe);

            if (pipe.TypePipe == Type.end && pipe.GetConnectorValue(Direction.right))
                return true;

            if (TrySolve(pipe))
                return true;
            else
                _winningWay.RemoveAt(_winningWay.Count - 1);

        }

        return false;
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

    public void StartSolving()
    {
        foreach(var pipes in _pipes)
        {
            foreach(var pipe in pipes)
            {
                pipe.StopAllRotation();
            }
        }

        _winningWay.Add(_pipes[0][0]);
        if (TrySolve(_pipes[0][0]))
        {
            StartWaterAnimation(_winningWay[0]);
            Debug.Log("SOLVED");
        }
        else
        {
            _winningWay.RemoveAt(0);
            foreach (var pipes in _pipes)
            {
                foreach (var pipe in pipes)
                {
                    pipe.ResetPathChecked();
                    pipe.ResumeAllRotation();
                }
            }
        }
    }

    private void StartWaterAnimation(Pipe pipe)
    {
        WaterAnimation waterAnimation = pipe.GetComponent<WaterAnimation>();
        waterAnimation.AddListenerOnEnd(PlayNextPipe);
        waterAnimation.waterComing = true;
    }


    private void PlayNextPipe()
    {
         _winningWay[_filledPipes].GetComponent<WaterAnimation>().RemoveListenerOnEnd(PlayNextPipe);
        
        _filledPipes += 1;
        if (_filledPipes < _winningWay.Count)
            StartWaterAnimation(_winningWay[_filledPipes]);
        else
            Debug.Log("Finished flling");
    }
}
