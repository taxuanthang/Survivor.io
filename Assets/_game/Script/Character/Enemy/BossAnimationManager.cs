using UnityEngine;

public class BossAnimationManager : CharacterAnimationManager
{
    public BossManager boss;
    public bool flipWhileMove = false;
    public bool walkRight = false;
    public override void Awake()
    {
        base.Awake();
        boss = GetComponent<BossManager>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        boss.onAttackMelee.AddListener(PlayAttackMeleeAnim);
        boss.onAttackRange.AddListener(PlayAttackRangeAnim);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        boss.onAttackMelee.RemoveListener(PlayAttackMeleeAnim);
        boss.onAttackRange.AddListener(PlayAttackRangeAnim);
    }
    public override void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
        _animator.SetFloat("MoveAmount", new Vector2(x, y).sqrMagnitude);
        if (flipWhileMove)
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
                boss.transform.localScale = new Vector3(scaleX, 1, 1);
            }
            else if (x < 0)
            {
                boss.transform.localScale = new Vector3(-scaleX, 1, 1);
            }
            if (y > 0)
            {
            }
            else if (y < 0)
            {
            }
        }
    }


    public void PlayAttackMeleeAnim()
    {

        _animator.Play("AttackMelee");
    }

    public void PlayDieAnim()
    {
        _animator.Play("Die");
    }
    public void PlayAttackRangeAnim()
    {
        _animator.Play("AttackRange");
    }

}

