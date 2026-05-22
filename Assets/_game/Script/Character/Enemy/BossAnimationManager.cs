using UnityEngine;

public class BossAnimationManager : CharacterAnimationManager
{
    public BossManager boss;
    public override void Awake()
    {
        base.Awake();
        boss = GetComponent<BossManager>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        //boss.onAttackMelee.AddListener(AttackMelee);
        //boss.onAttackRange.AddListener(AttackRange);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        //boss.onAttackMelee.RemoveListener(AttackMelee);
        //boss.onAttackRange.AddListener(AttackRange);
    }
    public override void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
        _animator.SetFloat("MoveAmount", new Vector2(x, y).sqrMagnitude);
        if (x > 0)
        {
            boss.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x < 0)
        {
            boss.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (y > 0)
        {
        }
        else if (y < 0)
        {
        }
    }

    public void AttackMelee()
    {
        _animator.Play("AttackMelee");
    }

    public void AttackRange()
    {
        _animator.Play("AttackRange");
    }

}

