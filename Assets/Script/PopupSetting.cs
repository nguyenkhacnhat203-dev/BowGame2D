using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : PopupBase
{
    [Header("Sprites")]
    public Sprite OnSound;
    public Sprite OffSound;
    public Sprite OnMusic;
    public Sprite OffMusic;
    public Sprite OnPhone;
    public Sprite OffPhone;

    [Header("Buttons Image")]
    public Image imgSound;
    public Image imgMusic;
    public Image imgVibrate;

    private bool isSoundOn;
    private bool isMusicOn;
    private bool isVibrateOn;





    protected override void OnEnable()
    {
        LoadSetting();
        UpdateUI();
    }
    

    #region BUTTON EVENTS
    public void ToggleSound()
    {
        AudioManager.Instance.BtnClick();
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);

        AudioManager.Instance.AdjustSoundEffectsVolume(isSoundOn ? 1f : 0f);
        UpdateUI();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.BtnClick();

        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("Music", isMusicOn ? 1 : 0);

        AudioManager.Instance.AdjustBackgroundMusicVolume(isMusicOn ? 1f : 0f);
        UpdateUI();
    }

    public void ToggleVibrate()
    {
        AudioManager.Instance.BtnClick();

        isVibrateOn = !isVibrateOn;
        PlayerPrefs.SetInt("Vibrate", isVibrateOn ? 1 : 0);

#if UNITY_ANDROID || UNITY_IOS
        if (isVibrateOn)
            Handheld.Vibrate();
#endif
        UpdateUI();
    }
    #endregion

    #region LOAD & UI
    void LoadSetting()
    {
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        isVibrateOn = PlayerPrefs.GetInt("Vibrate", 1) == 1;

        AudioManager.Instance.AdjustSoundEffectsVolume(isSoundOn ? 1f : 0f);
        AudioManager.Instance.AdjustBackgroundMusicVolume(isMusicOn ? 1f : 0f);
    }

    void UpdateUI()
    {
        imgSound.sprite = isSoundOn ? OnSound : OffSound;
        imgMusic.sprite = isMusicOn ? OnMusic : OffMusic;
        imgVibrate.sprite = isVibrateOn ? OnPhone : OffPhone;
    }
    #endregion
}
