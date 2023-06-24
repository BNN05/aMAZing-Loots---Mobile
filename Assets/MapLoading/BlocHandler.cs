using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlocHandler : MonoBehaviour
{
    public Piece Bloc { get; private set; }
    private MapManager _mapManager;
    public Color selectedColor;
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(Piece bloc, MapManager mapManager)
    {
        Bloc = bloc;
        _mapManager = mapManager;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        _mapManager.OnBlockSelected(this);
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            image.color = selectedColor;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
