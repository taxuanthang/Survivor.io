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

    public void Awake()
    {
        EventManager.instance.OnHealthChanged += UpdateHealth;
        EventManager.instance.OnPlayerAmmoChanged += UpdateAmmo;

        UpdateHealth(1);
    }
     private void OnDestroy()
    {
        EventManager.instance.OnHealthChanged -= UpdateHealth;
        EventManager.instance.OnPlayerAmmoChanged -= UpdateAmmo;

    }
}
