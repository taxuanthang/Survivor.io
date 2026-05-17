using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(fileName = "DirectionalAttack", menuName = "Attack/Directional")]
public class DirectionalAttack : Attack
{
    public float damage;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float hitboxDistanceBetweenEach = 2f; // khoảng cách giữa các hitbox
    public float hitboxCount = 20; // số lượng hitbox được tạo ra
    public float timeBetweenEachTimeCreateHitbox = 0.5f; // 

    public float modified = 1f;

    public GameObject directionPrefabs;



    public override async Task PerformAttack(Transform origin, Transform target)
    {

        Vector3 directionToTargetVector = (target.position - origin.position).normalized;

        Quaternion rotateQua = Quaternion.FromToRotation(Vector3.right, target.position - origin.position);
        GameObject direction = Instantiate(directionPrefabs, origin.transform.position, rotateQua);

        await CreateSpriteAndSpreadIt(origin,target,direction);

    }



    public async Task CreateSpriteAndSpreadIt(Transform origin, Transform target, GameObject direction)
    {
        float dis = Vector3.Distance(origin.position, target.position);
        DestroyTimer timer= direction.AddComponent<DestroyTimer>();
        timer.returnToPooled = false;
        BoxCollider2D collider=direction.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        HitBoxDame hitBoxDame=direction.AddComponent<HitBoxDame>();
        hitBoxDame.hitBoxType = HitBoxType.Rect;
        float expandTime = timeBetweenEachTimeCreateHitbox * hitboxCount;
        timer.timeToDestroy = expandTime;
        float currentTime = 0f;
        Vector3 baseScale = direction.transform.localScale;
        while (currentTime < expandTime)
        {
            Debug.Log(Vector3.Lerp(baseScale, new Vector3(baseScale.x, dis, 1), currentTime) + " " + direction.transform.localScale);
            float deltaX=Vector3.Lerp(baseScale, new Vector3(baseScale.x, dis, 1), currentTime / expandTime).y;
            direction.transform.localScale = new Vector3(deltaX, baseScale.y, baseScale.z);
            currentTime += modified*Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

    }
}
