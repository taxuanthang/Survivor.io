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
        if (player != null)
        {
            transform.DOMove(player.transform.position, flySpeed).OnComplete(() =>
            { 
                OnExpPointCollected.Invoke(expValue);
                PoolManager.instance.Return(PoolType.EXPOrb, gameObject);
            });
        }
    }

    public void SetTarget(PlayerManager player)
    {
        col.enabled = false;
        this.player = player;
    }
}
