using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBigShop : PopupBase
{
    public TextMeshProUGUI Stone_Text;

    [Header("Bow Settings")]
    public Button BowBasic;
    public GameObject tickBowBasic;
    public Button BowGem;
    public GameObject tickBowGem;
    public GameObject greenStoneBowGem;
    public int priceBowGem = 500;

    [Header("Arrow Settings")]
    public Button ArrowBasic;
    public GameObject tickArrowBasic;
    public Button ArrowPoison;
    public GameObject tickArrowPoison;
    public GameObject greenStoneArrowPoison;
    public int priceArrowPoison = 300;

    [Header("Wood Settings")]
    public Button WoodBasic;
    public GameObject tickWoodBasic;
    public Button WoodOakPoison;
    public GameObject tickWoodOakPoison;
    public GameObject greenStoneWoodOakPoison;
    public int priceWoodOak = 400;

    private void Awake()
    {
        BowBasic.onClick.AddListener(() => SelectItem("Bow", "Basic"));
        BowGem.onClick.AddListener(OnBowGemClick);

        ArrowBasic.onClick.AddListener(() => SelectItem("Arrow", "Basic"));
        ArrowPoison.onClick.AddListener(OnArrowPoisonClick);

        WoodBasic.onClick.AddListener(() => SelectItem("Wood", "Basic"));
        WoodOakPoison.onClick.AddListener(OnWoodOakClick);
    }

    protected override void OnShow()
    {
        CheckFirstTime();
        UpdateShopUI();
    }



    public override void DestroyPopup()
    {
        Destroy(gameObject);
        PopupManager.Instance.isShowPopup = false;
    }


    private void CheckFirstTime()
    {
        if (!PlayerPrefs.HasKey("FirstTime_Shop"))
        {
            PlayerPrefs.SetInt("Owned_BowGem", 0);
            PlayerPrefs.SetInt("Owned_ArrowPoison", 0);
            PlayerPrefs.SetInt("Owned_WoodOak", 0);

            PlayerPrefs.SetString("Equipped_Bow", "Basic");
            PlayerPrefs.SetString("Equipped_Arrow", "Basic");
            PlayerPrefs.SetString("Equipped_Wood", "Basic");

            PlayerPrefs.SetInt("FirstTime_Shop", 1);
            PlayerPrefs.Save();
        }
    }

    public void UpdateShopUI()
    {
        Stone_Text.text = ResourceManager.Instance.StoneGreen.ToString();

        bool hasBowGem = PlayerPrefs.GetInt("Owned_BowGem", 0) == 1;
        string equippedBow = PlayerPrefs.GetString("Equipped_Bow", "Basic");

        greenStoneBowGem.SetActive(!hasBowGem); 
        tickBowGem.SetActive(equippedBow == "Gem");
        tickBowBasic.SetActive(equippedBow == "Basic");

        bool hasArrowPoison = PlayerPrefs.GetInt("Owned_ArrowPoison", 0) == 1;
        string equippedArrow = PlayerPrefs.GetString("Equipped_Arrow", "Basic");

        greenStoneArrowPoison.SetActive(!hasArrowPoison);
        tickArrowPoison.SetActive(equippedArrow == "Poison");
        tickArrowBasic.SetActive(equippedArrow == "Basic");

        bool hasWoodOak = PlayerPrefs.GetInt("Owned_WoodOak", 0) == 1;
        string equippedWood = PlayerPrefs.GetString("Equipped_Wood", "Basic");

        greenStoneWoodOakPoison.SetActive(!hasWoodOak);
        tickWoodOakPoison.SetActive(equippedWood == "Oak");
        tickWoodBasic.SetActive(equippedWood == "Basic");
    }


    private void OnBowGemClick()
    {
        if (PlayerPrefs.GetInt("Owned_BowGem", 0) == 1)
        {
            SelectItem("Bow", "Gem");
        }
        else if (ResourceManager.Instance.StoneGreen >= priceBowGem)
        {
            ResourceManager.Instance.UseStoneGreen(priceBowGem);
            PlayerPrefs.SetInt("Owned_BowGem", 1);
            SelectItem("Bow", "Gem"); 
        }
    }

    private void OnArrowPoisonClick()
    {
        if (PlayerPrefs.GetInt("Owned_ArrowPoison", 0) == 1)
        {
            SelectItem("Arrow", "Poison");
        }
        else if (ResourceManager.Instance.StoneGreen >= priceArrowPoison)
        {
            ResourceManager.Instance.UseStoneGreen(priceArrowPoison);
            PlayerPrefs.SetInt("Owned_ArrowPoison", 1);
            SelectItem("Arrow", "Poison");
        }
    }

    private void OnWoodOakClick()
    {
        if (PlayerPrefs.GetInt("Owned_WoodOak", 0) == 1)
        {
            SelectItem("Wood", "Oak");
        }
        else if (ResourceManager.Instance.StoneGreen >= priceWoodOak)
        {
            ResourceManager.Instance.UseStoneGreen(priceWoodOak);
            PlayerPrefs.SetInt("Owned_WoodOak", 1);
            SelectItem("Wood", "Oak");
        }
    }

    private void SelectItem(string type, string value)
    {
        PlayerPrefs.SetString("Equipped_" + type, value);
        PlayerPrefs.Save();
        UpdateShopUI();

        if (type == "Bow")
        {
            var themes = FindObjectsOfType<BowTheme>();
            foreach (var t in themes) t.ApplyTheme();
        }
        else if (type == "Arrow")
        {
            var themes = FindObjectsOfType<ArrowTheme>();
            foreach (var t in themes) t.ApplyTheme();
        }
        else if (type == "Wood")
        {
            var themes = FindObjectsOfType<WoodTheme>();
            foreach (var t in themes) t.ApplyTheme();
        }

        Debug.Log($"Đã trang bị: {type} {value}");
    }

    private void OnDestroy()
    {
        BowBasic.onClick.RemoveAllListeners();
        BowGem.onClick.RemoveAllListeners();
        ArrowBasic.onClick.RemoveAllListeners();
        ArrowPoison.onClick.RemoveAllListeners();
        WoodBasic.onClick.RemoveAllListeners();
        WoodOakPoison.onClick.RemoveAllListeners();
    }
}