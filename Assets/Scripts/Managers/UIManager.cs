﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    private static readonly UIManager _instance = new UIManager();
    static UIManager(){}
    private UIManager(){}
    public static UIManager Instance
    {
        get { return _instance;}
    }

    public MainMenu mainMenu;
    public SettingsMenu settingsMenu;
    public GameMenu gameMenu;
    public LoadingMenu loadingMenu;
    public GameOverMenu gameOverMenu;

    public Canvas mainCanvas;
    private const string prefabUILocation = "Prefabs/";
    public void Init()
    {
        mainCanvas = Object.Instantiate(Resources.Load<Canvas>( prefabUILocation + "Canvas" ));

        mainMenu = UnityEngine.Object.Instantiate(Resources.Load<MainMenu>(prefabUILocation + "MainMenu"), mainCanvas.transform);
        mainMenu.Init();

        settingsMenu = UnityEngine.Object.Instantiate(Resources.Load<SettingsMenu>(prefabUILocation + "SettingsMenu"), mainCanvas.transform);
        settingsMenu.Init();

        loadingMenu = UnityEngine.Object.Instantiate(Resources.Load<LoadingMenu>(prefabUILocation + "LoadingMenu"), mainCanvas.transform);
        loadingMenu.Init();

        gameMenu = UnityEngine.Object.Instantiate(Resources.Load<GameMenu>(prefabUILocation + "GameMenu"), mainCanvas.transform);
        gameMenu.Init();
        
        gameOverMenu = UnityEngine.Object.Instantiate(Resources.Load<GameOverMenu>(prefabUILocation + "GameOverMenu"), mainCanvas.transform);
        gameOverMenu.Init();
    }
}
