
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    public string nameUpgrade;
    public string description;
    public UpgradeType type;
    public float value;
}
