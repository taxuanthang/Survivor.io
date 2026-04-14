using Unity.VisualScripting;
using UnityEngine;

public class PlayerAutoDetectManager : MonoBehaviour
{
    [SerializeField] CharacterManager currentEnemy;
    [SerializeField] PlayerManager player;
    [SerializeField] float detectRadius;
    [SerializeField] Color colorDetectArea;
    [SerializeField] bool showDetectArea;

    public void Awake()
    {
        if (player == null) player = GetComponent<PlayerManager>();
    }

    public void Update()
    {
        // spawn physic để check overlap
        currentEnemy = GetNearestOverlapEnemy();
    }

    public CharacterManager GetNearestOverlapEnemy()
    {
        Collider2D[] colliders= Physics2D.OverlapCircleAll(this.transform.position, detectRadius, UtilitiesManager.instance.enemyLayer);

        float distance = Mathf.Infinity;
        CharacterManager resultEnemy = null;
        foreach (Collider2D collider in colliders)
        {
            CharacterManager enemy = collider.GetComponent<CharacterManager>();
            if (enemy != null)
            {
                float currentDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    resultEnemy = enemy;
                }
            }
        }
        return resultEnemy;
    }

    void OnDrawGizmos()
    {
        if (showDetectArea)
        {
            Gizmos.color = colorDetectArea;
            Gizmos.DrawWireSphere(transform.position, detectRadius);
        }
    }
}
