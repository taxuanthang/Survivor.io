using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy = 0.5f;
    public void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
