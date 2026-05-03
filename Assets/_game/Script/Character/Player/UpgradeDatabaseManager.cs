using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeDatabaseManager : MonoBehaviour
{
    public static UpgradeDatabaseManager instance;


    public List<Upgrade> allUpgrades = new List<Upgrade>();

    public void Awake()
    {
        allUpgrades = Resources.LoadAll<Upgrade>("Upgrade").ToList();
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Upgrade GetUpgradeByName(string upgradeName)
    {
        return allUpgrades.FirstOrDefault(upgrade => upgrade.nameUpgrade == upgradeName);
    }

    public List<Upgrade> GetUpgradesByType(UpgradeType type)
    {
        return allUpgrades.Where(upgrade => upgrade.type == type).ToList();
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
        int randomIndex = Random.Range(0, allUpgrades.Count);
        return allUpgrades[randomIndex];
    }

}
