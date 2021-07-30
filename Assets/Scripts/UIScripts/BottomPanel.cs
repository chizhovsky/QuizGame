using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : Menu
{
    public Button[] bottomButtons;
    [SerializeField] private Color _idleColor;
    [SerializeField] private Color _pressedColor;
    private MainMenu _mainMenu;
    private Vector3 _newLocation;
    


    public void Init()
    {
        bottomButtons[0].onClick.AddListener(CupPressed);
        bottomButtons[1].onClick.AddListener(PugPressed);
        bottomButtons[2].onClick.AddListener(GearsPressed);
        _mainMenu = UIManager.Instance.mainMenu;
        _newLocation = new Vector3(0, 0, 0);
    }

    private void CupPressed()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.audioData.buttonPressed.clip);
        if (_mainMenu.page == 0)
        {
            _newLocation = _mainMenu.transform.position + new Vector3(Screen.width, 0, 0);
        }
        if (_mainMenu.page == 1)
        {
            _newLocation = _mainMenu.transform.position + new Vector3(2 * Screen.width, 0, 0);
        }
        StartCoroutine(_mainMenu.SmoothMove(_mainMenu.transform.position, _newLocation, _mainMenu.easing));
        _mainMenu.page = -1;
        _mainMenu.panelLocation = _newLocation;
        PaintBottomImage();
    }
    
    private void PugPressed()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.audioData.buttonPressed.clip);
        if (_mainMenu.page == -1)
        {
            _newLocation = _mainMenu.transform.position - new Vector3(Screen.width, 0, 0);
        }
        if (_mainMenu.page == 1)
        {
            _newLocation = _mainMenu.transform.position + new Vector3(Screen.width, 0, 0);
        }
        StartCoroutine(_mainMenu.SmoothMove(_mainMenu.transform.position, _newLocation, _mainMenu.easing));
        _mainMenu.page = 0;
        _mainMenu.panelLocation = _newLocation;
        PaintBottomImage();
    }

    private void GearsPressed()
    {
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.audioData.buttonPressed.clip);
        if (_mainMenu.page == -1)
        {
            _newLocation = _mainMenu.transform.position - new Vector3(2*Screen.width, 0, 0);
        }
        if (_mainMenu.page == 0)
        {
            _newLocation = _mainMenu.transform.position - new Vector3(Screen.width, 0, 0);
        }
        StartCoroutine(_mainMenu.SmoothMove(_mainMenu.transform.position, _newLocation, _mainMenu.easing));
        _mainMenu.panelLocation = _newLocation;
        _mainMenu.page = 1;
        PaintBottomImage();
    }

    public void PaintBottomImage()
    {
        if (_mainMenu.page == -1)
        {
            bottomButtons[0].GetComponent<Image>().color = _pressedColor;
            bottomButtons[1].GetComponent<Image>().color = _idleColor;
            bottomButtons[2].GetComponent<Image>().color = _idleColor;
        }
        else if (_mainMenu.page == 0)
        {
            bottomButtons[0].GetComponent<Image>().color = _idleColor;
            bottomButtons[1].GetComponent<Image>().color = _pressedColor;
            bottomButtons[2].GetComponent<Image>().color = _idleColor;            
        }
        else if (_mainMenu.page == 1)
        {
            bottomButtons[0].GetComponent<Image>().color = _idleColor;
            bottomButtons[1].GetComponent<Image>().color = _idleColor;
            bottomButtons[2].GetComponent<Image>().color = _pressedColor;;   
        }        
    }
}
