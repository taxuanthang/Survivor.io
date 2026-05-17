using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _bossName;
    public Slider _bossHealth;

    public void OnEnable()
    {
        EventManager.instance.OnBossHit.AddListener(UpdateHealth);
    }

    public void OnDisable()
    {
        EventManager.instance.OnBossHit.RemoveListener(UpdateHealth);
    }

    public void AssignBossName(string bossName)
    {
        _bossName.text = bossName;
    }
    public void UpdateHealth(float health)
    {
        _bossHealth.value = health;
    }
}
