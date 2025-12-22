using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupManager : Singleton<PopupManager>
{
    [Header("Popup Prefabs")]
    public GameObject popup_Pause;
    public GameObject popup_Shop;

    [Header("Popup Parent (Canvas)")]
    public Transform popupParent;

    [Header("State")]
    public bool isShowPopup;



    public void ShowPopup_Pause()
    {
        if (popup_Pause == null) return;
        isShowPopup = true;

        GameObject popup = Instantiate(popup_Pause, popupParent);
        popup.SetActive(true);
    }
      public void ShowPopup_Shop()
    {
        if (popup_Pause == null) return;
        isShowPopup = true;

        GameObject popup = Instantiate(popup_Shop, popupParent);
        popup.SetActive(true);
    }

}
