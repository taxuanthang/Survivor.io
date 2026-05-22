
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(fileName = "RangeAttackState", menuName = "ScriptableObjects/EnemyAI/RangeAttackState")]
public class RangeAttackState : State
{

    public List<Attack> rangeAttacks;

    public float currentRangeAttackCooldown;
    public float rangeAttackCooldown;

    public bool canAttack = true;

    CharacterManager target;

    EnemyAIManager _enemyAIManager;

    public bool initRanged = false;

    public override void Enter(EnemyAIManager enemy)
    {
        // run attack animation, etc.
        target = enemy.destinationSetter.target.gameObject.GetComponent<CharacterManager>();
        _enemyAIManager = enemy;

        if(!initRanged)
        {
            initRanged = true;
            for(int i =0; i <rangeAttacks.Count; i++)
            {
                rangeAttacks[i] = Instantiate(rangeAttacks[i]);
            }    
                
        }    
    }
    public override void Execute(EnemyAIManager enemy)
    {
        // perform attack logic, etc.
        // nếu đánh xong rồi mà vẫn ngoài tầm thì chuyển về chase state, còn trong tầm thì đánh tiếp
        // ko thể dùng đến couroutine trong này nên phải dùng đến task(bất đồng bộ)

        if (!canAttack)
        {
            return;
        }
        canAttack = false;
        Attack(enemy,target);




    }
    public override void Exit(EnemyAIManager enemy)
    {
        // stop attack animation, etc.
    }

    public async void Attack(EnemyAIManager enemy,CharacterManager target)
    {
        
        // thêm cooldown cho attack
        int index = UnityEngine.Random.Range(0, rangeAttacks.Count);
        enemy.enemyManager.onAttackMelee?.Invoke();
        Debug.Log("Enemy perform range attack: " + rangeAttacks[index].name);
        await rangeAttacks[index].PerformAttack(enemy.transform,target.transform);

        enemy.ChangeState(enemy._chaseState);
        canAttack = true;
        StartCooldown();
    }

    public async void StartCooldown()
    {
        currentRangeAttackCooldown = rangeAttackCooldown;
        while (currentRangeAttackCooldown > 0)
        {
            await Awaitable.NextFrameAsync();
            currentRangeAttackCooldown -= Time.deltaTime;
        }
        currentRangeAttackCooldown = 0;
    }

    public bool CanAttackRange()
    {

        if(currentRangeAttackCooldown <= 0)
        {
            StartCooldown();
            return true;
        }
        else 
        {             
            return false;
        }
    }

}