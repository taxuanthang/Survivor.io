using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public float existTime;
    public Vector2 dir;
    public Sprite bulletSprite;

    public Transform bulletTransform;
    public DestroyTimer destroyTimer;

    public BulletType bulletType;
    public void Awake()
    {
        if(transform == null) bulletTransform = GetComponent<Transform>();
        if (destroyTimer == null) destroyTimer = GetComponent<DestroyTimer>();

    }

    public void Update()
    {
        Move();
    }
    public void Move()
    {
        // Logic to move the bullet
        transform.position += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
    }
    public void OnHit(CharacterManager character)
    {
        character.HandleDamage(damage);
        // Logic to handle when the bullet hits a target
        // This could include playing a hit effect, sound, etc.

        destroyTimer.timeToDestroy = 0;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // như đã thấy n sẽ hit phải layer Player trước cta có thể thử 2 cách 1 là chỉnh sửa vị trí spawn đạn ra xa khỏi player hơn nhưng điều này có thể gặp vấn đề là nếu mà đạn nẩy bắn ngc lại cta thì cta vẫn ăn dame
        // cách 2 cta sẽ thử là set cho biết đạn là từ ai bắn rồi set layer Player hoặc Enemy
        CharacterManager character;
        switch (bulletType)
        {
            case BulletType.PlayerBullet:
                if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {

                    if (collision.TryGetComponent(out character))
                    {
                        Debug.Log("Bullet hit: Enemy");
                        OnHit(character);
                    }
                }
                break;
            case BulletType.EnemyBullet:
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Debug.Log("Bullet hit: Player");
                    if (collision.TryGetComponent(out character))
                    {
                        OnHit(character);
                    }
                }
                break;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {

            destroyTimer.timeToDestroy = 0;
        }
    }
}

public enum BulletType
{
    PlayerBullet,
    EnemyBullet
}


