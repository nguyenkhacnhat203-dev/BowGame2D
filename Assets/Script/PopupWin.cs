using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupWin : PopupBase
{

    private int rewardGreenStone;
    public TextMeshProUGUI rewardStone_Text;
    protected override void OnEnable()
    {
        CheckReward();
    }
    

    protected override void OnShow()
    {

    }
    public override void DestroyPopup()
    {
        Destroy(gameObject);
        PopupManager.Instance.isShowPopup = false;
    }

    private void CheckReward()
    {
        int level = LevelManager.Instance.currentLevelIndex+1;

        if (level >= 1 && level <=10)
        {
            rewardGreenStone = 5;
            rewardStone_Text.text = rewardGreenStone.ToString();
        }

        else if(level >10 && level <=50)
        {
            rewardGreenStone = 10;
            rewardStone_Text.text = rewardGreenStone.ToString();

        }
        else
        {
            rewardGreenStone = 15;
            rewardStone_Text.text = rewardGreenStone.ToString();

        }

    }







    public void X2Ads()
    {

#if UNITY_EDITOR

        Transition.Instance.PlayTransition();
        LevelManager.Instance.LoadNextLevel();
        ResourceManager.Instance.AddStoneGreen(rewardGreenStone*2);
        DestroyPopup();

#else

       if(AdManager.Instance.IsNoAds == true) 
        {
            Transition.Instance.PlayTransition();
            LevelManager.Instance.LoadNextLevel();
            ResourceManager.Instance.AddStoneGreen(rewardGreenStone * 2);
            DestroyPopup();
            return;

        }

         AdManager.Instance.ShowRewarded(() =>
        {
            Transition.Instance.PlayTransition();
            LevelManager.Instance.LoadNextLevel();
            ResourceManager.Instance.AddStoneGreen(rewardGreenStone * 2);
            DestroyPopup();
        });
#endif
    }

    public void NextLevel() 
    {




        Transition.Instance.PlayTransition();
        LevelManager.Instance.LoadNextLevel();
        ResourceManager.Instance.AddStoneGreen(rewardGreenStone);
        DestroyPopup();



    }


    public void ComeHome()
    {
        Transition.Instance.PlayTransitionWithSence("Home");
        DestroyPopup();
    }


}
