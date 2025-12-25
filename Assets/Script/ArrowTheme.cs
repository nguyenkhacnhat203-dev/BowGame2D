using UnityEngine;

public class ArrowTheme : MonoBehaviour
{
    public Sprite arrowBasic;
    public Sprite arrowPoison;
    private SpriteRenderer sr;

    private void Awake() => sr = GetComponent<SpriteRenderer>();

    private void Start() => ApplyTheme();

    public void ApplyTheme()
    {
        string equipped = PlayerPrefs.GetString("Equipped_Arrow", "Basic");
        sr.sprite = (equipped == "Poison") ? arrowPoison : arrowBasic;
    }


   
}