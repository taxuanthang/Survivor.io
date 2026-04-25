using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameOver_UI GameOver_UI;
    public Slider HealthSlider;
    public Slider AmmoSlider;

    public void SetUp()
    {
        GameOver_UI.enabled = true;
    }
    public void UpdateHealth(float health)
    {
        HealthSlider.value = health;
    }

    public void UpdateAmmo(float ammo)
    {
        AmmoSlider.value = ammo;
    }

    public void Start()
    {
        EventManager.instance.OnHealthChanged.AddListener(UpdateHealth);
        EventManager.instance.OnPlayerAmmoChanged.AddListener(UpdateAmmo);

        UpdateHealth(1);
    }
}
