using UnityEngine;

public class EnemyAnimationManager : CharacterAnimationManager
{
    public EnemyManager enemy;
    public bool flipWhileMove = false;
    public bool walkRight = false;
     public override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyManager>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        enemy.onAttackMelee.AddListener(AttackMelee);
        enemy.onAttackRange.AddListener(AttackRange);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        enemy.onAttackMelee.RemoveListener(AttackMelee);
        enemy.onAttackRange.AddListener(AttackRange);
    }
    public override void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
        _animator.SetFloat("MoveAmount", new Vector2(x, y).sqrMagnitude);
        if(flipWhileMove)
        {
            int scaleX = 0;
            if (walkRight)
            {
                scaleX = 1;
            }
            else
            {
                scaleX = -1;
            }
            if (x > 0)
            {
                enemy.transform.localScale = new Vector3(scaleX, 1, 1);
            }
            else if (x < 0)
            {
                enemy.transform.localScale = new Vector3(-scaleX, 1, 1);
            }
            if (y > 0)
            {
            }
            else if (y < 0)
            {
            }
        }
    }

    public void  AttackMelee()
    {
        
        _animator.Play("AttackMelee");
    }

    public void Die()
    {
        _animator.Play("Die");
    }
    public void AttackRange()
    {
        _animator.Play("AttackRange");
    }

}

