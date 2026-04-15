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

    Vector3 movingInput;
     public override void Awake()
    {
        base.Awake();
        _enemyHealthManager = GetComponent<EnemyHealthManager>();
        _enemyAIManager = GetComponent<EnemyAIManager>();
        _enemyAICombatManager = GetComponent<EnemyAICombatManager>();
        _enemyAnimationManager = GetComponent<EnemyAnimationManager>();
    }

    public void Update()
    {
        movingInput = (_enemyAIManager.destinationSetter.target.position - _enemyAIManager.aiPath.position).normalized;

        if (movingInput.x > 0.6) movingInput.x = 1;
        else if (movingInput.x < -0.6) movingInput.x = -1;
        else movingInput.x = 0;

        if (movingInput.y > 0.6) movingInput.y = 1;
        else if (movingInput.y < -0.6) movingInput.y = -1;
        else movingInput.y = 0;
        _enemyAnimationManager.UpdateMovingParameter((int)movingInput.x, (int)movingInput.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            // khi va chạm với player thi
            player.HandleDamage(10);
        }
    }
}

