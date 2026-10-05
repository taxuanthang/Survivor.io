using UnityEngine;

public class Shop : MonoBehaviour , IInteractable
{
    public virtual void OnPlayerInteract(PlayerManager player)
    {
        SpendCoin(100f);
    }


    public void SpendCoin(float amount)
    {
        EventManager.instance.onPlayerSpendCoin?.Invoke(amount);
    }    

    public virtual void OnPlayerFacingInto(PlayerManager player)
    {

    }    


}
