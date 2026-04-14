using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    // play anim tương ứng với trạng thái của player

    [SerializeField] Animator _animator;


    public void UpdateMovingParameter(float x, float y)
    {
        _animator.SetFloat("InputX", x);
        _animator.SetFloat("InputY", y);
    }
}

