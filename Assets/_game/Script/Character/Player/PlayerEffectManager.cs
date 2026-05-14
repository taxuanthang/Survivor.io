using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerEffectManager : CharacterEffectManager
{
    PlayerManager playerManager;

    public void Awake()
    {
        base.Awake();
        playerManager = GetComponent<PlayerManager>();
    }
    public override void SplitBlood(int dmg)
    {
        if (!playerManager.CanBeHitted())
        {
            return;
        }
        GameObject newBlood = PoolManager.instance.Get(PoolType.BloodVFX); // Get a blood from the pool
        newBlood.transform.position = character.transform.position;
        DestroyTimer timer = newBlood.gameObject.GetComponent<DestroyTimer>();
        timer.poolType = PoolType.BloodVFX;
        timer.timeToDestroy = bloodDuration;
    }
}
