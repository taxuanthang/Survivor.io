using UnityEngine;

public class UtilitiesManager : MonoBehaviour
{
    public static UtilitiesManager instance;

    public LayerMask enemyLayer;
    public LayerMask playerLayer;


    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
