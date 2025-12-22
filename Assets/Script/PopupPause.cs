using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPause : PopupBase
{
   
    public void ComeHome()
    {
        SceneTransition.Instance.PlayTransition("Home");
        Destroy(gameObject);
        PopupManager.Instance.isShowPopup = false;
    }

}
