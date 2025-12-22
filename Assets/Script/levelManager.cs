using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : Singleton<LevelManager>
{
    public List<LevelSetup> Levels;
    public Transform levelParent;

    public int currentLevelIndex = 0;
    private LevelSetup currentLevel;

    [Header("Runtime Count")]
    public int CountBowHit;
    public int CountAppleHit;
    public int remainingArrows;

    private bool isCheck;

    private const string LEVEL_KEY = "CURRENT_LEVEL";
    private ArrowSpawner arrowSpawner;


    public TextMeshProUGUI TextTaget_HitBow;
    public TextMeshProUGUI TextCountArrow;
    public TextMeshProUGUI Level_Text;
    private void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt(LEVEL_KEY, 0);

        currentLevelIndex = Mathf.Clamp(currentLevelIndex, 0, Levels.Count - 1);
        CloneLevel();
    }

    public void CloneLevel()
    {

        CountBowHit = 0;
        CountAppleHit = 0;
        isCheck = false;
        Level_Text.text = "LEVEL " + (currentLevelIndex + 1).ToString();
        if (currentLevel != null)
            Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(
            Levels[currentLevelIndex],
            Vector3.zero,
            Quaternion.identity,
            levelParent
        );
        remainingArrows = currentLevel.ArrowStart;

        UpdateTargetText();
        UpdateArrowText();
        Debug.Log("Load Level: " + currentLevelIndex);
    }

    private void Update()
    {
        CheckWin();
    }
   public void UpdateTargetText()
    {
        if (TextTaget_HitBow == null || currentLevel == null) return;
        TextTaget_HitBow.text = CountBowHit + "/" + currentLevel.HitArrows;
    }

    public void UpdateArrowText()
    {
        if (TextCountArrow == null || currentLevel == null) return;
        arrowSpawner = FindObjectOfType<ArrowSpawner>();
        TextCountArrow.text = (arrowSpawner.CountArrow).ToString();
    }



    private void CheckWin()
    {
        if (isCheck == true || remainingArrows>0) return;

        
        


        if (CountBowHit >= currentLevel.HitArrows)
        {

            isCheck = true;

            Debug.Log("Bạn thắng");

            SaveLevelProgress();

            LoadNextLevel();
        }
        else
        {
           

            isCheck = true;
            Debug.Log("Bạn thua");
        }
    }

    void SaveLevelProgress()
    {
        int nextLevel = currentLevelIndex + 1;

        int savedLevel = PlayerPrefs.GetInt(LEVEL_KEY, 0);

        if (nextLevel > savedLevel)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, nextLevel);
            PlayerPrefs.Save();
        }
    }

    void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= Levels.Count)
        {
            return;
        }

        CloneLevel();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LEVEL_KEY);
        currentLevelIndex = 0;
        CloneLevel();
    }
}
