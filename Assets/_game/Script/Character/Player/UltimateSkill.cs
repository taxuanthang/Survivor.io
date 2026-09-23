using UnityEngine;

[CreateAssetMenu(fileName = "New Ultimate Skill", menuName = "Skills/UltimateSkill")]
public class UltimateSkill : ScriptableObject
{
    public string ultimateName;
    public int ultimateLevel;
    public float ultimateCooldown;
    public float rageRequired = 10f; // Example rage requirement for using the skill

    public Power power; // Reference to the Power class
    public UltimateSkill(string name, int level, float cooldown)

    {
        ultimateName = name;
        ultimateLevel = level;
        ultimateCooldown = cooldown;
    }
    public void UseSkill()
    {
        power.UseAbility();
    }
}


