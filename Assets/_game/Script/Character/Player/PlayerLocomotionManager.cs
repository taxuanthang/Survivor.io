using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerLocomotionManager: MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] Transform playerTransform;
    [SerializeField] PlayerInteractionManager interactionManager;

    public void Awake()
    {
        if(playerTransform == null) playerTransform = GetComponent<Transform>();
        if(player == null) player = GetComponent<PlayerManager>();
    }

    public void FixedUpdate()
    {
        HandleMovement();
    }

    [Header("Moving Properties")]
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    public bool allowToMove = true;
    public float speed;
    public void HandleMovement()
    {
        if(!allowToMove)
        {
            return;
        }

        playerTransform.localPosition += new Vector3(horizontalInput, verticalInput, 0) * speed * Time.fixedDeltaTime;
    }

    public void UpdateMovingInput(float x, float y)
    {
        horizontalInput = x;
        verticalInput = y;
        moveAmount = new Vector2(horizontalInput, verticalInput).magnitude;
    }    


    internal void HandleInteract()
    {
        if(interactionManager.currentInteractable == null)
        {
            return;
        }
        interactionManager.currentInteractable.OnPlayerInteract(player);
    }
}