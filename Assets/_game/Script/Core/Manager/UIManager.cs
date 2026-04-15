using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider AmmoSlider;

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
        EventManager.instance.OnHealthChanged += UpdateHealth;
        EventManager.instance.OnPlayerAmmoChanged += UpdateAmmo;

        UpdateHealth(1);
    }
}
