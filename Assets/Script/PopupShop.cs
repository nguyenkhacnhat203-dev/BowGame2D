using TMPro;
using UnityEngine;

public class PopupShop : PopupBase
{
    private ArrowSpawner arrowSpawner;

    private int amount;
    private int Cost;
    public TextMeshProUGUI Stone_Text;


    protected override void OnEnable()
    {
        Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();
    }


    public void Add1Arrow()
    {
#if UNITY_EDITOR
        Cost = 0;
        amount = 1;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
     //AdManager.Instance.ShowRewarded(() =>
     //   {
      
     //   });
#endif
    }
    public void Add5Arrow()
    {
#if UNITY_EDITOR
        Cost = 50;
        if (ResourceManager.Instance.StoneGreen >= Cost)
        {
            ResourceManager.Instance.UseStoneGreen(Cost);
            Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        }
        else
        {
            return;
        }
        amount = 5;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
    
#endif
    }
        public void Add15Arrow()
    {
#if UNITY_EDITOR
        Cost = 150;
        if (ResourceManager.Instance.StoneGreen >= Cost)
        {
            ResourceManager.Instance.UseStoneGreen(Cost);
            Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        }
        else
        {
            return;
        }
        amount = 15;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
    
#endif
    }





         public void Add20Arrow()
    {
#if UNITY_EDITOR
        Cost = 200;
        if (ResourceManager.Instance.StoneGreen >= Cost)
        {
            ResourceManager.Instance.UseStoneGreen(Cost);
            Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        }
        else
        {
            return;
        }
        amount = 20;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
    
#endif
    }

    public void Add30Arrow()
    {
#if UNITY_EDITOR
        Cost = 300;
        if (ResourceManager.Instance.StoneGreen >= Cost)
        {
            ResourceManager.Instance.UseStoneGreen(Cost);
            Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        }
        else
        {
            return;
        }
        amount = 30;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
    
#endif
    }



     public void Add25Arrow()
    {
#if UNITY_EDITOR
        Cost = 250;
        if (ResourceManager.Instance.StoneGreen >= Cost)
        {
            ResourceManager.Instance.UseStoneGreen(Cost);
            Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        }
        else
        {
            return;
        }
        amount = 25;

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(amount);
        LevelManager.Instance.UpdateArrowText();
        DestroyPopup();

#else
    
#endif
    }












}
