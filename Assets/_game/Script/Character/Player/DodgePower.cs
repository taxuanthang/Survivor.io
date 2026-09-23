using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "dodgePower", menuName = "Power/dodgePower")]
public class DodgePower : Power
{

    [Header("Dodge Properties")]
    public bool allowToDodge = true;
    public float dodgeSpeed;
    public float dodgeDuration;
    public float dodgeCooldown;
    public async Task HandleDodge(Transform transform, PlayerLocomotionManager playerLocomotion)
    {
        // sửa lại idea thành trong fixed update sẽ chạy hàm này liên tục nếu bool dodgeInput bật lên true thì thực thi, đồng thời sửa lại logic while khả năng phải ngh
        // thêm đầu chặn để ko cho thực hiện dodge khi đang dodge
        if (allowToDodge)
        {
            allowToDodge = false;
            float tempDuration = dodgeDuration;
            // Set để ko di chuyển được khi đang dodge
            playerLocomotion.allowToMove = false;
            // không nhận input từ playerInput nữa để ko bị đổi hướng
            Vector2 dir = new Vector2(playerLocomotion.horizontalInput, playerLocomotion.verticalInput);
            while (dodgeDuration > 0)
            {
                transform.localPosition += new Vector3(dir.x, dir.y, 0) * dodgeSpeed * Time.fixedDeltaTime;
                dodgeDuration -= Time.fixedDeltaTime;
                await Awaitable.FixedUpdateAsync();
            }
            playerLocomotion.allowToMove = true;
            dodgeDuration = tempDuration;
            StartCooldown();
        }
    }
    async void StartCooldown()
    {
        // thêm cool down để tránh spam dodge
        float tempCooldown = dodgeCooldown;
        while (dodgeCooldown > 0)
        {
            dodgeCooldown -= Time.fixedDeltaTime;
            await Awaitable.FixedUpdateAsync();
        }
        dodgeCooldown = tempCooldown;
        allowToDodge = true;
    }

    public bool CanDodge() { return allowToDodge; }


    public async void UseAbility(Transform transform, PlayerManager player)
    {

        if (!CanDodge())
        {
            return;
        }
        player._playerHealthManager.isHittable = false;
        player._playerAnimationManager.PLayDodge();
        await HandleDodge(transform, player._playerLocomotionManager);
        player._playerHealthManager.isHittable = true;

    }


}



