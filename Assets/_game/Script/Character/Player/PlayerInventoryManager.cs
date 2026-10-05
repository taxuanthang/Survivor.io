using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public float totalCoinsHave = 0f;

    public void Awake()
    {
        EventManager.instance.onPlayerPickCoinUp.AddListener(AddCoins);
        EventManager.instance.onPlayerSpendCoin.AddListener(SpendCoins);
    }



    public void AddCoins(float amount)
    {
        totalCoinsHave += amount;
    }

    public void SpendCoins(float amount)
    {
        if (totalCoinsHave >= amount)
        {
            totalCoinsHave -= amount;
        }
        else
        {
            Debug.LogWarning("Not enough coins to spend!");
        }
    }
}
