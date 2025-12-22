using UnityEngine;

public class PopupShop : PopupBase
{
    private ArrowSpawner arrowSpawner;

   

    public void AddArrow(int amount)
    {
#if UNITY_EDITOR
        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();
        Debug.Log($"[EDITOR] Add {amount} arrows");

#else
    
#endif
    }
}
