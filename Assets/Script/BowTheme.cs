using UnityEngine;

public class BowTheme : MonoBehaviour
{
    public Sprite bowBasic;
    public Sprite bowGem;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyTheme();
    }

    public void ApplyTheme()
    {
        string equippedBow = PlayerPrefs.GetString("Equipped_Bow", "Basic");

        if (equippedBow == "Gem")
            spriteRenderer.sprite = bowGem;
        else
            spriteRenderer.sprite = bowBasic;
    }

   
}