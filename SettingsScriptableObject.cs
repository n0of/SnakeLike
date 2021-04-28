using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
public class SettingsScriptableObject : ScriptableObject
{
    public float PlayerSpeed;
    public float EnemySpeed;
    public float IFrames;
    public int EnemyCount;
    public int FruitCount;
    public int Health;
}
