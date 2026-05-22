using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    // play anim tương ứng với trạng thái của player

    [SerializeField] protected Animator _animator;
    CharacterManager character;

    public virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void OnEnable()
    {
        character.OnDie.AddListener(DieAnim);
    }

    public virtual void OnDisable()
    {
        character.OnDie.RemoveListener(DieAnim);
    }


    public virtual void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
    }


    public void DieAnim()
    {
        _animator.SetBool("Dead", true);
    }
}

