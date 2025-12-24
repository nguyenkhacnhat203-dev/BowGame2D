using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPause : PopupBase
{
   
    public void ComeHome()
    {
        Transition.Instance.PlayTransitionWithSence("Home");
        Destroy(gameObject);
        PopupManager.Instance.isShowPopup = false;
    }

}
