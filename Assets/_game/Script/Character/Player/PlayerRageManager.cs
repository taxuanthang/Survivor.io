using UnityEngine;

public class PlayerRageManager : MonoBehaviour
{
    public float maxRage = 100f;
    public float currentRage = 0f;

    public float rageGivenWhenHitEnemy = 10f;
    public float rageGivenWhenBeingHit = 10f;

    public void Awake()
    {
        EventManager.instance.OnPlayerHit.AddListener((dameToPlayer) => IncreaseRage(rageGivenWhenBeingHit));
        EventManager.instance.OnEnemyHit.AddListener(() => IncreaseRage(rageGivenWhenHitEnemy));
    }
    public void IncreaseRage(float amount)
    {
        currentRage += amount;
        if (currentRage > maxRage)
        {
            currentRage = maxRage;
        }
    }

    public void DecreaseRage(float amount)
    {
        currentRage -= amount;
        if (currentRage < 0f)
        {
            currentRage = 0f;
        }
    }

    public bool IsRageEnough(float amount)
    {
        return currentRage >= amount;
    }

    public bool IsRageFull()
    {
        return currentRage >= maxRage;
    }
}
