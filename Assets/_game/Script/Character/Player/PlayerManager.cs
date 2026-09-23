using System;
using UnityEngine;

public class PlayerManager: CharacterManager
{
    // Chia nhỏ các thành phần
    [Header("Player")]
    [SerializeField] public PlayerLocomotionManager _playerLocomotionManager;
    [SerializeField] public PlayerEquipmentManager _playerEquipmentManager;
    [SerializeField] public PlayerHealthManager _playerHealthManager;
    [SerializeField] public PlayerAnimationManager _playerAnimationManager;
    [SerializeField] public PlayerInteractionManager _playerInteractionManager;
    [SerializeField] public PlayerRageManager _playerRageManager;

    [SerializeField] PlayerSkillManager _playerSkillManager;



    public override void Awake()
    {
        base.Awake();
        if(_playerLocomotionManager == null) _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        if (_playerEquipmentManager == null) _playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        if(_playerHealthManager == null) _playerHealthManager = GetComponent<PlayerHealthManager>();
        if (_playerAnimationManager == null) _playerAnimationManager = GetComponent<PlayerAnimationManager>();
        if(_playerInteractionManager == null) _playerInteractionManager = GetComponent<PlayerInteractionManager>();
        if(_playerRageManager == null) _playerRageManager = GetComponent<PlayerRageManager>();
        if(_playerSkillManager == null) _playerSkillManager = GetComponent<PlayerSkillManager>();

        DontDestroyOnLoad(this);
    }

    public void OnEnable()
    {
        EventManager.instance.RestartGame.AddListener(Resurrect);

    }

    public void OnDisable()
    {
        EventManager.instance.RestartGame.RemoveListener(Resurrect);
    }

    #region handleInput
    public void HandleMoveInput(float x, float y)
    {
        // update moveInput
        _playerLocomotionManager.UpdateMovingInput(x, y);

        //
        _playerAnimationManager.UpdateMovingParameter(x, y);

        //
        _playerInteractionManager.playerFacingDirection = new Vector2(x, y).normalized;
    }

    internal void HandleShootInput(Vector3 currentTargetPos)
    {
        _playerEquipmentManager.HandleShoot(currentTargetPos);
    }

    public void HandleMousePos(Vector3 currentTargetPos)
    {
        _playerEquipmentManager.RotateGun(currentTargetPos);
    }

    public void HandleInteractInput()
    {
        _playerLocomotionManager.HandleInteract();
    }

    #endregion
    
    public void Resurrect()
    {
        Debug.Log("Player Resurrected");
        _playerHealthManager.HealFull();
        _playerHealthManager.isDead = false;
    }

    public bool CanBeHitted()
    {
        return _playerHealthManager.isHittable;
    }

    internal void HandleSkillInput()
    {
        _playerSkillManager.ActivateSkill();
    }

    internal void HandleUltimateInput()
    {
        _playerSkillManager.ActivateUltimateSkill();
    }
}


