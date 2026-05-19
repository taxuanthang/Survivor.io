using UnityEngine;

public class PlayerAnimationManager : CharacterAnimationManager
{
    public void PLayDodge()
    {
        _animator.Play("RollDissapear");
    }
}

