using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public List<Upgrade> currentUpgrades = new List<Upgrade>();

    public List<SynergyRecipe> allRecipes;

    public void AddUpgrade(Upgrade upgrade)
    {
        if (!currentUpgrades.Contains(upgrade))
        {
            currentUpgrades.Add(upgrade);
            CheckSynergy();
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

}
[System.Serializable]
public class SynergyRecipe
{
    public Upgrade result;
    public List<Upgrade> required;
}
