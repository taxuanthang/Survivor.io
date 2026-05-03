using Pathfinding;
using System;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    [Header("Enemy")]

    [SerializeField] EnemyHealthManager _enemyHealthManager;
    [SerializeField] EnemyAIManager _enemyAIManager;
    [SerializeField] EnemyAICombatManager _enemyAICombatManager;
    [SerializeField] EnemyAnimationManager _enemyAnimationManager;

    [SerializeField] EnemyType type;

    [Header("EXP")]
    public int expDropOfThisEnemy = 10;

    Vector3 movingInput;
     public override void Awake()
    {
        base.Awake();
        _enemyHealthManager = GetComponent<EnemyHealthManager>();
        _enemyAIManager = GetComponent<EnemyAIManager>();
        _enemyAICombatManager = GetComponent<EnemyAICombatManager>();
        _enemyAnimationManager = GetComponent<EnemyAnimationManager>();

        OnDie.AddListener(DropEXPOrb);
    }

    public void Update()
    {
        if (Time.timeScale ==0)
        {
            return;
        }

        movingInput = (_enemyAIManager.destinationSetter.target.position - _enemyAIManager.aiPath.position).normalized;

        if (movingInput.x > 0.6) movingInput.x = 1;
        else if (movingInput.x < -0.6) movingInput.x = -1;
        else movingInput.x = 0;

        if (movingInput.y > 0.6) movingInput.y = 1;
        else if (movingInput.y < -0.6) movingInput.y = -1;
        else movingInput.y = 0;
        _enemyAnimationManager.UpdateMovingParameter((int)movingInput.x, (int)movingInput.y);

        //update component
        _enemyAIManager.Tick();


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            // khi va chạm với player thi
            player.OnHit?.Invoke(10);
        }
    }

    public void SetUp(PlayerManager player, Vector3 spawnPos)
    {
        base.SetUp();
        this.transform.position = spawnPos;
        _enemyAIManager.SetUp(player);
    }

    public void DropEXPOrb()
    {

        ExpPoint expOrb = PoolManager.instance.Get(PoolType.EXPOrb).GetComponent<ExpPoint>();
        expOrb.transform.position = this.transform.position;
        expOrb.expValue = expDropOfThisEnemy;
        expOrb.col.enabled = true;
    }
}

