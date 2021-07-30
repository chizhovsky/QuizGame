using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : Menu, IDragHandler, IEndDragHandler
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Animator _startButtonAnim;
    [SerializeField] private GameObject _mainMenuPanel;
    private RectTransform canvasRt;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _leftPanel;
    [SerializeField] private GameObject _centralPanel;
    [SerializeField] private GameObject _rightPanel;
    public Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int page = 0;


    public void Init()
    {
        Debug.Log("Screen Width : " + Screen.width);
        _startButton.onClick.AddListener(StartOnClick);
        panelLocation = transform.position;
        canvasRt = UIManager.Instance.mainCanvas.GetComponent<RectTransform>();
        RectTransform panelRt = _mainPanel.GetComponent<RectTransform>();
        panelRt.sizeDelta = new Vector2(canvasRt.rect.width * 3f, canvasRt.rect.height);
    }
    private void StartOnClick()
    {
        StartCoroutine(StartAnimRoutine());
    }
    IEnumerator StartAnimRoutine()
    {
        _startButtonAnim.SetTrigger("IsPressed");
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.sfxSource.PlayOneShot(AudioManager.Instance.audioData.buttonPressed.clip);
        UIManager.Instance.loadingMenu.ShowMenu();
        UIManager.Instance.mainMenu.HideMenu();
        UIManager.Instance.bottomPanel.HideMenu();
        GameplayManager.Instance.LoadDataFromWeb();
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        if (page < 1 && page > -1)
        {
            transform.position = panelLocation - new Vector3(difference, 0, 0);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs (percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && page < 1)
            {
                newLocation += new Vector3(-Screen.width, 0, 0);
                page++;
                StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                Debug.Log(page);
            }
            else if (percentage < 0 && page > -1)
            {
                newLocation += new Vector3(Screen.width, 0, 0);
                page--;
                Debug.Log(page);
                StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            }
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    public IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        foreach (var btn in UIManager.Instance.bottomPanel.bottomButtons)
        {
            btn.interactable = false;
        }
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            UIManager.Instance.bottomPanel.PaintBottomImage();
            yield return null;
        }
        foreach (var btn in UIManager.Instance.bottomPanel.bottomButtons)
        {
            btn.interactable = true;
        }
    }
}
