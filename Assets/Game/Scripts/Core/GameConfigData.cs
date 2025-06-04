using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
public class GameConfigData : ScriptableObject
{
    public bool IsInfiniteStock = true;
    public int CubeCount = 20;
    public Sprite[] CubeSprites;
    public float MaxBuildOffset = 25f;
    public float MaxHorizontalOffsetRatio = 0.5f;
    public float MinVerticalOffset = 10f;
}
