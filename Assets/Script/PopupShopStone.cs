using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShopStone : PopupBase
{


    public void AddStone(int amount)
    {
#if UNITY_EDITOR
        ResourceManager.Instance.AddStoneGreen(amount); 
        DestroyPopup();

#else
    
#endif
    }
}
