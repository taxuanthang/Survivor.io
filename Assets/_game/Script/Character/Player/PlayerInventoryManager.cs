using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public float totalCoinsHave = 0f;

    public void Awake()
    {
        EventManager.instance.onPlayerPickCoinUp.AddListener(AddCoins);
    }



    public void AddCoins(float amount)
    {
        totalCoinsHave += amount;
    }

    public float SpendCoins(float amount)
    {
        if (totalCoinsHave >= amount)
        {
            totalCoinsHave -= amount;
            return totalCoinsHave;
        }
        else
        {
            Debug.LogWarning("Not enough coins to spend!");
            return totalCoinsHave;
        }
    }
}
