using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int skillLevel;
    public float cooldownTime;

    public float rageRequired = 10f; // Example rage requirement for using the skill

    public Power power; // Reference to the Power class
    public Skill(string name, int level, float cooldown)
    {
        skillName = name;
        skillLevel = level;
        cooldownTime = cooldown;
    }
    public void UseSkill(Transform transform, PlayerManager player)
    {
        power.UseAbility(transform, player);
    }
}


