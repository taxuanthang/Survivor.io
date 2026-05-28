using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeDatabaseManager : MonoBehaviour
{
    public static UpgradeDatabaseManager instance;


    public List<Upgrade> allUpgrades = new List<Upgrade>();

    public List<Upgrade> oneTimeUpgrades = new List<Upgrade>();

    public List<Upgrade> availablesUpgrades = new List<Upgrade>();

    public void Awake()
    {
        allUpgrades = Resources.LoadAll<Upgrade>("Upgrade").ToList();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        availablesUpgrades = allUpgrades.ToList();
    }

    public void OnEnable()
    {
        EventManager.instance.OnPlayerCompleteSelectingOneCard.AddListener(RemoveUpgradeFromAvailablesIfExist);
    }

    public void OnDisable()
    {
        EventManager.instance.OnPlayerCompleteSelectingOneCard.RemoveListener(RemoveUpgradeFromAvailablesIfExist);
    }
    public Upgrade GetUpgradeByName(string upgradeName)
    {
        return availablesUpgrades.FirstOrDefault(upgrade => upgrade.nameUpgrade == upgradeName);
    }

    public List<Upgrade> GetUpgradesByType(UpgradeType type)
    {
        return availablesUpgrades.Where(upgrade => upgrade.type == type).ToList();
    }

    public Upgrade GetRandomUpgradeByType(UpgradeType type)
    {
        var upgradesOfType = GetUpgradesByType(type);
        if (upgradesOfType.Count == 0)
            return null;
        int randomIndex = Random.Range(0, upgradesOfType.Count);
        return upgradesOfType[randomIndex];
    }

    public Upgrade GetRandomUpgrade()
    {
        int randomIndex = Random.Range(0, availablesUpgrades.Count);
        return availablesUpgrades[randomIndex];
    }

    public void RemoveUpgradeFromAvailablesIfExist(Upgrade upgrade)
    {
        if (upgrade == null) return;

        // Check if this upgrade is considered a one-time upgrade.
        if (oneTimeUpgrades.Contains(upgrade))
        {
            // Remove from available upgrades if present.
            availablesUpgrades.RemoveAll(u => u == upgrade);
        }
    }

}

public class OneTimeUpgradeCheck
{
    public Upgrade upgrade;
    public bool used;
}
