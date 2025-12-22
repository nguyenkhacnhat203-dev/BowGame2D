using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [Header("Số mũi tên ban đâu của người chơi trong level")]
    public int ArrowStart = 10;
    [Header("Lựa chọn cách mà bàn gỗ xoay trong level đó")]
    public Wood.RotateCase woodRotateCase;
    [Header("Số tên cần bắn trúng vào gỗ để thắng trong level đó")]
    public int HitArrows;
    [Header("Số táo cần bắn trúng để thắng trong level đó")]
    public int HitApple;

    [Header("Reference")]
    public ArrowSpawner arrowSpawner;
    public Wood wood;
   
    void Awake()
    {
        ApplyLevelConfig();
        if(HitArrows> ArrowStart)
        {
            HitArrows = ArrowStart;
        }
    }

    void ApplyLevelConfig()
    {
        if (arrowSpawner != null)
        {
            arrowSpawner.CountArrow = ArrowStart;
        }

        if (wood != null)
        {
            wood.rotateCase = woodRotateCase;
        }
    }
}
