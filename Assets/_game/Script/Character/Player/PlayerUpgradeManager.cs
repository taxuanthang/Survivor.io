using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{

    PlayerStatsManager playerStatsManager;

    public List<Upgrade> currentUpgrades = new List<Upgrade>();

    public List<SynergyRecipe> allRecipes;

    public Upgrade testUpgrade;

    public void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    public void Start()
    {
        EventManager.instance.OnPlayerCompleteSelectingCard.AddListener(AddUpgrade);
    }


    [Button]
    public void TestUp()
    {
        AddUpgrade(testUpgrade);
    }

    [Button]
    public void RemoveUp()
    {
        RemoveUpgrade(testUpgrade);
    }
    public void AddUpgrade(Upgrade upgrade)
    {
        if (!currentUpgrades.Contains(upgrade))
        {
            // tránh trùng lặp
        }
        currentUpgrades.Add(upgrade);
        CheckSynergy();
        ApplyUpgrade(upgrade);
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (currentUpgrades.Contains(upgrade))
        {
            currentUpgrades.Remove(upgrade);
            CheckSynergy();
            UnapplyUpgrade(upgrade);
        }
    }

    void CheckSynergy()
    {
        foreach (var recipe in allRecipes)
        {
            if (IsRecipeMet(recipe))
            {
                UnlockSynergy(recipe);
            }
        }
    }

    bool IsRecipeMet(SynergyRecipe recipe)
    {
        return recipe.required.All(r => currentUpgrades.Contains(r));
    }

    void UnlockSynergy(SynergyRecipe recipe)
    {
        Debug.Log("Unlocked: " + recipe.result);

        currentUpgrades.Add(recipe.result);

        // Optional: remove nguyên liệu nếu bạn muốn kiểu "combine"
        // foreach (var r in recipe.required)
        //     currentUpgrades.Remove(r);
    }


    public void ApplyUpgrade(Upgrade upgrade)
    {

        playerStatsManager.ApplyModifier(upgrade.type, upgrade.value);

    }

    public void UnapplyUpgrade(Upgrade upgrade)
    {

        playerStatsManager.ApplyModifier(upgrade.type, -upgrade.value);

    }



}
[System.Serializable]
public class SynergyRecipe
{
    public Upgrade result;
    public List<Upgrade> required;
}
