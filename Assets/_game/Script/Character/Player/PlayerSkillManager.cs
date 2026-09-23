using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] Skill playerSkill;

    [SerializeField] UltimateSkill playerUltimate;

    PlayerRageManager playerRageManager;
    PlayerManager player;

    public void Awake()
    {
        if(playerRageManager == null) playerRageManager = GetComponent<PlayerRageManager>();
        if(player == null) player = GetComponent<PlayerManager>();
    }
    public void ActivateSkill()
    {
        if (playerSkill == null)
        {
            Debug.LogWarning("No skill assigned to the player.");
            return;
        }
        if (playerRageManager.IsRageEnough(playerSkill.rageRequired))
        { 
            playerSkill.UseSkill(transform,player);
        }
    }

    public void ActivateUltimateSkill()
    {
        if (playerUltimate == null)
        {
            Debug.LogWarning("No skill assigned to the player.");
            return;
        }
        if (playerRageManager.IsRageEnough(playerUltimate.rageRequired))
        {
            playerUltimate.UseSkill();
        }

    }


}


