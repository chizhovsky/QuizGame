﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCircleAnimationScript : MonoBehaviour
{
    private RectTransform rectComponent;
    private float _rotateSpeed = 250f;

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rectComponent.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
    }
}