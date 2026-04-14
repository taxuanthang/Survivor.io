using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    [SerializeField] Gun currentGun;
    [SerializeField] Gun secondaryGun;

    public void SwapGun()
    {
        Gun temp = currentGun;
        currentGun = secondaryGun;
        secondaryGun = temp;
    }

    public void HandleShoot(Vector2 lookDir)
    {
        if(currentGun != null)
        {
            currentGun.Shoot(lookDir,BulletType.PlayerBullet);
        }
    }

    public void RotateGun(Quaternion lookAngle)
    {
        currentGun.transform.rotation = lookAngle;
    }
}


