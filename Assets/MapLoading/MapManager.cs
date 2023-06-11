using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    private Map _map;

    private BlocHandler _blocToRotate;

    [SerializeField]
    private EnergyManager _energyPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        //Load Json
        _map = JsonUtility.FromJson<Map>(MapJson.text);

        ErrorHandler();

        InitMapLayout();
        CreateAndSortBlocInArray();
        SetMapVisualToJsonValues();

    }

    private void InitMapLayout()
    {
        GridLayoutGroup layoutGrid = Grid.GetComponent<GridLayoutGroup>();
        Grid.GetComponent<RectTransform>().sizeDelta = new Vector2(layoutGrid.cellSize.x * _map.MapSize.x, layoutGrid.cellSize.y * _map.MapSize.y);
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

        else if (_map.Blocs == null || _map.Blocs.Length <= 0)
            Debug.LogError("No block data found");

        else if (_map.MapSize == null || _map.MapSize.x <= 0 || _map.MapSize.y <= 0)
            Debug.LogError("No map size data found");

        return;
    }

    private void CreateAndSortBlocInArray()
    {
        SortedListBloc = new List<List<GameObject>>(_map.MapSize.x);
        for (int i = 0; i < _map.MapSize.x; i++)
        {
            SortedListBloc.Add(new List<GameObject>(_map.MapSize.y));
        }

        for (int i = 0; i < (_map.MapSize.x * _map.MapSize.y); i++)
        {
            GameObject bloc = Instantiate(BlocGrid);
            bloc.transform.parent = Grid.transform;
            bloc.transform.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            int x = (i) % _map.MapSize.x;
            SortedListBloc[x].Add(bloc);
        }
    }

    private void SetMapVisualToJsonValues()
    {
        foreach (Bloc bloc in _map.Blocs)
        {
            SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].GetComponentInChildren<SpriteRenderer>().sprite = BlocsVisual[(TypeBloc)bloc.TypeBloc];
            SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, RotationBlocs[(RotationBloc)bloc.RotationBloc]);
            SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].AddComponent<BlocHandler>().Init(bloc, this);

            //Force set collider size
            SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].GetComponent<ResizeColliderRectTransform>().SetSizeTogo(SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].GetComponent<RectTransform>());
            SortedListBloc[bloc.PositionBloc.x][bloc.PositionBloc.y].GetComponent<ResizeColliderRectTransform>().Resize();
        }
    }
}
