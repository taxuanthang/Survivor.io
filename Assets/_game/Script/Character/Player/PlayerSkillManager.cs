using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] Skill playerSkill;

    [SerializeField] UltimateSkill playerUltimate;

    PlayerRageManager playerRageManager;
    PlayerManager player;

    public bool allowToUseSkill = true;

    public bool allowToUseUltimate = true;

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
        if (!playerRageManager.IsRageEnough(playerSkill.rageRequired))
        {
            return;

        }
        if (allowToUseSkill)
        {
            allowToUseSkill = false;
            playerRageManager.DecreaseRage(playerSkill.rageRequired);
            playerSkill.UseSkill(transform, player);
            StartCooldown(playerSkill.cooldownTime);
        }
    }

    public void ActivateUltimateSkill()
    {
        if (playerUltimate == null)
        {
            Debug.LogWarning("No skill assigned to the player.");
            return;
        }
        if (!playerRageManager.IsRageEnough(playerUltimate.rageRequired))
        {
            return;
        }
        if (allowToUseSkill)
        {
            allowToUseSkill = false;
            playerRageManager.DecreaseRage(playerUltimate.rageRequired);
            playerUltimate.UseSkill(transform, player);
            StartCooldown(playerUltimate.ultimateCooldown);
        }

    }

    async void StartCooldown(float amount)
    {
        // thêm cool down để tránh spam dodge
        while (amount > 0)
        {
            amount -= Time.fixedDeltaTime;
            await Awaitable.FixedUpdateAsync();
        }
        allowToUseSkill = true;
    }

    public bool CanUseSkill() { return allowToUseSkill; }
    public bool CanUseUltimate() { return allowToUseUltimate; }

}


