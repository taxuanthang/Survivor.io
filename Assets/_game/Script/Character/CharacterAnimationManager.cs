using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    // play anim tương ứng với trạng thái của player

    [SerializeField] Animator _animator;
    CharacterManager character;

    public void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public void OnEnable()
    {
        character.OnDie.AddListener(DieAnim);
    }

    public void OnDisable()
    {
        character.OnDie.RemoveListener(DieAnim);
    }


    public void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
    }


    public void DieAnim()
    {
        _animator.Play("Dead");
    }
}

