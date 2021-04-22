using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMenu : Menu
{
    private RectTransform _rectComponent;
    private float _rotateSpeed = 250f;
    [SerializeField] private GameObject _loadingCircle;
    
    public void Init()
    {
        _rectComponent = _loadingCircle.GetComponent<RectTransform>();        
    }
    private void Update()
    {
        _rectComponent.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
    }
}
