using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public int ammoCapacity;
    public int currentAmmo;
    public Sprite gunSprite;
    public Bullet bullet;

    public PlayerEquipmentManager playerEquipmentManager;
    public float critRate;
    public float critDamage;

    public void Shoot(Vector2 lookDir, BulletType bulletType)
    {
        if (currentAmmo > 0)
        {
            // Logic to shoot the gun
            currentAmmo--;
            // spawn bullet and set its properties based on the gun's properties
            Bullet newBullet = PoolManager.instance.Get(PoolType.Bullet).GetComponent<Bullet>(); // Get a bullet from the pool
            newBullet.destroyTimer.poolType = PoolType.Bullet;
            newBullet.destroyTimer.timeToDestroy = newBullet.existTime;
            newBullet.transform.position = transform.position;
            newBullet.transform.rotation = Quaternion.identity;
            newBullet.dir = lookDir.normalized; 
            newBullet.damage = GetDamageForCurrentBullet();
            newBullet.bulletSprite = gunSprite; // Set the bullet's sprite to the gun's sprite
            newBullet.bulletType = bulletType; // Set the bullet type (player or enemy)
            // nhớ làm object pooling sau để tối ưu hiệu suất thay vì instantiate và destroy bullet liên tục
        }
        else
        {
            // Logic to handle out of ammo
        }

        EventManager.instance.OnPlayerAmmoChanged?.Invoke((float)currentAmmo/ (float)ammoCapacity); // Notify listeners about ammo change
    }


    public void Reload()
    {
        // Logic to reload the gun
        currentAmmo = ammoCapacity;
    }

    public int GetDamageForCurrentBullet()
    {

        bool isCriticalHit = UnityEngine.Random.value < critRate;

        float finalDamage;
        if (isCriticalHit)
        {
            finalDamage = damage * critDamage;
        }
        else
        {
            finalDamage = damage;
        }
        return (int)finalDamage;
    }
}


