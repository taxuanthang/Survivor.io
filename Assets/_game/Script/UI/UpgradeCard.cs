using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public Upgrade upgradeData;

    public TextMeshProUGUI upgradeTitleText;
    public TextMeshProUGUI descriptionText;
    public Image upgradeImage;
    public Button upgradeInterface;
    public void Awake()
    {
        upgradeInterface = GetComponent<Button>();
    }

    public void Start()
    {
        
        upgradeInterface.onClick.AddListener(OnUpgradeCardClicked);
    }

    public void AssignUpgradeData(Upgrade newUpgradeData)
    {
        upgradeData = newUpgradeData;

        upgradeTitleText.text = upgradeData.nameUpgrade;
        descriptionText.text = upgradeData.description;
        upgradeImage.sprite = upgradeData.upgradeSprite;

    }

    [Button]
    public void Test()
    {
        AssignUpgradeData(upgradeData);

    }
    public void OnUpgradeCardClicked()
    {
        // Handle the upgrade card click event here
        Debug.Log($"Upgrade card clicked: {upgradeData.nameUpgrade}");
        // You can add logic to apply the upgrade or trigger any other actions
        EventManager.instance.OnPlayerCompleteSelectingCard.Invoke(upgradeData);
    }


}
