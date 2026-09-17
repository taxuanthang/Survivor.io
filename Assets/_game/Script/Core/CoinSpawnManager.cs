using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    public Coin coinPrefab;

    public PlayerManager player;

    public void Awake()
    {
        EventManager.instance.DropCoinForPlayer.AddListener(SpawnNumberCoinAroungPlayer);
    }

    public void SpawnNumberCoin(Vector3 position, float value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Coin newCoin = PoolManager.instance.Get(PoolType.Coin).GetComponent<Coin>();
            newCoin.transform.position = position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            newCoin.value = value;
        }
    }

    public void SpawnNumberCoinAroungPlayer( float value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Coin newCoin = PoolManager.instance.Get(PoolType.Coin).GetComponent<Coin>();
            newCoin.transform.position = player.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
            newCoin.value = value;
        }
    }
}
