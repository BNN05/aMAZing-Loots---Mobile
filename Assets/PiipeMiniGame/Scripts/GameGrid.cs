using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField]
    private WebSocketClient ws;

    private List<List<Pipe>> _pipes;

    private List<(Pipe, Direction)> _winningWay = new List<(Pipe, Direction)>();
    private int _filledPipes = 0;

    private bool _playing = false;
    public bool IsPlaying { get { return _playing; } }


    private bool _solving = false;
    public bool IsSolving { get { return _solving; } }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeGrid();
        InitializePipes();
    }

    // Update is called once per frame
    private void Update()
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
        List<(Pipe, Direction)> nextPipes = nextPipe.ConnectedPipes();

        if (nextPipes.Count == 0)
            return false;

        foreach ((Pipe, Direction) pipe in nextPipes)
        {
            _winningWay.Add(pipe);

            if (pipe.Item1.TypePipe == Type.end && pipe.Item1.GetConnectorValue(Direction.right))
                return true;

            if (TrySolve(pipe.Item1))
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
        foreach (var pipes in _pipes)
        {
            foreach (var pipe in pipes)
            {
                _solving = true;
                pipe.StopAllRotation();
            }
        }

        _winningWay.Add((_pipes[0][0], Direction.right));
        if (_pipes[0][0].GetConnectorValue(Direction.left) != false && TrySolve(_pipes[0][0]))
        {
            StartWaterAnimation(_winningWay[0].Item1, Direction.right, Direction.left);
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
                    _solving = false;
                }
            }
        }
    }

    private void StartWaterAnimation(Pipe pipe, Direction direction, Direction origin)
    {
        WaterAnimation waterAnimation = pipe.GetComponent<WaterAnimation>();
        waterAnimation.AddListenerOnEnd(PlayNextPipe);
        waterAnimation.StartFillingWater(direction, origin, pipe.GetComponent<RectTransform>().rotation.eulerAngles.z);
    }

    private void PlayNextPipe()
    {
        _winningWay[_filledPipes].Item1.GetComponent<WaterAnimation>().RemoveListenerOnEnd(PlayNextPipe);

        _filledPipes += 1;
        if (_filledPipes < _winningWay.Count - 1)
            StartWaterAnimation(_winningWay[_filledPipes].Item1, _winningWay[_filledPipes + 1].Item2, Pipe.GetOppositeDirection(_winningWay[_filledPipes].Item2));

        if (_filledPipes == _winningWay.Count - 1)
            StartWaterAnimation(_winningWay[_filledPipes].Item1, Direction.right, Pipe.GetOppositeDirection(_winningWay[_filledPipes].Item2));

        if (_filledPipes == _winningWay.Count)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("MiniGameManager");
            obj.GetComponent<GameHandler>().EndMiniGame();
        }
        else
        {
            ws.SendMessage("Aie ca coule :o !!!!");
        }
    }
}