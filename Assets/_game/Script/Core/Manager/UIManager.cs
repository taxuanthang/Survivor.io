using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameOver_UI gameOver_UI;
    public Slider healthSlider;
    public Slider ammoSlider;
    public UpgradeSelector_UI upgradeSelector_UI;
    public BossHealth_UI bossHealth_UI;

    public void UpdateHealth(float health)
    {
        healthSlider.value = health;
    }

    public void UpdateAmmo(float ammo)
    {
        ammoSlider.value = ammo;
    }

    public void OnEnable()
    {
        EventManager.instance.OnHealthChanged.AddListener(UpdateHealth);
        EventManager.instance.OnPlayerAmmoChanged.AddListener(UpdateAmmo);

        UpdateHealth(1);

        EventManager.instance.OnPlayerReachNewLevel.AddListener(OpenSelectCardUI);
        EventManager.instance.OnNoMoreUpgrade.AddListener(CloseSelectCardUI);

        EventManager.instance.OnEnterBossRoom.AddListener(OpenBossHealthUI);
        EventManager.instance.OnFinishBossRoom.AddListener(CloseBossHealthUI);
    }

    public void OnDisable()
    {
        EventManager.instance.OnHealthChanged.RemoveListener(UpdateHealth);
        EventManager.instance.OnPlayerAmmoChanged.RemoveListener(UpdateAmmo);
        EventManager.instance.OnPlayerReachNewLevel.RemoveListener(OpenSelectCardUI);
        EventManager.instance.OnNoMoreUpgrade.RemoveListener(CloseSelectCardUI);
    }

    public void OpenSelectCardUI()
    {
        upgradeSelector_UI.gameObject.SetActive(true);
    }

    public void CloseSelectCardUI()
    {
        upgradeSelector_UI.gameObject.SetActive(false);
    }

    public void OpenBossHealthUI(BossRoom bossRoom)
    {
        bossHealth_UI.gameObject.SetActive(true);
        string bossName = bossRoom.thisRoomBosses[0].name;
        bossHealth_UI.AssignBossName(bossName);
    }

    public void CloseBossHealthUI()
    {
        bossHealth_UI.gameObject.SetActive(false);
    }


}
