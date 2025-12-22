using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : Singleton<SceneTransition>
{
    public GameObject transitionObj;

    private Tween currentTween;

    public void PlayTransition(string sceneName)
    {
        if (transitionObj == null) return;

        currentTween?.Kill();

        Transform t = transitionObj.transform;
        transitionObj.SetActive(true);
        t.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        // Scale to 25
        seq.Append(t.DOScale(25f, 0.5f).SetEase(Ease.OutBack));

        // 👉 Sau khi to lên 25
        seq.AppendCallback(() =>
        {
            // Logic theo scene
            if (sceneName == "Home")
            {
                UiManager.Instance.ShowHomeUI();
            }
            else if (sceneName == "MainGame")
            {
                UiManager.Instance.ShowGameplayUI();
            }

            // Load scene
            SceneManager.LoadScene(sceneName);
        });

        seq.AppendInterval(0.05f);

        // Thu nhỏ lại
        seq.Append(t.DOScale(0f, 0.5f).SetEase(Ease.InBack));

        seq.OnComplete(() =>
        {
            transitionObj.SetActive(false);
        });

        currentTween = seq;
    }
}
