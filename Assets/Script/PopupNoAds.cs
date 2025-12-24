using UnityEngine;

public class PopupNoAds : PopupBase
{
    public void OnClickNoAds()
    {
#if UNITY_EDITOR
        AdManager.Instance.DisableAllAds();
        ResourceManager.Instance.AddStoneGreen(500);
        DestroyPopup();

#else

#endif
    }
}
