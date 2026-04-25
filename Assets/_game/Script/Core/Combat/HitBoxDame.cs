using UnityEngine;

public class HitBoxDame : MonoBehaviour
{
    // class này sẽ được attach vào hitbox prefab để xử lý va chạm va
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            // khi va chạm với player thi
            player.OnHit?.Invoke(10);
        }
    }
}
