using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    [SerializeField] CharacterManager currentEnemy;
    [SerializeField] PlayerManager player;
    [SerializeField] float detectRadius;
    [SerializeField] Color colorDetectArea;
    [SerializeField] bool showDetectArea;
    [SerializeField] float shootCooldown;
    float currentShootCooldown = 0f;

    public void Awake()
    {
        if (player == null) player = GetComponent<PlayerManager>();
    }

    public void Update()
    {
        // spawn physic để check overlap

        // check khi mà mục tiêu hiện tại chết thì sẽ tìm target mới
        if(currentEnemy == null)
        {
            currentEnemy = GetNearestOverlapEnemy();
        }
        else if (currentEnemy != null && currentEnemy.IsDead())
        {
            currentEnemy = GetNearestOverlapEnemy();
        }
        else if (currentEnemy != null && !currentEnemy.IsDead())
        {
            // xoay đên mục tiêu hiện tại
            player.HandleMousePos(currentEnemy.transform.position);
            // Bắn mục tiêu hiện tại
            if (currentShootCooldown > 0)
            {
                currentShootCooldown -= Time.deltaTime;
                return;
            }
            else
            {
                currentShootCooldown = shootCooldown;
            }

            if (currentEnemy != null && !currentEnemy.IsDead())
            {
                player.HandleShootInput(currentEnemy.transform.position);
            }
        }




        


    }

    public void ShootCurent()
    {

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
