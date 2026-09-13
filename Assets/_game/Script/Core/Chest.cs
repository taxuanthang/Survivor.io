using UnityEngine;

public class Chest : MonoBehaviour , IInteractable
{
    virtual public void OnPlayerInteract(PlayerManager player)
    {
        // Logic for opening the chest and giving rewards to the player
        Debug.Log("Chest opened! Player receives rewards.");
    }

    public void OnPlayerFacingInto(PlayerManager player)
    {
    }

}

public class MimicChest : Chest
{
    public override void OnPlayerInteract(PlayerManager player)
    {
        // Logic for the mimic chest attack
        Debug.Log("Mimic chest attacks the player!");
        // You can add damage logic here
    }
}

public class ConnectChest : Chest
{
    public override void OnPlayerInteract(PlayerManager player)
    {
        // Logic for connecting to another chest or room
        Debug.Log("Connecting to another chest or room.");
        // You can add connection logic here
    }
}

