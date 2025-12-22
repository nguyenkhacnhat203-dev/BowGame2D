using UnityEngine;
using TMPro;

public class ResourceManager : Singleton<ResourceManager>
{
    private const string STONE_GREEN_KEY = "STONE_GREEN";

    public int StoneGreen;
    public TextMeshProUGUI StoneGreen_Text;

    private void Start()
    {
        LoadStoneGreen();
        UpdateUI();
    }

   
    private void LoadStoneGreen()
    {
        if (!PlayerPrefs.HasKey(STONE_GREEN_KEY))
        {
            StoneGreen = 50;
            PlayerPrefs.SetInt(STONE_GREEN_KEY, StoneGreen);
            PlayerPrefs.Save();
        }
        else
        {
            StoneGreen = PlayerPrefs.GetInt(STONE_GREEN_KEY);
        }
    }


    public void AddStoneGreen(int amount)
    {
        StoneGreen += amount;
        SaveStoneGreen();
        UpdateUI();
    }   
    public void UseStoneGreen(int amount)
    {
        StoneGreen -= amount;
        SaveStoneGreen();
        UpdateUI();
    }

    private void SaveStoneGreen()
    {
        PlayerPrefs.SetInt(STONE_GREEN_KEY, StoneGreen);
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        if (StoneGreen_Text != null)
            StoneGreen_Text.text = StoneGreen.ToString();
    }
}
