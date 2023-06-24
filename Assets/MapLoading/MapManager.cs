using AYellowpaper.SerializedCollections;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    public TextAsset MapJson;

    [SerializeField]
    public GameObject Grid;

    [SerializeField]
    public Transform Canvas;

    public GameObject CameraMap;

    [SerializeField]
    public GameObject LineGrid;

    [SerializeField]
    public GameObject BlocGrid;

    [SerializedDictionary("Type bloc", "Visual Bloc")]
    public SerializedDictionary<TypeBloc, Sprite> BlocsVisual;

    [SerializedDictionary("Rotation bloc", "Rotation value")]
    public SerializedDictionary<RotationBloc, int> RotationBlocs;

    public List<List<GameObject>> SortedListBloc;

    [SerializeField]
    private GameHandler _gameManager;

    private bool _rotation = false;

    public bool IsRotating { get { return _rotation; } }

    private Map _map = new Map();

    private BlocHandler _blocToRotate;

    [SerializeField]
    private EnergyManager _energyPlayer;

    private SocketIoClientTest socketIOClient;

    public TMP_Text textLever;
    public int leverDown = 0;

    public BlocHandler blockSelected;

    // Start is called before the first frame update
    private void Start()
    {
        //Load Json
        //var goSocket = GameObject.FindGameObjectWithTag("SocketIO");
        //socketIOClient = goSocket.GetComponent<SocketIoClientTest>();
        //socketIOClient.OnGameOver.AddListener(OnGameOver);
        //socketIOClient.OnLever.AddListener(OnLeverDown);
        //socketIOClient.OnRotation.AddListener(OnRotationComputer);


        List<Piece> test = JsonConvert.DeserializeObject<List<Piece>>(MapJson.text);
        _map.pieces = test;

        ErrorHandler();

        InitMapLayout();
        CreateAndSortBlocInArray();
        SetMapVisualToJsonValues();

    }

    private void Update()
    {
        InitMapLayout();
    }
    private void InitMapLayout()
    {
        GridLayoutGroup layoutGrid = Grid.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = Grid.transform.parent.GetComponent<RectTransform>();
        layoutGrid.cellSize = new Vector2(Mathf.FloorToInt(rectTransform.rect.width / 6), Mathf.FloorToInt(rectTransform.rect.width / 6));
    }

    public void CheckEnergy(Position bloc)
    {
        _blocToRotate = SortedListBloc[bloc.x][bloc.y].GetComponent<BlocHandler>();
    }

    public void PlayMiniGame()
    {
        Canvas.gameObject.SetActive(false);
        CameraMap.SetActive(false);
        _gameManager.PlayMiniGame();
        _gameManager.MiniGameList.Where(m => m.Playing).FirstOrDefault().ListenToMiniGameEnd(MiniGameEnd);
    }

    public void MiniGameEnd(bool win)
    {
        if (win)
        {
            CameraMap.SetActive(true);
            Canvas.gameObject.SetActive(true);
            _energyPlayer.ModifyEnergy(1);
        }
    }
    private void ErrorHandler()
    {
        if (_map == null)
            Debug.LogError("No map loaded");

        else if (_map.pieces == null || _map.pieces.Count <= 0)
            Debug.LogError("No block data found");

        return;
    }

    private void CreateAndSortBlocInArray()
    {
        SortedListBloc = new List<List<GameObject>>(5);
        for (int i = 0; i < 5; i++)
        {
            SortedListBloc.Add(new List<GameObject>(5));
        }

        for (int i = 0; i < (5 * 5); i++)
        {
            GameObject bloc = Instantiate(BlocGrid);
            bloc.GetComponent<RectTransform>().localPosition = Vector3.zero;
            bloc.transform.parent = Grid.transform;
            bloc.transform.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            int x = (i) % 5;
            SortedListBloc[x].Add(bloc);
        }
    }

    private void SetMapVisualToJsonValues()
    {
        foreach (Piece bloc in _map.pieces)
        {
            SortedListBloc[bloc.x][bloc.y].GetComponentInChildren<Image>().sprite = BlocsVisual[(TypeBloc)bloc.piece];
            SortedListBloc[bloc.x][bloc.y].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, bloc.rotation);
            SortedListBloc[bloc.x][bloc.y].GetComponent<BlocHandler>().Init(bloc, this);

            //Force set collider size
            SortedListBloc[bloc.x][bloc.y].GetComponent<ResizeColliderRectTransform>().SetSizeTogo(SortedListBloc[bloc.x][bloc.y].GetComponent<RectTransform>());
            SortedListBloc[bloc.x][bloc.y].GetComponent<ResizeColliderRectTransform>().Resize();
        }
    }

    private void OnLeverDown(string leverDown)
    {

    }

    public void SendMalus()
    {

    }

    public void SendRotation()
    {

    }

    private void OnRotationComputer(string rotation)
    {

    }

    private void OnGameOver(string winComputer)
    {

    }

    public void OnBlockSelected(BlocHandler block)
    {
        if (blockSelected != null)
            blockSelected.SetSelected(false);

        blockSelected = block;
        blockSelected.SetSelected(true);

    }
}
