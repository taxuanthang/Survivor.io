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

    public void HandleShoot(Vector3 currentPointerPos)
    {
        Vector2 lookDir = currentPointerPos - this.transform.position;
        lookDir = lookDir.normalized;

        if (currentGun != null)
        {
            currentGun.Shoot(lookDir,BulletType.PlayerBullet);
        }
    }

    public void RotateGun(Vector3 currentPointerPos)
    {
        float lookAngle = Vector3.SignedAngle(Vector3.right, currentPointerPos - this.transform.position, Vector3.forward);
        if (lookAngle <= 90 && lookAngle >= -90)
        {
            currentGun.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (lookAngle > 90 || lookAngle < -90)
        {
            currentGun.transform.localScale = new Vector3(-1, 1, 1);
            if (lookAngle > 90)
            {
                lookAngle =  lookAngle - 180 ;
            }
            else if (lookAngle < -90)
            {
                lookAngle = lookAngle + 180;
            }
        }

        Quaternion lookAngleInQuater = Quaternion.AngleAxis(lookAngle, Vector3.forward);
        currentGun.transform.rotation = lookAngleInQuater;
    }
}


