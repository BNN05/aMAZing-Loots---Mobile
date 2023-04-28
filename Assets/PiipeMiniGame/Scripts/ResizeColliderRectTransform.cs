using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeColliderRectTransform : MonoBehaviour
{
    [SerializeField]
    private RectTransform _sizeToGo;
    [SerializeField]
    private BoxCollider2D _toResize;

    private bool _initialized = false;

    [SerializeField]
    private bool _update = false;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((_sizeToGo.rect.width != 0 && !_initialized) || (((_toResize.size.x != _sizeToGo.rect.width) || _toResize.size.y != _sizeToGo.rect.height) && _update))
        {
            _toResize.size = new Vector2(_sizeToGo.rect.width, _sizeToGo.rect.height);
            _initialized = true;
        }
    }

    public void Resize()
    {
        _toResize.size = new Vector2(_sizeToGo.rect.width, _sizeToGo.rect.height);
    }

    public void SetSizeTogo(RectTransform sizeToGo)
    {
        _sizeToGo = sizeToGo;
    }
}
