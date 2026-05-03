using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameOver_UI _gameOver_UI;
    public Slider _healthSlider;
    public Slider _ammoSlider;
    public UpgradeSelector_UI _upgradeSelector_UI;

    public void SetUp()
    {
        _gameOver_UI.enabled = true;
    }
    public void UpdateHealth(float health)
    {
        _healthSlider.value = health;
    }

    public void UpdateAmmo(float ammo)
    {
        _ammoSlider.value = ammo;
    }

    public void Start()
    {
        EventManager.instance.OnHealthChanged.AddListener(UpdateHealth);
        EventManager.instance.OnPlayerAmmoChanged.AddListener(UpdateAmmo);

        UpdateHealth(1);

        EventManager.instance.OnPlayerReachNewLevel.AddListener(OpenSelectCardUI);
        EventManager.instance.OnPlayerCompleteSelectingCard.AddListener(CloseSelectCardUI);
    }

    public void OpenSelectCardUI()
    {
        _upgradeSelector_UI.gameObject.SetActive(true);
    }

    public void CloseSelectCardUI(Upgrade selectedUpgrade)
    {
        _upgradeSelector_UI.gameObject.SetActive(false);
    }
}
