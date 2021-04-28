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
    private AudioManager _audioManager;
    private InputManager _inputManager;

    private void Awake() 
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start() 
    {
        _uiManager = UIManager.Instance;
        _gameplayManager = GameplayManager.Instance;
        _audioManager = AudioManager.Instance;
        _inputManager = InputManager.Instance;
        _audioManager.Init();
        _uiManager.Init();        
        _gameplayManager.Init();
    }

    private void Update()
    {
        _inputManager.Update();
    }
}
