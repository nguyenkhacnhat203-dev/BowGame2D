using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupLoss : PopupBase
{
    private ArrowSpawner arrowSpawner;

    [Header("UI")]
    public TextMeshProUGUI Text_Cost;
    public TextMeshProUGUI Stone_Text;
    public Image TimerFill;

    [Header("Revive Setting")]
    public int Cost = 5;
    public float timeToHome = 5f;

    private Tween timerTween;

    protected override void OnShow()
    {
        StartTimerToHome();
    }

    protected override void OnEnable()
    {
        Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();
        Text_Cost.text = Cost.ToString();
    }

    private void StartTimerToHome()
    {
        if (TimerFill == null) return;

        TimerFill.fillAmount = 1f;

        timerTween = TimerFill
            .DOFillAmount(0f, timeToHome)
            .SetEase(Ease.Linear)
            .SetUpdate(true) 
            .OnComplete(() =>
            {
                ComeHome();
            });
    }

    private void StopTimer()
    {
        if (timerTween != null && timerTween.IsActive())
        {
            timerTween.Kill();
        }
    }

    public override void DestroyPopup()
    {
        StopTimer();
        Destroy(gameObject);
        PopupManager.Instance.isShowPopup = false;
    }

    public void RevivedLevelBuyADS()
    {
#if UNITY_EDITOR
        StopTimer();

        LevelManager.Instance.isCheck = false;
        DestroyPopup();

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(3);
        LevelManager.Instance.UpdateArrowText();


#endif
        StopTimer();

        AdManager.Instance.ShowRewarded(() =>
        {
            LevelManager.Instance.isCheck = false;

            DestroyPopup();

            arrowSpawner = FindObjectOfType<ArrowSpawner>();
            arrowSpawner.AddArrow(3);
            LevelManager.Instance.UpdateArrowText();
        });
    }

    public void RevivedLevelBuyStone()
    {
#if UNITY_EDITOR
        if (ResourceManager.Instance.StoneGreen < Cost)
            return;

        StopTimer();

        ResourceManager.Instance.UseStoneGreen(Cost);
        Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        LevelManager.Instance.isCheck = false;
        DestroyPopup();

        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        arrowSpawner.AddArrow(3);
        LevelManager.Instance.UpdateArrowText();
#endif
       
    }

    public void ComeHome()
    {
        StopTimer();
        SceneTransition.Instance.PlayTransition("Home");
        DestroyPopup();
    }
}
