using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PopupManager : Singleton<PopupManager>
{



    [Header("Popup Prefabs")]
    public GameObject popup_Pause;
    public GameObject popup_Shop;
    public GameObject popup_Loss;
    public GameObject popup_Win;
    public GameObject popup_ShopStone;
    public GameObject popup_Setting;

    [Header("Popup Parent (Canvas)")]
    public Transform popupParent;

    [Header("State")]
    public bool isShowPopup;

    private Canvas parentCanvas;


    public Button Pause;
    public Button Shop;
    public Button ShopStone;
    public Button Setting;





    protected override void Awake()
    {
        base.Awake();
        if (Pause != null)
        {
            AddPointerDownListener(Pause, () => OnDown());
        }

        if (Shop != null)
        {
            AddPointerDownListener(Shop, () => OnDown());
        }
        if (ShopStone != null)
        {
            AddPointerDownListener(ShopStone, () => OnDown());
        }  
        if (Setting!= null)
        {
            AddPointerDownListener(Setting, () => OnDown());
        }
    }




    private void AddPointerDownListener(Button button, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { action(); });

        trigger.triggers.Add(entryDown);
    }

    void OnDown()
    {
        isShowPopup = true;
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
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (parentCanvas != null && parentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            parentCanvas.worldCamera = Camera.main;
        }
    }


    public void ShowPopup_Pause()
    {
        if (popup_Pause == null) return;
        CreatePopup(popup_Pause);
    }

    public void ShowPopup_Shop()
    {
        if (popup_Shop == null) return; 
        CreatePopup(popup_Shop);
    }

    public void ShowPopup_Loss()
    {
        if (popup_Loss == null) return; 
        CreatePopup(popup_Loss);
    }

    public void ShowPopup_Win()
    {
        if (popup_Win == null) return; 
        CreatePopup(popup_Win);
    } 
    public void ShowPopup_ShopStone()
    {
        if (popup_Win == null) return; 
        CreatePopup(popup_ShopStone);
    } 
    public void ShowPopup_Setting()
    {
        if (popup_Win == null) return; 
        CreatePopup(popup_Setting);
    }

    private void CreatePopup(GameObject prefab)
    {
        isShowPopup = true;
        GameObject popup = Instantiate(prefab, popupParent);
        popup.SetActive(true);

        UpdateCamera();
    }
}