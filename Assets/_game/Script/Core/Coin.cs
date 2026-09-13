using UnityEngine;

public class Coin : MonoBehaviour
{
    public float value = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((UtilitiesManager.instance.playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            EventManager.instance.onPlayerPickCoinUp.Invoke(value);
            PoolManager.instance.Return(PoolType.Coin, this.gameObject);
        }
    }
}
