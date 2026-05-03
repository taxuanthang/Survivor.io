using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ExpPoint : MonoBehaviour
{
    public int expValue = 10;
    public Collider2D col;
    [HideInInspector] public PlayerManager player;

    public UnityEvent<int> OnExpPointCollected;
    public float flySpeed = 1f;
    public float currentFlyTimeRemain = 0f;

    public void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Update()
    {
        FlyToPlayer();
    }

    public void FlyToPlayer()
    {
        if (currentFlyTimeRemain < 0f)
        {
            currentFlyTimeRemain = 0f;
            OnExpPointCollected.Invoke(expValue);
            PoolManager.instance.Return(PoolType.EXPOrb, gameObject);
            return;
        }
        else if (currentFlyTimeRemain > 0f)
        {
            currentFlyTimeRemain -= Time.deltaTime;
            if (player != null)
            {
                transform.DOMove(player.transform.position, flySpeed);
            }
        }
    }

    public void SetTarget(PlayerManager player, float flyTime)
    {
        col.enabled = false;
        this.player = player;
        currentFlyTimeRemain = flyTime;

    }
}
