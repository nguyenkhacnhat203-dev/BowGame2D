using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBigShop : PopupBase
{
    public TextMeshProUGUI Stone_Text;

    public Button BowBasic;
    public GameObject tickBowBasic;
    public Button BowGem;
    public GameObject tickBowGem;
    public GameObject greenStoneBowGem;
    public int priceBowGem = 500;
    public TextMeshProUGUI BowGem_Text;

    public Button ArrowBasic;
    public GameObject tickArrowBasic;
    public Button ArrowPoison;
    public GameObject tickArrowPoison;
    public GameObject greenStoneArrowPoison;
    public int priceArrowPoison = 300;
    public TextMeshProUGUI ArrowPoison_Text;

    public Button WoodBasic;
    public GameObject tickWoodBasic;
    public Button WoodOakPoison;
    public GameObject tickWoodOakPoison;
    public GameObject greenStoneWoodOakPoison;
    public int priceWoodOak = 400;
    public TextMeshProUGUI WoodOak_Text;

    public GameObject Combo1;
    public GameObject Combo2;

    private const string COMBO1_BOUGHT_KEY = "COMBO1_BOUGHT";
    private const string COMBO2_BOUGHT_KEY = "COMBO2_BOUGHT";

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
        AudioManager.Instance.BtnClick();
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
        BowGem_Text.gameObject.SetActive(!hasBowGem);
        tickBowGem.SetActive(equippedBow == "Gem");
        tickBowBasic.SetActive(equippedBow == "Basic");

        bool hasArrowPoison = PlayerPrefs.GetInt("Owned_ArrowPoison", 0) == 1;
        string equippedArrow = PlayerPrefs.GetString("Equipped_Arrow", "Basic");

        greenStoneArrowPoison.SetActive(!hasArrowPoison);
        ArrowPoison_Text.gameObject.SetActive(!hasArrowPoison);
        tickArrowPoison.SetActive(equippedArrow == "Poison");
        tickArrowBasic.SetActive(equippedArrow == "Basic");

        bool hasWoodOak = PlayerPrefs.GetInt("Owned_WoodOak", 0) == 1;
        string equippedWood = PlayerPrefs.GetString("Equipped_Wood", "Basic");

        greenStoneWoodOakPoison.SetActive(!hasWoodOak);
        WoodOak_Text.gameObject.SetActive(!hasWoodOak);
        tickWoodOakPoison.SetActive(equippedWood == "Oak");
        tickWoodBasic.SetActive(equippedWood == "Basic");

        if (Combo1 != null)
            Combo1.SetActive(PlayerPrefs.GetInt(COMBO1_BOUGHT_KEY, 0) == 0);

        if (Combo2 != null)
            Combo2.SetActive(PlayerPrefs.GetInt(COMBO2_BOUGHT_KEY, 0) == 0);
    }

    private void OnBowGemClick()
    {
        if (PlayerPrefs.GetInt("Owned_BowGem", 0) == 1)
            SelectItem("Bow", "Gem");
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
            SelectItem("Arrow", "Poison");
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
            SelectItem("Wood", "Oak");
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
        ApplyAllThemes();
    }

    public void BuyCombo1()
    {
        ResourceManager.Instance.AddStoneGreen(500);

        if (AdManager.Instance != null)
            AdManager.Instance.DisableAllAds();

        PlayerPrefs.SetInt("Owned_BowGem", 1);
        PlayerPrefs.SetString("Equipped_Bow", "Gem");

        PlayerPrefs.SetInt("Owned_WoodOak", 1);
        PlayerPrefs.SetString("Equipped_Wood", "Oak");

        PlayerPrefs.SetInt(COMBO1_BOUGHT_KEY, 1);
        PlayerPrefs.Save();

        UpdateShopUI();
        ApplyAllThemes();
    }

    public void BuyCombo2()
    {
        if (AdManager.Instance != null)
            AdManager.Instance.DisableAllAds();

        PlayerPrefs.SetInt("Owned_ArrowPoison", 1);
        PlayerPrefs.SetString("Equipped_Arrow", "Poison");

        PlayerPrefs.SetInt("Owned_WoodOak", 1);
        PlayerPrefs.SetString("Equipped_Wood", "Oak");

        PlayerPrefs.SetInt(COMBO2_BOUGHT_KEY, 1);
        PlayerPrefs.Save();

        UpdateShopUI();
        ApplyAllThemes();
    }

    private void ApplyAllThemes()
    {
        foreach (var t in FindObjectsOfType<BowTheme>()) t.ApplyTheme();
        foreach (var t in FindObjectsOfType<ArrowTheme>()) t.ApplyTheme();
        foreach (var t in FindObjectsOfType<WoodTheme>()) t.ApplyTheme();
    }

    public void ShowShopStone()
    {
        PopupManager.Instance.ShowPopup_ShopStone();
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
