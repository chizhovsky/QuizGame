using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    private static readonly InputManager _instance = new InputManager();
    static InputManager(){}
    private InputManager(){}
    public static InputManager Instance
    {
        get { return _instance;}
    }

    public void Update()
    {
    }
}
