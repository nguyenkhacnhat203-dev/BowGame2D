using UnityEngine;

public class WoodTheme : MonoBehaviour
{
    public Sprite woodBasic;
    public Sprite woodOak;
    private SpriteRenderer sr;

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    private void Start() => ApplyTheme();

    public void ApplyTheme()
    {
        string equipped = PlayerPrefs.GetString("Equipped_Wood", "Basic");
        sr.sprite = (equipped == "Oak") ? woodOak : woodBasic;
    }


   
}