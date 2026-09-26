using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "judgementCutPower", menuName = "Power/judgementCutPower")]
public class JudgementCutPower : Power
{

    [Header("Judgement Properties")]
    public float judgementInitiateTime;
    public float judgementDamage = 1000f;
    public float timeBetweenJudgementAttacks = 0.5f;
    public GameObject judgementAreaPrefab;
    public async Task HandleJudgement(Transform transform, PlayerManager player)
    {
        Debug.Log("activate judgement");
        player.SetShootable(false);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 100f, UtilitiesManager.instance.enemyLayer);
        // dừng hoạt động tất cả các enemy trong phạm vi
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyManager>().StopMovement();
        }
        // tạo một vùng judgement xung quanh player
        GameObject judgementArea = Instantiate(judgementAreaPrefab, transform.position, Quaternion.identity);
        // 
        await Task.Delay((int)(judgementInitiateTime * 1000));
        Destroy(judgementArea);
        // Tấn công toàn bộ enimies trong phạm vi
        Vector3 iniPositionPlayer = transform.position;
        foreach (Collider2D enemy in enemies)
        {
            transform.localPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y, transform.position.z);
            enemy.GetComponent<EnemyManager>().OnHit?.Invoke((int)(judgementDamage/enemies.Length));
            // tạo các hiệu ứng tấn công tại vị trí của từng enemy
            await Task.Delay((int)(timeBetweenJudgementAttacks * 1000));

        }

        transform.localPosition = iniPositionPlayer;

        player.SetShootable(true);
        // mở hoạt động lại toàn bộ enemy trong phạm vi
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyManager>().StartMovement();
        }
    }

    public override async void UseAbility(Transform transform, PlayerManager player)
    {
        Debug.Log("activate ultimate");
        player._playerHealthManager.isHittable = false;
        await HandleJudgement(transform, player);
        player._playerHealthManager.isHittable = true;

    }
}



