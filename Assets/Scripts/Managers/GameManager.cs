using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
	public static GameManager Instance
	{
		get { return _instance; }
	}

    private UIManager _uiManager;
    private GameplayManager _gameplayManager;
    
    private void Awake() 
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    private void Start() 
    {
        _uiManager = UIManager.Instance;
        _gameplayManager = GameplayManager.Instance;
        _uiManager.Init();
        _gameplayManager.Init();
    }
}
