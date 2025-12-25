using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Transition : Singleton<Transition>
{
    public GameObject transitionObj;

    private Tween currentTween;

    public void PlayTransitionWithSence(string sceneName)
    {
        if (transitionObj == null) return;

        currentTween?.Kill();

        Transform t = transitionObj.transform;
        transitionObj.SetActive(true);
        t.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(t.DOScale(32f, 0.65f).SetEase(Ease.OutBack));

        seq.AppendCallback(() =>
        {
            if (sceneName == "Home")
            {
                UiManager.Instance.ShowHomeUI();
            }
            else if (sceneName == "MainGame")
            {
                UiManager.Instance.ShowGameplayUI();
            }

            SceneManager.LoadScene(sceneName);
        });

        seq.AppendInterval(0.01f);

        seq.Append(t.DOScale(0f, 0.65f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            transitionObj.SetActive(false);
            AdManager.Instance.LoadBanner();

        });

        currentTween = seq;
    }


    public void PlayTransition()
    {
        if (transitionObj == null) return;

        currentTween?.Kill();

        Transform t = transitionObj.transform;
        transitionObj.SetActive(true);
        t.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(t.DOScale(32f, 0.65f).SetEase(Ease.OutBack));

        seq.AppendInterval(0.01f);

        seq.Append(t.DOScale(0f, 0.65f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            transitionObj.SetActive(false);
            AdManager.Instance.LoadBanner();
        });

        currentTween = seq;
    }






}
