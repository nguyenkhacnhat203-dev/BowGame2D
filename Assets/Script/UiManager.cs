using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class UiManager : Singleton<UiManager>
{
    public List<GameObject> UI_GamePlay;
    public List<GameObject> UI_Home;

    private Canvas mainCanvas;

    protected override void Awake()
    {
        base.Awake();
        mainCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignCamera();
    }

    private void AssignCamera()
    {
        if (mainCanvas != null)
        {
            mainCanvas.worldCamera = Camera.main;
        }
    }

    public void ShowHomeUI()
    {
        SetActiveList(UI_GamePlay, false);
        SetActiveList(UI_Home, true);
    }

    public void ShowGameplayUI()
    {
        SetActiveList(UI_Home, false);
        SetActiveList(UI_GamePlay, true);
    }

    private void SetActiveList(List<GameObject> list, bool isActive)
    {
        if (list == null) return;
        foreach (GameObject go in list)
        {
            if (go != null) go.SetActive(isActive);
        }
    }


    public void NextSenceGame()
    {
        SceneTransition.Instance.PlayTransition("MainGame");
    }
}