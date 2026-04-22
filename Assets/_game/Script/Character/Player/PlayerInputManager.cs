using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] InputSystem inputSystem;
    public PlayerManager player;

    [Header("Input Properties")]
    public Vector2 movingInput;
    public Vector2 mousePos;
    public bool dodgeInput =false;
    public bool shootInput = false;


    private void Awake()
    {
        inputSystem = new InputSystem();
        BindingInput();

        this.enabled = false;
    }

    public void SetUp()
    {
        EventManager.instance.OnEnterNewLevel.AddListener(Enable);
    }

    private void BindingInput()
    {
        inputSystem.Player.Move.performed += ctx => movingInput = ctx.ReadValue<Vector2>();
        inputSystem.Player.Move.canceled += ctx => movingInput = ctx.ReadValue<Vector2>();

        inputSystem.Player.Dodge.performed += ctx => dodgeInput = true;

        //inputSystem.Player.Look.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        //inputSystem.Player.Shoot.performed += ctx => shootInput = true;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    public void Update()
    {
        HandleMoveInput();
        HandleDodgeInput();
        HandleShootInput();
        //HandleMouse();
    }

    public void Enable()
    {
        this.enabled=true;
    }

    public void Disable()
    {
        this.enabled = false;
    }

    private void HandleMoveInput()
    {
        // clamping vì input chỉ có thể trả về 1 hoặc -1 nên nếu là 0.7 thì phải floor lên 1
        if (movingInput.x > 0) movingInput.x = 1;
        else if (movingInput.x < 0) movingInput.x = -1;
        else movingInput.x = 0;

        if (movingInput.y > 0) movingInput.y = 1;
        else if (movingInput.y < 0) movingInput.y = -1;
        else movingInput.y = 0;

        if(movingInput.magnitude >1)
        {
            movingInput = movingInput.normalized;
        }

        player.HandleMoveInput(movingInput.x,movingInput.y);

    }

    private void HandleDodgeInput()
    {
        if(!dodgeInput)
        {
            return;
        }
        dodgeInput = false;
        player.HandleDodgeInput();
    }

    private void HandleShootInput()
    {
        if (!shootInput)
        {
            return;
        }
        shootInput = false;

        Vector2 lookDir = mousePos - centerScreen;
        lookDir = lookDir.normalized;
        player.HandleShootInput(lookDir);
    }

    int width = Screen.width;
    int height = Screen.height;

    Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
    private void HandleMouse()
    {
        // 2 cách 
        // cách 1: Có vị trí của mouse trên screen thì cast qua world rồi tính vecto ra góc xoay
        // cách 2: tính luôn góc xoay dựa trên vị trí của mouse trên screen, sau đó chuyển góc xoay sang world

        player.HandleMousePos(mousePos);
    }
}

