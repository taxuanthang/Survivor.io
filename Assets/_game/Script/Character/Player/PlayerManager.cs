using System;
using UnityEngine;

public class PlayerManager: CharacterManager
{
    // Chia nhỏ các thành phần
    [Header("Player")]
    [SerializeField] PlayerLocomotionManager _playerLocomotionManager;
    [SerializeField] PlayerEquipmentManager _playerEquipmentManager;
    [SerializeField] PlayerHealthManager _playerHealthManager;
    [SerializeField] PlayerAnimationManager _playerAnimationManager;




    public override void Awake()
    {
        base.Awake();
        if(_playerLocomotionManager == null) _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        if (_playerEquipmentManager == null) _playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        if(_playerHealthManager == null) _playerHealthManager = GetComponent<PlayerHealthManager>();
        if (_playerAnimationManager == null) _playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public void OnEnable()
    {
        EventManager.instance.RestartGame.AddListener(Resurrect);

    }

    public void OnDisable()
    {
        EventManager.instance.RestartGame.RemoveListener(Resurrect);
    }

    public void HandleMoveInput(float x, float y)
    {
        // update moveInput
        _playerLocomotionManager.UpdateMovingInput(x, y);

        //
        _playerAnimationManager.UpdateMovingParameter(x, y);

    }

    internal void HandleDodgeInput()
    {
        StartCoroutine(_playerLocomotionManager.HandleDodge());
    }

    internal void HandleShootInput(Vector3 currentTargetPos)
    {
        _playerEquipmentManager.HandleShoot(currentTargetPos);
    }

    public void HandleMousePos(Vector3 currentTargetPos)
    {
        _playerEquipmentManager.RotateGun(currentTargetPos);
    }

    public void Resurrect()
    {
        Debug.Log("Player Resurrected");
        _playerHealthManager.HealAll();
        _playerHealthManager.isDead = false;
    }

}


