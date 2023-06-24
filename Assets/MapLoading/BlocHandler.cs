using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocHandler : MonoBehaviour
{
    public Piece Bloc { get; private set; }
    private MapManager _mapManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(Piece bloc, MapManager mapManager)
    {
        Bloc = bloc;
        _mapManager = mapManager;
    }

    public void LoadMiniGame(Position positionBloc)
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (_mapManager.IsRotating && GetComponent<MakeRotate>().enabled)
        //    GetComponent<MakeRotate>().enabled = false;
        //else if (!_mapManager.IsRotating && !GetComponent<MakeRotate>().enabled)
        //    GetComponent<MakeRotate>().enabled = true;

        if (Input.touchCount > 0)
        {
            Touch touchInfo = Input.GetTouch(0);
            if (touchInfo.phase == TouchPhase.Began)
            {
                Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(touchInfo.position);

                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

                RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

                if (hitInfo.collider != null && hitInfo.collider.gameObject == this.gameObject)
                {
                    //Faire tourner si énergie
                    //_mapManager.LoadMiniGame();
                }
            }
        }
    }
}
