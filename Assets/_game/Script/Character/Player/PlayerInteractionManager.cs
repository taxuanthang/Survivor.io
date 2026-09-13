using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    PlayerManager playerManager;

    public bool isInteracting = false;

    public float interactCircleRadius = 1f;

    public float distanceFromDectectPointToPlayerCentre = 1f;

    [HideInInspector] public Vector2 playerFacingDirection;
    [HideInInspector] public IInteractable currentInteractable;
    public void Awake()
    {
        if (playerManager == null) playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position + new Vector3(playerFacingDirection.x, playerFacingDirection.y, 0f) * distanceFromDectectPointToPlayerCentre, interactCircleRadius, UtilitiesManager.instance.obstacleLayer);

        if (collider != null)
        {
            IInteractable interact = collider.GetComponent<IInteractable>();
            isInteracting = true;
            interact.OnPlayerFacingInto(playerManager);
            currentInteractable = interact; 
        }
        else
        {
            isInteracting = false;
            currentInteractable = null;
        }
    }

    

}
