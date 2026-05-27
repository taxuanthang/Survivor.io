using Pathfinding;
using System;
using UnityEngine;

public class BossManager : EnemyManager, IPoolable
{
    [Header("Enemy")]

    [SerializeField] BossHealthManager _bossHealthManager;
    [SerializeField] BossAIManager _bossAIManager;
    [SerializeField] BossAnimationManager _bossAnimationManager;


    [SerializeField] bool triggered;

    Vector3 movingInput;
    public override void Awake()
    {
        base.Awake();
        _bossHealthManager = GetComponent<BossHealthManager>();
        _bossAIManager = GetComponent<BossAIManager>();
        _bossAnimationManager = GetComponent<BossAnimationManager>();

        OnDie.AddListener(DropEXPOrb);
    }

    public void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        if(!triggered)
        {
            return;
        }    
        movingInput = (_bossAIManager.destinationSetter.target.position - _bossAIManager.aiPath.position).normalized;

        if (movingInput.x > 0.6) movingInput.x = 1;
        else if (movingInput.x < -0.6) movingInput.x = -1;
        else movingInput.x = 0;

        if (movingInput.y > 0.6) movingInput.y = 1;
        else if (movingInput.y < -0.6) movingInput.y = -1;
        else movingInput.y = 0;
        _bossAnimationManager.UpdateMovingParameter((int)movingInput.x, (int)movingInput.y);

        //update component
        _bossAIManager.Tick();


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            // khi va chạm với player thi
            player.OnHit?.Invoke(10);
        }
    }

    public override void SetUp(PlayerManager player, Vector3 spawnPos)
    {
        base.SetUp();
        this.transform.position = spawnPos;
        _bossAIManager.SetUp(player);
    }

    public override void DropEXPOrb()
    {
        print("drop exp orb");
        ExpPoint expOrb = PoolManager.instance.Get(PoolType.EXPOrb).GetComponent<ExpPoint>();
        expOrb.transform.position = this.transform.position;
        expOrb.expValue = expDropOfThisEnemy;
        expOrb.col.enabled = true;
    }


    public override void OnSpawn()
    {
        _bossHealthManager.isDead = false;
    }

    public override void OnDespawn()
    {
        model.localScale = new Vector3(5.85f, 5.85f, 5.85f);
    }

    public void Activate(PlayerManager player)
    {
        SetUp(player, this.transform.position);
        triggered = true;
    }
}

